using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Labb_7.Models
{
    /// <summary>
    /// Question is contains text for the question text and options that user can choose from
    /// </summary>
    internal class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<Option> Options { get; set; }
        // Parameterless constructor for initial creation
        public Question() { }
        // Constructor used for creating a new question
        public Question(string text, List<Option> options)
        {
            Text = text;
            Options = options;

        }
    }
}
