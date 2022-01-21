using System;
using Marcador_de_referencia;
using System.IO;

namespace Program
{

    class Tela_Principal
    {
        static void Main(string[] args)
        {
           string path = @"C:\workspace\Marcador_de_referencia\";
            Console.WriteLine("Entre o nome do arquivo: ");
            path+=Console.ReadLine();

            List<string> referencias = new List<string>{};         
            
            using (StreamReader sr = File.OpenText(path))
            {
            string s;
                while ((s = sr.ReadLine()) != null)
                {
                referencias.Add(s);
                }
            }

            foreach(string referencia in referencias)
            {
                Console.WriteLine(referencia);
            }

        }
    }


}