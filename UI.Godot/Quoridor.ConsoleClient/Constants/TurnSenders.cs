using Quoridor.Model.PlayerTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.ConsoleClient.Constants
{
    static class TurnSenders
    {
        private static readonly Dictionary<string, Action<ConsolePlayer, string>> types = new()
        {
            ["MOVE"] = (player, @params) => 
            {
                var cell = CoordConvert.Cell(@params[0], @params[1]);
                player.OnMoveTurn(cell);
            },
            ["WALL"] = (player, @params) => 
            {
                var corner = CoordConvert.Cell(@params[0], @params[1]);
                bool isHorizontal = @params[2] == 'H';
                player.OnWallTurn(corner, isHorizontal);
            },
            ["JUMP"] = (player, @params) => 
            {
                var cell = CoordConvert.Cell(@params[0], @params[1]);
                player.OnMoveTurn(cell);
            },
        };

        public static Action<ConsolePlayer, string> ByType(string turnType) => types[turnType];
    }
}
