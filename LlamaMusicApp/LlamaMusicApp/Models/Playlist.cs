using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaMusicApp.Models
{
    public class MusicCollection
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; }
        //Methods
        public void addSong(Song song)
        {
            Songs.Add(song);
        }
        public void removeSong(Song song)
        {
            Songs.Remove(song);
        }
        //Constructors
        public MusicCollection(string name)
        {
            Name = name;
            Songs = new List<Song>();
        }
    }
}
