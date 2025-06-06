using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IMusicalButton
    {
        void OnPress();
        int Score { get; }
        bool IsMissed(int yPosition);

    }
}
