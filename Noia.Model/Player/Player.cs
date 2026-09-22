namespace Noia.PlayerNS
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public List<Attack> attacks { get; set; }
        public List<Spell> spells { get; set; }

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            Health = 100;
            Mana = 50;
            Armor = 5;
            MagicResistance = 5;
            attacks = new List<Attack>();
            spells = new List<Spell>();

            Attack basicAttack = new()
            {
                Name = "Basic Attack",
                Damage = 10,
                BaseCooldown = 0,
                CurrentCooldown = 0
            };
            attacks.Add(basicAttack);
        }

        public void LevelUp()
        {
            Level++;
            Experience = 0;
            Health += 10;
            Mana += 5;
            Armor += 2;
            MagicResistance += 2;
        }

        public void GainExperienceCombat()
        {
            
        }

        

        
    }
}