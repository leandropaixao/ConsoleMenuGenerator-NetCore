using System;
using System.Reflection;
using ConsoleMenuGenerator.MenuManager;

namespace ConsoleMenuGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                MensagemAjuda();
                return;
            }
            
            var path = args[1];

            if (args[0].Equals("-g"))
            {
                if (!File.Exists(path))
                {
                    ValidarCaminhoExtensao();
                    return;
                }

                var extension = Path.GetExtension(path);

                if (!extension.Equals(".json"))
                {
                    ValidarCaminhoExtensao(true);
                    Console.WriteLine(extension);
                    return;
                }
                
                IMenuManager menuManagerJson = new MenuManagerJson();

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(GetProjetoGithub());
                Console.ResetColor();
                menuManagerJson.GerarMenu(path);                
            }
            else if (args[0].Equals("-e"))
            {
                if (!Directory.Exists(path))
                {
                    ValidarCaminhoExtensao();
                    return;
                }
                IMenuManager menuManagerJson = new MenuManagerJson();

                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(GetProjetoGithub());
                Console.ResetColor();
                menuManagerJson.GerarModelo(path);
            }
            else
            {
                MensagemAjuda();
            }
        }

        private static string GetProjetoGithub() => "Projeto GitHub: https://github.com/leandropaixao/ConsoleMenuGenerator-NetCore";

        private static void MensagemAjuda()
        {
            var versionString = Assembly.GetEntryAssembly()?
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                                .InformationalVersion
                                .ToString();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(GetProjetoGithub());
            Console.WriteLine($"MenuGenerator v.{versionString}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Utilização: menuGenerator <opcao> [argumento]");
            Console.WriteLine("  Opções");
            Console.WriteLine("    -e         Exportar o arquivo no modelo .json");
            Console.WriteLine("    -g         Gerar um novo arquivo Program.cs");            
            Console.WriteLine("  Argumento");
            Console.WriteLine("    Caminho do arquivo de entrada ou de saída (.cs, .json)\n");
            Console.WriteLine("  Exemplos:");
            Console.WriteLine("    Exportanto modelo .json:");
            Console.WriteLine("    - Para exportar o modelo em formato .json:");
            Console.WriteLine("         ConsoleMenuGenerator.exe -e \"Caminho válido\"\n");
            Console.WriteLine("         dotnet ConsoleMenuGenerator.dll -e \"Caminho válido\"\n");            
            Console.WriteLine("    Gerando um novo menu (.cs):");
            Console.WriteLine("    - Para gerar um menu a partir do modelo .json:");
            Console.WriteLine("         ConsoleMenuGenerator.exe -g \"Caminho válido\"\n");
            Console.WriteLine("         dotnet ConsoleMenuGenerator.dll -g \"Caminho válido\"\n");            
            Console.ResetColor();
        }

        private static void ValidarCaminhoExtensao(bool extension = false)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(GetProjetoGithub());
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            if (extension)
                Console.WriteLine($"Tipo de arquivo inválido, deve ser .json");
            else
                Console.WriteLine("Caminho do arquivo inválido ou arquivo inexistente.");
            Console.ResetColor();
        }        
    }
}