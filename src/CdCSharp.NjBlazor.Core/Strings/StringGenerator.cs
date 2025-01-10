using System.Text;

namespace CdCSharp.NjBlazor.Core.Strings;

public static class StringGenerator
{
    //We use a character set that is a power of 2 in length (_allowedChars.Length), which ensures that the modulo operation doesn’t introduce bias
    private static readonly char[] _allowedChars =
           "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

    /// <summary>
    /// Generates a random alphanumeric string of the specified length.
    /// </summary>
    /// <param name="length">
    /// The length of the random string to generate. Defaults to 24 if no value is provided.
    /// </param>
    /// <returns>
    /// A random string consisting of uppercase letters, lowercase letters, and digits, with a total
    /// length equal to the specified <paramref name="length" />.
    /// </returns>
    /// <remarks>
    /// This method uses a pseudo-random number generator to select characters from a predefined set
    /// of alphanumeric characters. The resulting string is not cryptographically secure.
    /// </remarks>
    /// <example>
    /// Example usage:
    /// <code>
    ///string randomString = GenerateRandomString(12);
    ///// Example output: "aB3dE9GhKjL1"
    /// </code>
    /// </example>
    public static string GenerateRandomString(int length = 24)
    {
        if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.");

        byte[] randomBytes = new byte[length];
        Random.Shared.NextBytes(randomBytes);

        StringBuilder sb = new(length);
        foreach (byte randomByte in randomBytes)
            sb.Append(_allowedChars[randomByte % _allowedChars.Length]);
        return sb.ToString();
    }
}