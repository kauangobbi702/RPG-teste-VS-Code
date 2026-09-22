namespace Noia.Model;

public class Attack
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int BaseCooldown { get; set; }
    public int CurrentCooldown { get; set; }

    public Attack()
    {}
}