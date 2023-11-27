namespace Domain.Primitives.Results;

public sealed record class Error(string Title, params string[] Messages)
{
    public static implicit operator string(Error? error) 
        => string.Format(
            "{0}: \n {1}", 
            error?.Title, 
            string.Join(
                Environment.NewLine, 
                error!.Messages.Select(x => "-- " + x)
            )
        );

    internal static Error None => new(string.Empty);
}