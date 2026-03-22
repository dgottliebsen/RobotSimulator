using NUnit.Framework.Internal;
using RobotSimulator;

namespace RobotSimulatorTest
{
    /// <summary>
    /// Unit tests for the robot movement class and the command interpreter class
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Test that robot ignores commands until it is placed on the table
        /// </summary>
        [Test]
        public void TestRobotIgnoresCommandsUntilPlaced()
        {
            var movement = new RobotMovement();

            movement.Left();
            movement.Right();
            movement.Move();

            Assert.IsNull(movement.Report());

            movement.Place("0,0,NORTH");

            // Report and other commands should now work

            Assert.IsTrue(movement.Report() == "0,0,NORTH");

            movement.Move();
            movement.Right();
            movement.Move();
            movement.Left();
            movement.Move();

            Assert.IsTrue(movement.Report() == "1,2,NORTH");

            // A new place command overrides previous location and direction

            movement.Place("4,4,SOUTH");

            Assert.IsTrue(movement.Report() == "4,4,SOUTH");
        }

        /// <summary>
        /// Test that robot ignores commands until it is placed on the table
        /// </summary>
        [Test]
        public void TestRobotPlaceParameters()
        {
            var movement = new RobotMovement();
 
            movement.Place("0,0,NORTH");
            Assert.IsTrue(movement.Report() == "0,0,NORTH");

            // Place commands with invalid parameters should be ignored and robot location and facing unchanged

            movement.Place("5,0,NORTH");
            Assert.IsTrue(movement.Report() == "0,0,NORTH");

            movement.Place("0,5,NORTH");
            Assert.IsTrue(movement.Report() == "0,0,NORTH");

            movement.Place("0,0,NORTHEAST");
            Assert.IsTrue(movement.Report() == "0,0,NORTH");
        }

        /// <summary>
        /// Test that robot stops if commanded to move off the edge of the table
        /// </summary>
        [Test]
        public void TestRobotRespectsTableEdges()
        {
            var movement = new RobotMovement();

            // Test North edge

            movement.Place("0,4,NORTH");
            movement.Move();

            Assert.IsTrue(movement.Report() == "0,4,NORTH");

            // Test South edge

            movement.Place("0,0,SOUTH");
            movement.Move();

            Assert.IsTrue(movement.Report() == "0,0,SOUTH");

            // Test East edge

            movement.Place("4,0,EAST");
            movement.Move();

            Assert.IsTrue(movement.Report() == "4,0,EAST");

            // Test North edge

            movement.Place("0,0,WEST");
            movement.Move();

            Assert.IsTrue(movement.Report() == "0,0,WEST");
        }

        /// <summary>
        /// Test turning and ensure directions are in correct order and that 4 consecutive turns brings robot back to original facing direction in same location
        /// </summary>
        [Test]
        public void TestRobotDirections()
        {
            var movement = new RobotMovement();

            // Test turning left and ensure directions are in order and wrap back to the starting direction

            movement.Place("2,2,NORTH");
            movement.Left();

            Assert.IsTrue(movement.Report() == "2,2,WEST");

            movement.Left();

            Assert.IsTrue(movement.Report() == "2,2,SOUTH");

            movement.Left();

            Assert.IsTrue(movement.Report() == "2,2,EAST");

            movement.Left();

            Assert.IsTrue(movement.Report() == "2,2,NORTH");

            // Test turning right and ensure directions are in order and wrap back to the starting direction

            movement.Place("2,2,NORTH");
            movement.Right();

            Assert.IsTrue(movement.Report() == "2,2,EAST");

            movement.Right();

            Assert.IsTrue(movement.Report() == "2,2,SOUTH");

            movement.Right();

            Assert.IsTrue(movement.Report() == "2,2,WEST");

            movement.Right();

            Assert.IsTrue(movement.Report() == "2,2,NORTH");
        }

        /// <summary>
        /// Test a sequence of all robot command types 
        /// </summary>
        [Test]
        public void TestRobotMovement()
        {
            var movement = new RobotMovement();

            // Test repeated robot movements and turns 

            movement.Place("2,2,NORTH");
            movement.Right();
            movement.Move();
            movement.Move();

            Assert.IsTrue(movement.Report() == "4,2,EAST");

            // Now at edge - check edge is respected

            movement.Move();

            Assert.IsTrue(movement.Report() == "4,2,EAST");

            // Turn and continue

            movement.Right();

            Assert.IsTrue(movement.Report() == "4,2,SOUTH");

            movement.Move();

            Assert.IsTrue(movement.Report() == "4,1,SOUTH");

            movement.Move();

            Assert.IsTrue(movement.Report() == "4,0,SOUTH");

            movement.Right();

            Assert.IsTrue(movement.Report() == "4,0,WEST");

            movement.Move();
            movement.Move();

            Assert.IsTrue(movement.Report() == "2,0,WEST");

            movement.Right();

            Assert.IsTrue(movement.Report() == "2,0,NORTH");

            movement.Move();
            movement.Move();
            movement.Move();

            Assert.IsTrue(movement.Report() == "2,3,NORTH");
        }

        /// <summary>
        /// Test that the command interpreter coprrectly parses and executes each of the possible robot commands
        /// </summary>
        [Test]
        public void TestCommandInterpreted()
        {
            var intepreter = new CommandInterpreter();

            intepreter.Execute("PLACE 1,1,NORTH");
            intepreter.Execute("MOVE");
            intepreter.Execute("RIGHT");
            intepreter.Execute("MOVE");
            intepreter.Execute("LEFT");
            intepreter.Execute("MOVE");

            Assert.IsTrue(intepreter.Execute("REPORT") == "2,3,NORTH");
        }
    }
}