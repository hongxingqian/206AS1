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
                Console.WriteLine("\n---The Fight starts!---");
                bool isPlayerTurn = true;
                bool isTypeWrong = false;
                while (a_cPlayer.HP > 0 && a_cEnemy.HP > 0)
                {
                    // Player turn
                    isTypeWrong = true; // Assume input is wrong by default
                    while (isTypeWrong)
                    {
                        if (!isTypeWrong)
                        {
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
                            Console.WriteLine("Player attacked enemy and dealt " + (a_cPlayer.Attack - a_cEnemy.Defense) + " damage!");
                            isTypeWrong = false; // set to false once a valid choice is made
                        }
                        else if (choice == "h")
                        {
                            a_cPlayer.Healing();
                            Console.WriteLine("Player restores " + a_cPlayer.Heal + " health!");
                            isTypeWrong = false; // set to false once a valid choice is made
                        }
                        else
                        {
                            Console.WriteLine("Not a valid input! Try again!");
                        }
                    }

                    // Enemy turn
                    if (a_cEnemy.HP > 0)
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
                    a_cPlayer.Death();
                    Console.WriteLine("");
                }
            }

            // Enter function to allow players to go to rooms.
            void Enter(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter) {
                Console.WriteLine();
                a_rLocation.Description();
                Console.WriteLine();
                if (!a_hPlayer.DeathorLive)
                {
                    Console.WriteLine("You spent all your energy trying to get out, but you failed.");
                    Console.WriteLine("\n-----Game Over! You Lose!");
                    return;
                }
                if (a_cCharacter != null && a_cCharacter.IsBoss() && !a_cCharacter.DeathorLive)
                {
                    Console.WriteLine("The Boss is defeated!");
                    Console.WriteLine("*----------------You Win!----------------*");
                    return;
                }
                if(a_cCharacter == null)
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
                int var = lootChance.Next(3);

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
                if (room.Right != null)
                {
                    Console.WriteLine("\n-----There is a RIGHT path that you can go next:");
                    room.Right.PreDescription();
                }
                if(room.Left == null && room.Right == null)
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
                        Console.WriteLine("\n-------------------");
                        Console.WriteLine(a_cCharacter.Description);
                        Console.WriteLine("Enter 't' to talk, 's' to skip, or 'e' to leave. ");
                        string choice = Console.ReadLine();
                        if (choice == "t")
                        {
                            Item droppedKey = a_cCharacter.DropKey(); // local varibale

                            Console.WriteLine("\n-------------------");
                            Console.WriteLine(a_cCharacter.Conversation);
                            if (droppedKey != null)
                            {
                                Console.WriteLine("\n-------------------");
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
                            Console.WriteLine("\n-------------------");
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
                        Console.WriteLine("\n-------------------");
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
            Item bottleWater = new Item("A Bottle of Water", 0, 0, 20);
            Item bottleWater1 = new Item("A Bottle of Water", 0, 0, 20);
            Item axe = new Item("Old Axe", 3, 0, 0);
            Item axe1 = new Item("Old Axe", 3, 0, 0);
            Item sickle = new Item("Old Axe", 4, -1, -10);
            Item sickle1 = new Item("Old Axe", 4, -1, -10);
            Item ironStick = new Item("Iron Stick", 3, 1, 0);
            Item ironStick1 = new Item("Iron Stick", 3, 1, 0);
            Item sword = new Item("New Sword", 5, 0, 0);
            Item sword1 = new Item("New Sword", 5, 0, 0);
            Item purpWeapon = new Item("Great Sword", 6, 0, 0);
            Item purpWeapon1 = new Item("Great Sword", 6, 0, 0);

            Item lendWeapon = new Item("Legendary Sword", 7, 1, 10);
            Item cloth = new Item("Light Old Cloth", 0, 2, 0);
            Item brokenArmour = new Item("Broken Armour", 0, 3, 0);
            Item armor = new Item("New Armour", 0, 4, 0);
            Item armor1 = new Item("New Armour", 0, 4, 0);
            Item armor2 = new Item("New Armour", 0, 4, 0);
            Item purpArmour = new Item("Legendary Armour", 0, 5, 0);
            Item purpArmour1 = new Item("Legendary Armour", 0, 5, 0);
            Item lendArmour = new Item("Legendary Armour", 1, 5, 20);
            Item IronKey = new Item("Iron Key", 0, 0, 0);
            Item goldKey = new Item("Gold Key", 0, 0, 0);
            Item diamondKey = new Item("Diamond Key", 0, 0, 0);

            Human citizen = new Human("Eric", "A skinny girl with a white short hair, is looking at you.", "Hi, My sister got caught by the monster 'Alberto'! " +
                "\nHe brought her to his nest in the deepest section in the castle. Please defeat him and save her! " +
                "\nI found this key in the front yard. Maybe you can use it.", IronKey);
            Human citizen1 = new Human("Noya","A girl had a broken dress on her hands is sitting on the floor.", "Hi, I am the only survior from the village. " +
                "\nMonster 'Alberto' killed all my family...  \nPlease take this and kill him! For my revenge!", cloth);
            Human citizen2 = new Human("Eric", "A man hides in the cornor. He sees you and waving to let you come closer.",
                "\nDid you kill the witch? I heared her laughing, and after few minutes, I heared her screaming... Good Job! " +
                "\nThe next thing you need to do is to find **The Supreme Guard** to get the Diamond Key in order to enter Alberto's nest. " +
                "\nI wish you good luck.", null);
            Minion mini1 = new Minion("Tao", "A stupid goblin is looking at you and it wants to eat you!.", "Hi, I like to eat human and I am smart!", 1, 1,true, dagger, null);
            Minion mini2 = new Minion("Matt", "A tall goblin is looking at you and it wants to smash you!.", "Hi, I like to smash and I am smart!", 2, 1, true, axe, null);
            Minion mini3 = new Minion("Puppet", "You see a tall and thin puppet standing far away. It is perfectly still, wearing a huge sickle. " +
                "\nBut her eyes are watching you!", "gaz..gaz..gaz..", 4, 1, true, sickle, null);
            Minion mini4 = new Minion("Puppet", "You see a tall and thin puppet blocking the path. It is perfectly still, wearing a huge sickle. " +
    "\nBut her eyes are watching you!", "gaz..gaz..gaz..", 4, 1, true, sickle, null);
            Minion miniBoss = new Minion("Rin", "A Witch is flying in the sky. You can hear her loud laughing...", "You are mine! Die! Hahahaha!", 13, 1, true, brokenArmour, goldKey);
            Minion guard = new Minion("Bronze Guard", "A tall fully armored statue begins to move", "You are it's target!", 11, 1, true, ironStick, null);
            Minion guard1 = new Minion("Bronze Guard", "A tall fully armored statue begins to move", "You are it's target!", 11, 1, true, ironStick, null);
            Minion guard2 = new Minion("Bronze Guard", "A tall fully armored statue begins to move", "You are it's target!", 11, 1, true, ironStick, null);
            Minion eliteGuard1 = new Minion("Sliver Guard", "A tall fully armored statue begins to move", "You are it's target!", 14, 1, true, sword, null);
            Minion eliteGuard2 = new Minion("Gold Guard", "A tall and strong fully armored statue begins to move", "You are it's target!", 14, 1, true, sword, null);
            Minion eliteGuard3 = new Minion("Elite Gold Guard", "The Supreme statue begins to move", "You are it's target!", 17, 1, true, sword1, null);
            Minion eliteGuard4 = new Minion("Supreme Guard", "The Supreme white statue begins to move", "You are it's target!", 20, 1, true, lendArmour, diamondKey);
            Minion eliteGuard5 = new Minion("Supreme Guard", "The Supreme dark statue begins to move", "You are it's target!", 20, 1, true, lendWeapon, null);
            Boss professor = new Boss("Alberto", "A big giant ulgy dragon is screaming at you.", "You will die just like other heros who challenged me!", 28, 1, true, null, null);


            Room castle = new Room("Castle", "You see a big and old castle in front of you.", "There is a huge castle standing out in the top of the hill.", rock);
            Room frontYard = new Room("Front Yard", "You kept walking to the yard. There is no flowers and grass, only black  lifeless soil.", "You see a run-down front yard in front of the castle", citizen, stick);
            Room castleGate = new Room("Castle Gate", "There is a big door in the center of the castle!", "You see a big iron gate blocks your path and a citizen is crying in the front.", citizen1, rock1);
            Room castleChamberFront = new Room("Front Chamber", " You walked toward it. " +
                "\nThe Door is around 5 meters tall and it is impossible to open without a key.", "The Door is way too big compare to other tunnels.", stick1, IronKey);
            Room castleChamber = new Room("First Floor Chamber", " The inside chamber is really big with torches on the two sides. ", "You opened the door and passed through.", null);
            Room smallWareHouse = new Room("Small Ware House", "The room conatins many boxes and smell very bad.", "You can see a small room with some boxes inside.", null);
            Room room1 = new Room("Big Hole", "There is no light in the room. You can not see anything.", "There is a small tunnel and you can barely walk in.", mini1, paper1);
            Room room2 = new Room("Banquet hall", "You see a tiny room with a monster in the front.", "The way is going downstars and you can see subtle light coming from the room.", mini2, dagger);

            Room courtYard = new Room("CourtYard-Front", "There are still no living plants here, only black dust and rocks. " +
                "\nBut a tree? no a Puppet is standing on the ground.", "You kept walking and found there is a central Courtyard.", mini3, null);
            Room courtYard2 = new Room("CourtYard-Back", "As you walking, The sky is getting darker. " +
                "\nYou can see a black dot flying in the sky.", "There is a small path behind the Huge Rock.", miniBoss, bottleWater);

            Room darkChamberGate = new Room("Dark Chamber", "You can see there is a big Golden Door that blocks you way.", "You finally find the building where 'Alberto' lives.", mini4, bottleWater1);
            Room darkChamberGateFront = new Room("Dark Chamber", "The gold door needs a gold key to open it.", "You walked closer to the Door.", mini4, axe1, goldKey);
            Room darkChamberInside = new Room("First Floor Chamber", " The dark chamber is bigger than the chamber before, and there are only small blue flames in the air.", "You opened the door and passed through.", armor);

            Room darkChamber1 = new Room("First Floor Chamber", " With blue flames, you can barely see the room. There are many statues standing near the walls.", "**A unkown chamber**", guard, ironStick);
            Room darkChamber2 = new Room("First Floor Chamber", " With blue flames, you can barely see the room. There are many statues standing near the walls.", "A unkown chamber", guard1, armor1);

            Room darkChamber3 = new Room("First Floor Chamber", " With blue flames, you can barely see the room. There are many statues standing near the walls.", "A unkown chamber", guard2, ironStick1);
            Room darkChamber4 = new Room("First Floor Chamber", " With blue flames, yous see beautiful sliver statues. ", "**A chamber with brighter lights!**", eliteGuard1, armor2);
            Room darkChamber5 = new Room("First Floor Chamber", " With blue flames, yous see beautiful golden statues. ", "**A chamber with golden lights!**", eliteGuard2, null);

            Room darkChamber6 = new Room("First Floor Chamber", " With blue flames, yous see a big golden statue in the center. ", "**A chamber with bigger golden lights!**", eliteGuard3, purpWeapon);
            Room darkChamber7 = new Room("First Floor Chamber", " With blue flames, yous see a big golden statue in the center. ", "A chamber with no lights!", eliteGuard3, purpArmour);

            Room darkChamber8 = new Room("First Floor Chamber", " With blue flames, yous see a huge white statue in the center. ", "**A chamber with colorful lights!**", eliteGuard4, purpWeapon1);
            Room darkChamber9 = new Room("First Floor Chamber", " With blue flames, yous see a huge black statue in the center. ", "A chamber with black lights!", eliteGuard5, purpArmour1);

            Room nest = new Room("The Nest", " 'Alberto' is sleeping. But you footsteps sound awakes him up! ", "You finally found the nest.", professor, null, diamondKey);


            castle.leftRoom = frontYard;
            frontYard.rightRoom = castleGate;
            castleGate.leftRoom = castleChamberFront;
            castleChamberFront.leftRoom = castleChamber;
            castleChamber.leftRoom = room1;
            castleChamber.rightRoom = room2;
            room2.leftRoom = courtYard;
            courtYard.rightRoom = courtYard2;
            courtYard2.leftRoom = darkChamberGate;
            darkChamberGate.rightRoom = darkChamberGateFront;
            darkChamberGateFront.leftRoom = darkChamberInside;

            darkChamberInside.leftRoom = darkChamber1;
            darkChamberInside.rightRoom = darkChamber2;

            darkChamber1.leftRoom = darkChamber3;
            darkChamber1.rightRoom = darkChamber4;
            darkChamber4.rightRoom = darkChamber5;

            darkChamber5.leftRoom = darkChamber6;
            darkChamber5.rightRoom = darkChamber7;

            darkChamber6.leftRoom = darkChamber8;
            darkChamber6.rightRoom = darkChamber9;
            darkChamber8.rightRoom = nest;




            nest.previousRoom = darkChamber8;
            darkChamber9.previousRoom = darkChamber6;
            darkChamber8.previousRoom = darkChamber6;
            darkChamber6.previousRoom = darkChamber5;
            darkChamber7.previousRoom = darkChamber5;
            darkChamber5.previousRoom = darkChamber4;
            darkChamber3.previousRoom = darkChamber1;
            darkChamber4.previousRoom = darkChamber1;
            darkChamber1.previousRoom = darkChamberInside;
            darkChamber2.previousRoom = darkChamberInside;
            darkChamberInside.previousRoom = darkChamberGateFront;
            darkChamberGateFront.previousRoom = darkChamberGate;
            darkChamberGate.previousRoom = courtYard2;
            courtYard2.previousRoom = courtYard;
            courtYard.previousRoom = room2;
            room1.previousRoom = castleChamber;
            room2.previousRoom = castleChamber;
            castleChamber.previousRoom = castleChamberFront;
            castleChamberFront.previousRoom = castleGate;
            castleGate.previousRoom = frontYard;
            frontYard.previousRoom = castle;

            // Game.exe
            void GameStart()
            {
                Console.WriteLine("Enter your hero's name: ");
                string playerName = Console.ReadLine();

                Hero player = new Hero(playerName, "", "", 0, 1, true, null, null);
                player.Print();
                Console.WriteLine("You are a young teenage living in a beautiful village called Room206. " +
                    "\nOne day a monster called 'Alberto' came. " +
                    "\nHe pillaged the village and took away many of the villagers." +
                    "\n\n------You want to save them. Therefore, you Pack your bags and head to the castle on a road of no return." +
                    "\nAfter few days of walking, you finally reached the 'Alberto' 's territory.");
                Enter(castle, player, castle.GetCharacter);
            }
            GameStart();
        }
    }
}