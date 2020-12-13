using Konveyor.Core.Models;
using System.Linq;

namespace Konveyor.Core.ViewModels
{
    public class OfficeListViewModel
    {

        public OfficeListViewModel(IQueryable<Offices> officeList)
        {
            ActiveOffices = officeList;
        }

        public IQueryable<Offices> ActiveOffices { get; set; }
    }
}
