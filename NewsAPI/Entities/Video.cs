using MongoDB.Bson.Serialization.Attributes;
using NewsAPI.Entities.Enums;
using NewsAPI.Infra;

namespace NewsAPI.Entities;

public class Video : BaseEntity
{
    public Video(string hat, string title, string thumbnail, string author, string urlVideo, Status status)
    {
        Hat = hat;
        Title = title;
        Author = author;
        Thumbnail = thumbnail;
        UrlVideo = urlVideo;
        PublishDate = DateTime.Now;
        Slug = Helper.GenerateSlug.Generate(Title);
        Status = status;
            
        ValidateEntity();
    }
    
    [BsonElement("hat")]
    public string Hat { get; private set; }
        
    [BsonElement("title")]
    public string Title { get; private set; }
    
    [BsonElement("author")]
    public string Author { get; private set; }
        
    [BsonElement("thumbnail")]
    public string Thumbnail { get; private set; }
    
    [BsonElement("urlVideo")]
    public string UrlVideo { get; private set; }
    
    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Title, "O título é obrigatório");
        AssertionConcern.AssertArgumentNotEmpty(Hat, "O chapéu é obrigatório");
        
        AssertionConcern.AssertArgumentLength(
            Title, 100, "O título deve ter no máximo 100 caracteres");
        AssertionConcern.AssertArgumentLength(
            Hat, 50, "O chapéu deve ter no máximo 50 caracteres");
    }
}