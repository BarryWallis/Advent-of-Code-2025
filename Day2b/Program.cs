using System.Numerics;

using Day2b;

List<BigInteger> IDs = InputParser.GetInput(Console.In);
BigInteger result = IDs.Where(static id => !id.IsValid()).Aggregate(BigInteger.Zero, (acc, id) => acc + id);
Console.WriteLine(result);
