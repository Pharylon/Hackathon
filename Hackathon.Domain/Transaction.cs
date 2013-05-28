using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Domain
{
    public class Transaction
    {
        public virtual int Id { get; set; }
        public virtual decimal sales { get; set; }
        public virtual string description { get; set; }
        public virtual int Household { get; set; }
        public virtual DateTime dateTime { get; set; }
        public virtual int store { get; set; }
    }
}
