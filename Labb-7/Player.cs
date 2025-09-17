using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Labb_7
{
    internal class Player
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
    }
}
