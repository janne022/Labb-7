using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Labb_7.Models
{
    internal class Option
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrectOption { get; set; }

        public Option(string text, bool isCorrectOption)
        {
            Text = text;
            IsCorrectOption = isCorrectOption;
        }
    }
}
