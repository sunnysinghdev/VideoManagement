using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VMS.Models;

namespace VMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object _serviceEndpoint;
        private readonly string _downloadUrl;
        private readonly string _uploadUrl;


        public MainWindow()
        {
            InitializeComponent();
            videoList.Items.Clear();
            videoList.ItemsSource = GetVideos();
            mePlayer.ScrubbingEnabled = true;
            _serviceEndpoint = "http://localhost:5071/api/v1/";
            _downloadUrl = string.Format("{0}videos/download", _serviceEndpoint);
            _uploadUrl = string.Format("{0}videos", _serviceEndpoint);
            DataContext = this;



        }
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Stop();
        }

        private void handleViewDownloads(object sender, RoutedEventArgs e)
        {
            tb.Content = "View Uploads";
            contextLable.Content = "Downloaded Videos";
            videoList.ItemsSource = GetDowloadVideos();
        }
        private void handleViewUploads(object sender, RoutedEventArgs e)
        {
            tb.Content = "View Downloads";
            contextLable.Content = "Uploaded Videos";
            videoList.ItemsSource =  GetVideos();


        }
        private void videoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mePlayer.Stop();
            if (videoList.SelectedItem != null)
            {
                mePlayer.Source = new Uri((videoList.SelectedItem as VideoDetail)!.Url);
            }
        }
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            var fd = new OpenFileDialog();
            //Add filter
            if (fd.ShowDialog() == true)
            { 
                labelFileUpload.Content = fd.FileName;
                string title = "xyz";
                string description = "desc";
                var url = string.Format("{0}?title={1}&description={2}", _uploadUrl, title, description);
                using var webClient = new WebClient();
                webClient.UploadProgressChanged += WebClient_UploadProgressChanged;
                webClient.UploadFileCompleted += WebClient_UploadFileCompleted;
                webClient.UploadFileAsync(new Uri(url), fd.FileName);
            } 
        }

        private void WebClient_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            labelFileUpload.Content = "File Uploaded successfully!!!";
        }

        private void WebClient_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            labelFileUpload.Content = (labelFileUpload.Content.ToString() == "Uploading." ? "Uploading....": "Uploading.");
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            string id = "1";
            string fileName = "1.mp4";
            string downloadFolder = "Downloads";
            var downloadUri = new Uri(string.Format("{0}/{1}", _downloadUrl, id));
            using var webClient = new WebClient();
            webClient.DownloadFileAsync(downloadUri, fileName);
            webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
        }

        private void WebClient_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            txtStatusBar.Text = "Download completed.";
        }

        private List<VideoDetail> GetVideos()
        {
            return new List<VideoDetail>()
            {
                new () { Id=1, Title="Pavi playing", Description="Funny Video", Size=1, 
                    Url= "http://localhost:5071/api/videos/download/3.mp4"},
                new () { Id=2, Title="Republic day", Description="Funny Video including random vibes", Size=1, 
                    Url= "C:\\Users\\H552416\\OneDrive - Honeywell\\Documents\\VMS\\Sample\\Video\\2.mp4"},
                new () { Id=3, Title="Eating choclate", Description="Funny Video including random vibes", Size=1,
                    Url= "C:\\Users\\H552416\\OneDrive - Honeywell\\Documents\\VMS\\Sample\\Video\\3.mp4"},
                new () { Id=4, Title="Piku swing", Description="Funny Video including random vibes", Size=1,
                    Url= "C:\\Users\\H552416\\OneDrive - Honeywell\\Documents\\VMS\\Sample\\Video\\4.mp4"}
            };
        }
        private List<VideoDetail> GetDowloadVideos()
        {
            var videos = new List<VideoDetail>();
           string currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles("Downloads");
            int i = 1;
            foreach (var file in files)
            {
                var filePath = System.IO.Path.Combine(currentDirectory, file);
                if (File.Exists(filePath))
                {
                    videos.Add(new VideoDetail { Id = i++, Url = filePath });
                }
            }
            return videos;
        }
    }
   
}
