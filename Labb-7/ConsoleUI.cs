using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7
{
    internal class ConsoleUI
    {
        private Menu menu;
        readonly Dictionary<MenuOptions, Action> optionMenu = new Dictionary<MenuOptions, Action>()
        {
            {MenuOptions.StartQuiz, DisplayQuestions},
            {MenuOptions.ManageQuiz, ReadInput},
            {MenuOptions.Exit, Exit}

        };

        public ConsoleUI()
        {
            this.menu = new Menu();
        }
        // Calls ShowOptions to get the options that the user showed, then invokes the method using a simple dictonary lookup. Dictonary may return error if MenuOptions is expanded without dictonary expansion.
        public void ShowMenu()
        {
            MenuOptions chosenOption = menu.ShowOptions();
            Console.Clear();
            Action action = optionMenu[chosenOption];
            action.Invoke();
        }
        private static void ReadInput()
        {
            Console.WriteLine("Edit Quiz");
        }

        private static void DisplayQuestions()
        {
            Console.WriteLine("Quiz started");
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting");
        }
    }
}
