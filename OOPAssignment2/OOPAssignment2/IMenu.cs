using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    internal interface IMenu
    {
        void Run();
        void ShowMenu();

        int GetChoiceInputs();

        bool HandleChoiceInputs(int inputCode);

        void CloseMenu();
    }
}
