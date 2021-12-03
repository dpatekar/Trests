using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnostics.Windows.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using System.Threading;
using System.Threading.Tasks;

namespace Trests.Scenes
{
  class Scene3 : IScene
  {
    public Task Run()
    {
      BenchmarkRunner.Run<AsyncBenchmark>();
      return Task.CompletedTask;
    }
  }

  static class AsyncHelper
  {
    public static async Task<int> RunSleepAsync()
    {
      return await Task.Run(() =>
      {
        var ms = 100;
        Thread.Sleep(ms);
        return ms;
      });
    }
  }

  [MemoryDiagnoser]
  [TailCallDiagnoser]
  [EtwProfiler]
  [ConcurrencyVisualizerProfiler]
  [NativeMemoryProfiler]
  [ThreadingDiagnoser]
  public class AsyncBenchmark
  {
    [Benchmark(Baseline = true)]
    public async Task AwaitSleep()
    {
      await AsyncHelper.RunSleepAsync();
    }

    [Benchmark]
    public void BlockWait()
    {
      AsyncHelper.RunSleepAsync().Wait();
    }

    [Benchmark]
    public void BlockResult()
    {
      _ = AsyncHelper.RunSleepAsync().Result;
    }

    [Benchmark]
    public void BlockAwaiterResult()
    {
      _ = AsyncHelper.RunSleepAsync().GetAwaiter().GetResult();
    }
  }
}
