using Noia.Skills;

namespace Noia.MonsterNS
{
    public enum TauntEffectType
    {
        None,
        IncreaseAttackDamage,
        SkipTurn
    }

    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; set;}
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Armor { get; set; }
        public int MagicResistance { get; set; }
        public List<Attack> MonsterAttacks { get; set; } = new();
        public List<Spell> MonsterSpells { get; set; } = new();
        public int ExperienceReward { get; set; }
        public string CombatMessage { get; set; }
        public TauntEffectType TauntEffect { get; set; }
        public int MaxTauntStacks { get; set; }
        public int TauntStacks { get; private set; }
        public int AttackDamageBonusPerTauntStack { get; set; }

        public bool TryAddTauntStack()
        {
            if (TauntEffect == TauntEffectType.None || TauntStacks >= MaxTauntStacks)
            {
                return false;
            }

            TauntStacks++;
            return true;
        }

        public bool ConsumeSkipTurn()
        {
            if (TauntEffect != TauntEffectType.SkipTurn || TauntStacks == 0)
            {
                return false;
            }

            TauntStacks--;
            return true;
        }
    }
}