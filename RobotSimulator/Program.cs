using RobotSimulator;

// Create a command intepreter and monitor for commands

internal class Program
{
    private static void Main(string[] args)
    {
        var interpreter = new CommandInterpreter();

        interpreter.CommandLoop();
    }
}