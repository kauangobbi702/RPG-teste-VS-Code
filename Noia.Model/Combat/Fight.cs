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

            int choice = Convert.ToInt32(Console.ReadLine());
            while (choice != 1 && choice != 2)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            if (choice == 1)
            {
                DuringFight(player, monster);
            }
            else if (choice == 2)
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

                Console.WriteLine($"{monster.Name} \nLevel: {monster.Level} \nHealth: {monster.Health}");

                Console.WriteLine($"{player.Name} \nLevel: {player.Level} \nHealth: {player.Health} \nMana: {player.Mana}");

                Console.WriteLine("\nWhat will you do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Spell");
                Console.WriteLine("3. Defend");
                Console.WriteLine("4. Taunt");
                Console.WriteLine("5. Flee");

                Console.WriteLine("\n===============================================================");

                int choice = Convert.ToInt32(Console.ReadLine());
                while (choice < 1 || choice > 5)
                {
                    Console.WriteLine("Invalid choice. Please choose again.");
                    choice = Convert.ToInt32(Console.ReadLine());
                }

                switch (choice)
                {
                    case 1:
                        PlayerChooseAttack(player, out Attack selectedAttack);
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
            Console.WriteLine("Which attack will you use?\n");
            for (int i = 0; i < player.attacks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.attacks[i].Name} - Cooldown(Current/On use): {player.attacks[i].CurrentCooldown}/{player.attacks[i].BaseCooldown}");
            }

            int choice = Convert.ToInt32(Console.ReadLine());
            while (choice < 1 || choice > player.attacks.Count)
            {
                Console.WriteLine("Invalid choice. Please choose again.");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            selectedAttack = player.attacks[choice - 1];
        }

        public void PlayerAttack(Attack attack, Monster monster)
        {
            Console.WriteLine($"You attack with {attack.Name}");

            Console.WriteLine($"{monster.Name} takes {attack.Damage - monster.Armor} damage!");

            monster.Health -= attack.Damage - monster.Armor;
        }
    }
}