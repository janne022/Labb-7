using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.UI
{
    internal class Menu
    {
        private string[] menuOptions;

        public Menu(string[] menuOptions)
        {
            this.menuOptions = menuOptions;
        }
        public T ShowOptions<T>(string questionText) where T : Enum
        {
            // add error handling for if there are less enum values than menu options?
            int i = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(questionText+"\n");
                for (int j = 0; j < menuOptions.Length; j++)
                {
                    Console.BackgroundColor = i == j ? ConsoleColor.White : ConsoleColor.Black;
                    Console.ForegroundColor = i == j ? ConsoleColor.Black : ConsoleColor.White;
                    Console.WriteLine(menuOptions[j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (i < menuOptions.Length-1) i++;
                        break;

                    case ConsoleKey.UpArrow:
                        if (i > 0) i--;
                        break;
                    case ConsoleKey.Enter:
                        // returns specified enum
                        return (T)Enum.ToObject(typeof(T), i);

                }
            }
        }
    }
}
