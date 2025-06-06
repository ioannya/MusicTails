using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ButtonGenerator
    {
        private readonly Random _rand = new Random();

        // Перегрузка 1: без параметров
        public IMusicalButton GenerateButton()
        {
            return GenerateButton(false);
        }

        // Перегрузка 2: с параметром
        public IMusicalButton GenerateButton(bool onlyLong)
        {
            if (onlyLong)
                return new LongButton();
            else
                return _rand.Next(2) == 0 ? (IMusicalButton)new ShortButton() : new LongButton();
        }
    }
}
