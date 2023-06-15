using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DIALOGUE
{
    public class DL_COMMAND_DATA
    {
        public List<Command> commands;
        public const char COMMANDSPLITTER_ID = ',';
        public const char ARGUMENTSCONTAINER_ID = '(';
        private const string WAITCOMMAND_ID = "[wait]";

        public struct Command
        {
            public string name;
            public string[] arguments;
            public bool waitForCompletion;
        }

        public DL_COMMAND_DATA(string rawCommands)
        {
            commands = RipCommands(rawCommands);
        }

        private List<Command> RipCommands(string rawCommand)
        {
            string[] data = rawCommand.Split(COMMANDSPLITTER_ID, System.StringSplitOptions.RemoveEmptyEntries);
            List<Command> result = new List<Command>();

            foreach (string command in data)
            {
                Command cmd = new Command();

                int index = command.IndexOf(ARGUMENTSCONTAINER_ID);
                cmd.name = command.Substring(0, index).Trim();

                if (cmd.name.ToLower().StartsWith(WAITCOMMAND_ID))
                {
                    cmd.name = cmd.name.Substring(WAITCOMMAND_ID.Length);
                    cmd.waitForCompletion = true;
                }
                else
                    cmd.waitForCompletion = false;

                cmd.arguments = GetArgs(command.Substring(index + 1, command.Length - index - 2));
                result.Add(cmd);
            }

            return result;
        }

        private string[] GetArgs(string args)
        {
            List<string> argList = new List<string>();
            StringBuilder currentArg = new StringBuilder();

            bool inQuotes = false;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == '"')
                {
                    inQuotes = !inQuotes;
                    continue;
                }

                if (!inQuotes && args[i] == ' ')
                {
                    argList.Add(currentArg.ToString());
                    currentArg.Clear();
                    continue;
                }

                currentArg.Append(args[i]);
            }

            if (currentArg.Length > 0)
            {
                argList.Add(currentArg.ToString());
            }

            return argList.ToArray();
        }
    }
}