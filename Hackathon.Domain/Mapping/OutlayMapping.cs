using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Domain.Mapping
{
    class OutlayMapping : ClassMap<Outlay>
    {
        public OutlayMapping()
        {
            Table("dbo.Outlay");
            Id(x => x.Id);
            Map(x => x.NeighborhoodId);
            Map(x => x.OutlayAll);
            Map(x => x.OutlayBakery);
            Map(x => x.OutlayWine);
            Map(x => x.OutlayBeer);
            Map(x => x.OutlayDeli);
        }
    }
}
