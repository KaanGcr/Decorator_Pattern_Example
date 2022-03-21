namespace DecoratorPattern.Utilities
{
    public static class Pairing
    {
        public static KeyValuePair<string, string> Of(string Key, string Value)
        {
            return new KeyValuePair<string, string>(Key, Value);
        }
    }
}
