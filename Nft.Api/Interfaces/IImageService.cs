using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace NftApi.Interfaces;

public interface IImageService
{
    Task<ImageUploadResult> UploadPhotoAsync(IFormFile file);
    Task<DeletionResult> DeletePhotoAsync(string publicId);
}   
