namespace RPS.Api.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }

        public int Score { get; set; } = 0;

        public Weapon Selection { get; set; } = Weapon.None;

        public Weapon PreviousSelection { get; set; } = Weapon.None;
    }
}
