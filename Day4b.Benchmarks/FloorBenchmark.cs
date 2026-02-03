using BenchmarkDotNet.Attributes;
using System.IO;
using Microsoft.VSDiagnostics;

[CPUUsageDiagnoser]
public class FloorBenchmark
{
    private string _inputData = string.Empty;
    /// <summary>
    /// Loads the actual Advent of Code input data once before benchmarking.
    /// </summary>
    [GlobalSetup]
    public void Setup()
    {
        string dataFilePath = "data.txt";
        _inputData = File.ReadAllText(dataFilePath);
    }

    /// <summary>
    /// Benchmarks the CountAccessibleRolls method with real input data.
    /// </summary>
    [Benchmark]
    public int CountAccessibleRolls()
    {
        StringReader input = new(_inputData);
        Floor floor = new(input);
        return floor.CountAccessibleRolls();
    }

    /// <summary>
    /// Benchmarks the RemoveAllRolls method with real input data.
    /// This is the most important benchmark as it calls CountAccessibleRolls multiple times.
    /// </summary>
    [Benchmark]
    public int RemoveAllRolls()
    {
        StringReader input = new(_inputData);
        Floor floor = new(input);
        return floor.RemoveAllRolls();
    }

    /// <summary>
    /// Benchmarks just the Floor constructor parsing performance.
    /// </summary>
    [Benchmark]
    public object ParseFloor()
    {
        StringReader input = new(_inputData);
        return new Floor(input);
    }
}
