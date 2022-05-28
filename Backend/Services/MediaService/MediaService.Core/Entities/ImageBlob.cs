using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaService.Core.Entities
{
    public class ImageBlob : BaseBlob
    {
        public List<ImageUrl> ImageUrls { get; set; }
    }
}
