using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Marcador_de_referencia
{
    public class RefMkp
    {
        //marca tag simples, string e tag como parâmetro
        /*
            RefMkp.TagSimples("aaaaa", "source")

            Retorna [source]aaaaa[/source]

        */
        public static string TagSimples(string str, string tag)
        {
            return "[" + tag + "]" + str + "[/" + tag + "]";
        }

        //marca tag ref, recebe como parâmetro a referencia, o ID e o tipo
        /*
            RefMkp.TagRef("aaaaa", 1, "book")

            Retorna [ref id="r1" reftype="book"]aaaaa[/ref]

        */
        public static string TagRef(string str, int id, string tipo)
        {
            return "[ref id=\"r" +
            id +
            "\" retype=\"" +
            tipo +
            "\"]" +
            str +
            "[/ref]";
        }

        //marca tag Date, recebe como parâmetro o ano
        /*
            RefMkp.TagDate(2011) ou RefMkp.TagDate("2011")

            Retorna [date dateiso="20110000" specyear="2011"]2011[/date]

        */
        public static string TagDate(object year)
        {
            return "[date dateiso=\"" +
            year +
            "0000\" specyear=\"" +
            year +
            "\"]" +
            year +
            "[/date]";
        }

        //recebe a string com autores e retorna os autores taggeados
        public static string TagAuthors(string authors)
        {

            string pattern = @"(^[A-Z]\w+ [A-Z]+)";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            string teste = r.IsMatch(authors)?"---tem!--":"--nao tem--";
            

            string str = "[authors role=\"nd\"]";

            if(r.IsMatch(authors))
            {
            string[] delimitadores = new string[] { ", ", " and ", "&" };

            string[] autores =
                authors
                    .Split(delimitadores, StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                    .Select(p => p.Trim())
                    .ToArray();

            foreach (string autor in autores)
            {
                string aut = "";
                string[] ASplitted = autor.Split(' ');

                if (ASplitted.Length > 2)
                {
                    for (int i = 0; i < ASplitted.Length - 1; i++)
                    aut += ASplitted[i] + " ";

                    aut = aut.Trim();
                }
                else
                    aut = ASplitted[0];
                //Aqui parece um pouco confuso, mas é uma sobreposição da Função TagSimples. Como a tag autor tem tag dentro de tag, a função TagSimples é passada como referência...
                string temp =TagSimples(
                    TagSimples(aut, "surname") +
                    TagSimples(ASplitted[ASplitted.Length - 1], "fname"),
                     "pauthor");
                str += temp;
            }
            }else
            {
                string temp = TagSimples(authors.Trim(),"cauthor");
                str+=temp;
            }
            str += "[/authors]";
            return str;
        }

        public static List<string> GetRefs(string path)
        {
            List<string> referencias = new List<string> { };
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Trim().Length > 0) referencias.Add(s);
                }
            }
            return referencias;

        }


    }
}
