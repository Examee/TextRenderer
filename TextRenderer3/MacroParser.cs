﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TextRenderer3 {
    // This class is used to parse a string and replace macros with
    // the appropriate values. The macros are defined by the user
    // and are stored in a dictionary. The dictionary maps the macro
    // name to a function that returns the value of the macro
    public class CMacroParser {
        // This dictionary is used to store the macros and their
        // corresponding actions. The key is the macro name, and the
        // value is a function that takes an array of strings as input
        // and returns a string.
        Dictionary<string, Func<string[], string>> m_textMacros = new Dictionary<string, Func<string[], string>>();

        // This regular expression is used to match a macro in the input string
        // The macro is defined by a '#' followed by the macro name, and an optional
        // list of parameters separated by '$'
        // For example, the string '#s$param1$param2' will match the macro name 's',
        // and the parameters will be ['param1', 'param2']
        private static readonly Regex MacroRegex = new Regex(@"^#(\w+)(?:\$(\w+))*");


        public CMacroParser() { }

        // This method is used to register a macro with the parser.
        public void RegisterMacro(string name, Func<string[], string> action) {
            m_textMacros[name] = action;
        }

        public string RenderString(string input) {
            // Read the string and replace the macros with the appropriate values
            // Use the m_macros dictionary to look up the action for each macro

            // The result string will be built up as we process the input string
            StringBuilder result = new StringBuilder();

            // The position variable is used to keep track of the current position
            // in the input string
            int position = 0;

            while (position < input.Length) {
                string remainingText = input.Substring(position);

                Match match;
                // Check for macro
                if ((match = MacroRegex.Match(remainingText)).Success) {
                    string macroName = match.Groups[1].Value;

                    /* Explanation of the following line of code:
                     * Get the parameters for the macro (if any) and call the action
                     * If the input string is "#s$param1$param2", and the regular expression
                     * matches this string, the Groups collection might look like this:
                       •	Groups[0]: "#s$param1$param2" (entire match)
                       •	Groups[1]: "s" (macro name)
                       •	Groups[2]: "param1" (first parameter)
                       •	Groups[3]: "param2" (second parameter)
                       After applying the line of code:
                       •	parameters will be an array containing ["param1", "param2"].
                       This array is then used to call the macro's action function, which 
                       generates the replacement value for the macro in the input string.
                    Cast converts the CaptureCollection to an IEnumerable<Capture> to 
                    enable LINQ expressions and Select projects each Capture to its Value
                    property.
                     */
                    string[] parameters = match.Groups[2].Captures.
                        Cast<Capture>().Select(c => c.Value).ToArray();

                    // Call the action function for the macro and append the result to the output
                    result.Append(m_textMacros[macroName](parameters));

                    // Move the position to the end of the matched macro
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
}
