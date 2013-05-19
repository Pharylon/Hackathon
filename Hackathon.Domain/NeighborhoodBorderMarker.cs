using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Domain
{
    public class NeighborhoodBorderMarker
    {
        public virtual int Id{ get ; set; }
        public virtual int NeighborhoodId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual float Latitude { get; set; }
        public virtual float Longitude { get; set; }
    }
}
