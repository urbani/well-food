﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IInterractable
    {
        void showMsg(String msg, GlobalObj.ErrorLevels el);
        void showMsg(String msg, String header);
    }
}
