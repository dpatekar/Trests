using System;
using System.Threading.Tasks;

namespace Trests
{
  class Program
  {
    static async Task Main(string[] args)
    {
      Console.Write("Enter scene number: ");
      var sceneNumber = int.Parse(Console.ReadLine());
      var sceneType = Type.GetType($"Trests.Scenes.Scene{sceneNumber}");
      var scene = (IScene)Activator.CreateInstance(sceneType);
      await scene.Run();
    }
  }
}
