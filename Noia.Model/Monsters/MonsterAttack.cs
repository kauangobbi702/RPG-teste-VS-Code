using Noia.MonsterNS;
using Noia.Skills;

public class MonsterAttack
{
    public int IdMonster { get; set; }
    public Monster Monster { get; set; }
    public int IdAttack { get; set; }
    public Attack Attack { get; set; }

    public int CurrentCooldown { get; set; }
}