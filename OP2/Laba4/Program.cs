using Laba4;


namespace Laba4
{
    class Program
    {
        static void Main()
        {
            DigitStringHolder digitString = new DigitStringHolder("123456789");

            Console.WriteLine("Original digit string: " + digitString);

            Console.WriteLine("Length of digit string: " + digitString.GetLength());

            Console.WriteLine("Reversed digit string: " + digitString.ReverseDigits());
        }
    }
}