using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LongButton : MusicalButtonBase
    {
        public override void OnPress() => Score = 2;
    }
}
