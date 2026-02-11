using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

using BenchmarkDotNet.Attributes;

using Day5b;

using Microsoft.VSDiagnostics;

namespace BenchmarkSuite2;

[CPUUsageDiagnoser]
[DotNetObjectAllocDiagnoser]
public class IdRangeProcessorBenchmarks
{
    private string _smallInput = string.Empty;
    private string _mediumInput = string.Empty;
    private string _largeInput = string.Empty;
    [GlobalSetup]
    public void Setup()
    {
        // Small: 10 ranges, each with 10 IDs (100 total IDs)
        _smallInput = string.Join("\n", Enumerable.Range(0, 10).Select(static i => $"{i * 10}-{(i * 10) + 9}"));
        // Medium: 100 ranges, each with 100 IDs (10,000 total IDs)
        _mediumInput = string.Join("\n", Enumerable.Range(0, 100).Select(i => $"{i * 100}-{(i * 100) + 99}"));
        // Large: 500 ranges, each with 200 IDs (100,000 total IDs)
        _largeInput = string.Join("\n", Enumerable.Range(0, 500).Select(i => $"{i * 200}-{(i * 200) + 199}"));
    }

    // Original implementation benchmarks
    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Small")]
    public HashSet<BigInteger> Original_Small() => ParseIdRanges_Original(new StringReader(_smallInput));

    [Benchmark]
    [BenchmarkCategory("Medium")]
    public HashSet<BigInteger> Original_Medium() => ParseIdRanges_Original(new StringReader(_mediumInput));

    [Benchmark]
    [BenchmarkCategory("Large")]
    public HashSet<BigInteger> Original_Large() => ParseIdRanges_Original(new StringReader(_largeInput));

    // Optimized implementation benchmarks
    [Benchmark]
    [BenchmarkCategory("Small")]
    public HashSet<BigInteger> Optimized_Small() => IdRangeProcessor.ParseIdRanges(new StringReader(_smallInput));

    [Benchmark]
    [BenchmarkCategory("Medium")]
    public HashSet<BigInteger> Optimized_Medium() => IdRangeProcessor.ParseIdRanges(new StringReader(_mediumInput));

    [Benchmark]
    [BenchmarkCategory("Large")]
    public HashSet<BigInteger> Optimized_Large() => IdRangeProcessor.ParseIdRanges(new StringReader(_largeInput));

    // Parallel implementation benchmarks
    [Benchmark]
    [BenchmarkCategory("Small")]
    public HashSet<BigInteger> Parallel_Small() => IdRangeProcessor.ParseIdRangesParallel(new StringReader(_smallInput));

    [Benchmark]
    [BenchmarkCategory("Medium")]
    public HashSet<BigInteger> Parallel_Medium() => IdRangeProcessor.ParseIdRangesParallel(new StringReader(_mediumInput));

    [Benchmark]
    [BenchmarkCategory("Large")]
    public HashSet<BigInteger> Parallel_Large() => IdRangeProcessor.ParseIdRangesParallel(new StringReader(_largeInput));

    /// <summary>
    /// Original implementation using Split('-') and immediate HashSet population.
    /// </summary>
    private static HashSet<BigInteger> ParseIdRanges_Original(StringReader input)
    {
        HashSet<BigInteger> freshIds = [];
        string? line;
        while (!string.IsNullOrWhiteSpace(line = input.ReadLine()))
        {
            string[] ids = line.Split('-');
            if (ids.Length != 2)
            {
                continue;
            }

            BigInteger start = BigInteger.Parse(ids[0]);
            BigInteger end = BigInteger.Parse(ids[1]);
            for (BigInteger id = start; id <= end; id++)
            {
                freshIds.Add(id);
            }
        }

        return freshIds;
    }
}
