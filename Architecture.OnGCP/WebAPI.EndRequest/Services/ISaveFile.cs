using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.EndRequest.Services
{
    public interface ISaveFile
    {
        String UploadFile(/*HttpPostedFileBase image,*/ long id);
    }
}
