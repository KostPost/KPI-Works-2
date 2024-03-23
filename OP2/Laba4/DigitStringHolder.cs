
namespace Laba4
{

    public class DigitStringHolder : StringHolder
    {
        public DigitStringHolder(string value) : base(value)
        {
            if (!IsAllDigits(value))
            {
                throw new ArgumentException("The string must contain only digits.");
            }
        }

        private bool IsAllDigits(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        public string ReverseDigits()
        {
            char[] charArray = Value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}