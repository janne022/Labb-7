using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Labb_7
{
    internal class Option
    {
        // Sets this as the primary key for options
        [Key]
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrectOption {get; set;}

        public Option(string text, bool isCorrectOption)
        {
            this.Text = text;
            this.IsCorrectOption = isCorrectOption;
        }
    }
}
