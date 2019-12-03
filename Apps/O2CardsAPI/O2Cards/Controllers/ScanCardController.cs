using System;
using System.Web.Http;

namespace O2CardsAppApi.Controllers
{
    public class ScanCardController : ApiController
    {
        public string Get()
        {
            //var Ocr = new AutoOcr();
            //var Result = Ocr.Read(@"D:\VancoTechnologies\GIT\VancoTechnologies\Apps\O2CardsAPI\O2Cards\Images\Card.png");
            //Console.WriteLine(Result.Text);
            return "Test";
        }
    }
}
