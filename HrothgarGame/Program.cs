﻿using System;

namespace HrothgarGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new TmpGame())
                game.Run();
        }
    }
}