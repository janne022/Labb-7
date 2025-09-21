using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7
{
    // Provides a list of global enums that can be used throughout the project
    public enum MenuOptions
    {
        StartQuiz = 0,
        ManageQuiz = 1,
        Exit = 2,
    }
    public enum ManageQuestionOptions
    {
        CreateOption = 0,
        EditOption = 1,
        DeleteOption = 2,
        Back = 3,
    }
    public enum YesNo
    {
        Yes = 0,
        No = 1,
    }

    public enum QuestionOptions
    {
        Option1 = 0,
        Option2 = 1,
        Option3 = 2,
        Option4 = 3,
    }

    public enum PlayAgain
    {
        Home = 0,
        PlayAgain = 1,
        Exit = 2,
    }
}
