namespace SyriacSources.Backend.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors, object? body)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Body = body;
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public object? Body { get; init; } // Body for successful results

    public static Result Success(object? body)
    {
        return new Result(true, Array.Empty<string>(), body);
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors, null);
    }

    public static Result Failure(string error)
    {
        return Failure(new[] { error });
    }

    internal static Result Success()
    {
        throw new NotImplementedException();
    }
}
