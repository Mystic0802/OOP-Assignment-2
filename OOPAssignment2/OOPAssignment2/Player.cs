namespace OOPAssignment2
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsComputer { get; set; }

        public Player(string name, bool isComp = false)
        {
            Name = name;
            Score = 0;
            IsComputer = isComp;
        }
    }
}
