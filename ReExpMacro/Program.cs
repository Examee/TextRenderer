// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Hello, World!");




string input = "#macro$a[0][1]$b[2]";
string pattern = @"#(?<macro>\w+)(?:\$(?<param>\w+)(?:\[(?<index>\d+)\])*)*";

Match match = Regex.Match(input, pattern);

if (match.Success) {
    // Access the multiple captures
    var captures1 = match.Groups["param"].Captures;
    var captures2 = match.Groups["index"].Captures;
    for (int i = 0; i < captures1.Count; i++) {
        Console.WriteLine($"Capture #{i + 1}: {captures1[i].Value}");
        for (int j = 0; j < captures2.Count; j++) {
            Console.WriteLine($"Capture #{j + 1}: {captures2[j].Value}");
        }
    }

    
   
}

