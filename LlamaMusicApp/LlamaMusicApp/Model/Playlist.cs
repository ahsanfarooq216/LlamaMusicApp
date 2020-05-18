using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaMusicApp.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public ObservableCollection<Song> PlaylistSongs { get; set; }
        //Methods
        public void AddSong(Song song)
        {
            PlaylistSongs.Add(song);
        }
        public void RemoveSong(Song song)
        {
            PlaylistSongs.Remove(song);
        }
        //Constructors
        public Playlist(string name)
        {
            Name = name;
            PlaylistSongs = new ObservableCollection<Song>();
        }
    }
}
