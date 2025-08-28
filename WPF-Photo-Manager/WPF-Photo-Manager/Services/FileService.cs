using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        /// <summary>
        /// Scans a directory for image files and generates a list of Photo models.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to scan.</param>
        /// <returns>A Task that returns a list of Photo objects.</returns>
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
                BitmapImage fullImage = new BitmapImage();
                fullImage.BeginInit();
                fullImage.UriSource = new Uri(imagePath);
                fullImage.CacheOption = BitmapCacheOption.OnLoad;
                fullImage.DecodePixelWidth = thumbnailSize;
                fullImage.EndInit();

                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(fullImage));
                using (var stream = new FileStream(thumbnailPath, FileMode.Create))
                {
                    encoder.Save(stream);
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
