using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaMusicApp.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public List<Song> PlaylistSongs { get; set; }
        //Methods
        public void addSong(Song song)
        {
            PlaylistSongs.Add(song);
        }
        public void removeSong(Song song)
        {
            PlaylistSongs.Remove(song);
        }
        //Constructors
        public Playlist(string name)
        {
            Name = name;
            PlaylistSongs = new List<Song>();
        }
    }
}
