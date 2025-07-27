using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DbInit
{
    public interface IDbInit
    {
        Task InitializeDatabaseAsync();
    }
}
