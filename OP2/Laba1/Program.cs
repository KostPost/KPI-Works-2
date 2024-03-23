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

        int[] equalityTestValuesA = { 100, -8, 132 };
        int[] equalityTestValuesB = { 100, 125, 131 };
        Console.WriteLine("\nTesting CheckEquality and CheckEqualityReturn functions:");
        for (int i = 0; i < equalityTestValuesA.Length; i++)
        {
            bool isEqual = false;
            CheckEquality(equalityTestValuesA[i], equalityTestValuesB[i], ref isEqual);
            Console.WriteLine($"{equalityTestValuesA[i]} == {equalityTestValuesB[i]}: {isEqual}");

            bool isEqualReturn = CheckEqualityReturn(equalityTestValuesA[i], equalityTestValuesB[i]);
            Console.WriteLine($"{equalityTestValuesA[i]} == {equalityTestValuesB[i]}: {isEqualReturn}");
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

    static void CheckEquality(int value1, int value2, ref bool res)
    {
        res = value1 == value2;
    }

    static bool CheckEqualityReturn(int value1, int value2)
    {
        return value1 == value2;
    }
}