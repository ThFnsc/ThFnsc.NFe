using CliWrap;
using CliWrap.Buffered;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ThFnsc.NFe.Core.Services;

namespace ThFnsc.NFe.Infra.Services
{
    public class HtmlToPDF : IHtmlToPDF
    {
        private readonly ILogger<HtmlToPDF> _logger;

        public HtmlToPDF(ILogger<HtmlToPDF> logger)
        {
            _logger = logger;
        }

        public async Task<byte[]> ConvertHTMLToPDF(string html)
        {
            var htmlFilePath = new FileInfo(Path.GetTempFileName());
            var pdfFilePath = new FileInfo(Path.GetTempFileName());
            htmlFilePath.MoveTo(htmlFilePath.FullName + ".html");
            try
            {
                await File.WriteAllTextAsync(htmlFilePath.FullName, html, Encoding.UTF8);

                var res = await Cli.Wrap("node")
                    .WithArguments(new[] { "node/index", htmlFilePath.FullName, pdfFilePath.FullName })
                    .WithValidation(CommandResultValidation.None)
                    .WithStandardOutputPipe(PipeTarget.ToDelegate(s => _logger.LogInformation(s)))
                    .WithStandardErrorPipe(PipeTarget.ToDelegate(s => _logger.LogError(s)))
                    .ExecuteBufferedAsync();

                if (res.ExitCode != 0)
                    throw new Exception($"Exit code {res.ExitCode}:\nStandard Output: {res.StandardOutput}\nStandard Error: {res.StandardError}");

                var result = await File.ReadAllBytesAsync(pdfFilePath.FullName);

                if (result.Length == 0)
                    throw new Exception($"Zero-length result: {res.ExitCode}:\nStandard Output: {res.StandardOutput}\nStandard Error: {res.StandardError}");

                return result;
            }
            finally
            {
                htmlFilePath.Delete();
                pdfFilePath.Delete();
            }
        }
    }
}
