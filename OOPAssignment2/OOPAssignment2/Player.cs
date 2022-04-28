namespace OOPAssignment2
{
    internal class Player
    {
        #region Fields & Properties

        private string name;
        private int score;

        public string Name { get => name; set => name = value; }
        public int Score { get => score; set => score = value; }

        private bool isComputer;
        public bool IsComputer { get => isComputer; set => isComputer = value; }



        #endregion

        public Player(string name, bool isComputer = false)
        {
            Name = name;
            Score = 0;
            IsComputer = isComputer;
        }



    }
}
