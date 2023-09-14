using MongoDB.Bson.Serialization.Attributes;
using NewsAPI.Entities.Enums;
using NewsAPI.Infra;

namespace NewsAPI.Entities;

public class Gallery : BaseEntity
{
    public Gallery(string title, string legend, string author, string tags, Status status,
        IList<string> galleryImages, string thumbnail, DateTime publishDate)
    {
        Title = title;
        Legend = legend;
        Author = author;
        Tags = tags;
        Status = status;
        GalleryImages = galleryImages;
        Thumbnail = thumbnail;
        Slug = Helper.GenerateSlug.Generate(Title);
        PublishDate = DateTime.Now;
            
        ValidateEntity();
    }
    
    [BsonElement("title")]
    public string Title { get; private set; }
    
    [BsonElement("legend")]
    public string Legend { get; private set; }
    
    [BsonElement("author")]
    public string Author { get; private set; }
    
    [BsonElement("tags")]
    public string Tags { get; private set; }
        
    [BsonElement("thumbnail")]
    public string Thumbnail { get; private set; }
    
    [BsonElement("galleryImages")]
    public IList<string> GalleryImages { get; private set; }
    
    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Title, "O título é obrigatório");
        AssertionConcern.AssertArgumentNotEmpty(Legend, "A legenda é obrigatória");
        
        AssertionConcern.AssertArgumentLength(
            Title, 100, "O título deve ter no máximo 100 caracteres");
        AssertionConcern.AssertArgumentLength(
            Legend, 50, "A legenda deve ter no máximo 50 caracteres");
    }
}