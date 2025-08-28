using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using WPF_Photo_Manager.Models;

namespace WPF_Photo_Manager.ViewModels
{
    /// <summary>
    /// A ViewModel that wraps the Photo model and enables data binding.
    /// </summary>
    public class PhotoViewModel : INotifyPropertyChanged
    {
        private readonly Photo _photo;
        private BitmapImage _thumbnail;

        /// <summary>
        /// Event that fires when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the thumbnail image for display.
        /// </summary>
        public BitmapImage Thumbnail
        {
            get => _thumbnail;
            set
            {
                if (_thumbnail != value)
                {
                    _thumbnail = value;
                    OnPropertyChanged(nameof(Thumbnail));
                }
            }
        }

        /// <summary>
        /// Gets the file name from the wrapped Photo model.
        /// </summary>
        public string FileName => _photo.FileName;

        /// <summary>
        /// Initializes a new instance of the PhotoViewModel class.
        /// </summary>
        /// <param name="photo">The Photo model instance to wrap.</param>
        public PhotoViewModel(Photo photo)
        {
            _photo = photo;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
