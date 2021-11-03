using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.ConsoleClient
{
    class ConsoleAPI
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public (string turnType, string turnParameters) ReadTurnData()
        {
            var turnString = Read().ToUpper();

            var turnType = turnString.Split(' ')[0];
            var turnParameters = turnString.Split(' ')[1];

            return (turnType, turnParameters);
        }
    }
}
