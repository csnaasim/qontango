using System;
using System.Collections.Generic;
using System.Text;

namespace RoleUserApi.Models
{
    public class PictureModel
    {
        public string ImageSource { get; set; }
        public DateTime Date { get; set; }
        //public GpsLocation GpsLocation { get; set; }
        public bool SealPicture { get; set; }
    }
}
