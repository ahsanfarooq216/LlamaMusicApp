using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LlamaMusicApp.Models
{
    public class Song
    {
        private string _path;
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Path
        { 
            get { return _path; }
            set { _path = value; }
        }

        //Constructors
        public Song(string title, string artist, string path)
        {
            Title = title;
            Artist = artist;
            Path = path;
        }

        public Song(string path)
        {
            Path = path;
        }
    }
}
