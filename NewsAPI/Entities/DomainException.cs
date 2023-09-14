namespace NewsAPI.Entities;

public class DomainException : Exception
{
    
    /// <summary>
    /// Cria somente uma instancia
    /// </sumary>
    public DomainException() { }
    
    /// <summary>
    /// Passa uma mensagem personalizada
    /// </sumary>
    /// <param name="message"></param>
    public DomainException(string message) : base(message) { }
    
    /// <summary>
    /// Passa uma mensagem e uma exception que iniciou anteriormente e queremos retornar ela
    /// </sumary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}