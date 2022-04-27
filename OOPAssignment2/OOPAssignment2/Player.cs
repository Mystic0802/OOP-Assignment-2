namespace OOPAssignment2
{
    public class Player
    {
        #region Fields & Properties

        private string name;
        public string Name { get => name; set => name = value; }

        private int score;
        public int Score { get => score; set => score = value; }


        #endregion

        public Player(string name)
        {
            Name = name;
            Score = 0;
        }



    }
}
