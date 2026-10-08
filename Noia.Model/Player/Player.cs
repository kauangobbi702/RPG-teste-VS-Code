using Noia.Skills;
using Noia.MonsterNS;

namespace Noia.PlayerNS
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int ManaRegen { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public List<Attack> PlayerAttacks { get; set; } = new();
        public List<Spell> PlayerSpells { get; set; } = new();

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Experience = 0;
            ExperienceToNextLevel = 100;
            Health = 100;
            Mana = 50;
            ManaRegen = 5;
            Armor = 5;
            MagicResistance = 10;

            Attack basicAttack = new Attack("Basic Attack", 10, 0);
            PlayerAttacks.Add(basicAttack);
        }

        public void LevelUp()
        {
            Level++;
            Experience = 0;            
            ExperienceToNextLevel += 100;
            Health += 10;
            Mana += 5;
            Armor += 2;
            MagicResistance += 5;

            Console.WriteLine($"\nYou have reached level {Level}!");
        }

        public void GainExperienceCombat(Monster monster)
        {
            Experience += monster.ExperienceReward;
            Console.WriteLine($"\nYou gained {monster.ExperienceReward} experience points!");
            if (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        

        
    }
}