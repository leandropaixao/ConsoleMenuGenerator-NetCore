using System.Text;
using System.Text.Json;

namespace ConsoleMenuGenerator.MenuManager
{
    internal class MenuJson
    {
        public string? Title { get; set; }
        public string[]? Menu { get; set; }        
    }


    public class MenuManagerJson : IMenuManager
    {
        public void GenerateMenu(string pathOrigin, string pathDestiny)
        {
            var fileJson = File.ReadAllText(pathOrigin);
            var menuJson = JsonSerializer.Deserialize<MenuJson>(fileJson)!;

            if (menuJson == null) throw new NullReferenceException();
            
            var jsonSB = new StringBuilder();
            var optionsSB = new StringBuilder();

            jsonSB.AppendLine("using System;");
            jsonSB.AppendLine("namespace ConsoleMenu");
            jsonSB.AppendLine("{");
            jsonSB.AppendLine("    class Program");
            jsonSB.AppendLine("    {");
            jsonSB.AppendLine("        public static void Main(string[] args)");
            jsonSB.AppendLine("        {");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("            Console.Clear();");
            jsonSB.AppendLine("            var sair = false;");
            jsonSB.AppendLine("            var opcao = \"\";");
            jsonSB.AppendLine("            do");
            jsonSB.AppendLine("            {");
            jsonSB.AppendLine("                Console.ForegroundColor = ConsoleColor.DarkBlue;");
            jsonSB.AppendLine("                Console.WriteLine(\"+----- Menu console -----+\");");
            for(int i = 0; i < menuJson.Menu.Length; i++)
            {                
                jsonSB.AppendLine($"                Console.WriteLine(\"| {i+1} - {menuJson.Menu[i]}        |\");");
                optionsSB.AppendLine((i+1).ToString());
            }
            optionsSB.AppendLine("");
            jsonSB.AppendLine("                Console.WriteLine(\"+------------------------+\");");
            jsonSB.AppendLine("                Console.WriteLine(\"| S - Sair               |\");");
            jsonSB.AppendLine("                Console.WriteLine(\"+------------------------+\");");
            jsonSB.AppendLine("                Console.ResetColor();");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("                Console.Write(\"Informe um valor: \");");
            jsonSB.AppendLine("                opcao = Console.ReadLine();");
            jsonSB.AppendLine("                var number = 0;");
            jsonSB.AppendLine("                var isNumber = int.TryParse(opcao, out number);");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("                if (isNumber)");
            jsonSB.AppendLine("                {");
            jsonSB.AppendLine("                    switch(number)");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        case 1 :");
            jsonSB.AppendLine("                            Console.WriteLine(\"Escolhida a opcao 1\");");
            jsonSB.AppendLine("                            break;");
            for(int i = 0; i < menuJson.Menu.Length; i++)
            {
                jsonSB.AppendLine($"                        case {i+1} :");
                jsonSB.AppendLine($"                            Console.WriteLine(\"{menuJson.Menu[i]}\");");
                jsonSB.AppendLine("                            break;");
                optionsSB.AppendLine((i+1).ToString());
            }
            jsonSB.AppendLine("                        default:");
            jsonSB.AppendLine("                            Console.WriteLine();");
            jsonSB.AppendLine("                            Console.WriteLine(\"Opção inválida\");");
            jsonSB.AppendLine("                            break;");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                }");
            jsonSB.AppendLine("                else");
            jsonSB.AppendLine("                {");
            jsonSB.AppendLine("                    if (opcao.Equals(\"S\"))");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        sair = true;");
            jsonSB.AppendLine("                        Console.WriteLine(\"Saindo do sistema\");");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                    else");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        Console.Clear();");
            jsonSB.AppendLine("                        Console.WriteLine(\"Opção inválida. Informe outra opção\");");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                }");
            jsonSB.AppendLine("            } while (!sair);");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("        }");
            jsonSB.AppendLine("");


            for(int i = 0; i < menuJson.Menu.Length; i++)
            {
                jsonSB.AppendLine($"        public static void {menuJson.Menu[i]}()");
                jsonSB.AppendLine("        {\n            throw new NotImplementedException();\n        }");
                jsonSB.AppendLine("");
            }  

            jsonSB.AppendLine("    }");
            jsonSB.AppendLine("}");

            Console.WriteLine(jsonSB.ToString());
        }
    }
}