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
                        PlayerAttack(selectedAttack, monster);
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
                        // Implement spell logic here
                        break;
                    case 3:
                        // Implement defend logic here
                        break;
                    case 4:
                        // Implement taunt logic here
                        break;
                    case 5:
                        Console.WriteLine("You have fled the battle.");
                        return;
                }
            }
        }

        public void PlayerChooseAttack(Player player, out Attack selectedAttack)
        {
            Console.WriteLine("\nWhich attack will you use?\n");
            int atkNum = 1;
            foreach (var attackChoice in player.PlayerAttacks)
            {
                Console.WriteLine($"{atkNum} - {attackChoice.Name}");
                atkNum++;
            }
            string choice = Console.ReadLine() ?? "0";
            if (!int.TryParse(choice, out int choiceValue))
            {
                choiceValue = 0;
            }
            while (choiceValue < 1 || choiceValue > player.PlayerAttacks.Count)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                choice = Console.ReadLine() ?? "0";
                if (!int.TryParse(choice, out choiceValue))
                {
                    choiceValue = 0;
                }
            }

            selectedAttack = player.PlayerAttacks[choiceValue - 1];
        }

        public void PlayerAttack(Attack attack, Monster monster)
        {
            Console.WriteLine($"\nYou attack with {attack.Name}");

            Console.WriteLine($"\n{monster.Name} takes {attack.Damage - monster.Armor} damage!");

            monster.Health -= attack.Damage - monster.Armor;

            attack.CurrentCooldown = attack.BaseCooldown;

            Console.WriteLine($"\n{monster.Name} has {monster.Health} health remaining.");
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
            var attack = ChooseMonsterAttack(monster);
            if (attack == null)
            {
                Console.WriteLine($"\n{monster.Name} has no available attacks and skips its turn.");
                return;
            }
            Console.WriteLine($"\n{attack.Name} is used by {monster.Name}");

            Console.WriteLine($"\n{player.Name} takes {attack.Damage - player.Armor} damage!");

            player.Health -= attack.Damage - player.Armor;

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