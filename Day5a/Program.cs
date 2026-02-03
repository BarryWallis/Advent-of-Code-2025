using System.Numerics;
using System.Threading;

using Day5a;

using IntervalTree;

FreshIdDatabase freshIdDatabase = new(Console.In);
BigInteger result = freshIdDatabase.CheckIngredients(Console.In);
Console.WriteLine(result.ToString());

Thread.Sleep(100000);
