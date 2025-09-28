using System.Text.RegularExpressions;

namespace Marcador_de_referencia.BibliographyHandlers
{
    public class ArticleVolPagesHandler : AbstractHandler
    {
        private readonly Regex rgx = AppRegexes.ArticleVolPages();

        public override BibliographyResult Handle(string body)
        {
            var match = rgx.Match(body);
            if (match.Success)
            {
                var splits = match.Groups[1].ToString().Split('.');

                var counter = 1;
                var source = splits[^counter].Trim();
                //prevenir overflow
                while (counter <= splits.Length && source == string.Empty)
                {
                    counter++;
                    source = splits[^counter].Trim();
                }

                var arrtitle = string.Join('.', splits.Take(splits.Length - counter));
                //se for maior que 1, existe separação.
                var arrtitleAndSource =
                    splits.Length > 1
                        ? RefMkp.TagSimples(arrtitle, "arttitle")
                            + RefMkp.TagSimples(source, "source")
                        : match.Groups[1].ToString();

                var resultBody =
                    arrtitleAndSource
                    + " "
                    + RefMkp.TagSimples(match.Groups[2].ToString(), "volid")
                    + ":"
                    + RefMkp.TagSimples(match.Groups[3].ToString(), "pages");

                return new BibliographyResult(resultBody, ERefType.journal);
            }

            return base.Handle(body);
        }
    }
}
