using Day1b;

Console.WriteLine("=== Dial Passes Zero Manual Test ===\n");

static void TestRotation(int startPos, char direction, int distance, string description)
{
    Dial dial = new(startPos);
    Console.WriteLine($"Test: {description}");
    Console.WriteLine($"  Start: Position {dial.Position.Value}");
    
    Rotation rotation = new(direction, distance);
    int passes = dial.Rotate(rotation);
    
    Console.WriteLine($"  Rotation: {direction}{distance}");
    Console.WriteLine($"  End: Position {dial.Position.Value}");
    Console.WriteLine($"  Passes Zero: {passes}");
    Console.WriteLine();
}

// Test 1: No pass
TestRotation(10, 'R', 20, "Simple right rotation (no pass)");

// Test 2: No pass
TestRotation(50, 'L', 20, "Simple left rotation (no pass)");

// Test 3: Pass through zero (right)
TestRotation(90, 'R', 20, "Pass through zero going right");

// Test 4: Pass through zero (left)
TestRotation(10, 'L', 20, "Pass through zero going left");

// Test 5: Land exactly on zero (right)
TestRotation(50, 'R', 50, "Land exactly on zero going right");

// Test 6: Land exactly on zero (left)
TestRotation(50, 'L', 50, "Land exactly on zero going left");

// Test 7: Full rotation from zero
TestRotation(0, 'R', 100, "Full rotation starting at zero (right)");

// Test 8: Full rotation from zero
TestRotation(0, 'L', 100, "Full rotation starting at zero (left)");

// Test 9: Start at zero, move away (right)
TestRotation(0, 'R', 10, "Start at zero, move away (right) - should be 0");

// Test 10: Start at zero, move away (left)
TestRotation(0, 'L', 10, "Start at zero, move away (left) - should be 0");

// Test 11: Two full rotations
TestRotation(0, 'R', 200, "Two full rotations from zero");

// Test 12: One and a half rotations
TestRotation(25, 'R', 150, "1.5 rotations (25 + 150 = 175)");

// Test 13: One and a half rotations left
TestRotation(75, 'L', 150, "1.5 rotations left (75 - 150 = -75 = 25)");

// Test 14: Multiple rotations scenario from Program.cs
Console.WriteLine("=== Program.cs Scenario ===");
Dial programDial = new(50);
int totalPasses = 0;

Console.WriteLine($"Start: Position {programDial.Position.Value}");
Console.WriteLine();

int pass1 = programDial.Rotate(new Rotation('R', 10));
totalPasses += pass1;
Console.WriteLine($"R10: Position {programDial.Position.Value}, Passes: {pass1}, Total: {totalPasses}");

int pass2 = programDial.Rotate(new Rotation('R', 60));
totalPasses += pass2;
Console.WriteLine($"R60: Position {programDial.Position.Value}, Passes: {pass2}, Total: {totalPasses}");

int pass3 = programDial.Rotate(new Rotation('L', 80));
totalPasses += pass3;
Console.WriteLine($"L80: Position {programDial.Position.Value}, Passes: {pass3}, Total: {totalPasses}");

int pass4 = programDial.Rotate(new Rotation('R', 100));
totalPasses += pass4;
Console.WriteLine($"R100: Position {programDial.Position.Value}, Passes: {pass4}, Total: {totalPasses}");

Console.WriteLine();
Console.WriteLine($"Final Total Passes: {totalPasses} (Expected: 4)");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
