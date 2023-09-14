using MongoDB.Bson.Serialization.Attributes;
using NewsAPI.Entities.Enums;
using NewsAPI.Infra;

namespace NewsAPI.Entities
{
    public class News : BaseEntity
    {
        public News(string hat, string title, string text, string author, string img, Status status)
        {
            Hat = hat;
            Title = title;
            Text = text;
            Author = author;
            Img = img;
            PublishDate = DateTime.Now;
            Slug = Helper.GenerateSlug.Generate(Title);
            Status = status;
            
            ValidateEntity();
        }
        
        public Status ChangeStatus(Status status)
        {
            return status switch
            {
                Status.Active => Status.Active,
                Status.Inactive => Status.Inactive,
                Status.Draft => Status.Draft,
                _ => status
            };
        }
        
        [BsonElement("hat")]
        public string Hat { get; private set; }
        
        [BsonElement("title")]
        public string Title { get; private set; }
        
        [BsonElement("text")]
        public string Text { get; private set; }
        
        [BsonElement("author")]
        public string Author { get; private set; }
        
        [BsonElement("img")]
        public string Img { get; private set; }
        
        public void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Title, "O título é obrigatório");
            AssertionConcern.AssertArgumentNotEmpty(Hat, "O chapéu é obrigatório");
            AssertionConcern.AssertArgumentNotEmpty(Text, "A Descrição da notícia é obrigatória");
            
            AssertionConcern.AssertArgumentLength(
                Title, 100, "O título deve ter no máximo 100 caracteres");
            AssertionConcern.AssertArgumentLength(
                Hat, 50, "O chapéu deve ter no máximo 50 caracteres");
        }
    }
}