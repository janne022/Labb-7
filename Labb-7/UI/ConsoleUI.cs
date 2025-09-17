using Labb_7.DBHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.UI
{
    internal class ConsoleUI
    {
        readonly Dictionary<MenuOptions, Action> optionMenu = new Dictionary<MenuOptions, Action>()
        {
            {MenuOptions.StartQuiz, DisplayQuestions},
            {MenuOptions.ManageQuiz, ReadInput},
            {MenuOptions.Exit, Exit}

        };

        public ConsoleUI()
        {
            SQLitePCL.Batteries.Init();
        }
        // Calls ShowOptions to get the options that the user showed, then invokes the method using a simple dictonary lookup. Dictonary may return error if MenuOptions is expanded without dictonary expansion.
        public void ShowMenu()
        {
            Menu menu = new Menu(new string[] { "Start Quiz", "Manage Quiz", "Exit" });
            MenuOptions chosenOption = menu.ShowOptions<MenuOptions>();
            Console.Clear();
            Action action = optionMenu[chosenOption];
            action.Invoke();
        }
        // This method is responsible for allowing the user to edit the quiz and inserting their own questions with options.
        private static void ReadInput()
        {
            Console.WriteLine("Edit Quiz");
            Menu menu = new Menu(new string[] { "Create Question", "Edit Question", "Delete Question" });
            MenuOptions chosenOption = menu.ShowOptions<MenuOptions>();
            using (var context = new QuizDbContext())
            {
                // create database
                context.Database.EnsureCreated();

                // test to make a new object
                var option = new Option
                {
                    text = "Sweden",
                    IsCorrectOption = true
                };

                var option2 = new Option
                {
                    text = "Denmark",
                    IsCorrectOption = false
                };
                var options = new List<Option>() { option,option2 };
                var question = new Question("Vad heter Sverige på engelska?",options);

                context.Questions.Add(question);
                context.SaveChanges();
                foreach (var item in context.Questions)
                {
                    Console.WriteLine(item.Text);
                }
            }
        }
        // Responsible for starting the quiz, should create a new player and grab questions from db
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
