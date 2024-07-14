using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MoneyDreamAPI.Dto.ImageExtension
{
    public class ImageExtension
    {


        /// <summary>
        /// Uploads image files to Cloudinary and returns the secure URL of the uploaded image.
        /// </summary>
        /// <param name="files">The image files to be uploaded.</param>
        /// <returns>The secure URL of the uploaded image.</returns>
        /// <exception cref="ArgumentException">Thrown when the file is null or empty.</exception>
        public static List<object> UploadMultiple(List<IFormFile> files)
        {
            List<object> results = new List<object>();

            if (files == null || files.Count == 0)
            {
                throw new ArgumentException("No files to upload");
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var account = new Account(
                config["Cloudinary:Name"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            var cloudinary = new Cloudinary(account);

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException($"File {file.FileName} is null or empty");
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                results.Add(new
                {
                    SecureUrl = uploadResult.SecureUrl,
                    PublicId = uploadResult.PublicId
                });
            }

            return results;
        }

        /// <summary>
        /// Deletes an image from Cloudinary using the public ID.
        /// </summary>
        /// <param name="publicId">The public ID of the image to be deleted.</param>
        /// <returns>Result of the deletion operation.</returns>
        public static object Delete(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                throw new ArgumentException("Public ID is null or empty");
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var account = new Account(
                config["Cloudinary:Name"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            var cloudinary = new Cloudinary(account);

            var deletionParams = new DeletionParams(publicId);
            var deletionResult = cloudinary.Destroy(deletionParams);
            return deletionResult.Result; // Result could be "ok" or "not found"
        }

        /// <summary>
        /// Replaces an existing image on Cloudinary with a new one.
        /// </summary>
        /// <param name="publicId">The public ID of the image to be replaced.</param>
        /// <param name="file">The new image file.</param>
        /// <returns>The secure URL of the updated image.</returns>
        /// <exception cref="ArgumentException">Thrown when the file is null or empty, or public ID is null or empty.</exception>
        public static object Edit(string publicId, IFormFile file)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                throw new ArgumentException("Public ID is null or empty");
            }

            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is null or empty");
            }

            // Delete the existing image
            Delete(publicId);

            // Upload the new image
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var account = new Account(
                config["Cloudinary:Name"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            var cloudinary = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                PublicId = publicId // Reuse the same public ID
            };

            var uploadResult = cloudinary.Upload(uploadParams);
            return new
            {
                SecureUrl = uploadResult.SecureUrl,
                PublicId = uploadResult.PublicId
            };
        }
    }
}
