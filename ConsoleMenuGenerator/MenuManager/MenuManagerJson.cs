using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ConsoleMenuGenerator.MenuManager
{
    internal class MenuJson
    {
        public string? Title { get; set; }
        public string[]? Menu { get; set; }        
    }

    public class MenuManagerJson : IMenuManager
    {
        private StringBuilder _listMenu = new StringBuilder();
        private StringBuilder _listMethods = new StringBuilder();
        private StringBuilder _listSwitch = new StringBuilder();
        private int _size = 0;
        private string _name = "";        

        public void GenerateModel(string path)
        {
            var jsonModel = new MenuJson
            {
                Title = "Nome do sistema",
                Menu = new string[] {"Opcao do menu 1", "Opcao do menu 2"}
            };

            string jsonStr = JsonSerializer.Serialize(jsonModel);

            using (StreamWriter sw = File.CreateText(Path.Combine(path, "model.json")))
            {
                sw.WriteLine(jsonStr);
            }
            Console.WriteLine("Arquivo criado com sucesso");
        }

        public void GenerateMenu(string path)
        {
            ChargerInformations(path);

            var jsonSB = new StringBuilder();
            var optionsSB = new StringBuilder();

            jsonSB.AppendLine("using System;\n");
            jsonSB.AppendLine("namespace ConsoleMenu");
            jsonSB.AppendLine("{");
            jsonSB.AppendLine("    class Program");
            jsonSB.AppendLine("    {");
            jsonSB.AppendLine("        public static void Main(string[] args)");
            jsonSB.AppendLine("        {");
            jsonSB.AppendLine("            Console.Clear();");
            jsonSB.AppendLine("            var sair = false;");
            jsonSB.AppendLine("            var opcao = \"\";");
            jsonSB.AppendLine("            do");
            jsonSB.AppendLine("            {");
            jsonSB.AppendLine("                Console.ForegroundColor = ConsoleColor.DarkBlue;");

            var left = (_size - _name.Length)/2+3;
            var right = (_size - _name.Length)/2+2;
            if (right % 2 == 0)  right++;
            
            jsonSB.AppendLine($"                Console.WriteLine(\"+{new string('-',left)} {_name} {new string('-',right)}+\");");
            jsonSB.Append(_listMenu.ToString());
            jsonSB.AppendLine($"                Console.WriteLine(\"+-------{new string('-',_size)}+\");");
            jsonSB.AppendLine($"                Console.WriteLine(\"| S - Sair{new string(' ',_size-2)}|\");");
            jsonSB.AppendLine($"                Console.WriteLine(\"+-------{new string('-',_size)}+\");");
            jsonSB.AppendLine("                Console.ResetColor();");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("                Console.Write(\"Informe um valor: \");");
            jsonSB.AppendLine("                opcao = Console.ReadLine();");
            jsonSB.AppendLine("                var number = 0;");
            jsonSB.AppendLine("                var isNumber = int.TryParse(opcao, out number);");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("                if (isNumber)");
            jsonSB.AppendLine("                {");
            jsonSB.AppendLine("                    try");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        switch(number)");
            jsonSB.AppendLine("                        {");
            jsonSB.Append(_listSwitch.ToString());
            jsonSB.AppendLine("                            default:");
            jsonSB.AppendLine("                                Console.WriteLine();");
            jsonSB.AppendLine("                                Console.WriteLine(\"Op????o inv??lida\");");
            jsonSB.AppendLine("                                break;");
            jsonSB.AppendLine("                        }");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                    catch (Exception ex)");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        Console.Clear();");
            jsonSB.AppendLine("                        Console.WriteLine($\"Um erro foi gerado durante a execu????o: {ex.Message}\");");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                }");
            jsonSB.AppendLine("                else");
            jsonSB.AppendLine("                {");
            jsonSB.AppendLine("                    if (!string.IsNullOrEmpty(opcao) && opcao.Equals(\"S\"))");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        sair = true;");
            jsonSB.AppendLine("                        Console.WriteLine(\"Saindo do sistema\");");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                    else");
            jsonSB.AppendLine("                    {");
            jsonSB.AppendLine("                        Console.Clear();");
            jsonSB.AppendLine("                        Console.WriteLine(\"Op????o inv??lida. Informe outra op????o\");");
            jsonSB.AppendLine("                    }");
            jsonSB.AppendLine("                }");
            jsonSB.AppendLine("            } while (!sair);");
            jsonSB.AppendLine("");
            jsonSB.AppendLine("        }");
            jsonSB.AppendLine("");
            jsonSB.Append(_listMethods.ToString());
            jsonSB.AppendLine("    }");
            jsonSB.AppendLine("}");

            //Console.WriteLine(jsonSB.ToString());
            //Console.ReadLine();
            try
            {
                using (StreamWriter sw = File.CreateText("Program.cs"))
                {
                    sw.WriteLine(jsonSB.ToString());
                }
                Console.WriteLine("Arquivo criado com sucesso");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Erro ao criar o arquivo: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Pressione uma tecla para sair...");
                Console.ReadKey();
            }
        }

        private void ChargerInformations(string pathOrigin)
        {
            var fileJson = File.ReadAllText(pathOrigin);
            var menuJson = JsonSerializer.Deserialize<MenuJson>(fileJson)!;

            if (menuJson == null) throw new NullReferenceException();
            if (menuJson.Menu == null) throw new NullReferenceException(nameof(menuJson.Menu));
            if (string.IsNullOrEmpty(menuJson.Title)) throw new NullReferenceException(nameof(menuJson.Title));            
            if (menuJson.Menu.Length == 0) throw new NullReferenceException(nameof(menuJson.Menu.Length));

            _name = menuJson.Title.Substring(0, Math.Min(menuJson.Title.Length, 20));
            _size = menuJson.Menu.Select(x => x.Length > 30 ? x.Length : 30).OrderBy(y => y).Last();

            for(int i = 0; i < menuJson.Menu.Length; i++)
            {
                _listMenu.AppendLine($"                Console.WriteLine(\"| {i+1} - {menuJson.Menu[i]}  {new string(' ', _size - menuJson.Menu[i].Length)}|\");");

                _listMethods.AppendLine($"        static void {FormatNameMethod(menuJson.Menu[i])}()");
                _listMethods.AppendLine("        {\n            throw new NotImplementedException();\n        }");
                _listMethods.AppendLine("");
                
                _listSwitch.AppendLine($"                            case {i+1} :");
                _listSwitch.AppendLine($"                                {FormatNameMethod(menuJson.Menu[i])}();");
                _listSwitch.AppendLine("                                break;");
            }            
        }

        private string FormatNameMethod(string org)
        {
            var listOrg = org.Split(" ");
            var newName = "";

            foreach (var item in listOrg)
            {
                var itemRegex = Regex.Replace(item, "[^0-9a-zA-Z]+", "");
                if (!" de da do das dos a e i o u ".Contains(itemRegex) & itemRegex.Length > 0)
                {
                    newName += char.ToUpper(itemRegex[0]).ToString() + itemRegex.Substring(1);
                }
            }

            return newName;
        }
    }
}