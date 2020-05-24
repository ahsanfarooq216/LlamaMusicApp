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
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LlamaMusicApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Comment added to test github


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
            StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync("Derek_Clegg_-_Annalise.mp3");

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
                SongPath_UserInput.Text = file.Path;
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
            string title = SongTitle_UserInput.Text;
            string artist = SongArtist_UserInput.Text;
            //string album = Album_UserInput.Text;
            string audioFilePath = SongPath_UserInput.Text;
            //string imageFilePath = ImagePath_UserInput.Text;

            string imageFilePath = "/Assets/LlamaMusicLogo.png";


            var newSong = new Song(artist, title, audioFilePath, imageFilePath);
            Songs.Add(newSong);
            SwitchToContentView(ContentView.Home);
        }
        private static string GetRelativePath(string wholePath, string musicWord)
        {
            int startIndex = wholePath.IndexOf(musicWord);
            string path = wholePath.Substring(startIndex + musicWord.Length);
            return path;
        }

        private static string GetSongName(string wholePath)
        {
            var fileInfo = new FileInfo(wholePath);
            return fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
        }
        //Code added to edit info
        private async void EditInfo_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(BlankPage1));
            var myview = CoreApplication.CreateNewView();
            int newViewId = 0;
            await myview.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame newFrame = new Frame();
                newFrame.Navigate(typeof(EditInfo), null);
                Window.Current.Content = newFrame;
                Window.Current.Activate();
                newViewId = ApplicationView.GetForCurrentView().Id;

            });
            await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId, ViewSizePreference.UseMinimum);
            // await ApplicationViewSwitcher.TryShowAsViewModeAsync(newViewId, ApplicationViewMode.CompactOverlay);
            //Frame.Navigate(typeof(MainPage));
        }

        //Code added to edit albumcover
        private async void EditALbumCover_Click(object sender, RoutedEventArgs e)
        {


            var picker = new FileOpenPicker();


            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.ViewMode = PickerViewMode.Thumbnail;



            StorageFile file = await picker.PickSingleFileAsync();



            if (file != null)
            {
                // Application now has read/write access to the picked file
                //string filePath = file.Path;

                //var uri = new System.Uri("ms-appdata:///local/images/logo.png");
                //var file1 = await StorageFile.GetFileFromApplicationUriAsync(uri);

                //Image img = new Image();
                //img.Source = file1;

                //Image img = new Image();
                // img.Source = new BitmapImage(new Uri(@"/Assets/LlamaMusicBlue.png"));



                using (Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    BitmapImage bitmapImage =
                        new BitmapImage();
                    //MyImage.Source = bitmapImage;


                }

            }
            else
            {
                //this.textBlock.Text = "Operation cancelled.";
            }
        }
        //Code to delete 
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync("Derek_Clegg_-_Annalise.mp3");


            await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            // sampleSongs.Add(new Song("Derek", "Annalise", $"/Assets/SampleMusic/Derek_Clegg_-_Annalise.mp3"));
        }

        //Code to play the song

        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            StorageFile file = await folder.GetFileAsync("Derek_Clegg_-_Annalise.mp3");

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

        private async void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            //Filters the type of files acceptable to connect
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            //Allows user to select the song
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                ImagePath_UserInput.Text = file.Path;
                //ImagePath_UserInput.Text = ImagePath_UserInput.Text.Replace("\\", "/");
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            SwitchToContentView(ContentView.Home);
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Login));
        }
    }
}
