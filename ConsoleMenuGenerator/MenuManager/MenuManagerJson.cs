using System.Text;
using System.Text.Json;

namespace ConsoleMenuGenerator.MenuManager
{
    internal class MenuJson
    {
        public string? Title { get; set; }
        public string[]? Menu { get; set; }        
    }

    private StringBuilder() _listMenu = new StringBuilder();
    private StringBuilder() _listMethods = new StringBuilder();
    private StringBuilder() _listSwitch = new StringBuilder();

    public class MenuManagerJson : IMenuManager
    {
        public void GenerateMenu(string pathOrigin, string pathDestiny)
        {
            ChargerInformations(pathOrigin);

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
            jsonSB.AppendLine(_listMenu.ToString);
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
            jsonSB.AppendLine(_listSwitch.ToString);
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
            jsonSB.AppendLine(_listMethods.ToString);
            jsonSB.AppendLine("    }");
            jsonSB.AppendLine("}");

            Console.WriteLine(jsonSB.ToString());
        }

        private void ChargerInformations(string pathOrigin)
        {
            var fileJson = File.ReadAllText(pathOrigin);
            var menuJson = JsonSerializer.Deserialize<MenuJson>(fileJson)!;

            if (menuJson == null) throw new NullReferenceException();

            for(int i = 0; i < menuJson.Menu.Length; i++)
            {
                _listMenu.Add($"                Console.WriteLine(\"| {i+1} - {menuJson.Menu[i]}        |\");");

                _listMethods.Add($"        public static void {FormatNameMethod(menuJson.Menu[i])}()");
                _listMethods.Add("        {\n            throw new NotImplementedException();\n        }");
                _listMethods.Add("");
                
                _listSwitch.Add($"                        case {i+1} :");
                _listSwitch.Add($"                            Console.WriteLine(\"{menuJson.Menu[i]}\");");
                _listSwitch.Add("                            break;");
            }            
        }

        private string FormatNameMethod(string org)
        {
            var listOrg = org.Split(" ");
            var newName = "";

            foreach (var item in listOrg)
            {
                if (!" de da do das dos".Contains(item) & item.Length > 1)
                {
                    newName = item[0].ToUpper() + item.Substring(1);
                }
            }

            return newName;
        }
    }
}