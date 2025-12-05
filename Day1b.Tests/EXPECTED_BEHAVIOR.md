# Expected Behavior: Passes Zero Counting

## Rule Definition

The `Rotate` method should count the number of times the rotation **passes through OR lands on** position 0, with one exception:
- **Exception**: If the rotation STARTS at position 0, we don't count leaving position 0

## Visual Examples (Dial with 100 positions: 0-99)

### Example 1: Simple Right Rotation (No Pass)
```
Start: 10, Rotate Right 20
Path: 10 ? 11 ? 12 ? ... ? 30
Result: Position 30, Passes = 0 ?
```

### Example 2: Pass Through Zero (Right)
```
Start: 90, Rotate Right 20
Path: 90 ? 91 ? ... ? 99 ? [0] ? 1 ? ... ? 10
                              ? PASS!
Result: Position 10, Passes = 1 ?
```

### Example 3: Land Exactly on Zero (Right)
```
Start: 50, Rotate Right 50
Path: 50 ? 51 ? ... ? 99 ? [0]
                             ? LAND ON ZERO!
Result: Position 0, Passes = 1 ?
```

### Example 4: Start at Zero, Move Away (Right)
```
Start: 0, Rotate Right 10
Path: [0] ? 1 ? 2 ? ... ? 10
      ? START HERE (don't count)
Result: Position 10, Passes = 0 ?
```

### Example 5: Start at Zero, Move Away (Left)
```
Start: 0, Rotate Left 10
Path: [0] ? 99 ? 98 ? ... ? 90
      ? START HERE (don't count)
Result: Position 90, Passes = 0 ?
```

### Example 6: Full Rotation from Non-Zero (Right)
```
Start: 25, Rotate Right 100
Path: 25 ? 26 ? ... ? 99 ? [0] ? 1 ? ... ? 25
                              ? PASS!
Result: Position 25, Passes = 1 ?
```

### Example 7: Full Rotation from Zero (Right)
```
Start: 0, Rotate Right 100
Path: [0] ? 1 ? ... ? 99 ? [0]
      ?                      ? RETURN TO ZERO!
   START HERE            COUNT THIS!
Result: Position 0, Passes = 1 ?
```

### Example 8: Two Full Rotations from Zero (Right)
```
Start: 0, Rotate Right 200
Path: [0] ? ... ? 99 ? [0] ? ... ? 99 ? [0]
      ?                  ?                  ?
   START            1st PASS          2nd PASS
Result: Position 0, Passes = 2 ?
```

### Example 9: One and a Half Rotations (Right)
```
Start: 25, Rotate Right 150
Path: 25 ? ... ? 99 ? [0] ? ... ? 99 ? [0] ? ... ? 75
                       ?                  ?
                   1st PASS          2nd PASS
Result: Position 75, Passes = 2 ?

Breakdown:
- Distance to first zero: 100 - 25 = 75
- Remaining distance: 150 - 75 = 75
- This takes us to position 75, crossing zero once more
```

### Example 10: Pass Through Zero (Left)
```
Start: 10, Rotate Left 20
Path: 10 ? 9 ? ... ? 1 ? [0] ? 99 ? ... ? 90
                            ? PASS!
Result: Position 90, Passes = 1 ?
```

## Algorithm Breakdown

For a rotation starting at position `P` with distance `D` on a dial with `Count` positions:

### Case 1: Starting at Zero
```
if (oldPosition == 0):
    passesZero = D / Count
```
We don't count leaving zero, only full rotations back to zero.

### Case 2: Rotating Right (offset > 0)
```
fullRotations = D / Count
remainingDistance = D % Count

if (P + remainingDistance >= Count):
    passesZero = fullRotations + 1  // We cross/land on zero
else:
    passesZero = fullRotations       // We don't reach zero
```

### Case 3: Rotating Left (offset < 0)
```
fullRotations = D / Count
remainingDistance = D % Count

if (remainingDistance >= P):
    passesZero = fullRotations + 1  // We cross/land on zero
else:
    passesZero = fullRotations       // We don't reach zero
```

## Test Case Truth Table

| Start | Direction | Distance | End | Full Rotations | Partial Crosses? | Expected Passes |
|-------|-----------|----------|-----|----------------|------------------|-----------------|
| 10    | R         | 20       | 30  | 0              | No               | 0               |
| 90    | R         | 20       | 10  | 0              | Yes              | 1               |
| 50    | R         | 50       | 0   | 0              | Yes (lands)      | 1               |
| 0     | R         | 10       | 10  | 0              | No (start at 0)  | 0               |
| 0     | R         | 100      | 0   | 1              | N/A              | 1               |
| 0     | R         | 200      | 0   | 2              | N/A              | 2               |
| 25    | R         | 150      | 75  | 1              | Yes              | 2               |
| 10    | L         | 20       | 90  | 0              | Yes              | 1               |
| 50    | L         | 50       | 0   | 0              | Yes (lands)      | 1               |
| 0     | L         | 10       | 90  | 0              | No (start at 0)  | 0               |
| 0     | L         | 100      | 0   | 1              | N/A              | 1               |
| 75    | L         | 150      | 25  | 1              | Yes              | 2               |

## Common Mistakes to Avoid

1. ? Counting leaving position 0 as a pass
2. ? Not counting landing exactly on position 0
3. ? Using hardcoded values instead of `Count`
4. ? Not accounting for partial rotations that cross zero
5. ? Incorrect logic for determining if a partial rotation crosses zero

## Program.cs Scenario Walkthrough

Starting position: 50

**Rotation 1: R10**
- Start: 50, End: 60
- No zero crossing
- Passes: 0, Total: 0

**Rotation 2: R60**
- Start: 60, End: 20 (goes 60?99?0?20)
- Crosses zero once
- Passes: 1, Total: 1

**Rotation 3: L80**
- Start: 20, End: 40 (goes 20?0?99?40)
- Crosses zero once
- Passes: 1, Total: 2

**Rotation 4: R100**
- Start: 40, End: 40 (full rotation)
- Crosses zero once (full rotation)
- Passes: 1, Total: 3

**Wait, that's only 3, but test expects 4!**

Let me recalculate R100 from position 40:
- 40 + 100 = 140
- 140 / 100 = 1 full rotation
- 140 % 100 = 40 (ending position)
- Distance to first zero: 100 - 40 = 60
- This means we pass zero at position 60 of the rotation
- Then we continue for another 40 positions, ending at 40
- This is ONE pass through zero during the full rotation

Actually, I need to reconsider the logic. Let me re-examine...

After further analysis, the expected value of 4 might be incorrect, OR the interpretation of "passes zero or ends on zero" needs clarification.
