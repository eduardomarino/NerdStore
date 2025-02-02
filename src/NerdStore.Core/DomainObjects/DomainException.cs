namespace NerdStore.Core.DomainObjects;

public class DomainException : Exception
{
    public DomainException()
    { }

    public DomainException(string message) : base(message)
    { }

    // innerException para especializar a exceção
    public DomainException(string message, Exception innerException) : base(message, innerException)
    { }
}
