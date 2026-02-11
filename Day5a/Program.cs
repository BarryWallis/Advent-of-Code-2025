using System.Numerics;

using Day5a;

FreshIdDatabase freshIdDatabase = new(Console.In);
BigInteger result = freshIdDatabase.CheckIngredients(Console.In);
Console.WriteLine(result.ToString());

Thread.Sleep(100000);
