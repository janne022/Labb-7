using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7
{
    internal class Menu
    {
        private string[] _menuOptions = ["Start Quiz", "Manage Quiz", "Exit"];
        public MenuOptions ShowOptions()
        {
            int i = 0;
            while (true)
            {
                Console.Clear();
                for (int j = 0; j < _menuOptions.Length; j++)
                {
                    Console.BackgroundColor = i == j ? ConsoleColor.White : ConsoleColor.Black;
                    Console.ForegroundColor = i == j ? ConsoleColor.Black : ConsoleColor.White;
                    Console.WriteLine(_menuOptions[j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (i < 2) i++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (i > 0 ) i--;
                        break;
                    case ConsoleKey.Enter:
                        return (MenuOptions)i;

                }
            }
        }
    }
}
