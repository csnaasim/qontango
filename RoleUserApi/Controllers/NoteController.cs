using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleUserApi.Model;
using RoleUserApi.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RoleUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {







        private IHostingEnvironment Environment;

        public NoteController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }



        [HttpPost("AddNote")]
        public IActionResult AddNote(Note  note)
        {

            string filePath = "";
            byte[] imageBytes = null;
            TimeSpan epoch = (note.Inserted_At - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            var totalmilisecnd = (double)epoch.TotalMilliseconds;
            string debtorpathname = note.Debtor.Debtor_ID+"_"+totalmilisecnd.ToString()+".pdf";

            //code to create and check timestamp
            //List<string> lst = new List<string>();
            //for(int i = 0; i<200; i++)
            //{

            //    TimeSpan epoch = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //    var tota = (double)epoch.TotalSeconds;
            //    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            //    dtDateTime = dtDateTime.AddSeconds(tota).ToLocalTime();
            //    lst.Add( i+" timestamp"+ tota.ToString() + "time " + dtDateTime);
            //}

            if (note.url !="")
            {
                var a = note.url.Substring(28);
                byte[] bytes = System.Convert.FromBase64String(a);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                 imageBytes = Convert.FromBase64String(base64String);

                string wwwPath = this.Environment.WebRootPath;
                string contentPath = this.Environment.ContentRootPath;
                 filePath = contentPath + "\\" + note.Debtor.Debtor_ID;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath = filePath + "\\" + debtorpathname;

            }
         
            //string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //path = path + @"\"+ debtorpathname;


            //System.IO.File.WriteAllBytes(path, imageBytes);

            //***Save Base64 Encoded string as Image File***//

            //Convert Base64 Encoded string to Byte Array.

            //var a = ntobj.url.Substring(28);
            //byte[] bytes = System.Convert.FromBase64String(a);
            //string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //byte[] imageBytes = Convert.FromBase64String(base64String);

            //Save the Byte Array as Image File.
     
          //  System.IO.File.WriteAllBytes(filePath, imageBytes);
            try
            {
                var res = Note.AddNote(note, imageBytes, debtorpathname , filePath);
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [HttpPost("UpdateNote")]
        public IActionResult UpdateNote(Note note)
        {

            //string filePath = "";
            //byte[] imageBytes = null;
            //TimeSpan epoch = (note.Inserted_At - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            //var totalmilisecnd = (double)epoch.TotalMilliseconds;
            //string debtorpathname = note.Debtor.Debtor_ID + "_" + totalmilisecnd.ToString() + ".pdf";

            //if (note.url != "")
            //{
            //    var a = note.url.Substring(28);
            //    byte[] bytes = System.Convert.FromBase64String(a);
            //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            //    imageBytes = Convert.FromBase64String(base64String);

            //    string wwwPath = this.Environment.WebRootPath;
            //    string contentPath = this.Environment.ContentRootPath;
            //    filePath = contentPath + "\\" + note.Debtor.Debtor_ID;
            //    if (!Directory.Exists(filePath))
            //    {
            //        Directory.CreateDirectory(filePath);
            //    }
            //    filePath = filePath + "\\" + debtorpathname;

            //}

            try
            {
                var res = Note.UpdateNote(note);
                ResponseModelNew response = new ResponseModelNew();
                if (res != null)
                {
                    response.StatusCode = "200";
                    response.Data = res;
                    return Ok(response);
                }
                else
                {
                    response.StatusCode = "404";
                    response.Data = "Bad Request";
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError, e.Message);
            }
        }







    }
}
