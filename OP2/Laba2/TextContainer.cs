namespace Laba2;

public class TextContainer
{
    private List<MyString> text = new List<MyString>();

    public void AddLine(MyString line)
    {
        text.Add(line);
    }

    public void RemoveLine(MyString line)
    {
        text.Remove(line);
    }

    public void RemoveLinesContaining(string substring)
    {
        text = text.Where(line => !line.Contains(substring)).ToList();
    }

    public void ClearText()
    {
        text.Clear();
    }

    public MyString GetLongestLine()
    {
        return text.OrderByDescending(line => line.Length).FirstOrDefault();
    }

    public void CapitalizeFirstLetters()
    {
        foreach (var line in text)
        {
            line.MakeFirstLetterUpperCase();
        }
    }

    public override string ToString()
    {
        return String.Join("\n", text);
    }
}
