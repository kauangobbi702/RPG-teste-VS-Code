using Noia.MonsterNS;

public class MonsterSpell
{
    public int IdMonster { get; set; }
    public Monster Monster { get; set; }
    public int IdSpell { get; set; }
    public Spell Spell { get; set; }

    public int CurrentCooldown { get; set; }
}