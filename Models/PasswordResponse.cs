using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGeneratorFunction.Models
{
    public class PasswordResponse
    {
        public string Password { get; set; }
        public int Strength { get; set; } 
        public DateTime GeneratedAt { get; set; }
    }
}
