using System.IO;
using System.Windows.Media.Imaging;
using WPF_Photo_Manager.Models;

namespace WPF_Photo_Manager.Services
{
    /// <summary>
    /// A service for handling local file system operations.
    /// </summary>
    public class FileService
    {
        private readonly string[] _supportedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        public async Task<List<Photo>> GetPhotosAsync(string directoryPath)
        {
            return await Task.Run(() =>
            {
                List<Photo> photos = new List<Photo>();

                if (!Directory.Exists(directoryPath))
                {
                    return photos;
                }

                string[] filePaths = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);

                foreach (string filePath in filePaths)
                {
                    string extension = Path.GetExtension(filePath)?.ToLowerInvariant();
                    if (_supportedExtensions.Contains(extension))
                    {
                        try
                        {
                            Photo photo = new Photo(filePath);
                            photo.ThumbnailPath = GenerateThumbnail(filePath);
                            photos.Add(photo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                        }
                    }
                }
                return photos;
            });
        }

        /// <summary>
        /// Generates a thumbnail for an image and returns its path.
        /// </summary>
        private string GenerateThumbnail(string imagePath)
        {
            const int thumbnailSize = 200;
            string thumbnailFileName = $"thumb_{Guid.NewGuid()}_{Path.GetFileName(imagePath)}";
            string thumbnailPath = Path.Combine(Path.GetTempPath(), thumbnailFileName);

            try
            {
                // Use a FileStream to read the image into memory
                using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    BitmapImage fullImage = new BitmapImage();
                    fullImage.BeginInit();
                    fullImage.StreamSource = stream;
                    fullImage.CacheOption = BitmapCacheOption.OnLoad;
                    fullImage.DecodePixelWidth = thumbnailSize;
                    fullImage.EndInit();

                    // Freeze the image to make it safe for cross-thread access
                    fullImage.Freeze();

                    // Save the thumbnail as a JPEG file
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(fullImage));
                    using (var thumbnailStream = new FileStream(thumbnailPath, FileMode.Create))
                    {
                        encoder.Save(thumbnailStream);
                    }
                }

                return thumbnailPath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating thumbnail for {imagePath}: {ex.Message}");
                return null;
            }
        }
    }
}