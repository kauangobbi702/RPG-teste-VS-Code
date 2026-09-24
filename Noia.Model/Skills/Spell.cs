public class Spell
{
    public int IdSpell { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }
    public int BaseCooldown { get; set; }

    public List<PlayerSpell> PlayerSpells { get; set; } = new();
}