using Marcador_de_referencia;

namespace Program
{
    class Tela_Principal
    {
        static void Main(string[] args)
        {
            var refTagger = new RefTagger();
            string path = Directory.GetCurrentDirectory();
            path += @"\";

            string input = GetInput();
            path += input;
            path += ".txt";

            List<string> referenciasTag = new List<string> { };

            var referencias = FileUtils.GetRefs(path);

            int i = 1;
            foreach (string referencia in referencias)
            {
                referenciasTag.Add(refTagger.TagReference(referencia, i));
                i++;
            }
            RefMkp.CreateFile(referenciasTag, input);
            RefMkp.CreateFileInfo(referencias, input);
        }

        private static string GetInput()
        {
            string? result = null;
            while (string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine("Entre o nome do arquivo: (não precisa colocar o .txt)");
                result = Console.ReadLine();
            }

            return result;
        }
    }
}
