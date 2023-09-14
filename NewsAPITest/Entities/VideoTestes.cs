using NewsAPI.Entities;
using NewsAPI.Entities.Enums;

namespace NewsAPITest.Entities;

public class VideoTestes
{
    [Fact]
    public void News_Validate_Title_Lenght()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new Video(
            "Entretenimento",
            "A Band preparou uma série de atrações para agitar o fim de ano. Nesta terça-feira (21) teste teste teste",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.mp4",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O título deve ter no máximo 100 caracteres", result.Message);
    }

    [Fact]
    public void News_Validate_Hat_Length()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new Video(
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.mp4",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O chapéu deve ter no máximo 50 caracteres", result.Message);
    }

    [Fact]
    public void News_Validate_Title_Empty()
    {
        //Arrange & Act
        var results = Assert.Throws<DomainException>(() => new Video(
            "Entretenimento",
            string.Empty,
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.mp4",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O título é obrigatório", results.Message);
    }

    [Fact]
    public void News_Validate_Hat_Empty()
    {
        //Arrange & Act
        var result = Assert.Throws<DomainException>(() => new Video(
            string.Empty,
            "Fim de ano da Band traz programas especiais, filmes e shows excluisvos",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.webp",
            "Da Redação",
            "https://www.band.uol.com.br/img/band-tv/2021/12/21/2021_12_21_16_00_00_1_162.mp4",
            status: Status.Active
        ));
        
        //Assert
        Assert.Equal("O chapéu é obrigatório", result.Message);
    }
}