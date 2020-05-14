using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaMusicApp.Models
{
    public class User
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public List<Song> AllSongs { get; set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            AllSongs = new List<Song>();
        }
    }
}
