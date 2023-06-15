using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;
using TMPro;

public class TestCharacters : MonoBehaviour
{
    public TMP_FontAsset tempFont;

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
        Character Me = CharacterManager.instance.CreateCharacter("Me");
        Character Ben = CharacterManager.instance.CreateCharacter("Benjamin");

        List<string> lines = new List<string>()
        {
            "Hi!",
            "This is a line",
            "And another",
            "And{wa 2} a last one"
        };
        yield return Mifim.Say(lines);

        Mifim.SetNameColor(Color.red);
        Mifim.SetDialogueColor(Color.red);

        yield return Mifim.Say(lines);

        Mifim.ResetConfigurationData();

        yield return Mifim.Say(lines);

        lines = new List<string>()
        {
            "I am {c}Hui",
            "Hello"
        };

        yield return Me.Say(lines);

        yield return Ben.Say("Just a simple line.{a} It is a simple line");

        Debug.Log("Finished");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
