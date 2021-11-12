using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Trests.Scenes
{
  class Scene2 : IScene
  {
    public Task Run()
    {
      BenchmarkRunner.Run<NameParserBenchmarks>();
      return Task.CompletedTask;
    }
  }

  public class NameParser
  {
    public string GetLastName(string fullName)
    {
      var names = fullName.Split(" ");

      var lastName = names.LastOrDefault();

      return lastName ?? string.Empty;
    }

    public string GetLastNameUsingSubstring(string fullName)
    {
      var lastSpaceIndex = fullName.LastIndexOf(" ", StringComparison.Ordinal);

      return lastSpaceIndex == -1
          ? string.Empty
          : fullName.Substring(lastSpaceIndex + 1);
    }

    public ReadOnlySpan<char> GetLastNameWithSpan(ReadOnlySpan<char> fullName)
    {
      var lastSpaceIndex = fullName.LastIndexOf(' ');

      return lastSpaceIndex == -1
          ? ReadOnlySpan<char>.Empty
          : fullName.Slice(lastSpaceIndex + 1);
    }
  }

  [RankColumn]
  [Orderer(SummaryOrderPolicy.FastestToSlowest)]
  [MemoryDiagnoser]
  public class NameParserBenchmarks
  {
    private const string FullName = "John Doe";
    private static readonly NameParser Parser = new NameParser();

    [Benchmark(Baseline = true)]
    public void GetLastName()
    {
      Parser.GetLastName(FullName);
    }

    [Benchmark]
    public void GetLastNameUsingSubstring()
    {
      Parser.GetLastNameUsingSubstring(FullName);
    }

    [Benchmark]
    public void GetLastNameWithSpan()
    {
      Parser.GetLastNameWithSpan(FullName);
    }
  }
}
