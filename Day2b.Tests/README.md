# Day2b Test Suite

## Overview
Comprehensive test suite for the Day2b project with tests for `BigIntegerExtensions` and `InputParser` classes.

## Test Project Structure

### Day2b.Tests.csproj
- Target Framework: .NET 10.0
- Language Version: C# 14.0
- Test Framework: xUnit 2.9.2
- Project Reference: Day2b.csproj
- Test SDK: Microsoft.NET.Test.Sdk 17.11.1
- Code Coverage: coverlet.collector 6.0.2

## Test Files

### BigIntegerExtensionsTests.cs (38 tests)
Tests for the `BigIntegerExtensions` class covering:

#### IsValid Method (38 tests)
Tests the repeating pattern detection logic:

**No Repeating Pattern (Returns true)**:
- Single digits (1, 5, 9)
- Zero
- Two different digits (12, 23, 98)
- Three digits (123, 456, 789)
- Non-repeating patterns (1234, 5678, 12345678)
- Large non-repeating numbers

**Repeating Patterns (Returns false)**:
- One-digit patterns: 11, 22, 33, 99
- Two-digit patterns: 1212, 5656, 9999, 1010
- Three-digit patterns: 123123, 456456, 789789
- Four-digit patterns: 12341234, 56785678
- Five-digit patterns: 1234512345
- All same digits: 1111, 2222, 5555
- Triple repetitions: 121212, 343434, 123123123
- Patterns with zeros: 100100
- Large repeating patterns

### InputParserTests.cs (32 tests)
Tests for the `InputParser.GetInput` method covering:

#### Valid Input Parsing (9 tests)
- **Single range**: "1-3" ? [1, 2, 3]
- **Multiple ranges**: "1-3,5-7" ? [1, 2, 3, 5, 6, 7]
- **Single value range**: "5-5" ? [5]
- **Large numbers**: Very large BigInteger ranges
- **Three ranges**: "1-2,4-5,7-8"
- **Including zero**: "0-2" ? [0, 1, 2]
- **Order preservation**: Returns values in the order ranges are specified
- **Large ranges**: "1-100" returns 100 values
- **Consecutive ranges**: "1-3,4-6"
- **Overlapping ranges**: "1-3,2-4" includes duplicates [1, 2, 3, 2, 3, 4]

#### Range Validation (5 tests)
- **Reversed range**: "10-5" throws FormatException with appropriate message
- **Multiple ranges with reversed**: "1-3,10-5,7-9" throws FormatException
- **Negative start**: "-3--1" throws FormatException (negative numbers not allowed)
- **Negative end**: "1--5" throws FormatException (negative numbers not allowed)
- **Mixed negative/positive**: "-2-2" throws FormatException (negative numbers not allowed)

#### Invalid Format Handling (15 tests)
- **Empty input**: Throws InvalidOperationException
- **Invalid format**: "1", "abc", "1-2-3" throw FormatException
- **Mixed valid/invalid**: Throws FormatException
- **Non-numeric values**: "abc-def", "1-abc", "abc-1"
- **Multiple commas**: ",," throws FormatException
- **Trailing comma**: "1-2," throws FormatException
- **Leading comma**: ",1-2" throws FormatException
- **Only commas**: ",,," throws FormatException
- **Single hyphen**: "-" throws FormatException
- **Spaces around hyphen**: "1 - 3" throws FormatException
- **Scientific notation**: "1e5-1e6" throws FormatException
- **Decimal numbers**: "1.5-3.5" throws FormatException

#### Edge Cases (3 tests)
- **Whitespace handling**: " 1-3 " works correctly
- **Large BigInteger ranges**: Very large numbers across multiple values
- **Many small ranges**: "1-1,2-2,3-3,4-4,5-5"
- **Starting at zero**: "0-5"
- **Returns new instances**: Each call returns a new List instance
- **Equality of results**: Same input produces equal results

## Running Tests

### Run All Tests
```bash
dotnet test Day2b.Tests\Day2b.Tests.csproj
```

### Run Specific Test Class
```bash
# Run only BigIntegerExtensions tests
dotnet test Day2b.Tests\Day2b.Tests.csproj --filter "FullyQualifiedName~BigIntegerExtensionsTests"

# Run only InputParser tests
dotnet test Day2b.Tests\Day2b.Tests.csproj --filter "FullyQualifiedName~InputParserTests"
```

### Run Specific Test
```bash
dotnet test Day2b.Tests\Day2b.Tests.csproj --filter "IsValidWithRepeatingPatternReturnsFalse"
```

### Run with Detailed Output
```bash
dotnet test Day2b.Tests\Day2b.Tests.csproj --verbosity normal
```

## Test Results

```
Total Tests: 70
Expected Results: 70 passing

BigIntegerExtensionsTests: 38 tests
  - IsValid: 38 tests

InputParserTests: 32 tests
  - Valid parsing: 9 tests
  - Range validation: 5 tests
  - Invalid format: 15 tests
  - Edge cases: 3 tests
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

### BigIntegerExtensions ? 100%
- IsValid method (complex repeating pattern logic)
  - All pattern lengths (1-5+ digits)
  - All repetition counts
  - Edge cases with zeros, large numbers

### InputParser ? 100%
- Valid input parsing
- Range validation (including negative number rejection - new in Day2b)
- Format validation
- Error handling
- Edge cases with whitespace, special characters
- Large number handling

## Key Differences from Day2a

Day2b includes **additional validation** in `InputParser`:
```csharp
if (start < BigInteger.Zero)
{
    throw new FormatException($"Range start cannot be negative: {part}");
}
if (end < BigInteger.Zero)
{
    throw new FormatException($"Range end cannot be negative: {part}");
}
if (start > end)
{
    throw new FormatException($"Start of range must be less than or equal to end: {part}");
}
```

Day2b also has a **different `IsValid` implementation**:
- Day2a: Checks if first half matches second half (also has NumberOfDigits and IsOdd methods)
- Day2b: Checks for any repeating pattern (more complex, only has IsValid method)

Day2b **does not support negative numbers** in input ranges:
- Day2a: Allows negative numbers in ranges
- Day2b: Throws FormatException for any negative numbers

## Notes

1. **IsValid Method Complexity**: The Day2b version is more sophisticated than Day2a, checking for patterns of any divisor length, not just half-length matches.

2. **Range Validation**: Day2b adds validation to ensure range start ? end and that both values are non-negative, which Day2a doesn't have for negative numbers.

3. **Comprehensive Coverage**: Tests cover:
   - Normal cases
   - Edge cases
   - Error conditions
   - Large values (BigInteger purpose)
   - Zero and boundary conditions

4. **Test Data**: Uses realistic test data including:
   - Small numbers (1-digit to 4-digit)
   - Large BigIntegers (30+ digits)
   - Zero
   - Edge cases (empty, whitespace, special chars)
   - Error validation for negative numbers

## Contributing

When adding new tests:
1. Follow existing naming conventions
2. Use Theory/InlineData for multiple similar scenarios
3. Add descriptive test names
4. Group related tests together
5. Update this documentation
