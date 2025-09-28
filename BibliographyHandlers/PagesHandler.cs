using System.Text.RegularExpressions;

namespace Marcador_de_referencia.BibliographyHandlers
{
    public class PagesHandler : AbstractHandler
    {
        private readonly Regex rgx = AppRegexes.Pages();

        public override BibliographyResult Handle(string body)
        {
            var match = rgx.Match(body);
            if (match.Success)
            {
                var bodyResult =
                    match.Groups[1] + " " + RefMkp.TagSimples(match.Groups[2].ToString(), "pages");

                return new BibliographyResult(bodyResult, ERefType.book);
            }
            return base.Handle(body);
        }
    }
}
