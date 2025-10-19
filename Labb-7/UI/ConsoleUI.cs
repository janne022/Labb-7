using Labb_7.Data;
using Labb_7.DBHandling;
using Labb_7.Models;
using Labb_7.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.UI
{
    public class ConsoleUI
    {
        // Dictionary which contains an enums as key and Actions (anonymous methods)
        readonly Dictionary<MenuOptions, Action> optionMenu = new Dictionary<MenuOptions, Action>()
        {
            {MenuOptions.StartQuiz, DisplayQuestions},
            {MenuOptions.ManageQuiz, ReadInput},
            {MenuOptions.Leaderboard, DisplayLeaderboard},
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
                MenuOptions chosenOption = Menu.ReadOption<string, MenuOptions>("What would you like to do?", ["Start Quiz", "Manage Quiz", "Leaderboard", "Exit"]);
                Console.Clear();
                Action action = optionMenu[chosenOption];
                // Calls whatever method it got from the dictionary
                action.Invoke();
            }
        }
        // This method is responsible for allowing the user to edit the quiz and inserting their own questions with options.
        private static void ReadInput()
        {
            bool continueLoop = true;
            do
            {
                ManageQuestionOptions chosenOption = Menu.ReadOption<string, ManageQuestionOptions>("What would you like to do?", ["Create Question", "Edit Question", "Delete Question", "Back"]);
                switch (chosenOption)
                {
                    case ManageQuestionOptions.CreateOption:
                        Console.Clear();
                        Question createdQuestion = CreateQuestion();
                        using (var context = new QuizDbContext())
                        {
                            // Add question to database
                            new QuestionRepository(context).Add(createdQuestion);
                        }
                        break;
                    case ManageQuestionOptions.EditOption:
                        do
                        {
                            Console.Clear();
                            using (var context = new QuizDbContext())
                            {
                                // Get all questions and also create an array of only the question text
                                var questionRepository = new QuestionRepository(context);
                                var questions = questionRepository.GetAll().ToList();
                                questions.Add(new Question("Back", new List<Option>()));
                                var questionStrings = questions.Select(q => q.Text).ToArray();
                                // List up all questions with readoption
                                int optionInput = Menu.ReadOptionIndex<string>($"Which question would you like to edit?", questionStrings);
                                if (optionInput == questionStrings.Length - 1)
                                {
                                    return;
                                }
                                // Make user create new question, then swap out values in originalQuestion with new question
                                Question editedQuestion = CreateQuestion();
                                Question originalQuestion = questions[optionInput];
                                originalQuestion.Text = editedQuestion.Text;
                                originalQuestion.Options = editedQuestion.Options;
                                // Update original object in database
                                questionRepository.Update(originalQuestion);
                            }
                        }
                        while (true);
                    case ManageQuestionOptions.DeleteOption:
                        do
                        {
                            Console.Clear();
                            using (var context = new QuizDbContext())
                            {
                                // Get all questions and also create an array of only the question text
                                var questionRepository = new QuestionRepository(context);
                                var questions = questionRepository.GetAll().ToList();
                                questions.Add(new Question("Back", new List<Option>()));
                                var questionStrings = questions.Select(q => q.Text).ToArray();
                                // List up all questions with readoption
                                int optionInput = Menu.ReadOptionIndex<string>($"Which question would you like to delete?", questionStrings);
                                if (optionInput == questionStrings.Length - 1)
                                {
                                    return;
                                }
                                YesNo removeQuestionInput = Menu.ReadOption<string, YesNo>("Are you sure you wish to delete this?", ["Yes", "No"]);
                                bool isCertain = (removeQuestionInput == YesNo.Yes) ? true : false;
                                if (isCertain)
                                {
                                    // Delete question
                                    var removeQuestion = questions[optionInput];
                                    questionRepository.Remove(removeQuestion);
                                }
                            }
                        }
                        while (true);
                    case ManageQuestionOptions.Back:
                        continueLoop = false;
                        break;
                }
            }
            while (continueLoop);
        }
        // Responsible for starting the quiz, should create a new player and grab questions from db
        private static void DisplayQuestions()
        {
            // Retrieve player name
            string name = Menu.ReadInput("What would you like your name to be?", 1, 14);
            var player = new Player(name);
            using (var context = new QuizDbContext())
            {
                new PlayerRepository(context).Add(player);
            }
            // Start quiz
            QuizService.StartQuiz(player);
        }
        // Display leaderboard
        private static void DisplayLeaderboard()
        {
            using (var context = new QuizDbContext())
            {
                // Get all players and sort them in descending order via player score
                var playerRepository = new PlayerRepository(context);
                var players = playerRepository.GetAll().OrderByDescending(p => p.Score).Where(p => p.Score > 0).ToArray();
                // Print out player and score
                Console.WriteLine("Player\t\tScore");
                for (int i = 0; i < players.Length; i++)
                {
                    Console.WriteLine($"{players[i].Name}\t\t{players[i].Score}");
                }
                Console.WriteLine("\nPress Enter to go back to the Main Menu");
                Console.ReadLine();
            }
        }
        private static Question CreateQuestion()
        {
            var optionList = new List<Option>();
            // Ask for question text
            string questionInput = Menu.ReadInput("Type in your question:", 3, 100);
            // Ask for 4 options, every question can be correct or false currently
            for (int i = 0; i < 4; i++)
            {
                string optionInput = Menu.ReadInput($"Choose a name for option: {i + 1}\tQuestion: {questionInput}", 1, 100);
                YesNo correctAnswer = Menu.ReadOption<string, YesNo>("Is this the correct answer?", ["Yes", "No"]);
                bool isCorrectAnswer = (correctAnswer == YesNo.Yes) ? true : false;
                Option createdOption = new Option(optionInput, isCorrectAnswer);
                optionList.Add(createdOption);
            }
            return new Question(questionInput, optionList);
        }
        // Exits the program
        private static void Exit()
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }
}
