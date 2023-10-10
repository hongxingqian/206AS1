using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    public class Character
    {
        protected const int m_iMaxInventorySize = 100;
        protected Item[] m_iInventory = new Item[m_iMaxInventorySize];
        protected Item m_iDropableItem;
        protected Item m_iKey = null;
        // check th relationship of the character, 1 = enemy, 0 = friendly.
        protected int m_iRelationship = 0;
        // check if the character died or still alive.
        protected bool m_bDeadLive;
        protected string m_sName = "";
        protected string m_sDescription = "";
        protected string m_sConversation = "";
        protected float m_fHP = 50.0f;
        protected float m_fMaxHP = 50.0f;
        protected float m_fDefense = 1.0f;
        protected float m_fAttack = 10.0f;
        protected int m_nLevel = 0;
        protected float m_fHeal = 1.0f;
        public Random random = new Random();

        // A Friendly Character that does not have dropable loot.
        public Character(string a_sName, string a_sDescription, string a_sConversation, Item a_iKey)
        {
            Init(a_sName, a_sDescription, a_sConversation, 1, 0, true, null, a_iKey);
        }
        // A normal Character
        public Character(string a_name, string a_sDescription, string a_sConversation,  int a_nLevel, int m_iRelationship, Boolean a_bDeadLive, Item a_iDropableItem, Item a_iKey)
        {
            Init(a_name, a_sDescription, a_sConversation, a_nLevel, m_iRelationship, a_bDeadLive, a_iDropableItem, a_iKey);
        }

        public virtual void Init (string a_sName, string a_sDescription, string a_sConversation, int a_nLevel,int a_iRelationship, Boolean a_bDeadLive, Item a_iDropableItem, Item a_iKey)
        {
            if (a_iRelationship == 0 || a_iRelationship == 1)
            {
                m_sName = a_sName;
                m_fHP += a_nLevel;
                m_bDeadLive = a_bDeadLive;
                m_fMaxHP += a_nLevel;
                m_nLevel += a_nLevel;
                m_fAttack += m_nLevel;
                m_fDefense += m_nLevel;
                m_fHeal += m_nLevel / 2;
                m_iRelationship = a_iRelationship;
                m_sDescription = a_sDescription;
                m_sConversation = a_sConversation;
                m_iDropableItem = a_iDropableItem;
                m_iKey = a_iKey;
            }
            else
            {
                Console.WriteLine("error, relationship can only be 1 or 0.");
            }
        }

        public virtual bool IsBoss()
        {
            return false;
        }
        public Item DropKey()
        {
            return m_iKey;
        }
        public void Death()
        {
            m_bDeadLive = false;

        }
        public int Level
        {
            get { return m_nLevel; }
        }
        public float HP
        {
            get { return m_fHP; }
        }
        public float Defense
        { 
            get { return m_fDefense; } 
        }
        public float Attack
        {
            get { return m_fAttack; }
        }
        public float Heal
        {
            get { return m_fHeal; }
        }
        public string Name
        {
            get { return m_sName; }
        }
        public int Relationship
        {
            get { return m_iRelationship; }
        }

        public string Description
        {
            get { return m_sDescription; }
        }
        public bool DeathorLive
        {
            get { return m_bDeadLive; }
        }
        public string Conversation
        {
            get { return m_sConversation; }
        }

        public Item[] Inventory
        {
            get { return m_iInventory; }
        }

        public void Fight (Character a_character)
        {
            a_character.Fight(this.Attack); 
        }
        protected void Fight (float a_fIncomingDamage)
        {
            float fCombatResult = m_fDefense - a_fIncomingDamage;
            if (fCombatResult < 0)
            {
                m_fHP += fCombatResult;
            }
        }
        public void Healing()
        {
            if ((m_fHP += this.Heal) <=  m_fMaxHP) 
            {
                m_fHP += this.Heal;
            }
            else
            {
                m_fHP = m_fMaxHP;
            }
        }
        public void Resting()
        {
            if ((m_fHP += 20) <= m_fMaxHP)
            {
                m_fHP += 20;
            }
            else
            {
                m_fHP = m_fMaxHP;
            }
        }


        public void Levelup()
        {
            m_fHP += 5;
            m_fMaxHP += 5;
            m_nLevel += 1;
            m_fAttack += 3;
            m_fDefense += 2;
            m_fHeal += 1 / 2;
        }

        public Item DroppableItem
        {
            get { return m_iDropableItem; }
        }

        public void DeleteItem()
        {
            m_iDropableItem = null;
        }

        public void DeleteKey()
        {
            m_iKey = null;
        }

        public Item DropLoot()
        {
            // If the character has a droppable item, then drop it
            if (m_iDropableItem != null)
            {
                Random lootChance = new Random();
                int var = lootChance.Next(3);
                if (var == 0) { return m_iDropableItem; }
                else
                {
                    return null;
                }
            }

            else
            {
                return null;
            }
        }

        public void PrintInventory()
        {
            Console.WriteLine("Inventory Items:");
            bool isEmpty = true;

            for (int i = 0; i < m_iMaxInventorySize; i++)
            {
                if (m_iInventory[i] != null)
                {
                    Console.WriteLine("- " + m_iInventory[i].LootName);
                    isEmpty = false;
                }
            }

            if (isEmpty)
            {
                Console.WriteLine("Inventory is empty.");
            }
        }

        public void GainLoot(Item loot)
        {
            PutInInventory(loot);
            AddItemStatusToCharacter(loot);
        }

        public void PutInInventory(Item loot)
        {
            if(loot != null)
            {
                bool itemAdded = false;
                for (int i = 0; i < m_iMaxInventorySize; i++)
                {
                    if (m_iInventory[i] == null)
                    {
                        m_iInventory[i] = loot;
                        Console.WriteLine("You gained " + loot.LootName);
                        loot.PrintStatus();
                        itemAdded = true;
                        break;
                    }
                }
                if (!itemAdded)
                {
                    Console.WriteLine("\n-----Your Inventory is full!");
                }
            }
            else
            {
                Console.WriteLine("The Item you gain is null!");
            }
        }


        public void AddItemStatusToCharacter(Item loot)
        {
            if (IsItemInInventory(loot))
            {
                m_fMaxHP += loot.HP;
                m_fAttack += loot.Attack;
                m_fDefense += loot.Defense;
            }
            else
            {
                Console.WriteLine("The item " + loot.LootName + " is not in the inventory. Stats not updated.");
            }
        }

        private bool IsItemInInventory(Item loot)
        {
            for (int i = 0; i < m_iMaxInventorySize; i++)
            {
                if (m_iInventory[i] == loot)
                {
                    return true;
                }
            }
            return false;
        }

        public void DropFromInventory(Item loot)
        {
            if (RemoveFromInventory(loot))
            {
                SubtractItemStatusFromCharacter(loot);
            }
        }

        public bool RemoveFromInventory(Item loot)
        {
            bool itemRemoved = false;
            for (int i = 0; i < m_iMaxInventorySize; i++)
            {
                if (m_iInventory[i] == loot)
                {
                    Console.WriteLine("You dropped " + loot.LootName);
                    m_iInventory[i] = null;
                    itemRemoved = true;
                    break;
                }
            }
            if (!itemRemoved)
            {
                Console.WriteLine("Item not found in inventory!");
            }
            return itemRemoved;
        }

        public void SubtractItemStatusFromCharacter(Item loot)
        {
            if (IsItemInInventory(loot))
            {
                m_fMaxHP -= loot.HP;
                m_fAttack -= loot.Attack;
                m_fDefense -= loot.Defense;
            }
            else
            {
                Console.WriteLine("The item " + loot.LootName + " is not in the inventory. Stats not updated.");
            }
        }

        public void Print()
        {
            Console.WriteLine("");
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Level " + this.Level);
            Console.WriteLine("HP " + this.HP);
            Console.WriteLine("Attack " + this.Attack);
            Console.WriteLine("Defense " + this.Defense);
            Console.WriteLine("Heal Ability " + this.Heal);

        }
    }
}
