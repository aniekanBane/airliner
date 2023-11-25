namespace Domain.Utilities;

public static class RandomGenerator
{
    private static readonly Random _rng = new();

    public static string GenerateRandomString(int lenght)
    {
        const string characters = "abcdefghijklmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        int charsLength = characters.Length;
        var buffer = new char[lenght];

        lock(_rng)
        {
            for(int i = 0; i < lenght; i++)
                buffer[i] = characters[_rng.Next(charsLength)];
        }

        return new string(buffer);
    }
}
