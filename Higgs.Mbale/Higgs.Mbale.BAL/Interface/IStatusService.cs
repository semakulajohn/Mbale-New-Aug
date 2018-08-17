using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Higgs.Mbale.Models;


namespace Higgs.Mbale.BAL.Interface
{
    public interface IStatusService
    {
        IEnumerable<Status> GetAllStatuses();
    }
}
