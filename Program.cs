using System;
using System.IO;
using System.Text.RegularExpressions;
using Marcador_de_referencia;
using System.Text;

namespace Program
{
    class Tela_Principal
    {
        //Essa função divide a referência em 3 partes: Autores, ano e o resto. Estou fazendo pouco a pouco, então por enquanto se
        // o programa só marcar os autores e data automático, já vai me economizar bastante tempo...
        private static void SplitRef(
            string referencia,
            ref string autores,
            ref string date,
            ref string body
        )
        {
            string pattern = @"(.*)\((\d{4})\)(.*)";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);

            Match m = r.Match(referencia);

            autores = m.Groups[1].ToString();
            date = m.Groups[2].ToString();
            body = m.Groups[3].ToString();
        }

        static void Main(string[] args)
        {
            string path = @"C:\workspace\Marcador_de_referencia\";

            Console.WriteLine("Entre o nome do arquivo: ");
            path += Console.ReadLine();

            List<string> referencias = new List<string> { };
            List<string> referenciasTag = new List<string> { };

            referencias = RefMkp.GetRefs(path);
            
            
            int i = 1;
            foreach (string referencia in referencias)
            {
                string autores = "";
                string date = "";
                string body = "";
                SplitRef(referencia, ref autores, ref date, ref body);
                string temporaria =
                    RefMkp
                        .TagRef((
                        RefMkp.TagAuthors(autores) + RefMkp.TagDate(date)
                        ) +
                        body,
                        i,
                        "book");
                referenciasTag.Add (temporaria);
                i++;
            }

            RefMkp.CreateFile(referenciasTag);

 
        }
    }
}
