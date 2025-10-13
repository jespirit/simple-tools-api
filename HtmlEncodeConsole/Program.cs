// See https://aka.ms/new-console-template for more information
using System.Web;

// Note: This changes the output encoding of the console and current session.
//Console.OutputEncoding = System.Text.Encoding.UTF8;

var inputStrings = new List<string>();
//inputStrings.Add("<script>alert('XSS');</script>");
//inputStrings.Add("<div>Some content & more content</div>");
//inputStrings.Add("Normal text with <b>bold</b> and <i>italic</i> tags.");
//inputStrings.Add("Text with special characters: <, >, &, \", '.");
//inputStrings.Add("<p>Avec le moteur ProVisual, vos photos et vidéos prennent vie ! " +
//    "Couleurs plus justes, détails précis et contraste optimisé, pour des " +
//    "souvenirs éclatants à chaque instant.6</p>");
inputStrings.Add("<p>Unicode \ud800\udfa0");
inputStrings.Add("<p>Unicode \u044f");
inputStrings.Add("<p>Unicode \ud800\uffff");
inputStrings.Add("<p>Unicode \ud800\uaa00 Legal");

for (int i = 0; i < inputStrings.Count; i++)
{
    var input = inputStrings[i];
    var encoded = HttpUtility.HtmlEncode(input);
    Console.WriteLine($"Input {i + 1}: {input}");
    Console.WriteLine($"Encoded {i + 1}: {encoded}");
    Console.WriteLine();
}

#if TEST
using (TextWriter writer = new StreamWriter(
    new FileStream("html_encode_output.txt", FileMode.Create, FileAccess.Write), System.Text.Encoding.Unicode)
)
{
    for (int i = 0; i < inputStrings.Count; i++)
    {
        var input = inputStrings[i];
        var encoded = HttpUtility.HtmlEncode(input);
        writer.WriteLine($"Input {i + 1}: {input}");
        writer.WriteLine($"Encoded {i + 1}: {encoded}");
        writer.WriteLine();
    }
}
#endif