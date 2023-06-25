using CHARACTERS;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TitleHeader : MonoBehaviour
{
    public Image banner;
    public TextMeshProUGUI titleText;

    private string title { get { return titleText.text; } set { titleText.text = value; } }

    protected Coroutine co_revealing;
    public bool isRevealing => co_revealing != null;

    public void Show(string displayTitle)
    {
        title = displayTitle;

        if (isRevealing)
            StopCoroutine(co_revealing);

        co_revealing = StartCoroutine(ShowingOrHiding());
    }

    public void Hide()
    {
        if (isRevealing)
            StopCoroutine(co_revealing);

        co_revealing = null;

        banner.enabled = false;
        titleText.enabled = false;
    }

    public IEnumerator ShowingOrHiding()
    {
        banner.enabled = true;
        titleText.enabled = true;

        return null;
    }
}
