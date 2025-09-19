using Labb_7.DBHandling;
using Labb_7.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7 
{
    internal static class QuizService
    {
        // Starts quiz and retrieves questions using QuestionRepository
        public static void StartQuiz(Player player)
        {
            List<Question> questions;
            // Retrieve questions
            using (var context = new QuizDbContext())
            {
                QuestionRepository quiz = new QuestionRepository(context);
                // TODO: change length so it can be set by user
                questions = quiz.getRandomQuestions(2);
            }
            // Display questions
            for (int i = 0; i < questions.Count(); i++)
            {
                Console.WriteLine($"Question {i}: Options count = {questions[i].Options?.Count}");
                string[] optionsText = { questions[i].Options[0].Text, questions[i].Options[1].Text, questions[i].Options[2].Text, questions[i].Options[3].Text };
                QuestionOptions userQuestion = Menu.ReadOption<QuestionOptions>(questions[i].Text,optionsText);
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
            }
        }
        static void AskQuestion(Question question) { }
        static void CalculateScore(Player player) { }
    }
}
