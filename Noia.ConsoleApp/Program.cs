using Noia.PlayerNS;
using Noia.MonsterNS;
using Noia.Skills;
using Noia.Combat;

Fight fight = new Fight();

Attack basicSlimeTackle = new Attack("Tackle", 10, 0);
Attack basicSlimeBounce = new Attack("Bounce", 15, 2);

Spell fireBall = new Spell
{
    Name = "Fireball",
    Damage = 20,
    ManaCost = 10,
    BaseCooldown = 2
};

Monster basicSlime = new Monster
{
    Name = "Basic Slime",
    Level = 1,
    Health = 50,
    Mana = 10,
    Armor = 2,
    MagicResistance = 5,
    ExperienceReward = 20,
    CombatMessage = "The Slime sways back and forth, menacingly.",
    MonsterAttacks = new List<Attack> { basicSlimeTackle, basicSlimeBounce }
};

Player player = new Player("Hero");

player.PlayerSpells.Add(fireBall);

fight.StartFight(player, basicSlime);

