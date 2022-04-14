using System;
using System.Reflection;
using ConsoleMenuGenerator.MenuManager;

namespace ConsoleMenuGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Count() != 3)
            {
                MessageHelp();
                return;
            }
            
            var path = args[0];

            if (!File.Exists(path))
            {
                InvalidPathExtension();
                return;
            }

            var extension = Path.GetExtension(path);

            if (!extension.Equals(".proto") && !extension.Equals(".json"))
            {
                InvalidPathExtension(true);
                Console.WriteLine(extension);
                return;
            }

            IMenuManager menuManagerJson = new MenuManagerJson();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(GetProjectGithub());    
            Console.ResetColor();        
            menuManagerJson.GenerateMenu(path, "/Users/leandromayerpaixao/Projets/ConsoleMenuGenerator-NetCore/ConsoleMenuGenerator/MenuManager/model.json");
        }

        private static string GetProjectGithub()
        {
            return "Projeto GitHub: https://github.com/leandropaixao/ConsoleMenuGenerator-NetCore";
        }

        private static void MessageHelp()
        {
            var versionString = Assembly.GetEntryAssembly()?
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                .InformationalVersion
                                .ToString();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(GetProjectGithub());
            Console.WriteLine($"MenuGenerator v.{versionString}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Utilização: menuGenerator <opcao> <tipo> [argumento]");
            Console.WriteLine("  Opcoes");
            Console.WriteLine("    -e         Exportar o arquivo modelo");
            Console.WriteLine("    -g         Gerar novo arquivo Porgram.cs");
            Console.WriteLine("  Tipo entrada");
            Console.WriteLine("    -j         Formato JSON");
            Console.WriteLine("    -p         Formato PROTO\n");
            Console.WriteLine("  Argumento");
            Console.WriteLine("    Caminho de saída do arquivo .cs\n");
            Console.WriteLine("    Exportanto modelo (.json|.proto):");
            Console.WriteLine("    - Para exportar o modelo para leiem json:");
            Console.WriteLine("    ConsoleMenuGenerator.exe -e -j \"Caminho válido\"\n");
            Console.WriteLine("    - Para exportar o novo modelo em json:");
            Console.WriteLine("    ConsoleMenuGenerator.exe -e -p \"Caminho válido\"\n");
            Console.WriteLine("    Gerando um novo menu (.cs):");
            Console.WriteLine("    - Para exportar o modelo para leiem json:");
            Console.WriteLine("    ConsoleMenuGenerator.exe -g -j \"Caminho válido\"\n");
            Console.WriteLine("    - Para exportar o novo modelo em json:");
            Console.WriteLine("    ConsoleMenuGenerator.exe -g -p \"Caminho válido\"");
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

