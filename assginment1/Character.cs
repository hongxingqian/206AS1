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
        public Character(string a_sName, string a_sDescription, string a_sConversation)
        {
            Init(a_sName, a_sDescription, a_sConversation, 1, 0, true);
        }
        public Character(string a_name, string a_sDescription, string a_sConversation,  int a_nLevel, int m_iRelationship, Boolean a_bDeadLive)
        {
            Init(a_name, a_sDescription, a_sConversation, a_nLevel, m_iRelationship, a_bDeadLive);
        }

        public virtual void Init (string a_sName, string a_sDescription, string a_sConversation, int a_nLevel,int a_iRelationship, Boolean a_bDeadLive)
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
            }
            else
            {
                Console.WriteLine("error, relationship can only be 1 or 0.");
            }
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
            if ((m_fHP += this.Heal) <  m_fMaxHP) 
            {
                m_fHP += this.Heal;
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
