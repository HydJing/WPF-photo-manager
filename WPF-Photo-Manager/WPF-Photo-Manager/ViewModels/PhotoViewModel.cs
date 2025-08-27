using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;

/// <summary>
/// A ViewModel class that wraps the Photo model.
/// This class handles UI-related logic and implements INotifyPropertyChanged
/// to notify the UI of property changes, enabling data binding.
/// </summary>
public class PhotoViewModel : INotifyPropertyChanged
{
    private readonly Photo _photo;
    private BitmapImage _thumbnail;

    /// <summary>
    /// Event that fires when a property value changes.
    /// This is a key part of the MVVM pattern for data binding.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Gets the full file path from the underlying Photo model.
    /// </summary>
    public string FilePath => _photo.FilePath;

    /// <summary>
    /// Gets the file name from the underlying Photo model.
    /// </summary>
    public string FileName => _photo.FileName;

    /// <summary>
    /// Gets the thumbnail image to be displayed in the UI.
    /// The property setter notifies the UI when the thumbnail is loaded.
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
    /// Initializes a new instance of the PhotoViewModel class.
    /// </summary>
    /// <param name="photo">The Photo model instance to wrap.</param>
    public PhotoViewModel(Photo photo)
    {
        _photo = photo;
        // The thumbnail itself will be set later by the file scanning service,
        // which will trigger the 'set' block and notify the UI.
    }

    /// <summary>
    /// Notifies listeners that a property value has changed.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
