﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public interface IMenu
    {
        List<string> Options { get; }

        void Run();
        void HandleInputs();
        void CloseMenu();
    }
}
