using Laba3;


class Program
{
    static void Main()
    {
        Segment s1 = new Segment(1, 1, 4, 5);
        Segment s2 = new Segment(s1);
        Segment s3 = s1 + s2; 
        Segment s4 = s1 - s2; 

        Console.WriteLine("Segment s1: " + s1);
        Console.WriteLine("Segment s2 (Copy of s1): " + s2);
        Console.WriteLine("Length of s1: " + s1.Length());
        Console.WriteLine("Segment s3 (s1 + s2): " + s3);
        Console.WriteLine("Segment s4 (s1 - s2): " + s4);
    }
}
