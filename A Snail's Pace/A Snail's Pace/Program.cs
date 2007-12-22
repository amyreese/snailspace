using System;

namespace A_Snail_s_Pace
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SnailsPace game = new SnailsPace())
            {
                game.Run();
            }
        }
    }
}
