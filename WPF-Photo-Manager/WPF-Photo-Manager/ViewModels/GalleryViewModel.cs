using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Photo_Manager.Services;

namespace WPF_Photo_Manager.ViewModels
{
    /// <summary>
    /// ViewModel for the main photo gallery view.
    /// It manages the collection of photos and the loading state of the application.
    /// </summary>
    public class GalleryViewModel : INotifyPropertyChanged
    {
        private readonly FileService _fileService = new FileService();
        private bool _isLoading;

        /// <summary>
        /// A collection of PhotoViewModel objects that the UI can bind to.
        /// ObservableCollection automatically notifies the UI of additions, deletions, or updates.
        /// </summary>
        public ObservableCollection<PhotoViewModel> Photos { get; } = new ObservableCollection<PhotoViewModel>();

        /// <summary>
        /// Gets or sets a value indicating whether the application is currently loading photos.
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged(nameof(IsLoading));
                }
            }
        }

        /// <summary>
        /// Event that fires when a property value changes, used for data binding.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the GalleryViewModel class and starts loading photos.
        /// </summary>
        public GalleryViewModel()
        {
            _ = LoadPhotosAsync();
        }

        /// <summary>
        /// Asynchronously loads photos from a specified directory.
        /// </summary>
        public async Task LoadPhotosAsync()
        {
            IsLoading = true;
            Photos.Clear();

            try
            {
                // The directory path is hardcoded. You should change this to a valid path on your system.
                string directoryPath = @"E:\test images";

                var photos = await _fileService.GetPhotosAsync(directoryPath);

                foreach (var photo in photos)
                {
                    // Use the Dispatcher to ensure the UI collection is updated on the UI thread.
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Photos.Add(new PhotoViewModel(photo));
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading photos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
