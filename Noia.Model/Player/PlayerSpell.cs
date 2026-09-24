using Noia.PlayerNS;

public class PlayerSpell
{
    public int IdPlayer { get; set; }
    public Player Player { get; set; }
    public int IdSpell { get; set; }
    public Spell Spell { get; set; }

    public int CurrentCooldown { get; set; }
}