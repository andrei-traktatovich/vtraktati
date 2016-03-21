using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTraktate.Core.Interfaces.BusinessLogic.Orders;
using VTraktate.Domain;

namespace VTraktate.BL.Orders
{
    public class OrderNameMaker : IOrderNameMaker
    {
        const string OFFICE_CODE_AND_CUSTOMER_CODE_SEPARATOR = "_";
        const string LITERAL_AND_NUMBER_SEPARATOR = "-";
        const string NUMBER_AND_POSTFIX_SEPARATOR = "_";
        public string MakeLiteral(string customerCode, string officeCode, bool useOfficeCode)
        {
            if (string.IsNullOrWhiteSpace(customerCode))
                throw new ArgumentNullException("customerCode");

            if (useOfficeCode)
            {
                if (string.IsNullOrEmpty(officeCode))
                    throw new ArgumentNullException("officeCode");
                return string.Format("{0}{1}{2}", officeCode, OFFICE_CODE_AND_CUSTOMER_CODE_SEPARATOR, customerCode);
            }
            else
                return customerCode;
        }

        public string MakeNumber(string customerCode, string officeCode, bool useOfficeCode, int? number, string postFix = null)
        {
            if (postFix == null && number == null)
                throw new ArgumentNullException("Number and/or postfix should be specified");

            var literal = MakeLiteral(customerCode, officeCode, useOfficeCode);

            if (number.HasValue)
            {
                if (number.Value <= 0)
                    throw new ArgumentOutOfRangeException("number");
                literal = string.Format("{0}{1}{2}", literal, LITERAL_AND_NUMBER_SEPARATOR, number);
            }

            if (!string.IsNullOrWhiteSpace(postFix))
            {
                var separator = number.HasValue ? NUMBER_AND_POSTFIX_SEPARATOR : LITERAL_AND_NUMBER_SEPARATOR;
                literal = string.Format("{0}{1}{2}", literal, separator, postFix);
            }

            return literal;
        }

        public string MakeLiteral(Customer customer, Office office)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");
            if (office == null)
                throw new ArgumentNullException("office");

            var result = MakeLiteral(customer.Code, office.Code, customer.UseOfficeCode);
            return result;
        }


        public string MakeNumber(OrderNumberComponents orderNumberInfo)
        {
            return MakeNumber(orderNumberInfo.CustomerCode, orderNumberInfo.OfficeCode, orderNumberInfo.useOfficeCode, orderNumberInfo.Number, orderNumberInfo.PostFix);
        }
    }
}
