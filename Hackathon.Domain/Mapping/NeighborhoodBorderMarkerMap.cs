using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Hackathon.Domain.Mapping
{
    class NeighborhoodBorderMarkerMap : ClassMap<NeighborhoodBorderMarker>
    {
        public NeighborhoodBorderMarkerMap()
        {
            Table("dbo.NeighborhoodBorderMarker");
            Id(x => x.Id);
            Map(x => x.NeighborhoodId);
            Map(x => x.OrderId);
            Map(x => x.Latitude);
            Map(x => x.Longitude);
        }
    }
}
