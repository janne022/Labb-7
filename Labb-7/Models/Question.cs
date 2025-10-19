using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Labb_7.Models
{
    internal class Question
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<Option> Options { get; set; }
        // Parameterless constructor for when creating db
        public Question() { }
        public Question(string text, List<Option> options)
        {
            Text = text;
            Options = options;

        }
    }
}
