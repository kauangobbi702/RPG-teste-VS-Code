using Noia.Skills;

namespace Noia.PlayerNS
{
    public class Player
    {
        public int IdPlayer { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public List<PlayerAttack> PlayerAttacks { get; set; } = new();
        public List<PlayerSpell> PlayerSpells { get; set; } = new();

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            Health = 100;
            Mana = 50;
            Armor = 5;
            MagicResistance = 5;
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