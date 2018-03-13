using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.EndRequest.Controllers
{
    [Produces("application/json")]
    [Route("api/PDF")]
    public class PDFController : Controller
    {
        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }    

}