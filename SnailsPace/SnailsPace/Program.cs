using System;

namespace SnailsPace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
			System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
			using (SnailsPace game = new SnailsPace())
            {
				game.Run();
            }
        }
    }
}

