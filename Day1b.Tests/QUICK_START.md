# ?? Quick Start Guide - Debugging Passes Zero

## Current Status
? 131 tests passing  
? 8 tests failing (all in "passes zero" logic)

## The Problem
Your `Dial.Rotate()` method should count how many times a rotation passes through or lands on position 0, with one exception: if you START at position 0, leaving doesn't count.

## 3-Minute Fix Checklist

### Step 1: Fix the Hardcoded Value ?? HIGH PRIORITY
**File**: `Day1b\Dial.cs`, **Line**: 159

**Current Code**:
```csharp
int passesZero = rotation.Distance / 100;  // ? WRONG
```

**Fixed Code**:
```csharp
int passesZero = rotation.Distance / Count;  // ? CORRECT
```

### Step 2: Run Tests
```bash
dotnet test Day1b.Tests\Day1b.Tests.csproj --filter "FullyQualifiedName~DialPassesZeroTests"
```

### Step 3: Fix Remaining Logic

After Step 1, you'll still have failing tests. Replace the entire `Rotate` method with:

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

    // Special case: starting at zero, only count full rotations
    if (oldPosition == 0)
    {
        return rotation.Distance / Count;
    }

    // Calculate full rotations
    int fullRotations = rotation.Distance / Count;
    int remainingDistance = rotation.Distance % Count;
    int passesZero = fullRotations;

    // Check if the partial rotation crosses or lands on zero
    if (offset > 0)
    {
        // Rotating right: check if we reach or pass zero
        if (oldPosition + remainingDistance >= Count)
        {
            passesZero++;
        }
    }
    else
    {
        // Rotating left: check if we reach or pass zero
        if (remainingDistance >= oldPosition)
        {
            passesZero++;
        }
    }

    return passesZero;
}
```

### Step 4: Verify All Tests Pass
```bash
dotnet test Day1b.Tests\Day1b.Tests.csproj
```

You should see:
```
Test summary: total: 139, failed: 0, succeeded: 139, skipped: 0
```

## Understanding the 8 Failing Tests

### 1. StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero
- **Expected**: 0, **Actual**: 1
- Start at 0, rotate left by 10 ? Should NOT count leaving 0

### 2. RotateRightTwoFullRotationsCountsAsTwo  
- **Expected**: 2, **Actual**: 1
- Start at 0, rotate right by 200 ? Two complete laps

### 3. RotateLeftTwoFullRotationsCountsAsTwo
- **Expected**: 2, **Actual**: 3  
- Start at 0, rotate left by 200 ? Two complete laps

### 4 & 5. One and Half Rotations (Right & Left)
- **Expected**: 2, **Actual**: 1
- Partial rotation after full rotation not being counted

### 6-8. Theory tests and multi-rotation scenarios
- Related to the above issues

## Visual Example

### ? Current Behavior
```
Start: 0, Rotate Left 10
  Current: Returns 1 (WRONG - we're leaving 0, not passing it)
  Expected: Returns 0
```

### ? After Fix
```
Start: 0, Rotate Left 10
  After Fix: Returns 0 (CORRECT - special case handled)
```

## Test Files Reference

| File | Purpose |
|------|---------|
| `DialPassesZeroTests.cs` | 39 test cases for passes zero logic |
| `TEST_RESULTS.md` | Detailed test analysis |
| `DEBUGGING_PASSES_ZERO.md` | Root cause analysis |
| `EXPECTED_BEHAVIOR.md` | Visual examples of correct behavior |
| `DEBUG_HELPER.md` | Debugging code snippets |

## Next Steps After Fixing

1. Run all tests: `dotnet test Day1b.Tests\Day1b.Tests.csproj`
2. Verify Program.cs output matches expected results
3. Remove any temporary debug code
4. Commit your fixes

## Need More Help?

- Read `DEBUGGING_PASSES_ZERO.md` for detailed analysis
- Read `EXPECTED_BEHAVIOR.md` for visual examples
- Add debug output using `DEBUG_HELPER.md`
- Run individual tests to isolate issues

## Key Takeaway

The main issues are:
1. **Hardcoded 100** instead of using `Count` ? Fix this first!
2. **Starting at 0** not handled as special case
3. **Partial rotations** not detecting zero crossings correctly

Fix these three things and all tests will pass! ??
