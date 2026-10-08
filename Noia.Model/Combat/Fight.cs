using Noia.PlayerNS;
using Noia.MonsterNS;
using Noia.Skills;

namespace Noia.Combat
{

    public class Fight
    {

        
        public void StartFight(Player player, Monster monster)
        {
            Console.Clear();

            Console.WriteLine("===============================================================");
            Console.WriteLine($"Beware! {monster.Name} appears before you!\n");

            Console.WriteLine($"{monster.Name} \nLevel: {monster.Level} \nHealth: {monster.Health}");

            Console.WriteLine("\nWhat will you do?");
            Console.WriteLine("1. Fight");
            Console.WriteLine("2. Flee");

            Console.WriteLine("\n===============================================================");

            string choice = Console.ReadLine();
            if (!int.TryParse(choice, out int choiceValue))
            {
                choiceValue = 0;
            }
            while (choiceValue != 1 && choiceValue != 2)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                choice = Console.ReadLine();
                if (!int.TryParse(choice, out choiceValue))
                {
                    choiceValue = 0;
                }
            }

            if (choiceValue == 1)
            {
                DuringFight(player, monster);
            }
            else if (choiceValue == 2)
            {
                Console.WriteLine("You have fled the battle.");
            }
        }

        

        public void DuringFight(Player player, Monster monster)
        {
            while (player.Health > 0 || monster.Health > 0)
            {
                Console.Clear();
                Console.WriteLine("===============================================================");
                Console.WriteLine($"{monster.CombatMessage}");

                Console.WriteLine($"\n{monster.Name} \nLevel: {monster.Level} \nHealth: {monster.Health}");

                Console.WriteLine($"\n{player.Name} \nLevel: {player.Level} \nHealth: {player.Health} \nMana: {player.Mana}");

                Console.WriteLine("\nWhat will you do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Spell");
                Console.WriteLine("3. Defend");
                Console.WriteLine("4. Taunt");
                Console.WriteLine("5. Flee");

                Console.WriteLine("\n===============================================================");

                string choice = Console.ReadLine() ?? "0";
                if (!int.TryParse(choice, out int choiceValue))
                {
                    choiceValue = 0;
                }
                while (choiceValue < 1 || choiceValue > 5)
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                    choice = Console.ReadLine() ?? "0";
                    if (!int.TryParse(choice, out choiceValue))
                    {
                        choiceValue = 0;
                    }
                }

                switch (choiceValue)
                {
                    case 1:
                        PlayerChooseAttack(player, out Attack selectedAttack);
                        if (selectedAttack == null)
                        {
                            continue;
                        }
                        PlayerUseAttack(selectedAttack, monster);
                        if (monster.Health <= 0)
                        {
                            Console.WriteLine($"\nYou have defeated {monster.Name}!");
                            player.GainExperienceCombat(monster);
                            return;
                        }
                        else
                        {
                            MonsterAttack(player, monster);
                            ReduceMonsterAttackCooldowns(monster);
                            if (player.Health <= 0)
                            {
                                Console.WriteLine($"\nYou have been defeated by {monster.Name}!");
                                return;
                            }
                        }
                        break;
                    case 2:
                        PlayerChooseSpell(player, out Spell selectedSpell);
                        if (selectedSpell == null)
                        {
                            continue;
                        }
                        PlayerUseSpell(selectedSpell, monster, player);
                        if (monster.Health <= 0)
                        {
                            Console.WriteLine($"\nYou have defeated {monster.Name}!");
                            player.GainExperienceCombat(monster);
                            return;
                        }
                        else
                        {
                            MonsterAttack(player, monster);
                            ReduceMonsterAttackCooldowns(monster);
                            if (player.Health <= 0)
                            {
                                Console.WriteLine($"\nYou have been defeated by {monster.Name}!");
                                return;
                            }
                        }
                        break;
                    case 3:
                        PlayerDefend(player);
                        if (monster.Health <= 0)
                        {
                            Console.WriteLine($"\nYou have defeated {monster.Name}!");
                            player.GainExperienceCombat(monster);
                            return;
                        }
                        else
                        {
                            MonsterAttack(player, monster);
                            ReduceMonsterAttackCooldowns(monster);
                            if (player.Health <= 0)
                            {
                                Console.WriteLine($"\nYou have been defeated by {monster.Name}!");
                                return;
                            }
                        }
                        break;
                    case 4:
                        PlayerTaunt(player, monster);
                        MonsterAttack(player, monster);
                        ReduceMonsterAttackCooldowns(monster);
                        if (player.Health <= 0)
                        {
                            Console.WriteLine($"\nYou have been defeated by {monster.Name}!");
                            return;
                        }
                        break;
                    case 5:
                        Console.WriteLine("You have fled the battle.");
                        return;
                }
            }
        }

        public void PlayerChooseAttack(Player player, out Attack selectedAttack)
        {
            Console.WriteLine("\nWhich attack will you use? (Type 0 to cancel)\n");
            int atkNum = 1;
            foreach (var attackChoice in player.PlayerAttacks)
            {
                Console.WriteLine($"{atkNum} - {attackChoice.Name} | Damage: {attackChoice.Damage} | Cooldown: {attackChoice.CurrentCooldown}/{attackChoice.BaseCooldown}");
                atkNum++;
            }
            string choice = Console.ReadLine() ?? "0";
            if (!int.TryParse(choice, out int choiceValue))
            {
                choiceValue = -1;
            }
            if (choiceValue == 0)
            {
                selectedAttack = null;
                return;
            }
            while (choiceValue < 1 || choiceValue > player.PlayerAttacks.Count)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                choice = Console.ReadLine() ?? "0";
                if (!int.TryParse(choice, out choiceValue))
                {
                    choiceValue = -1;
                }
                if (choiceValue == 0)
                {
                    selectedAttack = null;
                    return;
                }
            }
            while (player.PlayerAttacks[choiceValue - 1].CurrentCooldown > 0)
            {
                Console.WriteLine("This attack is on cooldown. Please choose another attack.");
                choice = Console.ReadLine() ?? "0";
                if (!int.TryParse(choice, out choiceValue))
                {
                    choiceValue = -1;
                }
                while (choiceValue < 1 || choiceValue > player.PlayerAttacks.Count)
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                    choice = Console.ReadLine() ?? "0";
                    if (!int.TryParse(choice, out choiceValue))
                    {
                        choiceValue = -1;
                    }
                    if (choiceValue == 0)
                    {
                        selectedAttack = null;
                        return;
                    }
                }
            }
            

            selectedAttack = player.PlayerAttacks[choiceValue - 1];
        }

        public void PlayerUseAttack(Attack attack, Monster monster)
        {
            Console.WriteLine($"\nYou attack with {attack.Name}");

            Console.WriteLine($"\n{monster.Name} takes {attack.Damage - monster.Armor} damage!");

            monster.Health -= attack.Damage - monster.Armor;

            attack.CurrentCooldown = attack.BaseCooldown;

            Console.WriteLine($"\n{monster.Name} has {monster.Health} health remaining.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void PlayerChooseSpell(Player player, out Spell selectedSpell)
        {
            Console.WriteLine("\nWhich spell will you use? (Type 0 to cancel)\n");
            int spellNum = 1;
            foreach (var spellChoice in player.PlayerSpells)
            {
                Console.WriteLine($"{spellNum} - {spellChoice.Name} | Damage: {spellChoice.Damage} | Mana Cost: {spellChoice.ManaCost} | Cooldown: {spellChoice.CurrentCooldown}/{spellChoice.BaseCooldown}");
                spellNum++;
            }
            string choice = Console.ReadLine() ?? "0";
            if (!int.TryParse(choice, out int choiceValue))
            {
                choiceValue = -1;
            }
            if (choiceValue == 0)
            {
                selectedSpell = null;
                return;
            }
            while (choiceValue < 1 || choiceValue > player.PlayerSpells.Count)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                choice = Console.ReadLine() ?? "0";
                if (!int.TryParse(choice, out choiceValue))
                {
                    choiceValue = -1;
                }
                if (choiceValue == 0)
                {
                    selectedSpell = null;
                    return;
                }
            }
            while (player.PlayerSpells[choiceValue - 1].CurrentCooldown > 0)
            {
                Console.WriteLine("This spell is on cooldown. Please choose another spell.");
                choice = Console.ReadLine() ?? "0";
                if (!int.TryParse(choice, out choiceValue))
                {
                    choiceValue = -1;
                }
                if (choiceValue == 0)
                {
                    selectedSpell = null;
                    return;
                }
                while (choiceValue < 1 || choiceValue > player.PlayerSpells.Count)
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                    choice = Console.ReadLine() ?? "0";
                    if (!int.TryParse(choice, out choiceValue))
                    {
                        choiceValue = -1;
                    }
                    if (choiceValue == 0)
                    {
                        selectedSpell = null;
                        return;
                    }
                }
            }
            

            selectedSpell = player.PlayerSpells[choiceValue - 1];
        }

        public void PlayerUseSpell(Spell spell, Monster monster, Player player)
        {
            Console.WriteLine($"\nYou cast {spell.Name}");

            Console.WriteLine($"\n{monster.Name} takes {spell.Damage - monster.MagicResistance} damage!");

            monster.Health -= spell.Damage - monster.MagicResistance;

            player.Mana -= spell.ManaCost;

            spell.CurrentCooldown = spell.BaseCooldown;

            player.Mana += player.ManaRegen;

            Console.WriteLine($"\n{monster.Name} has {monster.Health} health remaining.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public void PlayerDefend(Player player)
        {
            Console.WriteLine($"\nYou brace yourself for the next attack");
            player.Armor += 10;
            player.MagicResistance += 5;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }   

        public void PlayerTaunt(Player player, Monster monster)
        {
            Console.WriteLine($"\nYou taunt {monster.Name}, trying to provoke it.");
            switch (monster.TauntEffect)
            {
                case TauntEffectType.None:
                    Console.WriteLine($"{monster.Name} is unaffected by the taunt.");
                    break;
                case TauntEffectType.IncreaseAttackDamage:
                    if (monster.TryAddTauntStack())
                    {
                        Console.WriteLine(
                            $"{monster.Name} grows angrier! Its attacks gain " +
                            $"{monster.AttackDamageBonusPerTauntStack} damage " +
                            $"({monster.TauntStacks}/{monster.MaxTauntStacks} stacks).");
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Name} is already as angry as it can get.");
                    }
                    break;
                case TauntEffectType.SkipTurn:
                    if (monster.TryAddTauntStack())
                    {
                        Console.WriteLine($"{monster.Name} flinches and loses its next turn.");
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Name} is already flinching.");
                    }
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private Attack? ChooseMonsterAttack(Monster monster)
        {
            var availableAttacks = monster.MonsterAttacks
                .Where(attack => attack.CurrentCooldown == 0)
                .ToList();

            if (availableAttacks.Count == 0)
                return null;

            return availableAttacks[Random.Shared.Next(availableAttacks.Count)];
        }

        public void MonsterAttack(Player player, Monster monster)
        {
            if (monster.ConsumeSkipTurn())
            {
                Console.WriteLine($"\n{monster.Name} is still flinching and loses its turn.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var attack = ChooseMonsterAttack(monster);
            if (attack == null)
            {
                Console.WriteLine($"\n{monster.Name} has no available attacks and skips its turn.");
                return;
            }
            Console.WriteLine($"\n{attack.Name} is used by {monster.Name}");

            int tauntDamageBonus = monster.TauntEffect == TauntEffectType.IncreaseAttackDamage
                ? monster.TauntStacks * monster.AttackDamageBonusPerTauntStack
                : 0;
            int damage = attack.Damage + tauntDamageBonus - player.Armor;

            Console.WriteLine($"\n{player.Name} takes {damage} damage!");

            player.Health -= damage;

            attack.CurrentCooldown = attack.BaseCooldown;

            Console.WriteLine($"\nYou have {player.Health} health remaining.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ReduceMonsterAttackCooldowns(Monster monster)
        {
            foreach (var attack in monster.MonsterAttacks)
            {
                if (attack.CurrentCooldown > 0)
                    attack.CurrentCooldown--;
            }
        }
    } 
}