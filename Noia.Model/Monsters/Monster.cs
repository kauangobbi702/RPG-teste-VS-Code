namespace Noia.MonsterNS
{

    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set;}
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public List<Attack> attacks { get; set; }
        public List<Spell> spells { get; set; }
        public int ExperienceReward { get; set; }
        public string CombatMessage { get; set; }
    }
}