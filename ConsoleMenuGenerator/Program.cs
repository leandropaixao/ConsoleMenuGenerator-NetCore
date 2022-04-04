using System;

namespace ConsoleMenuGenerator
{
    class Program
    {
       public static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                MessageHelp();
                return;
            }
            
            var path = args[0];

            if (!File.Exists(path))
            {
                InvalidPathExtenion();
                return;
            }

            var extension = Path.GetExtension(path);

            if (!extension.Equals(".proto") && !extension.Equals(".json"))
            {
                InvalidPathExtenion(true);
                Console.WriteLine(extension);
                return;
            }

        }

        private static string GetProjectGithub()
        {
            return "Projeto GitHub: https://github.com/leandropaixao/ConsoleMenuGenerator-NetCore";
        }

        private static void MessageHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(GetProjectGithub());
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Utilização: dotnet run [argumento]");
            Console.WriteLine("");
            Console.WriteLine("  argumento:");
            Console.WriteLine("    - caminho do arquivo .json ou do arquivo .proto");
            Console.WriteLine("");
            Console.ResetColor();
        }

        private static void InvalidPathExtension(bool extension = false)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(GetProjectGithub());
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            if (extension)
                Console.WriteLine("Tipo de arquivo inválido, deve ser .json ou .proto.");    
            else
                Console.WriteLine("Caminho do arquivo inválido ou arquivo inexistente.");
            Console.ResetColor();
        }        
    }
}

