# Debugging Helper Code

Add this temporary debugging code to your `Dial.Rotate()` method to see what's happening:

## Add to Dial.cs (Temporary Debugging)

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

    // === DEBUG OUTPUT START ===
    Console.WriteLine($"?? Rotate Debug ?????????????????????");
    Console.WriteLine($"? Direction: {rotation.Direction}");
    Console.WriteLine($"? Distance: {rotation.Distance}");
    Console.WriteLine($"? Old Position: {oldPosition}");
    Console.WriteLine($"? New Position: {Position.Value}");
    Console.WriteLine($"? Offset: {offset}");
    Console.WriteLine($"? Count: {Count}");
    Console.WriteLine($"? Full Rotations: {rotation.Distance / Count}");
    Console.WriteLine($"? Remaining Distance: {rotation.Distance % Count}");
    // === DEBUG OUTPUT END ===

    int passesZero = rotation.Distance / 100;  // Current line 159
    switch (offset)
    {
        case > 0 when Position.Value < oldPosition && Position.Value != 0:
            Console.WriteLine($"? Case: Right rotation wrapped, new != 0");
            passesZero++;
            break;
        case < 0 when Position.Value > oldPosition && Position.Value != 0:
            Console.WriteLine($"? Case: Left rotation wrapped, new != 0");
            passesZero++;
            break;
        case == 0:
            Console.WriteLine($"? Case: No offset");
            break;
        default:
            Console.WriteLine($"? Case: No special case");
            break;
    }

    Console.WriteLine($"? Passes Zero: {passesZero}");
    Console.WriteLine($"?????????????????????????????????????");
    Console.WriteLine();

    return passesZero;
}
```

## Example Output

When you run a test like `Start: 90, R20`, you'll see:

```
?? Rotate Debug ?????????????????????
? Direction: R
? Distance: 20
? Old Position: 90
? New Position: 10
? Offset: 20
? Count: 100
? Full Rotations: 0
? Remaining Distance: 20
? Case: Right rotation wrapped, new != 0
? Passes Zero: 1
?????????????????????????????????????
```

This helps you see:
1. What the inputs are
2. What the calculations produce
3. Which case in the switch statement is triggered
4. What the final result is

## Quick Test Command

Run a specific failing test with this output:

```bash
dotnet test Day1b.Tests\Day1b.Tests.csproj --filter "StartingAtZeroAndRotatingLeftDoesNotCountAsPassingZero"
```

You'll see the debug output showing why it's failing.

## Remove Debug Output

Once you've fixed the issues, remove all the `Console.WriteLine` debug statements.

## Alternative: Use a Debugger

Instead of console output, you can:

1. Set a breakpoint in `Dial.Rotate()` at line 146
2. Run the test in debug mode
3. Step through and watch the variables
4. Inspect the values of:
   - `oldPosition`
   - `newPosition` 
   - `Position.Value`
   - `passesZero`
   - The switch case that executes

Visual Studio Test Explorer ? Right-click test ? Debug Selected Tests
