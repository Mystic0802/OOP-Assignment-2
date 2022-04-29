using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal interface IMenu
    {
        List<string> Choices { get; }

        IMenu Run();
        int GetChoiceInputs();
        bool HandleChoiceInputs(int inputCode);
        void CloseMenu();
    }
}
