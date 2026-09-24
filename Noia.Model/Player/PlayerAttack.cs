using Noia.PlayerNS;
using Noia.Skills;

public class PlayerAttack
{
    public int PlayerId { get; set; }
    public Player player { get; set; }
    public int AttackId { get; set; }
    public Attack attack { get; set; }

    public int CurrentCooldown { get; set; }
}