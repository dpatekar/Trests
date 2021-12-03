using System;
using System.Text.Json;

namespace Trests
{
  public static class Extensions
  {
    public static void Dump<T>(this T x)
    {
      var json = JsonSerializer.Serialize<T>(x, new JsonSerializerOptions
      {
        WriteIndented = true
      });
      Console.WriteLine(json);
    }
  }
}
