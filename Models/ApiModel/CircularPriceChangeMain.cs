using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ApiModel
{
    public class CircularPriceChangeMain
    {
        public int CircularId { get; set; }
        public string CircularName { get; set; }
        public string EffectiveDate { get; set; }
        public string UpdateBy { get; set; }
        public IEnumerable<CircularPriceChangeSub> CircularPriceChangeSubList { get; set; }

    }
}
