using NewsAPI.Entities;
using NewsAPI.Entities.Enums;

namespace NewsAPITest.Entities;

public class NewsTests
{
    
    [Fact]
    public void News_Validate_Title_Lenght()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new News(
            "Entretenimento",
            "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21) teste teste teste",
            "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21)",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O título deve ter no máximo 100 caracteres", result.Message);
    }

    [Fact]
    public void News_Validate_Hat_Length()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new News(
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21), ás 22:30, " +
            "o publico acompanha o MasterChef Profissionais. O reality culinário vai reunir os 16 melhores cozinheiros" +
            " das temporadas anteriores para uma disputa emocionante. A apresentação fica por conta de Ana Paula Padrão," +
            "com a participação dos jurados Henrique Fogaça, Paola Carosella e Erick Jacquin.",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O chapéu deve ter no máximo 50 caracteres", result.Message);
    }

    [Fact]
    public void News_Validate_Title_Empty()
    {
        //Arrange & Act
        var results = Assert.Throws<DomainException>(() => new News(
            "Entretenimento",
            string.Empty,
            "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21), ás 22:30, " +
            "o publico acompanha o MasterChef Profissionais. O reality culinário vai reunir os 16 melhores cozinheiros" +
            " das temporadas anteriores para uma disputa emocionante. A apresentação fica por conta de Ana Paula Padrão," +
            "com a participação dos jurados Henrique Fogaça, Paola Carosella e Erick Jacquin.",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O título é obrigatório", results.Message);
    }

    [Fact]
    public void News_Validate_Hat_Empty()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new News(
            string.Empty,
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21), ás 22:30, " +
            "o publico acompanha o MasterChef Profissionais. O reality culinário vai reunir os 16 melhores cozinheiros" +
            " das temporadas anteriores para uma disputa emocionante. A apresentação fica por conta de Ana Paula Padrão," +
            "com a participação dos jurados Henrique Fogaça, Paola Carosella e Erick Jacquin.",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O chapéu é obrigatório", result.Message);
    }

    [Fact]
    public void News_Validate_Description_Empty()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new News(
            "Entretenimento",
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            string.Empty,
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("A Descrição da notícia é obrigatória", result.Message);
    }
}