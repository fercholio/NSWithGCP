using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using WebAPI.EndRequest.Services;

namespace WebAPI.EndRequest.Controllers
{
    [Produces("application/json")]
    [Route("api/Converter")]
    public class ConvertController : Controller
    {
        private IPathProvider pathProvider;

        public ConvertController(IPathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            string[][] movies = {
                      new[] {
                        "Cp1252",
                        "A Very long Engagement (France)",
                        "directed by Jean-Pierre Jeunet",
                        "Un long dimanche de fian\u00e7ailles"
                      },
                      new[] {
                        "Cp1250",
                        "No Man's Land (Bosnia-Herzegovina)",
                        "Directed by Danis Tanovic",
                        "Nikogar\u0161nja zemlja"
                      },
                      new[] {
                        "Cp1251",
                        "You I Love (Russia)",
                        "directed by Olga Stolpovskaja and Dmitry Troitsky",
                        "\u042f \u043b\u044e\u0431\u043b\u044e \u0442\u0435\u0431\u044f"
                      },
                      new[] {
                        "Cp1253",
                        "Brides (Greece)",
                        "directed by Pantelis Voulgaris",
                        "\u039d\u03cd\u03c6\u03b5\u03c2"
                      }
                    };


            var fileName = "result" + System.Guid.NewGuid() + ".pdf";
            var pdfFilePath = @".\\" + fileName;
            var stream = new FileStream(pdfFilePath, FileMode.Create);

            // step 1
            var document = new Document();

            // step 2
            PdfWriter.GetInstance(document, stream);
            // step 3
            document.AddAuthor("fcanul");
            document.Open();

            // step 4
            string font = pathProvider.MapPath("OpenSans-Bold.ttf");
            for (var i = 0; i < 4; i++)
            {
                var bf = BaseFont.CreateFont(font, movies[i][0], BaseFont.EMBEDDED);
                document.Add(new Paragraph(
                    $"Font: {bf.PostscriptFontName} with encoding: {bf.Encoding}"
                ));
                document.Add(new Paragraph(movies[i][1]));
                document.Add(new Paragraph(movies[i][2]));
                document.Add(new Paragraph(movies[i][3], new Font(bf, 12)));
                document.Add(Chunk.Newline);
            }

            document.Close();
            var uploadService = new SaveFile("pdfresults");
            var link = uploadService.UploadFile(stream, System.DateTime.Now.Ticks);
            stream.Dispose();            

            return Ok(link);
        }

        public interface IPathProvider
        {
            string MapPath(string path);
        }

        public class PathProvider : IPathProvider
        {
            private IHostingEnvironment _hostingEnvironment;

            public PathProvider(IHostingEnvironment environment)
            {
                _hostingEnvironment = environment;
            }

            public string MapPath(string path)
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, path);
                return filePath;
            }
        }        
    }
}