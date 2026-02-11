using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

Summary[] _ = BenchmarkRunner.Run(typeof(Program).Assembly);
