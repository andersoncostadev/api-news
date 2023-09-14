using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NewsAPI.Infra;


public static class Helper
{
    public static class GenerateSlug
    {
        public static string Generate(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var slug = RemoveSpecialCharacters(input).Replace(" ", "-").ToLower();

            return slug;
        }

        private static string RemoveSpecialCharacters(string str)
        {
            var normalizedString = str.Normalize(NormalizationForm.FormD);
            var result = new StringBuilder();

            foreach (var c in from c in normalizedString
                     let category = CharUnicodeInfo.GetUnicodeCategory(c)
                     where category != UnicodeCategory.NonSpacingMark
                     select c)
            {
                result.Append(c);
            }

            var cleanedString = Regex.Replace(result.ToString(), @"[^\p{L}0-9\s-]", "");
            return cleanedString;
        }
    }
}