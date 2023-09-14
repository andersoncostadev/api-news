namespace NewsAPI.Entities;

public class AssertionConcern
{
    
    
    /// <summary>
    /// Validação de tamanho máximo de string
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="maxLength"></param>
    /// <param name="message"></param>
    /// <exception cref="DomainException"></exception>
    public static void AssertArgumentLength(string argument, int maxLength, string message)
    {
        var length = argument.Trim().Length;
        if (length > maxLength)
        {
            throw new DomainException(message);
        }
    }
    
    /// <summary>
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="message"></param>
    /// <exception cref="DomainException"></exception>
    public static void AssertArgumentNotNull(object argument, string message)
    {
        if (argument == null)
        {
            throw new DomainException(message);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="argument"></param>
    /// <param name="message"></param>
    /// <exception cref="DomainException"></exception>
    public static void AssertArgumentNotEmpty(string argument, string message)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new DomainException(message);
        }
    }
}