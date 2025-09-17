using Labb_7.DBHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Menu startMenu = new Menu(new string[] { "Start Quiz", "Manage Quiz", "Exit" });
            MenuOptions chosenOption = startMenu.ShowOptions<MenuOptions>("What would you like to do?");
            Console.Clear();
            Action action = optionMenu[chosenOption];
            action.Invoke();
        }
        // This method is responsible for allowing the user to edit the quiz and inserting their own questions with options.
        private static void ReadInput()
        {
            Console.WriteLine("Edit Quiz");
            Menu questionMenu = new Menu(new string[] { "Create Question", "Edit Question", "Delete Question" });
            QuestionOptions chosenOption = questionMenu.ShowOptions<QuestionOptions>("What would you like to do?");
            var optionList = new List<Option>();
            Question createdQuestion = null;
            switch (chosenOption)
            {
                case QuestionOptions.CreateOption:
                    Console.Clear();
                    Console.Write("Question Text: ");
                    string questionInput = Console.ReadLine();
                    // Always create 4 options
                    for (int i = 0; i < 4; i++)
                    {
                        Console.Clear();
                        Console.Write($"Option {i + 1}: ");
                        string optionInput = Console.ReadLine();
                        Menu correctAnswerMenu = new Menu(new string[] { "Yes", "No" });
                        YesNo correctAnswer = correctAnswerMenu.ShowOptions<YesNo>("Is this the correct answer?");
                        bool isCorrectAnswer = (correctAnswer == YesNo.Yes) ? true : false;
                        Option createdOption = new Option(optionInput, isCorrectAnswer );
                        optionList.Add( createdOption );
                    }
                    createdQuestion = new Question(questionInput,optionList);
                    break;
                case QuestionOptions.EditOption:
                    break;
                case QuestionOptions.DeleteOption:
                    break;
            }
            using (var context = new QuizDbContext())
            {
                // create database
                if (questionMenu != null)
                {
                    context.Database.EnsureCreated();
                    context.Questions.Add(createdQuestion);
                    context.SaveChanges();
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
