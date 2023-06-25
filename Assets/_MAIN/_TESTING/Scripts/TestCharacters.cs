using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;
using TMPro;

public class TestCharacters : MonoBehaviour
{
    public TMP_FontAsset tempFont;

    private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

    // Start is called before the first frame update
    void Start()
    {
        Character Mifim = CharacterManager.instance.CreateCharacter("Mifim");
        Character Mifim2 = CharacterManager.instance.CreateCharacter("Mifim");
        Character Me = CharacterManager.instance.CreateCharacter("Me");
        Character Hui = CharacterManager.instance.CreateCharacter("Hui");
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        Character_Sprite Mifim = CreateCharacter("Миша Ефимов") as Character_Sprite;

        yield return new WaitForSeconds(1);

        Mifim.Animate("Hop");

        yield return new WaitForSeconds(1);

        Mifim.Animate("Shiver", true);

        yield return new WaitForSeconds(1);

        Mifim.Animate("Shiver", false);

        yield return new WaitForSeconds(2f);

        yield return Mifim.UnHighlight();

        yield return new WaitForSeconds(1);

        yield return Mifim.TransitionColor(Color.red);

        yield return new WaitForSeconds(1);

        yield return Mifim.Highlight();

        yield return Mifim.TransitionColor(Color.white);

        Character Kui = CreateCharacter("Kui as Mifim");

        Mifim.SetPosition(Vector2.zero);
        Kui.SetPosition(new(0.5f, 0f));

        Mifim.Show();
        Kui.Show();

        yield return new WaitForSeconds(2f);

        Sprite MifimFun = Mifim.GetSprite("fun");

        Mifim.TransitionSprite(MifimFun);

        yield return Mifim.MoveToPosition(new(1, 0), smooth: true);
        Mifim.MoveToPosition(new(0, 0), smooth: true);

        Sprite MifimSad = Mifim.GetSprite("sad");

        Mifim.TransitionSprite(MifimSad);

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
