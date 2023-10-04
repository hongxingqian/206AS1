using System;

namespace assginment1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // combat actions between player and enemy!
             void Combat(Character a_cPlayer, Character a_cEnemy)
            {
                Console.WriteLine("The Fight starts!");
                bool isPlayerTurn = true;
                bool isTypeWrong = false;
                while (a_cPlayer.HP > 0 && a_cEnemy.HP > 0)
                {
                    // Player turn
                    if (!isTypeWrong) {
                        Console.WriteLine("");
                        Console.WriteLine("-- Player Turn --");
                        Console.WriteLine("-----------------");
                        Console.WriteLine("Player Hp - " + a_cPlayer.HP + ". Enemy HP - " + a_cEnemy.HP);
                    }

                    Console.WriteLine("Enter 'a' to attack or 'h' to heal. ");

                    string choice = Console.ReadLine();

                        if (choice == "a")
                        {

                            a_cPlayer.Fight(a_cEnemy);
                            Console.WriteLine("Player attack enemy and deals " + (a_cPlayer.Attack - a_cEnemy.Defense) + " damage!");
                            isPlayerTurn = false;

                        }
                        else if (choice == "h")
                        {
                            a_cPlayer.Healing();
                            Console.WriteLine("Player restores " + a_cPlayer.Heal + " health!");
                            isPlayerTurn = false;

                        }
                        else
                        {
                            Console.WriteLine("Not valid input! Try again!");
                        isTypeWrong = true;
                    }



                    // Enemy Turn
                    if (a_cEnemy.HP > 0 && !isPlayerTurn)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("-- Enemy Turn --");
                        Console.WriteLine("----------------");
                        Console.WriteLine("Player Hp - " + a_cPlayer.HP + ". Enemy HP - " + a_cEnemy.HP);
                        int enemyChoice = a_cEnemy.random.Next(0, 2);

                        if (enemyChoice == 0)
                        {
                            a_cEnemy.Fight(a_cPlayer);
                            Console.WriteLine("");
                            Console.WriteLine("Enemy attacks and deals " + (a_cEnemy.Attack - a_cPlayer.Defense) + " damage!");
                        }
                        else
                        {
                            a_cEnemy.Healing();
                            Console.WriteLine("");
                            Console.WriteLine("Enemy restores " + a_cEnemy.Heal + " health!");
                        }
                    }
                }
                if (a_cPlayer.HP > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("\n-----You Win!");
                    Console.WriteLine("");
                    Console.WriteLine("Level Up: Lv " + a_cPlayer.Level + " --> " + "Lv " + (a_cPlayer.Level + 1));
                    a_cPlayer.Levelup();
                    a_cPlayer.Print();

                    Item droppedItem = a_cEnemy.DropLoot(); 
                    Item droppedKey = a_cEnemy.DropKey();
                    if (droppedItem != null)
                    {
                        Console.WriteLine("The enemy dropped a loot: " + droppedItem.LootName);
                        a_cPlayer.GainLoot(droppedItem);
                        a_cPlayer.AddItemStatusToCharacter(droppedItem);
                        a_cEnemy.DeleteItem();
                    }
                    else
                    {
                        Console.WriteLine("No loot was dropped by the enemy.");
                    }

                    if (droppedKey != null)
                    {
                        Console.WriteLine("The enemy dropped a key-shape thing: " + droppedKey.LootName);
                        a_cPlayer.GainLoot(droppedKey);
                        a_cPlayer.AddItemStatusToCharacter(droppedKey);
                        a_cEnemy.DeleteKey();
                    }

                    a_cEnemy.Death();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("\n-----Game Over! You Lose!");
                }
            }

            // Enter function to allow players to go to rooms.
            void Enter(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter) {
                Console.WriteLine();
                a_rLocation.Description();
                Console.WriteLine();

                if (a_cCharacter == null)
                {
                    EnterDecision(a_rLocation, a_hPlayer, a_cCharacter);
                }

                else if (a_cCharacter.DeathorLive == false)
                {
                    a_rLocation.Death();
                    EnterDecision(a_rLocation, a_hPlayer, a_cCharacter);
                }
                else
                {
                    Encouter(a_rLocation, a_cCharacter.Description, a_hPlayer, a_cCharacter);
                }

            }

            // Helper function to choose which path to go without encouter.
            void EnterDecision(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter)
            {
                Console.WriteLine("It seems no one is here.");
                bool isTypeCorrect = false;
                Random lootChance = new Random();
                int var = lootChance.Next(0);

                while (!isTypeCorrect)
                {
                    Console.WriteLine("\n-----Enter 'S' to search around or 'M' to move around to find a spot to rest and check your Bags. ");
                    string choice = Console.ReadLine();

                    switch (choice.ToUpper())
                    {
                        case "S":
                            if (var == 0 && a_rLocation.Item != null)
                            {
                                Console.WriteLine("Congratulate! You find a Loot!");
                                a_hPlayer.GainLoot(a_rLocation.Item);
                                a_rLocation.DeleteLoot();
                            }
                            else
                            {
                                Console.WriteLine("Sadly, you find nothing.");
                            }
                            isTypeCorrect = true;
                            break;

                        case "M":
                            Console.WriteLine("You move around to see if there is a good spot to take a nap.");
                            Console.WriteLine("You took a nap and healed 20 HP! ");
                            a_hPlayer.Resting();
                            a_hPlayer.Print();
                            a_hPlayer.PrintInventory();
                            isTypeCorrect = true;
                            break;



                        default:
                            Console.WriteLine("Invalid Input! Try new actions again!");
                            break;
                    }
                }

                ShowPaths(a_rLocation);
                Console.WriteLine("\n-----Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
            }

            // helper function to show possible paths
            void ShowPaths(Room room)
            {
                if (room.Left != null)
                {
                    Console.WriteLine("\n-----There is a LEFT path that you can go next:");
                    room.Left.PreDescription();
                }
                else if (room.Right != null)
                {
                    Console.WriteLine("\n-----There is a RIGHT path that you can go next:");
                    room.Right.PreDescription();
                }
                else
                {
                    Console.WriteLine("\n-----There is no Room.");
                }

            }


            void WaitingValidInput(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter)
            {
                // 0 => go right, 1=> go left, 2 => go back.
                int direction = -1;

                while (direction == -1)
                {
                    string choice = Console.ReadLine().ToUpper();

                    if (choice == "R")
                    {
                        if (a_rLocation.Right != null)
                        {
                            direction = 0;
                        }
                        else
                        {
                            Console.WriteLine("There is no Path on the Right!");
                        }
                    }
                    else if (choice == "L")
                    {
                        if (a_rLocation.Left != null)
                        {
                            direction = 1;
                        }
                        else
                        {
                            Console.WriteLine("There is no Path on the Left!");
                        }
                    }
                    else if (choice == "B")
                    {
                        if (a_rLocation.Previous != null)
                        {
                            direction = 2;
                        }
                        else
                        {
                            Console.WriteLine("There is no Path to go Back!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("It is not a valid input! Please try again.");
                    }
                }

                // Once out of the loop, we know direction has a valid value.
                if (direction == 0)
                {
                    if(a_rLocation.Right.DoorNeedsKey(a_hPlayer.Inventory))
                    {
                        Enter(a_rLocation.Right, a_hPlayer, a_rLocation.Right.GetCharacter);
                    }
                    else
                    {
                        Console.WriteLine("\n******* You do not have the Key to open the door.");
                        Console.WriteLine("To open it, it seems that you needs a " + a_rLocation.Right.KeyName() + " to open it.");
                        ShowPaths(a_rLocation);
                        Console.WriteLine("\n-----Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                        WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
                    }
                }
                else if (direction == 1)
                    if (a_rLocation.Left.DoorNeedsKey(a_hPlayer.Inventory))
                    {
                        Enter(a_rLocation.Left, a_hPlayer, a_rLocation.Left.GetCharacter);
                    }
                    else
                    {
                        Console.WriteLine("\n******* You do not have the Key to open the door.");
                        Console.WriteLine("To open it, it seems that you needs a " + a_rLocation.Left.KeyName() + " to open it.");
                        ShowPaths(a_rLocation);
                        Console.WriteLine("\n-----Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                        WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
                    }
                else if (direction == 2)
                {
                    Enter(a_rLocation.Previous, a_hPlayer, a_rLocation.Previous.GetCharacter);
                }
                else
                {
                    Console.WriteLine("You found a bug!");  // This shouldn't be reached if the while loop is functioning correctly.
                }
            }

            // Helper function for when player meet with an npc.
            void Encouter(Room a_rLocation, string a_sEncounterDescrip, Hero a_hPlayer, Character a_cCharacter)
            {
               bool isTypeCorrect = false;
                while(!isTypeCorrect)
                {
                    if (a_cCharacter.Relationship == 0)
                    {
                        Console.WriteLine("-------------------");
                        Console.WriteLine(a_cCharacter.Description);
                        Console.WriteLine("Enter 't' to talk, 's' to skip, or 'e' to leave. ");
                        string choice = Console.ReadLine();
                        if (choice == "t")
                        {
                            Item droppedKey = a_cCharacter.DropKey(); // local varibale

                            Console.WriteLine("-------------------");
                            Console.WriteLine(a_cCharacter.Conversation);
                            if (droppedKey != null)
                            {
                                Console.WriteLine("-------------------");
                                Console.WriteLine("The person gives a key-shape thing to you: " + droppedKey.LootName);
                                a_hPlayer.GainLoot(droppedKey);
                                a_hPlayer.AddItemStatusToCharacter(droppedKey);
                                a_cCharacter.DeleteKey();

                            }
                            ShowPaths(a_rLocation);
                            Console.WriteLine("\n-----Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                            WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
                            isTypeCorrect = true;

                        }

                        else if (choice == "s")
                        {
                            Console.WriteLine("-------------------");
                            ShowPaths(a_rLocation);
                            Console.WriteLine("\n-----Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                            WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
                            isTypeCorrect = true;

                        }
                        else if (choice == "e")
                        {
                            Console.WriteLine("You decided to go Back.");
                            Enter(a_rLocation.Previous, a_hPlayer, a_rLocation.Previous.GetCharacter);
                            isTypeCorrect = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input! Do your action again!");
                        }
                    }
                    else if (a_cCharacter.Relationship == 1)
                    {
                        Console.WriteLine("-------------------");
                        Console.WriteLine(a_cCharacter.Description);
                        Console.WriteLine("\n-----Enter 'a' to attack or 'e' to escape. ");
                        string choice = Console.ReadLine();
                        if (choice == "a")
                        {
                            Combat(a_hPlayer, a_cCharacter);
                            ShowPaths(a_rLocation);
                            Console.WriteLine("\n-----Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                            WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
                            isTypeCorrect = true;
                        }
                        else if (choice == "e")
                        {
                            Console.WriteLine("You decided to run away and go back.");
                            Enter(a_rLocation.Previous, a_hPlayer, a_rLocation.Previous.GetCharacter);
                            isTypeCorrect = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input! Do your action again!");
                        }
                    }
                }
               
            }

            Item rock = new Item("just a rock", 1, 0, 0);
            Item rock1 = new Item("just a rock", 1, 0, 0);
            Item paper = new Item("a paper barly cover you body", 0, 1, 0);
            Item paper1 = new Item("a paper barly cover you body", 0, 1, 0);
            Item stick = new Item("Stick", 1, 0, 0);
            Item stick1 = new Item("Stick", 1, 0, 0);
            Item dagger = new Item("dagger", 2, 0, 0);
            Item axe = new Item("Old Axe", 3, 0, 0);
            Item sword = new Item("New Sword", 5, 0, 0);
            Item lendWeapon = new Item("Legendary Sword", 7, 1, 10);
            Item cloth = new Item("Light Old Cloth", 0, 2, 0);
            Item brokenArmour = new Item("Broken Armour", 0, 3, 0);
            Item armor = new Item("New Armour", 0, 4, 0);
            Item lendArmour = new Item("Legendary Armour", 1, 5, 20);
            Item key1 = new Item("Iron Key", 0, 0, 0);
            Item key2 = new Item("Gold Key", 0, 0, 0);

            Human citizen = new Human("Eric", "A skinny girl with a white short hair, is looking at you.", "Hi, My sister got caught by the monster 'Alberto'! " +
                "\nHe brought her to his nest in the deepest section in the castle. Please defeat him and save her! " +
                "\nI found this key in the front yard. Maybe you can use it.", key1);
            Human citizen1 = new Human("Noya","A skinny short girl is looking at you.", "Hi, I got caught by the monster 'Alberto'!  Please defeat him and save me!", null);
            Hero player = new Hero("max", "A nice guy.", "I am Max! I am a nice guy!", 0, 1, true, null, null);
            Boss professor = new Boss("Alberto", "A big giant ulgy dragon.", "You will die just like other heros who challenged me!", 15, 1, true, lendWeapon, null);
            Minion mini1 = new Minion("Tao", "A stupid goblin is looking at you and it wants to eat you!.", "Hi, I like to eat human and I am smart!", 1, 1,true, dagger, null);
            Minion mini2 = new Minion("Matt", "A tall goblin is looking at you and it wants to smash you!.", "Hi, I like to smash and I am smart!", 2, 1, true, axe, key1);


            Room castle = new Room("Castle", "You see a big and old castle in front of you.", "There is a huge castle standing out in the top of the hill.", rock);
            Room frontYard = new Room("Front Yard", "You see a run-down front yard in front of the castle", "There is no flowers and grass, only black  lifeless soil.", citizen, stick);
            Room castleGate = new Room("Castle Gate", "You see a big iron gate blocks your path and a citizen is crying in the front.", "There is a big door in the center of the castle!",citizen1, rock1);
            Room castleChamber = new Room("First Floor Chamber", "You opens the big door and enters the castle.", " You found the chamber is really big with torches on two sides.", stick1, key1);
            Room room1 = new Room("Big Hole", "There is no light in the room. You can not see anything.", "There is a small tunnel and you can barely walk in.", mini1, paper1);
            Room room2 = new Room("Banquet hall", "You see a tiny room with a monster in the front.", "The way is going downstars and you can see subtle light coming from the room.", mini2, dagger);
            castle.leftRoom = frontYard;
            frontYard.rightRoom = castleGate;
            castleGate.leftRoom = castleChamber;
            castleChamber.leftRoom = room1;
            castleChamber.rightRoom = room2;
            castleChamber.previousRoom = castleGate;
            castleGate.previousRoom = frontYard;
            frontYard.previousRoom = castle;
            room1.previousRoom = castleChamber;
            room2.previousRoom = castleChamber;

            Enter(castle, player, castle.GetCharacter);
        }
    }
}