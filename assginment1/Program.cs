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
                bool isPlayerTurn = true;
                bool isTypeWrong = false;
                while (a_cPlayer.HP > 0 && a_cEnemy.HP > 0)
                {
                    // Player turn
                    if (!isTypeWrong) {
                        Console.WriteLine("");
                        Console.WriteLine("-- Player Turn --");
                        Console.WriteLine("Player Hp - " + a_cPlayer.HP + ". Enemy HP - " + a_cEnemy.HP);
                    }

                    Console.WriteLine("Enter 'a' to attack or 'h' to heal. ");

                    string choice = Console.ReadLine();

                        if (choice == "a")
                        {

                            a_cPlayer.Fight(a_cEnemy);
                            Console.WriteLine("");
                            Console.WriteLine("Player attack enemy and deals " + (a_cPlayer.Attack - a_cEnemy.Defense) + " damage!");
                            isPlayerTurn = false;

                        }
                        else if (choice == "h")
                        {
                            a_cPlayer.Healing();
                            Console.WriteLine("");
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
                    Console.WriteLine("You Win!");
                    Console.WriteLine("");
                    Console.WriteLine("Level Up: Lv " + a_cPlayer.Level + " --> " + "Lv " + (a_cPlayer.Level + 1));
                    a_cPlayer.Levelup();
                    a_cPlayer.Print();
                    a_cEnemy.Death();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("You Lose!");
                }
            }

            // Enter function to allow players to go to rooms.
            void Enter(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter) {
                a_rLocation.description();
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
            //void EnterDecision(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter)
            //{
            //    Console.WriteLine("It seems no one is here.");
            //    bool isTypeCorrect = false;
            //    Random lootChance = new Random();
            //    int var = lootChance.Next(4);
            //    Console.WriteLine("Enter 'S' to search around or 'M' to move around. ");
            //    string choice = Console.ReadLine();
            //    while (isTypeCorrect = false)
            //    {
            //        if (choice == "S") 
            //        { 
            //            if (var == 0) 
            //            {
            //                Console.WriteLine("Congratulate! You find a Loot!");
            //                isTypeCorrect = true;
            //            }
            //            else 
            //            {
            //                Console.WriteLine("Sadly, you find nothing.");
            //                isTypeCorrect = true;
            //            }

            //        }
            //        else if (choice == "M")
            //        {
            //            Console.WriteLine("You move around to see if there is new path.");
            //            isTypeCorrect = true;
            //        }
            //        else
            //        {
            //            Console.WriteLine("Invalid Input! Try new actions again!");
            //        }
            //    }

            //    if (a_rLocation.Left != null)
            //    {
            //        Console.WriteLine("");
            //        Console.WriteLine("There is a LEFT path that you can go next:");
            //        a_rLocation.Left.description();
            //    }
            //    if (a_rLocation.Right != null)
            //    {
            //        Console.WriteLine("");
            //        Console.WriteLine("There is a RIGHT path that you can go next:");
            //        a_rLocation.Right.description();
            //    }
            //    Console.WriteLine("Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
            //    WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);

            //}
            // Helper function to choose which path to go without encouter.
            void EnterDecision(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter)
            {
                Console.WriteLine("It seems no one is here.");
                bool isTypeCorrect = false;
                Random lootChance = new Random();
                int var = lootChance.Next(4);

                while (!isTypeCorrect)
                {
                    Console.WriteLine("Enter 'S' to search around or 'M' to move around. ");
                    string choice = Console.ReadLine();

                    switch (choice.ToUpper())
                    {
                        case "S":
                            if (var == 0)
                            {
                                Console.WriteLine("Congratulate! You find a Loot!");
                            }
                            else
                            {
                                Console.WriteLine("Sadly, you find nothing.");
                            }
                            isTypeCorrect = true;
                            break;

                        case "M":
                            Console.WriteLine("You move around to see if there is a new path.");
                            isTypeCorrect = true;
                            break;

                        default:
                            Console.WriteLine("Invalid Input! Try new actions again!");
                            break;
                    }
                }

                ShowPaths(a_rLocation);
                Console.WriteLine("Enter 'R' to GO RIGHT or 'L' to GO LEFT, or 'B' to GO BACK. ");
                WaitingValidInput(a_rLocation, a_hPlayer, a_cCharacter);
            }

            // helper function to show possible paths
            void ShowPaths(Room room)
            {
                if (room.Left != null)
                {
                    Console.WriteLine("\nThere is a LEFT path that you can go next:");
                    room.Left.description();
                }
                if (room.Right != null)
                {
                    Console.WriteLine("\nThere is a RIGHT path that you can go next:");
                    room.Right.description();
                }
            }


            void WaitingValidInput(Room a_rLocation, Hero a_hPlayer, Character a_cCharacter)
            {
                // 0 => go right, 1=> go left, 2 => go back.
                int direction = -1;

                while (direction == -1)
                {
                    string choice = Console.ReadLine().ToUpper();  // Making the input case-insensitive

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
                    Enter(a_rLocation.Right, a_hPlayer, a_rLocation.Right.GetCharacter);
                }
                else if (direction == 1)
                {
                    Enter(a_rLocation.Left, a_hPlayer, a_rLocation.Left.GetCharacter);
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
                        Console.WriteLine(a_cCharacter.Description);
                        Console.WriteLine("Enter 't' to talk or 'e' to leave. ");
                        string choice = Console.ReadLine();
                        if (choice == "t")
                        {
                            Combat(a_hPlayer, a_cCharacter);
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
                        Console.WriteLine(a_cCharacter.Description);
                        Console.WriteLine("Enter 'a' to attack or 'e' to escape. ");
                        string choice = Console.ReadLine();
                        if (choice == "a")
                        {
                            Combat(a_hPlayer, a_cCharacter);
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



            Human citizen = new Human("Noya","A skinny short girl is looking at you.");
            Hero player = new Hero("max", "A nice guy.", 3, 1, true);
            Boss professor = new Boss("Alberto", "A big giant ulgy dragon.", 15, 1, true);
            Minion mini1 = new Minion("tao", "A stupid goblin.");

            Room castle = new Room("Castle", "You see a big and old castle in front of you.");
            Room castleGate = new Room("castleGate", "You see a big iron gate blocks your path and a citizen is crying in the front.",citizen);
            Room room1 = new Room("Room1", "There is no light in the room. You can not see anything.", mini1);
            Room room2 = new Room("Room2","You see a tiny room with a monster in the front.", mini1);
            castle.m_Left = castleGate;
            castleGate.m_Left = room1;
            castleGate.m_Right = room2;
            castleGate.m_previous = castle;


            player.Print();
            citizen.Print();
            mini1.Print();
            professor.Print();
            Enter(castle, player, castle.GetCharacter);
            // Combat(player, mini1);
        }
    }
}