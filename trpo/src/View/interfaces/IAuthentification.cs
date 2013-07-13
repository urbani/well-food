using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trpo.View
{
    interface IAuthentification:IShareInterface
    {

        String getLogin();
        String getPassword();
        void clearAuthData();
    }
}
