using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using WPF_Photo_Manager.Models;

namespace WPF_Photo_Manager.ViewModels
{
    /// <summary>
    /// ViewModel for a single photo.
    /// It wraps the Photo model and provides properties suitable for UI binding.
    /// </summary>
    public class PhotoViewModel : INotifyPropertyChanged
    {
        private readonly Photo _photo;
        private BitmapImage _thumbnail;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the name of the photo.
        /// </summary>
        public string Name => _photo.FileName;

        /// <summary>
        /// Gets the thumbnail image of the photo. This property now correctly
        /// notifies the UI when its value changes.
        /// </summary>
        public BitmapImage Thumbnail
        {
            get => _thumbnail;
            private set
            {
                if (_thumbnail != value)
                {
                    _thumbnail = value;
                    OnPropertyChanged(nameof(Thumbnail));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the PhotoViewModel class.
        /// </summary>
        /// <param name="photo">The Photo model to wrap.</param>
        public PhotoViewModel(Photo photo)
        {
            _photo = photo;
            LoadThumbnail();
        }

        private void LoadThumbnail()
        {
            if (string.IsNullOrEmpty(_photo.ThumbnailPath))
            {
                return;
            }

            try
            {
                // We use a FileStream to explicitly open and close the file.
                // This is the most reliable way to prevent file locking issues.
                using (var stream = new FileStream(_photo.ThumbnailPath, FileMode.Open, FileAccess.Read))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // Ensure the file is released immediately
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze(); // Make the image immutable and thread-safe

                    this.Thumbnail = bitmap; // Use the property setter, which will trigger OnPropertyChanged
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading thumbnail {_photo.ThumbnailPath}: {ex.Message}");
                this.Thumbnail = null; // Use the property setter, which will trigger OnPropertyChanged
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
