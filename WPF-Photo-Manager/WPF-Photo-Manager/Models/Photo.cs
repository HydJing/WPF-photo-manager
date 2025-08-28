using System;
using System.IO;

namespace WPF_Photo_Manager.Models
{
    /// <summary>
    /// Represents a photo with its file properties.
    /// </summary>
    public class Photo
    {
        public string FilePath { get; }
        public string FileName { get; }
        public DateTime CreationDate { get; }
        public string ThumbnailPath { get; set; }

        public Photo(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            CreationDate = File.GetCreationTime(filePath);
        }
    }
}
