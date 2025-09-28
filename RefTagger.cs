using System.Text.RegularExpressions;
using Marcador_de_referencia.BibliographyHandlers;

namespace Marcador_de_referencia
{
    public class RefTagger
    {
        private readonly Regex BaseRgx = AppRegexes.Base();
        private IHandler _chainOfHandlers;

        public RefTagger()
        {
            var articleVolPagesHandler = new ArticleVolPagesHandler();
            var extenthandler = new Extenthandler();
            var PagesHandler = new PagesHandler();
            var eLocationHandler = new ELocationHandler();
            var AccessedOnHandler = new AccessedOnHandler();

            articleVolPagesHandler
                .SetNext(extenthandler)
                .SetNext(PagesHandler)
                .SetNext(eLocationHandler)
                .SetNext(AccessedOnHandler);
            _chainOfHandlers = articleVolPagesHandler;
        }

        public string TagReference(string reference, int index)
        {
            string autores;
            string date;
            string body;

            var match = BaseRgx.Match(reference);

            autores = match.Groups[1].ToString();
            date = match.Groups[2].ToString();
            body = match.Groups[3].ToString();

            var result = _chainOfHandlers.Handle(body);

            return RefMkp.TagRef(
                RefMkp.TagAuthors(autores) + RefMkp.TagDate(date) + result.Body,
                index,
                result.RefType
            );
        }
    }
}
