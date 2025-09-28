using System.Text.RegularExpressions;

namespace Marcador_de_referencia.BibliographyHandlers
{
    public class AccessedOnHandler : AbstractHandler
    {
        private readonly Regex rgx = AppRegexes.AccessedOn();

        public override BibliographyResult Handle(string body)
        {
            var match = rgx.Match(body);
            if (match.Success)
            {
                var dateTime = GetDateTime(match.Groups[3].Value);

                if (dateTime != null)
                {
                    var bodyResult = string.Join(
                        ' ',
                        match.Groups[1],
                        match.Groups[2],
                        RefMkp.TagCited(
                            match.Groups[3].Value,
                            dateTime.Value.Year,
                            dateTime.Value.Month,
                            dateTime.Value.Day
                        )
                    );
                    return new BibliographyResult(bodyResult, ERefType.book);
                }
            }

            return base.Handle(body);
        }

        private static readonly string[] Months =
        [
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December",
        ];

        private static DateTime? GetDateTime(string? match)
        {
            if (match == null)
            {
                return null;
            }

            int month = 1;
            var splits = match.Split(' ');

            for (int i = 0; i < Months.Length; i++)
            {
                if (Months[i].StartsWith(splits[0], StringComparison.OrdinalIgnoreCase))
                {
                    month = i + 1;
                }
            }

            var dayTrimmed = splits[1].TrimEnd(',', ' ', '.');
            //mock values
            int day;
            bool tryParseDay = int.TryParse(dayTrimmed, out day);
            int year;
            bool tryParseYear = int.TryParse(splits[2].Substring(0, 4), out year);

            bool result = DateTime.TryParse($"{year}-{month}-{day}", out var dateResult);

            return result && tryParseDay && tryParseYear ? dateResult : null;
        }
    }
}
