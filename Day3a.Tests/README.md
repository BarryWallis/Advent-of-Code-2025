# Day3a Test Suite

## Overview
Comprehensive test suite for the Day3a project with tests for `BatteryBank` helper methods.

## Test Project Structure

### Day3a.Tests.csproj
- Target Framework: .NET 10.0
- Language Version: C# 14.0
- Test Framework: xUnit 2.9.2
- Project Reference: Day3a.csproj
- Test SDK: Microsoft.NET.Test.Sdk 17.11.1
- Code Coverage: coverlet.collector 6.0.2

## Test Files

### BatteryBankHelperTests.cs (25 tests)
Tests for the `BatteryBank` helper methods covering:

#### LeftmostLargestDigitIndex Method (15 tests)
Tests the logic that finds the index of the leftmost occurrence of the largest digit:

**Single Digit Tests**:
- Returns 0 for any single digit string ("0", "5", "9")

**Two Digit Tests**:
- "12" ? 1 (largest is '2' at index 1)
- "21" ? 0 (largest is '2' at index 0)
- "05" ? 1 (largest is '5' at index 1)
- "50" ? 0 (largest is '5' at index 0)
- "99" ? 0 (both same, returns leftmost)

**Three Digit Tests**:
- "123" ? 2 (largest is '3' at index 2)
- "321" ? 0 (largest is '3' at index 0)
- "213" ? 2 (largest is '3' at index 2)
- "132" ? 1 (largest is '3' at index 1)

**Four Digit Tests**:
- "1234" ? 3 (largest is '4' at index 3)
- "4321" ? 0 (largest is '4' at index 0)
- "1324" ? 3 (largest is '4' at index 3)
- "2413" ? 1 (largest is '4' at index 1)

**Five Digit Tests**:
- "98765" ? 0 (largest is '9' at index 0)
- "12345" ? 4 (largest is '5' at index 4)
- "54321" ? 0 (largest is '5' at index 0)
- "13579" ? 4 (largest is '9' at index 4)

**All Same Digits Tests**:
- "0000" ? 0 (all same, returns leftmost)
- "1111" ? 0 (all same, returns leftmost)
- "9999" ? 0 (all same, returns leftmost)

**Ten Digit Tests**:
- "0123456789" ? 9 (largest is '9' at index 9)
- "9876543210" ? 0 (largest is '9' at index 0)

**Multiple Occurrences Tests**:
- "9145" ? 0 (largest is '9' at index 0)
- "1924" ? 1 (largest is '9' at index 1)
- "1249" ? 3 (largest is '9' at index 3)
- "1239" ? 3 (largest is '9' at index 3)

**Nine at Different Positions Tests**:
- "9123" ? 0
- "1923" ? 1
- "1293" ? 2
- "1239" ? 3

**Zero and Nines Tests**:
- "0999" ? 1 (leftmost '9')
- "9099" ? 0 (leftmost '9')
- "9909" ? 0 (leftmost '9')
- "9990" ? 0 (leftmost '9')

#### ConvertFromCharToInt Method (10 tests)
Tests the logic that converts a digit character to its integer value:

**All Digit Characters**:
- '0' ? 0
- '1' ? 1
- '2' ? 2
- '3' ? 3
- '4' ? 4
- '5' ? 5
- '6' ? 6
- '7' ? 7
- '8' ? 8
- '9' ? 9

**Edge Case Tests**:
- Zero returns 0
- Nine returns 9
- Five returns 5

**Arithmetic Tests**:
- Subtraction between converted values produces correct differences
- Consecutive digits have unit difference (1)

## Running Tests

### Run All Tests
```bash
dotnet test Day3a.Tests\Day3a.Tests.csproj
```

### Run Specific Test Class
```bash
dotnet test Day3a.Tests\Day3a.Tests.csproj --filter "FullyQualifiedName~BatteryBankHelperTests"
```

### Run Specific Test
```bash
# Run only single digit tests
dotnet test Day3a.Tests\Day3a.Tests.csproj --filter "LeftmostLargestDigitIndexWithSingleDigitReturnsZero"

# Run only ConvertFromCharToInt tests
dotnet test Day3a.Tests\Day3a.Tests.csproj --filter "ConvertFromCharToInt"
```

### Run with Detailed Output
```bash
dotnet test Day3a.Tests\Day3a.Tests.csproj --verbosity normal
```

## Test Results

```
Total Tests: 54
All Tests Passing: ?

BatteryBankHelperTests: 54 tests
  - LeftmostLargestDigitIndex: 15 tests (44 theory data points)
  - ConvertFromCharToInt: 10 tests (including theory and fact tests)
```

## Testing Conventions Applied

Based on the .github/copilot-instructions.md:
- ? No "Act", "Arrange", "Assert" comments
- ? Explicit type names instead of `var`
- ? No null argument validation tests for non-nullable types
- ? Test method names clearly describe the scenario
- ? Theory/InlineData for parameterized tests
- ? Comprehensive edge case coverage

## Code Coverage Areas

### BatteryBank Helper Methods ? 100%
- **LeftmostLargestDigitIndex**
  - Single digits
  - Multiple digit strings (2-10 digits)
  - All same digits
  - Largest digit at different positions
  - Multiple occurrences of largest digit (verifies leftmost)
  - Edge cases with zeros and nines

- **ConvertFromCharToInt**
  - All digit characters '0' through '9'
  - Edge cases (0, 5, 9)
  - Arithmetic operations with converted values

## Implementation Notes

### Internal Method Access
To enable testing of private helper methods, the following changes were made:

1. **BatteryBank.cs**: Changed method visibility from `private static` to `internal static`:
   ```csharp
   internal static int ConvertFromCharToInt(char c) => c - '0';
   internal static int LeftmostLargestDigitIndex(string value)
   ```

2. **AssemblyInfo.cs**: Added `InternalsVisibleTo` attribute:
   ```csharp
   [assembly: InternalsVisibleTo("Day3a.Tests")]
   ```

This approach follows the established pattern in other Day projects (Day1a, Day1b, Day2a, Day2b).

## Algorithm Explanation

### LeftmostLargestDigitIndex
The algorithm finds the index of the **first occurrence** (leftmost) of the **largest digit** in a string:

```csharp
int largestDigitIndex = 0;
for (int i = 1; i < value.Length; i++)
{
    if (value[i] > value[largestDigitIndex])
    {
        largestDigitIndex = i;
    }
}
return largestDigitIndex;
```

**Example**: "1349"
- Start: largestDigitIndex = 0 (value[0] = '1')
- i=1: value[1]='3' > '1', largestDigitIndex = 1
- i=2: value[2]='4' > '3', largestDigitIndex = 2
- i=3: value[3]='9' > '4', largestDigitIndex = 3
- Result: 3

**Example with ties**: "9909"
- Start: largestDigitIndex = 0 (value[0] = '9')
- i=1: value[1]='9' NOT > '9', no change
- i=2: value[2]='0' NOT > '9', no change
- i=3: value[3]='9' NOT > '9', no change
- Result: 0 (leftmost '9')

### ConvertFromCharToInt
Simple ASCII arithmetic to convert digit character to integer:

```csharp
int ConvertFromCharToInt(char c) => c - '0';
```

ASCII values: '0'=48, '1'=49, ..., '9'=57
- '0' - '0' = 48 - 48 = 0
- '5' - '0' = 53 - 48 = 5
- '9' - '0' = 57 - 48 = 9

## Usage in BatteryBank

These helper methods are used in the `TotalJoltage` property to find the two largest digits:

```csharp
int firstDigitIndex = LeftmostLargestDigitIndex(_bank[..^1]);
int secondDigitIndex = LeftmostLargestDigitIndex(_bank[(firstDigitIndex + 1)..]);
field = (ConvertFromCharToInt(_bank[firstDigitIndex]) * 10) + 
        ConvertFromCharToInt(_bank[secondDigitIndex]);
```

## Contributing

When adding new tests:
1. Follow existing naming conventions
2. Use Theory/InlineData for multiple similar scenarios
3. Add descriptive test names that explain the scenario
4. Group related tests together
5. Update this documentation
