using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Security.Cryptography.Core;
using LlamaMusicApp.Model;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LlamaMusicApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer player;
        bool playing;

        private ObservableCollection<Song> Songs;
        private enum ContentView
        {
            Home,
            AddNewSong
        }
        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            SongManager.GetAllMusic(Songs);

            player = new MediaPlayer();
            playing = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MenuSplitView.IsPaneOpen == false)
            {
                MenuSplitView.IsPaneOpen = true;
            }
            else if (MenuSplitView.IsPaneOpen == true)
            {
                MenuSplitView.IsPaneOpen = false;
            }
        }

        private void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            MyMediaElement.Source = new Uri(BaseUri, song.AudioFilePath);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Derek_Clegg_-_Annalise.mp3");

            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);

            if (playing)
            {
                player.Source = null;
                playing = false;
            }
            else
            {
                player.Play();
                playing = true;

            }
        }

        private async void AddNewSongButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToContentView(ContentView.AddNewSong);
            //Instantiates File Open picker and opens the dialogue
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.MusicLibrary;

            //Filters the type of files acceptable to connect
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".m4a");

            //Allows user to select the song
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                string filePath = file.Path;
            }
            else
            {
                //this.textBlock.Text = "Operation cancelled.";
            }
        }

        private void SwitchToContentView(ContentView destContentView)
        {

            switch (destContentView)
            {
                //case ContentView.PlayListCreation:
                //    PlayListPlayBackView.Visibility = Visibility.Collapsed;
                //    PlayListCreationView.Visibility = Visibility.Visible;
                //    // Hide the Add New Songs view
                //    AddNewSongView.Visibility = Visibility.Collapsed;

                //    break;
                case ContentView.Home:
                    //xxxx.Visibility = Visibility.Collapsed;
                    SongGridView.Visibility = Visibility.Visible;
                    MenuGridView.Visibility = Visibility.Visible;
                    // Hide the Add New Songs view
                    AddNewSongView.Visibility = Visibility.Collapsed;

                    break;
                case ContentView.AddNewSong:
                    // Hide the Song Grid view
                    SongGridView.Visibility = Visibility.Collapsed;
                    // Hide the playlist playback view
                    MenuGridView.Visibility = Visibility.Collapsed;
                    //xxxx.Visibility = Visibility.Collapsed;
                    // Show the Add New Songs view
                    AddNewSongView.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void SongSaveButton_Click(object sender, RoutedEventArgs e)
        {
            //Allow user to save name, artist, and genre
            string name = SongTitle_UserInput.Text;
            string artist = SongArtist_UserInput.Text;
            //string album = Album_UserInput.Text;
            //string filepath = "test";

            var newSong = new Song(name, artist);
            Songs.Add(newSong);
            SwitchToContentView(ContentView.Home);
        }
    }
}
