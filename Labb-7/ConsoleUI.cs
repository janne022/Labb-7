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

        public ConsoleUI()
        {
            this.menu = new Menu();
        }

        public void ShowMenu()
        {
            menu.ShowOptions();
        }
        void ReadInput()
        {
        
        }

        void DisplayQuestions() { }
    }
}
