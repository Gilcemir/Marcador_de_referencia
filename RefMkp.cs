using System;
using System.IO;
using System.Linq;
using System.Text;
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
        public static string TagRef(string str, int id, ERefType type)
        {
            return "[ref id=\"r" + id + "\" reftype=\"" + type.ToString() + "\"]" + str + "[/ref]";
        }

        //marca tag Date, recebe como parâmetro o ano
        /*
            RefMkp.TagDate(2011) ou RefMkp.TagDate("2011")

            Retorna [date dateiso="20110000" specyear="2011"]2011[/date]

        */
        public static string TagDate(string year)
        {
            string temp = year.Length == 5 ? year.Substring(0, 4) : year;
            return "[date dateiso=\""
                + temp
                + "0000\" specyear=\""
                + year
                + "\"]"
                + year
                + "[/date]";
        }

        //recebe a string com autores e retorna os autores taggeados
        public static string TagAuthors(string authors)
        {
            //Esse padrão verifica se o primeiro autor é uma pessoa, ou seja,
            //começa com uma letra maiúscula, seguida de outras letras + espaço + uma ou mais letras MAIÚSCULAS.
            //Se não tiver esse padrão, é uma referência com autor corporativo (cauthor)
            string pattern = @"(^[A-Z][\w-]+ [A-Z]+)";
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            string str = "[authors role=\"nd\"]";

            if (r.IsMatch(authors))
            {
                string[] delimitadores = new string[] { ", ", " and ", "&" };

                string[] autores = authors
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
                    string temp = TagSimples(
                        TagSimples(aut, "surname")
                            + " "
                            + TagSimples(ASplitted[ASplitted.Length - 1], "fname"),
                        "pauthor"
                    );
                    str += temp + ", ";
                }
                str = str.Substring(0, str.Length - 2);
            }
            else
            {
                string temp = TagSimples(authors.Trim(), "cauthor");
                str += temp;
            }
            str += "[/authors]";
            return str;
        }

        public static void CreateFile(List<string> referenciasTag, string input)
        {
            string pathOut = Directory.GetCurrentDirectory() + @"\refsTag" + input + ".txt";
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(pathOut))
                {
                    foreach (string referencia in referenciasTag)
                    {
                        Byte[] refer = new UTF8Encoding(true).GetBytes(
                            referencia + Environment.NewLine
                        );
                        fs.Write(refer, 0, refer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void CreateFileInfo(List<string> referencias, string input)
        {
            string pathOut = Directory.GetCurrentDirectory() + @"\refs" + input + ".txt";
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(pathOut))
                {
                    int i = 1;
                    foreach (string referencia in referencias)
                    {
                        string autores = "";
                        string date = "";
                        string body = "";
                        string pattern = @"(.*)\((\d{4}\w?)\)(.*)";
                        Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                        Match m = r.Match(referencia);

                        autores = m.Groups[1].ToString();
                        date = m.Groups[2].ToString();
                        body = m.Groups[3].ToString();

                        string str = "r" + i + " = " + autores + " | " + date;
                        Byte[] refer = new UTF8Encoding(true).GetBytes(str + Environment.NewLine);
                        fs.Write(refer, 0, refer.Length);
                        i++;
                    }
                }

                // Open the stream and read it back.
                /*
                using (StreamReader sr = File.OpenText(pathOut))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine (s);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
