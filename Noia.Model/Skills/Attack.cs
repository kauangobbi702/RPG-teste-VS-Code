namespace Noia.Skills
{

    public class Attack
    {
        public int IdAttack { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int BaseCooldown { get; set; }
        //public int CurrentCooldown { get; set; }

        public List<PlayerAttack> PlayerAttacks { get; set; } = new();
        public Attack()
        {}
    }
}