using DIALOGUE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class TestDialogueFiles : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;

        // Start is called before the first frame update
        void Start()
        {
            StartConversation();
        }

        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            //foreach (string line in lines)
            //{
            //    if(string.IsNullOrEmpty(line)) 
            //        continue;

            //    DIALOGUE_LINE dl = DialogueParser.Parse(line);

            //    for (int i = 0; i < dl.commandsData.commands.Count; i++)
            //    {
            //        DL_COMMAND_DATA.Command cmd = dl.commandsData.commands[i];
            //        Debug.Log($"Command [{i}] '{cmd.name}' has args [{string.Join(",", cmd.arguments)}]");
            //    }
            //}

            DialogueSystem.instance.Say(lines);
        }
    }
}
