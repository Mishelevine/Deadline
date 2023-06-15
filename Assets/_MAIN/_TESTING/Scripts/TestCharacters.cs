using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;

public class TestCharacters : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Character Mifim = CharacterManager.instance.CreateCharacter("Mifim");
        //Character Mifim2 = CharacterManager.instance.CreateCharacter("Mifim");
        //Character Me = CharacterManager.instance.CreateCharacter("Me");
        //Character Hui = CharacterManager.instance.CreateCharacter("Hui");
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        Character Mifim = CharacterManager.instance.CreateCharacter("Mifim");

        List<string> lines = new List<string>()
        {
            "Hi!",
            "This is a line",
            "And another",
            "And a last one"
        };
        yield return Mifim.Say(lines);

        Debug.Log("Finished");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
