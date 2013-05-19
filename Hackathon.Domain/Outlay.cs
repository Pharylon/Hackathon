using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Domain
{
    class Outlay
    {
        public virtual int Id { get; set; }
        public virtual int NeighborhoodId { get; set; }
        public virtual decimal OutlayAll { get; set; }
        public virtual decimal OutlayBakery { get; set; }
        public virtual decimal OutlayWine { get; set; }
        public virtual decimal OutlayBeer { get; set; }
        public virtual decimal OutlayDeli { get; set; }
    }
}
