namespace Laba5;
    class Program
    {
        static void Main()
        {
            // Створення об'єктів
            Strings myString = new Strings("Hello World");
            Strings upperString = new UpperCaseStrings("Hello World");
            Strings lowerString = new LowerCaseStrings("Hello World");

            // Вивід довжини
            Console.WriteLine("Length of myString: " + myString.Length());
            Console.WriteLine("Length of upperString: " + upperString.Length());
            Console.WriteLine("Length of lowerString: " + lowerString.Length());

            // Вивід оброблених рядків
            Console.WriteLine("Sorted myString:");
            myString.SortAndPrint();
            Console.WriteLine("Sorted upperString:");
            upperString.SortAndPrint();
            Console.WriteLine("Sorted lowerString:");
            lowerString.SortAndPrint();
        }
    }

