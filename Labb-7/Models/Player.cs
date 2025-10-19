using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Labb_7.Models
{
    internal class Player(string name)
    {
        // Declare properties, id is set automatically via EF
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int Score { get; set; } = 0;

        public Player(string name)
        {
            Name = name;
        }
    }
}
