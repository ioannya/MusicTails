using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IScoreSavable
    {
        void Save(ScoreEntry[] entries);
        ScoreEntry[] Load();
    }
}
