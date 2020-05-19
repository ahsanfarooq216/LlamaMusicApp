using LlamaMusicApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaMusicApp.Model
{
    public class User
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public ObservableCollection<Song> MyMusicCollection { get; set; }

        public User(string uName, string password)
        {
            Username = uName;
            Password = password;
            MyMusicCollection = new ObservableCollection<Song>();
            MyMusicCollection = SongManager.GetAllMusic(MyMusicCollection);
        }
    }
}
