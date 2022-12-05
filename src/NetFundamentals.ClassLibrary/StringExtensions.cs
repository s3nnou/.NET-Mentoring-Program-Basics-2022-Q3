using System.Text;

namespace NetFundamentals.ClassLibrary
{
    public static class StringExtensions
    {
        public static string AddTimeStamp(this string text)
        {
            var builder = new StringBuilder();
            builder.Append(DateTime.Now.ToShortTimeString());
            builder.Append(' ');
            builder.Append(text);
            return builder.ToString();
        }
    }
}