using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Reumed.DataAccess.Database
{
    internal interface IDatabaseAccess
    {
        IDbConnection GetConfigurationConnection();
    }
}
