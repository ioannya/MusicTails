using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class MusicalButtonBase : IMusicalButton
    {
        protected int _yPosition;
        public int Score { get; protected set; }

        public int YPosition => _yPosition;

        public void MoveDown(int delta) => _yPosition += delta;

        public abstract void OnPress();

        public virtual bool IsMissed(int clickZoneY) => _yPosition > clickZoneY;
    }
}
