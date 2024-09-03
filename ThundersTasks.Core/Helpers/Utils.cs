namespace ThundersTasks.Core.Helper
{
    public class Utils
    {
        public static string AbbreviateText(string input, int maxLength)
        {
            if (input.Length <= maxLength)
            {
                return input;
            }
            else
            {
                return string.Concat(input.AsSpan(0, maxLength - 3), "...");
            }
        }
    }
}
