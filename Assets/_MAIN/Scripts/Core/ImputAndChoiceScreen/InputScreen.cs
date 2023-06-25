using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputScreen : MonoBehaviour
{
    public static InputScreen instance;

    public TMP_InputField inputField;
    public static string currentInput { get { return instance.inputField.text; } }

    public TitleHeader titleHeader;

    public GameObject root;

    protected static Coroutine co_revealing;
    public static bool isRevealing => co_revealing != null;

    public static bool isWaitingForUserInput => instance.root.activeInHierarchy;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    public static void Show(string title, bool clearCurrentInput = true)
    {
        instance.root.SetActive(true);

        if(clearCurrentInput)
            instance.inputField.text = "";

        if (title != "")
            instance.titleHeader.Show(title);
        else
            instance.titleHeader.Hide();

        if (isRevealing)
            instance.StopCoroutine(co_revealing);

        co_revealing = instance.StartCoroutine(Revealing());
    }

    public static void Hide()
    {
        instance.root.SetActive(false);
        instance.titleHeader.Hide();
    }

    private static IEnumerator Revealing()
    {
        instance.inputField.gameObject.SetActive(false);

        while(instance.titleHeader.isRevealing)
            yield return new WaitForEndOfFrame();

        instance.inputField.gameObject.SetActive(true);

        co_revealing = null;
    }

    public void Accept()
    {
        Hide();
    }
}
