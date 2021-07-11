using System;
using System.IO;
using System.Threading.Tasks;

namespace ThFnsc.NFe.Infra.Services.Chrome
{
    public static class ChromeFinder
    {
        private static string[] _possiblePaths = new string[]
        {
            "/usr/bin/google-chrome",
            @"C:\Program Files\Google\Chrome\Application\chrome.exe"
        };

        private static readonly Lazy<string> _finder = new(() =>
        {
            foreach (var possiblePath in _possiblePaths)
                if (File.Exists(possiblePath))
                    return possiblePath;
            throw new FileNotFoundException("No chrome executable found in predefined paths");
        });

        public static Task<string> FindChromeAsync() =>
            Task.FromResult(_finder.Value);
    }
}
