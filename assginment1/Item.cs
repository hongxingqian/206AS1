using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    public class Item
    {
        protected string m_sName;
        protected Character m_cBelonger = null;
        protected float m_fAttak;
        protected float m_fDefense;
        protected float m_fHP;

        public Item(string a_sName, Character a_cBelonger, float a_fAttack, float a_fDefense, float a_fHP)
        {
            m_sName = a_sName;
            m_cBelonger = a_cBelonger;
            m_fAttak = a_fAttack;
            m_fDefense = a_fDefense;
            m_fHP = a_fHP;
        }

        public Item(string a_sName, float a_fAttack, float a_fDefense, float a_fHP)
        {
            m_sName = a_sName;
            m_fAttak = a_fAttack;
            m_fDefense = a_fDefense;
            m_fHP = a_fHP;

        }

        public string LootName
        {
            get { return m_sName; }
        }

        public Character Belonger
        {
            get { return m_cBelonger; }
        }
        public float Attack
        {
            get { return m_fAttak; }
        }
        public float Defense
        {
            get { return m_fDefense; }
        }
        public float HP
        {
            get { return m_fHP; }
        }
        public void PrintStatus()
        {
            //Console.WriteLine("Item Name: " + m_sName);
            Console.WriteLine("Attack: " + m_fAttak);
            Console.WriteLine("Defense: " + m_fDefense);
            Console.WriteLine("HP: " + m_fHP);
        }

        void MakeDropItemDisapper()
        {
            this.m_sName = null;
            this.m_cBelonger = null;
            this.m_fAttak = 0;
            this.m_fDefense = 0;
            this.m_fHP = 0;

        }

    }
}
