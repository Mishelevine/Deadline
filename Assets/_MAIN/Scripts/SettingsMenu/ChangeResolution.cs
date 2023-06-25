using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChangeResolution : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;

    public void Change()
    {
        if (dropdown.value == 0) Screen.SetResolution(1920 , 1080,Screen.fullScreen);
        else if (dropdown.value == 1) Screen.SetResolution(1600, 900, Screen.fullScreen);
        else if (dropdown.value == 2) Screen.SetResolution(1366, 768, Screen.fullScreen);
        else if (dropdown.value == 3) Screen.SetResolution(2560, 1440, Screen.fullScreen);
    }
}
