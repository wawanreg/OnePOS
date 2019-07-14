using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace OnePOS.Models
{
    public class UploadModel
    {
        public bool IsSuccess { get; set; }
        public int SuccessCtr { get; set; }
        public int FailedCtr { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
    }

}