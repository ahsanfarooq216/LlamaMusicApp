using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LlamaMusicApp.Models;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LlamaMusicApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> Songs;

       
        public MainPage()
        {
            this.InitializeComponent();

            

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Getting access to music library

            //Storage folder are classes that give information about the folder
            //Storage files are classes that give information about the files in the filesystem


            StorageFolder folder =KnownFolders.MusicLibrary;

            //Getting a list of all the music files
            var allSongs =new ObservableCollection<StorageFile>();
            //Passing allSongs and the parent folder
            await RetreiveFilesInFolders(allSongs, folder);
            var songs = AllSongs(allSongs);

            //Getting metadata of the songs

            await SongList(songs);



        }
        //Making the observable collecion to a list

        private List<StorageFile> AllSongs(ObservableCollection<StorageFile> allSongs)
        {
            var songs = new List<StorageFile>();
               for(var i=0; i < songs.Count; i++)
            {
                songs.Add(allSongs[i]);

            }

                return songs;



        }

        //parent is the Knownfolders.MusicLibrary
        private async Task RetreiveFilesInFolders(ObservableCollection<StorageFile> list,StorageFolder parent)
        {
            foreach (var item in await parent.GetFilesAsync())
            {
                if (item.FileType == ".mp3")
                    list.Add(item);
            }
            foreach (var item in await parent.GetFoldersAsync())
            {
                //here the parent is item and we are passing the list from the above step

                await RetreiveFilesInFolders(list, item);
            }
        }

        private async Task SongList(List<StorageFile> files)
        {
            foreach (var file in files)
            {
                MusicProperties songProperties = await file.Properties.GetMusicPropertiesAsync();
                StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 200, ThumbnailOptions.UseCurrentScale);
                var song = new Song();
                var albumCover = new BitmapImage();
                song.Title = songProperties.Title;
                song.Artist = songProperties.Artist;
                song.Album = songProperties.Album;
                song.AlbumCover = albumCover;
                song.SongFile = file;

                Songs.Add(song);




            }
        }
    }
}
