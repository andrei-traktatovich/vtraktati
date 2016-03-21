using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VTraktate.DAL
{
    public interface IIdNamePair
    {
        int Id { get; set; }
        string Name { get; set; }
    }
    public class IdTitlePair  
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public static Expression<Func<IIdNamePair, IdTitlePair>> Create = x => new IdTitlePair { Id = x.Id, Title = x.Name };
    }
    public partial class AspNetRole : IIdNamePair
    {

    }
   

    public partial class Office : IIdNamePair
    {

    }

    public partial class EmployeeTitle : IIdNamePair
    {

    }

    public partial class EmploymentStatus : IIdNamePair
    {

    }

     public partial class FreelanceStatus : IIdNamePair
    {

    } 
}
