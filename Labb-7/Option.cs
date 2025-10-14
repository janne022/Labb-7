using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Labb_7
{
    /// <summary>
    /// An option for a question. Multiple options can be correct
    /// </summary>
    internal class Option(string text, bool isCorrectOption)
    {
        // Declare properties, id is set automatically via EF
        [Key]
        public int Id { get; set; }
        public string Text { get; set; } = text;
        public bool IsCorrectOption { get; set; } = isCorrectOption;
    }
}
