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
            // Make sure database and sqlite is instalized
            using (var context = new QuizDbContext())
            {
                context.Database.EnsureCreated();
            }
            SQLitePCL.Batteries.Init();
        }
        // Calls ShowOptions to get the options that the user showed, then invokes the method using a simple dictonary lookup. Dictonary may return error if MenuOptions is expanded without dictonary expansion.
        public void ShowMenu()
        {
            while (true)
            {
                MenuOptions chosenOption = Menu.ReadOption<MenuOptions>("What would you like to do?", ["Start Quiz", "Manage Quiz", "Exit"]);
                Console.Clear();
                Action action = optionMenu[chosenOption];
                action.Invoke();
            }
        }
        // This method is responsible for allowing the user to edit the quiz and inserting their own questions with options.
        private static void ReadInput()
        {
            Console.WriteLine("Edit Quiz");
            ManageQuestionOptions chosenOption = Menu.ReadOption<ManageQuestionOptions>("What would you like to do?", [ "Create Question", "Edit Question", "Delete Question" ]);
            var optionList = new List<Option>();
            switch (chosenOption)
            {
                case ManageQuestionOptions.CreateOption:
                    Console.Clear();
                    string questionInput = Menu.ReadInput("What would you like to name your question?",3,100);
                    // Always create 4 options
                    for (int i = 0; i < 4; i++)
                    {
                        string optionInput = Menu.ReadInput($"Choose a name for option: {i + 1}\tQuestion: {questionInput}", 3, 20);
                        YesNo correctAnswer = Menu.ReadOption<YesNo>("Is this the correct answer?",[ "Yes", "No" ]);
                        bool isCorrectAnswer = (correctAnswer == YesNo.Yes) ? true : false;
                        Option createdOption = new Option(optionInput, isCorrectAnswer );
                        optionList.Add( createdOption );
                    }
                    var createdQuestion = new Question(questionInput,optionList);
                    using (var context = new QuizDbContext())
                    {
                        // Add question to database
                        new QuestionRepository(context).Add(createdQuestion);
                    }
                    break;
                case ManageQuestionOptions.EditOption:
                    break;
                case ManageQuestionOptions.DeleteOption:
                    break;
            }
        }
        // Responsible for starting the quiz, should create a new player and grab questions from db
        private static void DisplayQuestions()
        {
            // Retrieve player name
            string name = Menu.ReadInput("What would you like your name to be?",3,14);
            var player = new Player(name);
            using (var context = new QuizDbContext())
            {
                new PlayerRepository(context).Add(player);
            }
            // Start quiz
            QuizService.StartQuiz(player);
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting");
            return;
        }
    }
}
