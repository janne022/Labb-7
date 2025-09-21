using Labb_7.DBHandling;
using Labb_7.UI;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7 
{
    internal static class QuizService
    {
        // Starts quiz and retrieves questions using QuestionRepository
        public static void StartQuiz(Player player)
        {
            bool replay = false;
            do
            {
                // Retrieve questions
                using (var context = new QuizDbContext())
                {
                    int questionAmount = context.Questions.Count();
                    string[] questionRange = Enumerable.Range(1, questionAmount).Select(q => q.ToString()).ToArray();
                    int roundAmount = Menu.ReadSlider($"How many questions do you want? Choose from 1-{questionAmount}", questionRange);
                    var quiz = new QuestionRepository(context);
                    // TODO: change length so it can be set by user
                    replay = DisplayQuestions(quiz.getRandomQuestions(roundAmount), player);
                }
            }
            while (replay);
        }
        private static bool DisplayQuestions(List<Question> questions,Player player)
        {
            // Display questions
            for (int i = 0; i < questions.Count; i++)
            {
                var correctOption = questions[i].Options.Where(option => option.IsCorrectOption);
                Console.WriteLine($"Question {i}: Options count = {questions[i].Options?.Count}");
                string[] optionsText = { questions[i].Options[0].Text, questions[i].Options[1].Text, questions[i].Options[2].Text, questions[i].Options[3].Text };
                QuestionOptions userQuestion = Menu.ReadOption<QuestionOptions>(questions[i].Text, optionsText);
                // Check if picked option is correct
                if (questions[i].Options[((int)userQuestion)].IsCorrectOption)
                {
                    player.score += 50;
                    using (var context = new QuizDbContext())
                    {
                        PlayerRepository playerRepository = new PlayerRepository(context);
                        playerRepository.Update(player);
                    }
                    Console.WriteLine($"Great job! You now have {player.score} points! Click Enter for next question");
                    Console.ReadLine();
                }
                else
                {
                    string correctOptionsString = string.Join(" or ",correctOption.Select(option => option.Text));
                    Console.WriteLine($"Ouch, not quite right. The correct answer was: {correctOptionsString}. You Currently have {player.score} points. Click Enter for next question");
                    Console.ReadLine();
                }
            }
            // Show Score
            return CalculateScore(player);
        }
        private static bool CalculateScore(Player player)
        {
            var gameChoices = Menu.ReadOption<PlayAgain>($"Congrats, you finished the game with a score of: {player.score} points!.", ["Home","Play Again","Exit"]);
            switch (gameChoices)
            {
                case PlayAgain.Home:
                    return false;
                case PlayAgain.PlayAgain:
                    return true;
                case PlayAgain.Exit:
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
            }
            return false;
        }
    }
}
