using NewsAPI.Infra;

namespace NewsAPITest.Infra;

public class HelperTests
{
    
    [Fact]
    public void Should_return_Validate_Slug()
    {
        //Arrange
       const string title = "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça feira (21)";
        
        //Act
        var slug = Helper.GenerateSlug.Generate(title);
        
        //Assert
        Assert.Equal("a-band-preparou-uma-serie-de-atracoes-para-agitar-o-fim-de-ano-nesta-terca-feira-21", slug);
    }
}