using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        if(args.Length < 2)
        {
            Console.WriteLine("Uso: TrxToHtml <arquivo.trx> <saida.html>");
            return;
        }

        string trxFile = args[0];
        string htmlFile = args[1];

        if (!File.Exists(trxFile))
        {
            Console.WriteLine("Arquivo TRX não encontrado.");
            return;
        }

        var doc = XDocument.Load(trxFile);
        XNamespace ns = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

        var results = doc.Descendants(ns + "UnitTestResult")
                         .Select(r => new {
                             TestName = r.Attribute("testName")?.Value,
                             Outcome = r.Attribute("outcome")?.Value,
                             ErrorMessage = r.Element(ns + "Output")?.Element(ns + "ErrorInfo")?.Element(ns + "Message")?.Value ?? "",
                             StackTrace = r.Element(ns + "Output")?.Element(ns + "ErrorInfo")?.Element(ns + "StackTrace")?.Value ?? ""
                         });

        var sb = new StringBuilder();

        sb.AppendLine("<html><head><meta charset='utf-8'><style>");
        sb.AppendLine("body { font-family: sans-serif; } table { border-collapse: collapse; width: 100%; }");
        sb.AppendLine("th, td { border: 1px solid #ccc; padding: 8px; }");
        sb.AppendLine("tr.pass { background-color: #e6ffe6; } tr.fail { background-color: #ffe6e6; }");
        sb.AppendLine("</style></head><body>");
        sb.AppendLine("<h1>Relatório de Testes (.trx)</h1>");
        sb.AppendLine("<table><thead><tr><th>Teste</th><th>Status</th><th>Mensagem</th><th>StackTrace</th></tr></thead><tbody>");

        foreach(var r in results)
        {
            var cssClass = r.Outcome == "Passed" ? "pass" : "fail";
            sb.AppendLine($"<tr class='{cssClass}'>");
            sb.AppendLine($"<td>{r.TestName}</td>");
            sb.AppendLine($"<td>{r.Outcome}</td>");
            sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(r.ErrorMessage)}</td>");
            sb.AppendLine($"<td>{System.Net.WebUtility.HtmlEncode(r.StackTrace)}</td>");
            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</tbody></table></body></html>");

        File.WriteAllText(htmlFile, sb.ToString());

        Console.WriteLine($"Relatório HTML gerado em {htmlFile}");
    }
}
