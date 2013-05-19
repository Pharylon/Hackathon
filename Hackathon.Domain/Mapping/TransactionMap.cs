using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Domain.Mapping
{
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap(){
            Table("dbo.Transactions");
            Id(x => x.Id);
            Map(x => x.sales);
            Map(x => x.description);
            Map(x => x.Household);
            Map(x => x.dateTime);
        }

    }
}
