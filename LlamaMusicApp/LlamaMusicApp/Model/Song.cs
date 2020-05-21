using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace LlamaMusicApp.Model
{
    public class Song
    {
        private string _audioFilePath;
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string ImagePath { get; set; }
        public string AudioFilePath
        { 
            get { return _audioFilePath; }
            set { _audioFilePath = value; }
        }

        //Constructors
        public Song(string artist, string title, string audioFilePath)
        {
            Artist = artist;
            Title = title;
            AudioFilePath = audioFilePath;
            ImagePath = "/Assets/LlamaMusicLogo.png";
        }

        public Song(string artist, string title)
        {
            Artist = artist;
            Title = title;
            ImagePath = "/Assets/LlamaMusicLogo.png";
        }

        public Song(string audioFilePath)
        {
            AudioFilePath = audioFilePath;
            ImagePath = "/Assets/LlamaMusicLogo.png";
        }

        public Song(Song source)
        {
            Artist = source.Artist;
            Title = source.Title;
            Genre = source.Genre;
            Album = source.Album;
            ImagePath = source.ImagePath;
            AudioFilePath = source.AudioFilePath;
        }
    }
}
