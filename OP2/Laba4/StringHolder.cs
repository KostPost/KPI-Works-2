
namespace Laba4
{
    public class StringHolder
    {
        public string Value { get; private set; }

        public StringHolder(string value)
        {
            Value = value;
        }

        public int GetLength()
        {
            return Value.Length;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
