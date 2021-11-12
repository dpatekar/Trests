using System.Threading.Tasks;
using Trests.Scenes;

namespace Trests
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var s = new Scene1();
      await s.Run();
    }
  }
}
