using NewsAPI.Entities;
using NewsAPI.Entities.Enums;
using NewsAPI.Services;

namespace NewsAPITest.Services;

public class UploadServiceTests
{
    [Theory]
    [InlineData(Media.Image, "image.webp")]
    [InlineData(Media.Video, "video.mp4")]

    public void Should_verify_if_Type_Image_or_Video(Media media, string fileName)
    {
        // Arrange
        var uploadService = new UploadService();
        
        // Act
        var result = uploadService.GetTypeMedia(fileName);

        // Assert
        Assert.Equal(media, result);
    }
    
    [Theory]
    [InlineData(Media.Image, "image.psd")]
    [InlineData(Media.Video, "video.mp3")]
    
    public void Should_throw_DomainException_if_TypeMedia_is_invalid(Media media, string fileName)
    {
        // Arrange
        var uploadService = new UploadService();
        
        // Act
        var result = Assert.Throws<DomainException>(() => uploadService.GetTypeMedia(fileName));

        // Assert
        Assert.Equal("Tipo de arquivo inválido para upload", result.Message);
    }
}