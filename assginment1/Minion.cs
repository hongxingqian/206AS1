using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assginment1
{
    internal class Minion : Character
    {
        public Minion(string a_sName, string a_sDescription) : base(a_sName, a_sDescription)
        {

        }
        public Minion(string a_sName, string a_sDescription, int a_nLevel, int a_iRelationship, Boolean a_bDeadLive) : base(a_sName, a_sDescription, a_nLevel, a_iRelationship, a_bDeadLive)
        {

        }
    }
}
