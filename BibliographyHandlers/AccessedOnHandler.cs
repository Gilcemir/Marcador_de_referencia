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
                var bodyResult = body; //todo
                return new BibliographyResult(bodyResult, ERefType.book);
            }

            return base.Handle(body);
        }
    }
}
