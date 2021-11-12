using Newtonsoft.Json;
using System;

namespace Trests
{
  public static class Extensions
  {
    public static void Dump<T>(this T x)
    {
      string json = JsonConvert.SerializeObject(x, Formatting.Indented);
      Console.WriteLine(json);
    }
  }
}
