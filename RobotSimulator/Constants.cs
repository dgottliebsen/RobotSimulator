using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotSimulator
{
    /// <summary>
    /// Constant definitions for the robot simulator
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Dimensions of the table surface
        /// </summary>
        public const int TableWidth = 5;
        public const int TableHeight = 5;

        /// <summary>
        /// Commands recognised by the simulation
        /// </summary>

        public enum Commands : int
        {
            Place = 0,
            Move = 1,
            Left = 2,
            Right = 3,
            Report = 4
        }

        public static readonly string[] CommandNames = ["PLACE ", "MOVE", "LEFT", "RIGHT", "REPORT"]; // Place command must have a space after it to be valid

        /// <summary>
        /// Directions recognised by the simulation
        /// </summary>

        public enum Directions : int
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }

        public static readonly string[] DirectionNames = [ "NORTH", "EAST", "SOUTH", "WEST" ];
    }
}
