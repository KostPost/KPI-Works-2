using System.Text.RegularExpressions;

namespace Laba2;

public static class StringMethods
{
    public static int CountConsonants(string text)
    {
        int consonantCount = 0;
        string vowels = "aeiouAEIOU";
        foreach (char c in text)
        {
            if (!char.IsWhiteSpace(c) && !char.IsPunctuation(c) && !vowels.Contains(c))
            {
                consonantCount++;
            }
        }
        return consonantCount;
    }

    public static string ReplaceMultipleSpaces(string text)
    {
        return Regex.Replace(text, @"\s+", " ");
    }

    public static string TrimAndNormalize(string text)
    {
        return text.Trim();
    }
}
