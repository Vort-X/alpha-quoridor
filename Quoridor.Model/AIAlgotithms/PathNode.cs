using Queridor.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Queridor.AIAlgotithms
{
    public class PathNode
    {
        public Cell Position { get; set; }
        public int PathLengthFromStart { get; set; }
        public PathNode CameFrom { get; set; }
        public int HeuristicEstimatePathLength { get; set; }
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }
    }
}
