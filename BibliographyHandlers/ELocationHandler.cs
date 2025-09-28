using System.Text.RegularExpressions;

namespace Marcador_de_referencia.BibliographyHandlers
{
    public class ELocationHandler : AbstractHandler
    {
        private readonly Regex rgx = AppRegexes.ELocation();

        public override BibliographyResult Handle(string body)
        {
            var match = rgx.Match(body);
            if (match.Success)
            {
                var bodyResult =
                    match.Groups[1]
                    + " "
                    + RefMkp.TagSimples(match.Groups[2].ToString(), "volid")
                    + ":"
                    + RefMkp.TagSimples(match.Groups[3].ToString(), "elocatid");

                return new BibliographyResult(bodyResult, ERefType.book);
            }

            return base.Handle(body);
        }
    }
}
