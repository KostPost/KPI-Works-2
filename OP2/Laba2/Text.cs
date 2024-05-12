namespace Laba2;

public class Text : TextContainer
{
    public Text()
    {
        Text = string.Empty;
    }

    public override void Display()
    {
        Console.WriteLine(Text);
    }
}