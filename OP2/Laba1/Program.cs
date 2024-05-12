class Program
{
    static void Main()
    {
        int[] incrementTestValues = { 16, 61, -37 };
        Console.WriteLine("Testing Increment and IncrementReturn functions:");
        foreach (int value in incrementTestValues)
        {
            int incrementedValue = value;
            Decrement(ref incrementedValue);
            Console.WriteLine($"Original value: {value}, Incremented: {incrementedValue}");

            int incrementedReturnValue = DecrementReturn(value);
            Console.WriteLine($"Original value: {value}, IncrementReturn: {incrementedReturnValue}");
        }

        int[] inequalityTestValuesA = { 100, -8, 132 };
        int[] inequalityTestValuesB = { 100, 125, 131 };
        Console.WriteLine("\nTesting CheckLessThan and CheckLessThanReturn functions:");
        for (int i = 0; i < inequalityTestValuesA.Length; i++)
        {
            bool isLessThan = false;
            CheckLessThan(inequalityTestValuesA[i], inequalityTestValuesB[i], ref isLessThan);
            Console.WriteLine($"{inequalityTestValuesA[i]} < {inequalityTestValuesB[i]}: {isLessThan}");

            bool isLessThanReturn = CheckLessThanReturn(inequalityTestValuesA[i], inequalityTestValuesB[i]);
            Console.WriteLine($"{inequalityTestValuesA[i]} < {inequalityTestValuesB[i]}: {isLessThanReturn}");
        }
    }

    static void Decrement(ref int value)
    {
        value--;
    }

    static int DecrementReturn(int value)
    {
        return value - 1;
    }

    static void CheckLessThan(int value1, int value2, ref bool res)
    {
        res = value1 < value2;
    }

    static bool CheckLessThanReturn(int value1, int value2)
    {
        return value1 < value2;
    }
}