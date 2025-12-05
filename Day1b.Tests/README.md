# Day1b Test Suite

## Overview
Comprehensive test suite for the Day1b project (Advent of Code 2025) with 139 tests total.

## Test Project Structure

### Day1b.Tests.csproj
- Target Framework: .NET 10.0
- Test Framework: xUnit 2.9.2
- Project Reference: Day1b.csproj
- Test SDK: Microsoft.NET.Test.Sdk 17.11.1
- Code Coverage: coverlet.collector 6.0.2

## Test Files

### RotationTests.cs (45 tests)
Tests for the `Rotation` record class covering:
- **Constructor tests**: Valid directions ('L', 'R') with various distances
- **Property tests**: Direction and Distance property validation
- **Equality tests**: Record equality semantics
- **Validation tests**: Invalid direction and non-positive distance handling
- **Immutability tests**: With-expressions for record modification

### DialTests.cs (55 tests)
Tests for the `Dial` class and its nested `DialPosition` record covering:
- **Constructor tests**: Default parameters, custom positions, normalization
- **Rotation tests**: Right/left rotation, wraparound behavior
- **Operator tests**: Addition operator for immutable rotations
- **Property tests**: Count and Position property validation
- **Edge cases**: Boundary conditions, wraparound

### DialPassesZeroTests.cs (39 tests) ??
**Specialized tests for "passes zero" counting logic**
- Tests counting how many times rotations pass through or land on position 0
- **Current Status**: 31 passing, 8 failing
- See [TEST_RESULTS.md](TEST_RESULTS.md) for detailed analysis

## Current Test Results

```
Total Tests: 139
Passed: 131 ?
Failed: 8 ?
```

### Passing Tests (131) ?
- All Rotation class tests
- All basic Dial functionality tests
- Most "passes zero" scenarios

### Failing Tests (8) ?
All failures are in the "passes zero" counting logic:
- Starting at zero and moving away
- Multiple full rotations from zero
- Partial rotations (1.5x) that cross zero
- Complex multi-rotation scenarios

## Documentation Files

### For Debugging "Passes Zero" Issues

1. **TEST_RESULTS.md** - Quick summary of test results and next steps
2. **DEBUGGING_PASSES_ZERO.md** - Detailed analysis of failures and root causes
3. **EXPECTED_BEHAVIOR.md** - Visual examples and correct algorithm
4. **DEBUG_HELPER.md** - Code snippets to add debugging output
5. **ManualTest.cs** - Standalone manual test program (not in project)

## Running Tests

### Run All Tests
```bash
dotnet test Day1b.Tests\Day1b.Tests.csproj
```

### Run Specific Test Class
```bash
# Run only Rotation tests
dotnet test --filter "FullyQualifiedName~RotationTests"

# Run only Dial tests
dotnet test --filter "FullyQualifiedName~DialTests"

# Run only PassesZero tests
dotnet test --filter "FullyQualifiedName~DialPassesZeroTests"
```

### Run Specific Test
```bash
dotnet test --filter "StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero"
```

### Run with Detailed Output
```bash
dotnet test --verbosity normal
```

## Known Issues

### Issue 1: Hardcoded Value (Line 159 in Dial.cs)
```csharp
int passesZero = rotation.Distance / 100;  // ? Should be: rotation.Distance / Count
```

**Impact**: Incorrect calculations for dials with count != 100

**Fix**:
```csharp
int passesZero = rotation.Distance / Count;
```

### Issue 2: Starting at Zero Logic
When starting at position 0 and rotating away, it shouldn't count as passing zero.

**Current Behavior**: Left rotation from 0 incorrectly counts as 1 pass

**Expected Behavior**: Should return 0

### Issue 3: Partial Rotation Detection
Rotations that are not full rotations (e.g., 1.5 rotations) don't correctly detect crossing zero in the partial portion.

**Example**: Start at 25, rotate right 150 positions
- Expected: 2 (one full rotation + one partial that crosses zero)
- Actual: 1

## Testing Conventions Applied

Based on the .github/copilot-instructions.md:
- ? No "Act", "Arrange", "Assert" comments
- ? Explicit type names instead of `var`
- ? No null argument validation tests for non-nullable types
- ? Test method names clearly describe the scenario
- ? Theory/InlineData for parameterized tests
- ? Comprehensive edge case coverage

## Next Steps

1. Fix the hardcoded `100` on line 159 to use `Count`
2. Add special case handling for starting at position 0
3. Fix the partial rotation detection logic
4. Re-run tests to verify all pass
5. Update documentation once fixed

## Quick Debugging Workflow

1. **Identify the failing test**:
   ```bash
   dotnet test --filter "FullyQualifiedName~DialPassesZeroTests" --verbosity normal
   ```

2. **Read the detailed analysis**:
   - Open `DEBUGGING_PASSES_ZERO.md`
   - Find your failing test
   - Understand what's expected vs actual

3. **Add debug output** (optional):
   - Follow instructions in `DEBUG_HELPER.md`
   - Add temporary `Console.WriteLine` statements
   - Re-run the specific test

4. **Fix the issue in Dial.cs**:
   - Start with line 159 (hardcoded 100)
   - Then fix the logic in the switch statement
   - Test after each change

5. **Verify the fix**:
   ```bash
   dotnet test Day1b.Tests\Day1b.Tests.csproj
   ```

## Code Coverage Areas

### Rotation Class ? 100%
- Constructor validation
- Property validation
- Record equality
- With-expressions
- Object initializers
- Invalid input handling

### Dial Class ? ~85%
- Constructor with default and custom parameters
- Position normalization
- Rotate method (mutable operations) ?? Has bugs
- Addition operator (immutable operations)
- Count and Position properties
- DialPosition value normalization

### Known Gaps
- "Passes zero" counting logic (8 failing tests)
- Zero-distance rotations (not tested)

## Contributing

When adding new tests:
1. Follow existing naming conventions
2. Use Theory/InlineData for multiple scenarios
3. Add descriptive test names
4. Group related tests together
5. Update documentation if adding new test categories
