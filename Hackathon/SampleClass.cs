using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon
{
    public class SampleClass
    {
        HTDataSet myDataSet;
        HTDataSetTableAdapters.customer_dataTableAdapter myCustomerAdapter;

        void MessWithData()
        {
            myDataSet = new HTDataSet();
            myCustomerAdapter = new HTDataSetTableAdapters.customer_dataTableAdapter();
            myCustomerAdapter.Fill(myDataSet.customer_data);

            IEnumerable<HTDataSet.customer_dataRow> myEnumerableRows =
                from row in myDataSet.customer_data
                where row.net_sales > 100        
                select row;


            //If you have Head First C#, this syntax is thorougly explained on page 699.
            //It is returning an enumerable squence of enumerbale sequences (so the first
            //sequence is the lowest number segID with sales over 100. The next group is
            //the next lowest SegID with sales over 100, etc.
            var gropuedRows =
                from row in myDataSet.customer_data
                where row.net_sales > 100
                group row by row.seg_id
                    into segIdGroup
                    orderby segIdGroup.Key
                    select segIdGroup;

        }
    }
}
