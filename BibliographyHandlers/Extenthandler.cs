using System.Text.RegularExpressions;

namespace Marcador_de_referencia.BibliographyHandlers
{
    public class Extenthandler : AbstractHandler
    {
        private readonly Regex rgx = AppRegexes.Extent();

        public override BibliographyResult Handle(string body)
        {
            var match = rgx.Match(body);
            if (match.Success)
            {
                string suffix =
                    match.Groups.Count > 3 && match.Groups[3].Success
                        ? match.Groups[3].Value.Trim()
                        : string.Empty;

                var bodyResult =
                    match.Groups[1]
                    + " "
                    + RefMkp.TagSimples(match.Groups[2].ToString(), "extent")
                    + "p"
                    + suffix;

                return new BibliographyResult(bodyResult, ERefType.book);
            }

            return base.Handle(body);
        }
    }
}
