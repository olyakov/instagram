using Instagram.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Instagram.Services.Interfaces
{
    public interface IReport
    {
        void AddReport(Report report);
        IEnumerable<Report> GetUserReport(string username);

    }
}
