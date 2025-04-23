using Microsoft.AspNetCore.Routing.Template;

namespace MamyCare.Helpers
{
    public static class EmailBodyBuilder
    {
        public static string GenerateBodyBuilder(string template ,Dictionary<string , string> templatemodel)
        {
            //used to replace the placeholders in the email template in html
            var templatePath = $"{Directory.GetCurrentDirectory()}/Templates/{template}.html";
            var streamreader = new StreamReader(templatePath);
            var body = streamreader.ReadToEnd();
            streamreader.Close();
            foreach (var item in templatemodel)
            {
                body = body.Replace(item.Key , item.Value);
            }
            return body;
        }
    }
}
