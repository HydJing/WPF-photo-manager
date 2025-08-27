using System;
using System.IO;

/// <summary>
/// Represents a photo or video file in the application.
/// This model class holds the core data for a media item.
/// </summary>
public class Photo
{
    /// <summary>
    /// Gets or sets the full file path of the photo.
    /// </summary>
    public string FilePath { get; set; }

    /// <summary>
    /// Gets or sets the name of the file (e.g., "IMG_1234.jpg").
    /// </summary>
    public string FileName { get; set; }

    /// <summary>
    /// Gets or sets the date and time the file was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the file path to the generated thumbnail image.
    /// This is used for quick UI display.
    /// </summary>
    public string ThumbnailPath { get; set; }

    /// <summary>
    /// Initializes a new instance of the Photo class.
    /// </summary>
    /// <param name="filePath">The full path to the media file.</param>
    public Photo(string filePath)
    {
        FilePath = filePath;
        // Use FileInfo to get file properties without needing to handle
        // the file itself, making this class efficient.
        try
        {
            FileInfo fileInfo = new FileInfo(filePath);
            FileName = fileInfo.Name;
            CreationDate = fileInfo.CreationTime;
        }
        catch (Exception ex)
        {
            // Log the error but proceed with default values to prevent app crash.
            Console.WriteLine($"Error initializing Photo model for file {filePath}: {ex.Message}");
            FileName = Path.GetFileName(filePath);
            CreationDate = DateTime.MinValue;
        }
    }
}
