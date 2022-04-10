using System;
using ConsoleMenuGenerator.MenuManager;

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

        private static void MenuConsole()
        {
            Console.Clear();
            var sair = false;
            var opcao = "";
            do
            {                 
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("+----- Menu console -----+");
                Console.WriteLine("| 1 - Opcao 1            |");
                Console.WriteLine("| 2 - Opcao 2            |");
                Console.WriteLine("| 3 - Opcao 3            |");
                Console.WriteLine("| 4 - Opcao 4            |");
                Console.WriteLine("| 5 - Opcao 5            |");
                Console.WriteLine("+------------------------+");
                Console.WriteLine("| S - Sair               |");
                Console.WriteLine("+------------------------+");
                Console.ResetColor();

                Console.Write("Informe um valor: ");
                opcao = Console.ReadLine();                             
                {
                    var number = 0;
                    var isNumber = int.TryParse(opcao, out number);

                    if (isNumber)
                    {
                        switch(number)
                        {
                            case 1 : 
                                Console.WriteLine("Escolhida a opcao 1");
                                break;
                            case 2 : 
                                Console.WriteLine("Escolhida a opcao 2");
                                break;
                            case 3 : 
                                Console.WriteLine("Escolhida a opcao 3");
                                break;
                            case 4 : 
                                Console.WriteLine("Escolhida a opcao 4");
                                break;
                            case 5 : 
                                Console.WriteLine("Escolhida a opcao 5");
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine("Opção inválida");
                                break;
                        }

                    }
                    else
                    {
                        if (opcao.Equals("S"))
                        {
                            sair = true;
                            Console.WriteLine("Saindo do sistema");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Opçào inválida. Informe outra opção");
                        }
                    }
                }
            } while (!sair);            
        }
    }
}

