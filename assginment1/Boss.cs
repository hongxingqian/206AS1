using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    public class Boss : Character
    {
        public Boss(string a_sName, string a_sDescription, string a_sConversation, Item a_iKey) : base(a_sName, a_sDescription, a_sConversation, a_iKey) 
        {

        }
        public Boss(string a_sName, string a_sDescription, string a_sConversation, int a_nLevel, int a_iRelationship, Boolean a_bDeadLive, Item a_iDropableItem, Item a_iKey) :
            base(a_sName, a_sDescription, a_sConversation, a_nLevel, a_iRelationship, a_bDeadLive, a_iDropableItem, a_iKey)
        {

        }

        public override void Init(string a_sName, string a_sDescription, string a_sConversation, int a_nLevel, int a_iRelationship, Boolean a_bDeadLive, Item a_iDropableItem, Item a_iKey)
        {
            if (a_iRelationship == 0 || a_iRelationship == 1)
            {
                m_sName = a_sName;
                m_fHP += a_nLevel + 20;
                m_bDeadLive = a_bDeadLive;
                m_fMaxHP += a_nLevel;
                m_nLevel += a_nLevel;
                m_fAttack += m_nLevel + 2;
                m_fDefense += m_nLevel + 1;
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
    }
}
