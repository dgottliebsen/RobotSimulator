using static RobotSimulator.Constants;

namespace RobotSimulator
{
    public class CommandInterpreter
    {
        private readonly RobotMovement movement = new RobotMovement();

        /// <summary>
        /// Monitor stdin for robot commands
        /// </summary>
        public void CommandLoop()
        {
            string? line = Console.ReadLine();

            // While there are more lines to be read

            while (line != null)
            {
                // Execute the line and display any output the command produces

                string? output = Execute(line);

                if (output != null)
                {
                    Console.WriteLine(output);
                }

                // Read next line

                line = Console.ReadLine();
            }
        }

        /// <summary>
        /// Interpret the given command and call the appropriate function
        /// </summary>
        /// <param name="command">The command to perform</param>
        /// <returns>The output of the command if there is any</returns>
        public string? Execute(string command)
        {
            string? result = null;

            // Check for Place command which has a string followed by parameters after the command name

            if (command.StartsWith(CommandNames[(int)Commands.Place], StringComparison.OrdinalIgnoreCase))
            {
                // Place command is followed by parameters after the command name, so pass the remainder of the line after the command

                movement.Place(command.Substring(CommandNames[(int)Commands.Place].Length));
            }
            else if (string.Compare(command, CommandNames[(int)Commands.Move], true) == 0)
            {
                movement.Move();
            }
            else if (string.Compare(command, CommandNames[(int)Commands.Left], true) == 0)
            {
                movement.Left();
            }
            else if (string.Compare(command, CommandNames[(int)Commands.Right], true) == 0)
            {
                movement.Right();
            }
            else if (string.Compare(command, CommandNames[(int)Commands.Report], true) == 0)
            {
                result = movement.Report();
            }

            return result;
        }
    }
}
