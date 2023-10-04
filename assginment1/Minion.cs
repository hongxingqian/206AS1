using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    internal class Minion : Character
    {
        public Minion(string a_sName, string a_sDescription, string a_sConversation, Item a_iKey) : base(a_sName, a_sDescription, a_sConversation, a_iKey)
        {

        }
        public Minion(string a_sName, string a_sDescription, string a_sConversation, int a_nLevel, int a_iRelationship, Boolean a_bDeadLive, Item a_iDropableItem, Item a_iKey) 
            : base(a_sName, a_sDescription, a_sConversation, a_nLevel, a_iRelationship, a_bDeadLive, a_iDropableItem, a_iKey)
        {

        }
    }
}
