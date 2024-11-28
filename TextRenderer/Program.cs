// See https://aka.ms/new-console-template for more information

using System.Text;
using System.Text.RegularExpressions;
using Randomize;




List<string> list = new List<string>();
RandomContext context = new RandomContext();
SerialPeaker serialPeaker = new SerialPeaker();
MacroParser parser = new MacroParser();
parser.RegisterMacro("s", () =>  serialPeaker.NextSerial().ToString());

list.Add("Find the result of the following addition : ");
list.Add("\n");
list.Add(parser.RenderString("#s)"));
list.Add(context.Next("A").ToString());
list.Add(" + ");
list.Add(context.Next("B").ToString());
list.Add("\n");
list.Add("Solution : ");
list.Add((context.RecallValue("A") + context.RecallValue("B")).ToString());

foreach (string s in list) {
    Console.Write(s);
}

public class MacroParser {
    Dictionary<string, Func<string>> m_macros = new Dictionary<string, Func<string>>();

    private static readonly Regex MacroRegex = new Regex(@"#(\w+)");
    
    public void RegisterMacro(string name, Func<string> action) {
        m_macros[name] = action;
    }

    public string RenderString(string input) {
        // Read the string and replace the macros with the appropriate values
        // Use the m_macros dictionary to look up the action for each macro

        StringBuilder result = new StringBuilder();
        int position = 0;

        while (position < input.Length) {
            string remainingText = input.Substring(position);

            Match match;
            // Check for macro
            if ((match = MacroRegex.Match(remainingText)).Success) {
                result.Append(m_macros[match.Groups[1].Value]());
                position += match.Length;
            }
            // Plain text
            else {
                result.Append(remainingText[0]);
                position++;
            }
        }

        return result.ToString();
    }
}

public class SerialPeaker {
    int m_serial = 0;
    public int NextSerial() {
        return m_serial++;
    }
    public int CurrentSerial() {
        return m_serial;
    }
}
