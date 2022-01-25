using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Marcador_de_referencia;

namespace Program
{
    class Tela_Principal
    {

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
               referenciasTag.Add (RefMkp.ReferenciaTagSimples(referencia, i));
               i++; 
            }
            RefMkp.CreateFile (referenciasTag);
        }
    }
}
