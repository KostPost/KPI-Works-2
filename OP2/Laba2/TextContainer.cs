namespace Laba2;

public abstract class TextContainer
{
    protected string Text { get; set; }

    public void AddLine(string line)
    {
        Text += line + Environment.NewLine;
    }

    public void RemoveLine(string line)
    {
        Text = Text.Replace(line + Environment.NewLine, "");
    }

    public void ClearText()
    {
        Text = string.Empty;
    }

    public int ShortestLineLength()
    {
        string[] lines = Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        return lines.Min(line => line.Length);
    }

    public double ConsonantPercentage()
    {
        if (Text.Length == 0)
            return 0;

        int consonantCount = StringMethods.CountConsonants(Text);
        double totalChars = Text.Count(char.IsLetter);
        return (consonantCount / totalChars) * 100;
    }

    public void ReplaceMultipleSpaces()
    {
        Text = StringMethods.ReplaceMultipleSpaces(Text);
    }

    public void TrimAndNormalize()
    {
        Text = StringMethods.TrimAndNormalize(Text);
    }

    public abstract void Display();
}