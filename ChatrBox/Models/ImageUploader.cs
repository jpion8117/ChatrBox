#nullable disable
using ChatrBox.Data;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace ChatrBox.Models
{
    /// <summary>
    /// Provides methods and properties necessary for users to upload personalized icon 
    /// images representing an asset or for retrieving a randomly assigned default asset.
    /// </summary>
    public class ImageUploader
    {
        private string _assetName;

        /// <summary>
        /// Asset types that can be assigned an image
        /// </summary>
        public enum AssetType
        {
            ChatrIcon,
            CommunityIcon
        }

        /// <summary>
        /// Type of asset being assigned an image
        /// </summary>
        public AssetType Type { get; set; }

        /// <summary>
        /// Selects a random default icon and gets its path on the disk
        /// </summary>
        public static string GetDefaultIconPath
        {
            get
            {
                var path = Path.Combine(HostPath, "Images","defaultIcons");
                var directory = new DirectoryInfo(path);
                var files = directory.GetFiles();
                var filenames = new string[files.Length];

                for (int i = 0; i < files.Length; i++)
                    filenames[i] = files[i].Name;

                var rng = new Random();
                var index = rng.Next(filenames.Length);
                return Path.Combine(path, filenames[index]);
            }
        }

        /// <summary>
        /// Location of the wwwroot folder on the drive.
        /// </summary>
        public static string HostPath { get; set; }

        /// <summary>
        /// Representation of the form field contiaing image
        /// </summary>
        public IFormFile Image { get; set; }

        /// <summary>
        /// Name of the asset being saved.
        /// </summary>
        public string AssetName 
        {
            get => _assetName + "_" + Type.ToString();
            set => _assetName = value; 
        }

        /// <summary>
        /// Uploads image file to the server, computes image hash, and populates 
        /// icon database reference properties through IIcon interface reference.
        /// </summary>
        /// <param name="iconDbKey">Icon reference key to be saved to the database</param>
        /// <returns>true if image upload was successful</returns>
        public IImageDbReference Upload(out bool? success)
        {
            ImageBase iconDbKey = new ImageBase();

            if (Image == null)
            {
                success = false; 
                return AssignDefaultIcon();
            }
            var path = Path.Combine(HostPath, "Images","userIcons");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            iconDbKey.ImageUrl = Path.Combine(path, AssetName);

            using (var fs = new FileStream(iconDbKey.ImageUrl, FileMode.Create))
            {
                Image.CopyTo(fs);
            }            

            using (var sha = SHA256.Create()) 
            {
                using (var fs = new FileStream(iconDbKey.ImageUrl, FileMode.Open))
                {
                    var hash = sha.ComputeHash(fs);
                    iconDbKey.ImageHash = Convert.ToHexString(hash);
                }
            }

            
            success = File.Exists(iconDbKey.ImageUrl);
            return iconDbKey;
        }

        /// <summary>
        /// Randomly selects one of the default icons in the images/defaultIcons 
        /// directory and assigns the necessarry properties to a database icon 
        /// reference through the IIcon interface.
        /// </summary>
        /// <param name="iconDbKey">icon reference key to be saved to the database</param>
        public static IImageDbReference AssignDefaultIcon()
        {
            ImageBase iconDbKey = new ImageBase();
            
            if(string.IsNullOrEmpty(HostPath)) 
            {
                iconDbKey.ImageUrl = "";
                iconDbKey.ImageHash = "";
            }
            
            //grab a random icon from the default icons folder
            iconDbKey.ImageUrl = GetDefaultIconPath;

            //create a hash of the icon that can be used to verify file integrety before
            //displaying it to the user
            using (var sha = SHA256.Create())
            {
                using (var fs = new FileStream(iconDbKey.ImageUrl, FileMode.Open))
                {
                    var hash = sha.ComputeHash(fs);
                    iconDbKey.ImageHash = Convert.ToHexString(hash);
                }
            }

            return iconDbKey;
        }        
    }
}
