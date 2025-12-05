# Test Suite Summary - Passes Zero Functionality

## Quick Start

Run the tests with:
```bash
dotnet test Day1b.Tests\Day1b.Tests.csproj --filter "FullyQualifiedName~DialPassesZeroTests"
```

## Test Results Summary

### ? Passing Tests (31/39)

These scenarios work correctly:
- Simple rotations without crossing zero
- Rotating exactly to zero (both directions)
- Single pass through zero (both directions)
- Full rotation from non-zero position
- Multiple full rotations from non-zero position
- Small dial rotations
- Most theory test cases

### ? Failing Tests (8/39)

| Test | Expected | Actual | Issue |
|------|----------|--------|-------|
| StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero | 0 | 1 | Starting at 0 and moving away |
| RotateRightTwoFullRotationsCountsAsTwo | 2 | 1 | Under-counting full rotations |
| RotateLeftTwoFullRotationsCountsAsTwo | 2 | 3 | Over-counting full rotations |
| RotateRightOneAndHalfRotationsCountsAsTwo | 2 | 1 | Not counting partial rotation pass |
| RotateLeftOneAndHalfRotationsCountsAsTwo | 2 | 1 | Not counting partial rotation pass |
| ProgramScenarioMultipleRotations | 4 | 3 | Complex multi-rotation scenario |
| Theory test variants | Various | Various | Related to above issues |

## Key Issues Identified

### 1. Hardcoded Value (Line 159 in Dial.cs)
```csharp
int passesZero = rotation.Distance / 100;  // ? Should be: rotation.Distance / Count
```

### 2. Special Case: Starting at Zero
The current logic doesn't properly handle starting at position 0:
- Left rotation from 0 incorrectly counts as 1 pass
- Should not count leaving position 0

### 3. Full Rotations from Zero
- Right: Expected 2 for distance 200, getting 1
- Left: Expected 2 for distance 200, getting 3
Inconsistent handling of full rotations

### 4. Partial Rotations Missing
The logic doesn't account for partial rotations (< full rotation) that cross zero:
- Example: Start at 25, rotate right 150 (1.5 rotations)
  - Should count: 1 full + 1 partial = 2
  - Actually counting: 1

## Files Created for Debugging

1. **DialPassesZeroTests.cs** - 39 comprehensive test cases
2. **DEBUGGING_PASSES_ZERO.md** - Detailed analysis of failures
3. **EXPECTED_BEHAVIOR.md** - Visual examples and algorithm explanation
4. **ManualTest.cs** - Manual test program (compile and run separately)
5. **TEST_RESULTS.md** - This file

## Recommended Fix Steps

1. **Fix the hardcoded value first:**
   ```csharp
   int passesZero = rotation.Distance / Count;
   ```

2. **Handle starting at zero:**
   ```csharp
   if (oldPosition == 0)
   {
       return rotation.Distance / Count;
   }
   ```

3. **Fix the partial rotation logic:**
   ```csharp
   int fullRotations = rotation.Distance / Count;
   int remainingDistance = rotation.Distance % Count;
   int passesZero = fullRotations;
   
   if (offset > 0)  // Rotating right
   {
       if (oldPosition + remainingDistance >= Count)
           passesZero++;
   }
   else  // Rotating left
   {
       if (remainingDistance >= oldPosition)
           passesZero++;
   }
   ```

## How to Use These Tests for Debugging

1. **Start with the simple failing tests:**
   ```bash
   dotnet test --filter "StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero"
   ```

2. **Add console output to Dial.Rotate() to see values:**
   ```csharp
   Console.WriteLine($"Old: {oldPosition}, New: {Position.Value}, Offset: {offset}, Passes: {passesZero}");
   ```

3. **Run the manual test program:**
   - Add it as a separate project or console app
   - Step through with debugger
   - See visual output of each rotation

4. **Fix issues one at a time:**
   - Fix hardcoded value ? run tests
   - Fix starting at zero ? run tests
   - Fix partial rotations ? run tests

5. **Verify all tests pass:**
   ```bash
   dotnet test Day1b.Tests\Day1b.Tests.csproj
   ```

## Test Data Examples

### Simple Case (Should Pass)
```
Start: 10, R20 ? End: 30, Passes: 0 ?
```

### Cross Zero (Should Pass)
```
Start: 90, R20 ? End: 10, Passes: 1 ?
```

### Start at Zero (Currently Failing)
```
Start: 0, L10 ? End: 90, Passes: 0 (Getting: 1) ?
```

### Multiple Full Rotations (Currently Failing)
```
Start: 0, R200 ? End: 0, Passes: 2 (Getting: 1) ?
Start: 0, L200 ? End: 0, Passes: 2 (Getting: 3) ?
```

### Partial Rotation (Currently Failing)
```
Start: 25, R150 ? End: 75, Passes: 2 (Getting: 1) ?
Start: 75, L150 ? End: 25, Passes: 2 (Getting: 1) ?
```

## Next Actions

1. ? Tests created and documented
2. ? Fix Dial.cs line 159 (hardcoded 100)
3. ? Fix starting-at-zero logic
4. ? Fix partial rotation detection
5. ? Run full test suite
6. ? Verify Program.cs produces correct output

## Questions to Consider

1. **When we land EXACTLY on zero, should it count?** 
   - Current tests assume: YES ?

2. **When we START at zero, should leaving count?**
   - Current tests assume: NO ?

3. **How should full rotations be counted?**
   - Current tests assume: Count each time we pass/land on 0

4. **What about zero-distance rotations?**
   - Not currently tested, might need edge case handling
