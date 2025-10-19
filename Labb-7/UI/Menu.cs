using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Labb_7.UI
{
    public static class Menu
    {
        /// <summary>
        /// Displays a menu with options for the user to choose from. They can navigate using arrow keys up and down. Clears console before displaying.
        /// </summary>
        /// <typeparam name="T">An array that is going to be displayed as strings</typeparam>
        /// <typeparam name="TEnum">An enum that represents options in menu, where each index is an option</typeparam>
        /// <param name="questionText">A string displayed at the top of the menu</param>
        /// <param name="menuOptions">Options in an array that is displayed to the user</param>
        /// <returns>Returns an Enum with chosen enum index</returns>
        public static TEnum ReadOption<T, TEnum>(string questionText, T[] menuOptions) where TEnum : Enum
        {
            int i = 0;
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                // Write out question and display options, currently selected index gets highlighted
                Console.WriteLine(questionText + "\n");
                for (int j = 0; j < menuOptions.Length; j++)
                {
                    Console.BackgroundColor = i == j ? ConsoleColor.White : ConsoleColor.Black;
                    Console.ForegroundColor = i == j ? ConsoleColor.Black : ConsoleColor.White;
                    Console.WriteLine(menuOptions[j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                // Reset color and read the key that user presses
                Console.ResetColor();
                ConsoleKey key = Console.ReadKey(true).Key;
                // Check what user pressed and go up or down in index as long as it is within the length of menuOptions. Enter returns Enum
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (i < menuOptions.Length - 1)
                        {
                            i++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (i > 0)
                        {
                            i--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        // returns specified enum
                        return (TEnum)Enum.ToObject(typeof(TEnum), i);

                }
            }
        }
        /// <summary>
        /// Displays a menu with options for the user to choose from. They can navigate using arrow keys up and down. Clears console before displaying.
        /// </summary>
        /// <typeparam name="T">An array that is going to be displayed as strings</typeparam>
        /// <param name="questionText">A string displayed at the top of the menu</param>
        /// <param name="menuOptions">Options in an array that is displayed to the user</param>
        /// <returns>Returns the index chosen by user</returns>
        public static int ReadOptionIndex<T>(string questionText, T[] menuOptions)
        {
            int i = 0;
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(questionText + "\n");
                for (int j = 0; j < menuOptions.Length; j++)
                {
                    Console.BackgroundColor = i == j ? ConsoleColor.White : ConsoleColor.Black;
                    Console.ForegroundColor = i == j ? ConsoleColor.Black : ConsoleColor.White;
                    Console.WriteLine(menuOptions[j]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (i < menuOptions.Length - 1)
                        {
                            i++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (i > 0)
                        {
                            i--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        // returns index as int
                        return i;

                }
            }
        }
        // Helper method, allow user to press right or left arrow keys to control value in middle and Enter to choose
        /// <summary>
        /// Displays a slider-style menu that allows user to press left or right arrow keys to go to the next or previous value
        /// </summary>
        /// <param name="questionText">A string displayed at the top of the menu</param>
        /// <param name="menuOptions">Options in an array that is displayed to the user</param>
        /// <returns>Returns the index chosen by user</returns>
        public static int ReadSlider(string questionText, string[] menuOptions)
        {
            int i = 0;
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(questionText + "\n");
                var sliderStructure = new StringBuilder(menuOptions[i]);
                if (i > 0)
                {
                    sliderStructure.Insert(0, "<- ");
                }
                if (i < menuOptions.Length)
                {
                    sliderStructure.Append(" ->");
                }
                Console.WriteLine(sliderStructure);
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (i < menuOptions.Length - 1)
                        {
                            i++;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (i > 0)
                        {
                            i--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return i;

                }
            }
        }
        /// <summary>
        /// Prompts the user with a question, reads their input, and validates it against specified length constraints.
        /// </summary>
        /// <remarks>The method repeatedly prompts the user until valid input is provided. Input is
        /// considered valid if it meets the specified length constraints. Feedback is displayed to the user indicating
        /// whether their input was valid or invalid.</remarks>
        /// <param name="questionText">The text of the question to display to the user.</param>
        /// <param name="minLength">The minimum allowable length for the input. Defaults to 1.</param>
        /// <param name="maxLength">The maximum allowable length for the input. Defaults to <see cref="int.MaxValue"/>.</param>
        /// <returns>The validated user input as a string.</returns>
        public static string ReadInput(string questionText, int minLength = 1, int maxLength = int.MaxValue)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(questionText);
                string? userInput = Console.ReadLine();
                if (userInput != null)
                {
                    (bool, string) verifyInput = VerifyString(userInput, minLength, maxLength);
                    if (verifyInput.Item1)
                    {
                        DisplayStatus(verifyInput.Item2, ConsoleColor.Green);
                        return userInput;
                    }
                    else
                    {
                        DisplayStatus(verifyInput.Item2, ConsoleColor.Red);
                    }
                }

            }
        }
        /// <summary>
        /// Prompts the user with a question and reads an integer input within a specified range.
        /// </summary>
        /// <remarks>If the user enters a value that is not a valid integer or is outside the specified
        /// range,  an error message is displayed, and the user is prompted again until a valid input is
        /// provided.</remarks>
        /// <param name="questionText">The text of the question to display to the user.</param>
        /// <param name="minInt">The minimum allowable value for the input. Defaults to 0.</param>
        /// <param name="maxInt">The maximum allowable value for the input. Defaults to <see cref="int.MaxValue"/>.</param>
        /// <returns>The integer value entered by the user that falls within the specified range.</returns>
        public static int ReadInt(string questionText, int minInt = 0, int maxInt = int.MaxValue)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(questionText);
                string? userInput = Console.ReadLine();
                if (userInput != null)
                {
                    int parsedValue;
                    bool success = int.TryParse(userInput, out parsedValue);
                    if (success && parsedValue >= minInt && parsedValue <= maxInt)
                    {
                        return parsedValue;
                    }
                    else if (!success)
                    {
                        DisplayStatus("Error: You must provide a real number!", ConsoleColor.Red);
                    }
                    else
                    {
                        DisplayStatus("Error: That number is outside of the allowed range", ConsoleColor.Red);
                    }
                }
            }
        }
        /// <summary>
        /// Verifies whether a string meets the specified length constraints.
        /// </summary>
        /// <remarks>If the string is shorter than <paramref name="minLength"/>, the method returns <see
        /// langword="false"/>  and a message indicating the minimum required length. If the string is longer than
        /// <paramref name="maxLength"/>,  the method returns <see langword="false"/> and a message indicating the
        /// maximum allowed length.  Otherwise, the method returns <see langword="true"/> and a success
        /// message.</remarks>
        /// <param name="unverifiedString">The string to be verified.</param>
        /// <param name="minLength">The minimum allowable length of the string. Defaults to 1.</param>
        /// <param name="maxLength">The maximum allowable length of the string. Defaults to <see cref="int.MaxValue"/>.</param>
        /// <returns>A tuple containing a <see cref="bool"/> and a <see cref="string"/>: <list type="bullet">
        /// <item><description><see langword="true"/> if the string meets the length constraints; otherwise, <see
        /// langword="false"/>.</description></item> <item><description>A message describing the result of the
        /// verification.</description></item> </list></returns>
        public static (bool, string) VerifyString(string unverifiedString, int minLength = 1, int maxLength = int.MaxValue)
        {
            if (unverifiedString.Length < minLength)
            {
                return (false, $"You must enter at least {minLength} character(s)!");
            }
            if (unverifiedString.Length > maxLength)
            {
                return (false, $"You can maximum enter {maxLength} characters");
            }
            return (true, "Success!");
        }
        /// <summary>
        /// Displays a status message in the specified console color and waits for user input to continue.
        /// </summary>
        /// <remarks>The console text color is temporarily changed to the specified color while the
        /// message is displayed. After displaying the message, the console color is reset to its default
        /// value.</remarks>
        /// <param name="message">The message to display in the console.</param>
        /// <param name="color">The <see cref="ConsoleColor"/> to use for the message text.</param>
        public static void DisplayStatus(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"\n{message}");
            Console.ResetColor();
            Console.WriteLine("Click any key to continue...");
            Console.ReadKey(true);

        }
    }
}
