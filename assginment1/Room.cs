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
        private Character m_cCharacter;
        public Room m_previous = null;
        public Room m_Left = null;
        public Room m_Right = null;

        public Room(string a_sName, string a_sDescription, string a_sPreDescription, Character a_cCharacter)
        {
            m_sName = a_sName;
            m_sDescription = a_sDescription;
            m_sPreDescription = a_sPreDescription;
            m_cCharacter = a_cCharacter;

        }
        public Room(string a_sName, string a_sDescription, string a_sPreDescription)
        {
            m_sName = a_sName;
            m_sDescription = a_sDescription;
            m_sPreDescription = a_sPreDescription;
            m_cCharacter = null;
        }
        public Room Previous
        {
            get { return m_previous; }
        }
        public Room Left
        {
            get { return m_Left; }
        }
        public Room Right
        {
            get { return m_Right; }
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
    }

    
}
