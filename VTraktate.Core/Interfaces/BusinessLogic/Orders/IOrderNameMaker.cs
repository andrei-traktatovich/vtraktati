using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Domain;

namespace VTraktate.Core.Interfaces.BusinessLogic.Orders
{
    public interface IOrderNameMaker
    {
        string MakeLiteral(string customerCode, string officeCode, bool useOfficeCode);
        string MakeLiteral(Customer customer, Office office);
        string MakeNumber(string customerCode, string officeCode, bool useOfficeCode, int? number, string postFix = null);


        string MakeNumber(OrderNumberComponents orderNumberInfo);
    }
}
