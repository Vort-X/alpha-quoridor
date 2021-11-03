using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.ConsoleClient.Constants
{
    static class CoordConvert
    {
        private static readonly Dictionary<char, int> cellX = new()
        {
            ['A'] = 1,
            ['B'] = 2,
            ['C'] = 3,
            ['D'] = 4,
            ['E'] = 5,
            ['F'] = 6,
            ['G'] = 7,
            ['H'] = 8,
            ['I'] = 9,
        };

        private static readonly Dictionary<char, int> wallX = new()
        {
            ['S'] = 1,
            ['T'] = 2,
            ['U'] = 3,
            ['V'] = 4,
            ['W'] = 5,
            ['X'] = 6,
            ['Y'] = 7,
            ['Z'] = 8,
        };

        public static Tuple<int, int> Cell(char x, char y)
        {
            return new Tuple<int, int>(cellX[x] - 1, y - '0' - 1);
        }

        public static Tuple<int, int> Wall(char x, char y)
        {
            return new Tuple<int, int>(wallX[x] - 1, y - '0' - 1);
        }
    }
}
