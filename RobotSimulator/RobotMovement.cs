using static RobotSimulator.Constants;

namespace RobotSimulator
{
    public class RobotMovement
    {
        /// <summary>
        /// Location and current facing direction of the robot
        /// </summary>
        private int currentXPos = -1;
        private int currentYPos = -1;
        private int currentDirection = -1;

        /// <summary>
        /// Interpret the parameters of the Place command and perform the action
        /// </summary>
        /// <param name="parameters">Parameters after the Place command</param>
        public void Place(string parameterString)
        {
            int newXPos = -1;
            int newYPos = -1;
            int newDirection = -1;

            // Get the parameters of the command and parse them

            try
            {
                string[] parameters = parameterString.Split(',');

                if (parameters.Length != 3) return;

                // Parse the location parameters

                if (int.TryParse(parameters[0], out newXPos) == false) return;

                if (int.TryParse(parameters[1], out newYPos) == false) return;

                // Parse the direction parameter 

                foreach (Directions direction in Enum.GetValues(typeof(Directions)))
                {
                    if (string.Compare(parameters[2], DirectionNames[(int)direction], true) == 0)
                    {
                        newDirection = (int)direction;
                        break;
                    }
                }

                // If we have parsed correct values then update the robots position

                if (newXPos >= 0 && newXPos < Constants.TableWidth && newYPos >= 0 && newYPos < Constants.TableHeight && newDirection >= 0)
                {
                    currentXPos = newXPos;
                    currentYPos = newYPos;
                    currentDirection = newDirection;
                }
            }
            catch
            {
                // Invalid parameters etc are ignored here
            }
        }

        /// <summary>
        /// Move the robot in the current facing direction, ensuring it doesn't fall off the table
        /// </summary>
        public void Move()
        {
            // Only perform command if robot is placed on table

            if (IsRobotPlacedYet() == true)
            {
                // Handle robot movement in current facing direction. Check that robot will not go over the edge before performing the movement.

                switch (currentDirection)
                {
                    case (int)Directions.North:
                        if (currentYPos < (TableHeight - 1))
                        {
                            currentYPos += 1;
                        }

                        break;

                    case (int)Directions.South:
                        if (currentYPos > 0)
                        {
                            currentYPos -= 1;
                        }

                        break;

                    case (int)Directions.East:
                        if (currentXPos < (TableWidth - 1))
                        {
                            currentXPos += 1;
                        }

                        break;

                    case (int)Directions.West:
                        if (currentXPos > 0)
                        {
                            currentXPos -= 1;
                        }

                        break;

                }
            }
        }

        /// <summary>
        /// Rotate the robot to the left
        /// </summary>
        public void Left()
        {
            // Only perform command if robot is placed on table

            if (IsRobotPlacedYet() == true)
            {
                // Rotate the robot to the left

                currentDirection -= 1;

                // Check for wrapping around

                if (currentDirection < (int)Directions.North)
                {
                    currentDirection = (int)Directions.West;
                }
            }
        }

        /// <summary>
        /// Rotate the robot to the right
        /// </summary>
        public void Right()
        {
            // Only perform command if robot is placed on table

            if (IsRobotPlacedYet() == true)
            {
                // Rotate the robot to the right

                currentDirection += 1;

                // Check for wrapping around

                if (currentDirection > (int)Directions.West)
                {
                    currentDirection = (int)Directions.North;
                }
            }
        }

        /// <summary>
        /// Format the current location and facing direction
        /// </summary>
        /// <returns>The formatted values</returns>
        public string? Report()
        {
            string? result = null;

            // Only perform command if robot is placed on table

            if (IsRobotPlacedYet() == true)
            {
                // Format the current position and facing direction of the robot 

                result = string.Format("{0},{1},{2}", currentXPos, currentYPos, DirectionNames[currentDirection]);
            }

            return result;
        }

        /// <summary>
        /// Check whether robot has been placed on the table yet
        /// </summary>
        /// <returns>True if the robot is on the table</returns>
        private bool IsRobotPlacedYet()
        {
            return currentXPos >= 0 && currentYPos >= 0;
        }
    }
}
