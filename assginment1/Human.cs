using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    internal class Human : Character
    {
        public Human(string a_sName, string a_sDescription, string a_sConversation) : base(a_sName, a_sDescription, a_sConversation)
        {

        }
        public Human(string a_sName, string a_sDescription,string a_sConversation, int a_nLevel, int a_iRelationship, Boolean a_bDeadLive) : base(a_sName, a_sDescription, a_sConversation, a_nLevel, a_iRelationship, a_bDeadLive)
        {

        }
    }
}
