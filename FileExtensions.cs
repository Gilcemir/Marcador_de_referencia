using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcador_de_referencia
{
    public static class FileUtils
    {
        public static List<string> GetRefs(string path)
        {
            List<string> referencias = [];
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (s.Trim().Length > 0)
                        referencias.Add(s);
                }
            }
            return referencias;
        }
    }
}