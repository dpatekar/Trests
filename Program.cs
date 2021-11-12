using System;
using System.Threading.Tasks;
using Trests.EntityFramework;

namespace Trests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var s1 = new Scene1();
            await s1.Run();
        }
    }
}
