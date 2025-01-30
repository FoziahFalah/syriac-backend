namespace SyriacSources.Backend.Domain.Exceptions;

public class DuplicateRecordException : Exception
{
    public DuplicateRecordException(string value)
        : base($"Value \"{value}\" already exists.")
    {
    }
}
