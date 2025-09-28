using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Marcador_de_referencia
{
    public static partial class AppRegexes
    {
        [GeneratedRegex(@"(.*)\((\d{4}\w?)\)(.*)", RegexOptions.IgnoreCase)]
        public static partial Regex Base();

        //Essa pattern pega tambem o centro do texto , arrtitle e source, pra já marcar TODO
        [GeneratedRegex(@"(.*)\((\d{4}\w?)\)(.*) (\d+) ?: ?(\d+(-\d+)?)", RegexOptions.IgnoreCase)]
        public static partial Regex Article();

        //Esse pattern pega o volume/páginas nos formatos 1:11-11, 1 : 11-11, 1:11
        [GeneratedRegex(@"(.*) (\d+) ?: ?(\d+(-\d+)?)", RegexOptions.IgnoreCase)]
        public static partial Regex ArticleVolPages();

        //Pattern para marcar tag EXTENT 444p 444 p
        [GeneratedRegex(@"(.*) (\d+) ?p(.*)", RegexOptions.IgnoreCase)]
        public static partial Regex Extent();

        //Pattern pages p. 2313-313, p. 1111  ou p.111-11
        [GeneratedRegex(@"(.*p.) ?(\d+(-\d+)?)", RegexOptions.IgnoreCase)]
        public static partial Regex Pages();

        //Pattern to match articles with ELocation
        [GeneratedRegex(@"(.*) (\d+) ?: ?(e\d+)", RegexOptions.IgnoreCase)]
        public static partial Regex ELocation();

        //Pattern para Accessed on
        [GeneratedRegex(@"(.*)(Accessed on) (.*)", RegexOptions.IgnoreCase)]
        public static partial Regex AccessedOn();
    }
}
