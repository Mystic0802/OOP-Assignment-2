using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPAssignment2
{
    public class SettingsMenu : MenuIO, IMenu
    {
        #region [ Settings ]

        public static readonly int nameCharLimit = 25;

        public static int DiceSideCount { get; private set; }
        public static int UnfairDiceCount { get; private set; }


        #endregion

        public List<string> Options => throw new NotImplementedException();

        public IMenu NextMenu { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CloseMenu()
        {
            throw new NotImplementedException();
        }

        public void HandleInputs()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
