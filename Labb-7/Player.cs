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
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; } = 0;

        public Player(string name)
        {
            this.Name = name;
        }
    }
}
