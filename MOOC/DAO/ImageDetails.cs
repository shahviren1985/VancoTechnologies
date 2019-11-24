using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    [Serializable]
    public class ImageDetails
    {
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ReNamedName { get; set; }
        public string Id { get; set; }
        public int ImageSize { get; set; }
    }
}
