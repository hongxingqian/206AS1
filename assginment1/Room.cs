using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    internal class Room
    {
        private string m_sName = "Room";
        private string m_sDescription = "";
        private string m_sPreDescription = "";
        private Character m_cCharacter = null;
        private Item m_iItem = null;
        private Item m_iKeyRequire = null;
        public Room previousRoom = null;
        public Room leftRoom = null;
        public Room rightRoom = null;


        // A room that has a Character but does not require a key.
        public Room(string a_sName, string a_sDescription, string a_sPreDescription, Character a_cCharacter, Item a_iItem)
        {
            m_sName = a_sName;
            m_sDescription = a_sDescription;
            m_sPreDescription = a_sPreDescription;
            m_cCharacter = a_cCharacter;
            m_iItem = a_iItem;
        }
        // A room that does not require a key and does not have a Character.
        public Room(string a_sName, string a_sDescription, string a_sPreDescription, Item a_iItem)
        {
            m_sName = a_sName;
            m_sDescription = a_sDescription;
            m_sPreDescription = a_sPreDescription;
            m_iItem = a_iItem;
        }
        // A room that has a Character and requires a key.
        public Room(string a_sName, string a_sDescription, string a_sPreDescription, Character a_cCharacter, Item a_iItem, Item a_iKeyRequire)
        {
            m_sName = a_sName;
            m_sDescription = a_sDescription;
            m_sPreDescription = a_sPreDescription;
            m_cCharacter = a_cCharacter;
            m_iItem = a_iItem;
            m_iKeyRequire = a_iKeyRequire;
        }

        // A room that does not have a Character but requires a key.
        public Room(string a_sName, string a_sDescription, string a_sPreDescription, Item a_iItem, Item a_iKeyRequire)
        {
            m_sName = a_sName;
            m_sDescription = a_sDescription;
            m_sPreDescription = a_sPreDescription;
            m_iItem = a_iItem;
            m_iKeyRequire = a_iKeyRequire;
        }
        public Room Previous
        {
            get { return previousRoom; }
        }
        public Room Left
        {
            get { return leftRoom; }
        }
        public Room Right
        {
            get { return rightRoom; }
        }
        public void Description()
        {
            Console.WriteLine(m_sDescription);
        }
        public void PreDescription()
        {
            Console.WriteLine(m_sPreDescription);
        }
        public void Death()
        {
            m_cCharacter = null;
        }
        public Character GetCharacter
        {
            get { return m_cCharacter; }
        }
        public Item Item 
        {
            get { return m_iItem; } 
        }
        public void DeleteLoot()
        {
            m_iItem = null;
        }

        public void DeleteKey()
        {
            m_iKeyRequire = null;
        }

        public string KeyName()
        {
            return m_iKeyRequire.LootName;
        }

        // check if the inventory has the needed key.
        public bool DoorNeedsKey(Item[] inventory)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if(m_iKeyRequire == null)
                {
                    return true;
                }
                else if (inventory[i] != null && m_iKeyRequire == inventory[i])
                {
                    return true;
                }
            }
            return false;
        }
    }

    
}
