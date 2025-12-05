# Dial Passes Zero - Test Results and Debugging Guide

## Test Summary
- **Total Tests**: 39
- **Passed**: 31
- **Failed**: 8

## Failed Tests Analysis

### 1. StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero
**Expected**: 0  
**Actual**: 1  
**Scenario**: Start at position 0, rotate left by 10
- **Issue**: When starting at zero, rotating away from zero should NOT count as passing zero
- Starting position: 0
- Rotation: L10
- Ending position: 90
- The logic incorrectly counts this as 1 pass

### 2. RotateLeftTwoFullRotationsCountsAsTwo
**Expected**: 2  
**Actual**: 3  
**Scenario**: Start at 0, rotate left by 200
- **Issue**: Two full rotations are being counted as 3
- Starting position: 0
- Rotation: L200
- Ending position: 0
- Logic is over-counting by 1

### 3. RotateRightOneAndHalfRotationsCountsAsTwo
**Expected**: 2  
**Actual**: 1  
**Scenario**: Start at 25, rotate right by 150
- **Issue**: 1.5 rotations should pass zero twice
- Starting position: 25
- Rotation: R150
- Ending position: 75
- Path: 25 ? 0 (1st pass) ? 75 (2nd pass at 100/0 boundary)
- Logic is under-counting by 1

### 4. RotateLeftOneAndHalfRotationsCountsAsTwo
**Expected**: 2  
**Actual**: 1  
**Scenario**: Start at 75, rotate left by 150
- **Issue**: 1.5 rotations should pass zero twice
- Starting position: 75
- Rotation: L150
- Ending position: 25
- Path: 75 ? 0 (1st pass) ? 25 (2nd pass at 0/99 boundary)
- Logic is under-counting by 1

### 5. ProgramScenarioMultipleRotations
**Expected**: 4  
**Actual**: 3  
**Scenario**: Multiple rotations starting at 50
- R10: 50?60 (0 passes) ?
- R60: 60?20 (1 pass) ?
- L80: 20?40 (1 pass) ?
- R100: 40?40 (1 pass expected, but getting 0)
- **Issue**: Full rotation not being counted correctly

### 6-8. Theory Tests Failing
Multiple theory test cases with similar issues as above

## Root Cause Analysis

Looking at the `Dial.Rotate` method (lines 146-161):

```csharp
public int Rotate(Rotation rotation)
{
    int offset = rotation.Direction switch
    {
        'R' => rotation.Distance,
        'L' => -rotation.Distance,
        _ => throw new InvalidOperationException($"Invalid rotation direction: {rotation.Direction}")
    };

    int oldPosition = Position.Value;
    int newPosition = Position.Value + offset;
    Position = new DialPosition(newPosition, this);

    int passesZero = rotation.Distance / 100;  // ?? HARDCODED 100!
    switch (offset)
    {
        case > 0 when Position.Value < oldPosition && Position.Value != 0:
        case < 0 when Position.Value > oldPosition && Position.Value != 0:
            passesZero++;
            break;
        case == 0:
            break;
    }

    return passesZero;
}
```

## Identified Issues

### Issue 1: Hardcoded Value (Line 159)
```csharp
int passesZero = rotation.Distance / 100;  // Should be: rotation.Distance / Count
```
This causes incorrect calculations for any dial with count != 100.

### Issue 2: Logic Flaws
The current logic has several problems:

1. **Starting at Zero**: When starting at 0 and rotating away, it shouldn't count as passing zero
   - Current logic: `Position.Value != 0` tries to handle this but fails when rotating left from 0
   
2. **Landing on Zero**: When ending exactly on 0, it's not being counted
   - The `Position.Value != 0` condition excludes this case

3. **Full Rotations**: The base calculation `rotation.Distance / Count` doesn't account for:
   - Partial rotation that crosses zero
   - The starting position relative to zero

## Correct Algorithm

The algorithm should count:
1. How many times we cross or land on position 0
2. Handle the case where we START at 0 (don't count leaving 0)
3. Handle the case where we END at 0 (count it)

### Suggested Fix Logic

```csharp
public int Rotate(Rotation rotation)
{
    int offset = rotation.Direction switch
    {
        'R' => rotation.Distance,
        'L' => -rotation.Distance,
        _ => throw new InvalidOperationException($"Invalid rotation direction: {rotation.Direction}")
    };

    int oldPosition = Position.Value;
    int newPosition = Position.Value + offset;
    Position = new DialPosition(newPosition, this);

    // Don't count if we start at zero and move away
    if (oldPosition == 0)
    {
        return rotation.Distance / Count;
    }

    // Calculate full rotations
    int fullRotations = rotation.Distance / Count;
    int remainingDistance = rotation.Distance % Count;

    int passesZero = fullRotations;

    // Check if the partial rotation crosses zero
    if (offset > 0)
    {
        // Rotating right: check if we cross or land on 0
        if (oldPosition + remainingDistance >= Count)
        {
            passesZero++;
        }
    }
    else
    {
        // Rotating left: check if we cross or land on 0
        if (remainingDistance >= oldPosition)
        {
            passesZero++;
        }
    }

    return passesZero;
}
```

## Test Cases Summary

### Passing Tests (31) ?
These scenarios are working correctly:
- Simple rotations without crossing zero
- Rotating exactly to zero from various positions
- Single pass through zero
- Full rotations from non-zero positions

### Failing Tests (8) ?
These scenarios need fixing:
- Starting at zero and rotating away
- Multiple full rotations from zero
- Partial rotations (1.5x, 2.5x)
- Complex multi-rotation scenarios

## Next Steps

1. Fix the hardcoded `100` on line 159 to use `Count`
2. Revise the passes-zero counting logic to handle:
   - Starting at zero
   - Ending at zero
   - Multiple full rotations
   - Partial rotations that cross zero
3. Run the tests again to verify the fix
4. Consider edge cases like zero-distance rotations
