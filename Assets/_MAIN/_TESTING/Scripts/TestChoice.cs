using COMMANDS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class TestChoice : MonoBehaviour
    {
        public string displayTitle = "";

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
                InputScreen.Show(displayTitle);

            if (Input.GetKeyDown(KeyCode.B) && InputScreen.isWaitingForUserInput)
            {
                InputScreen.instance.Accept();

                print($"{InputScreen.currentInput}");
            }
        }
    }
}

