using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoderIvi.Message
{
    public class PictogramCode
    {
        public object countryCode { get; set; }
        public ServiceCategoryCode? serviceCategoryCode { get; set; }
        public PictogramCategoryCode? pictogramCategoryCode { get; set; }
    }
}
