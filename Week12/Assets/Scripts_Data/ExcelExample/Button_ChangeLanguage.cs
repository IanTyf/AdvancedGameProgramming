using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ChangeLanguage : MonoBehaviour
{
    string[] languageName = { "English", "SimplifiedChinese" };

    public void ChangeLanguage()
    {
        if (TextDisplayer.CURRENT_LANGUAGE == languageName[0])
        {
            TextDisplayer.CURRENT_LANGUAGE = languageName[1];
        }
        else
        {
            TextDisplayer.CURRENT_LANGUAGE = languageName[0];
        }

        TextDisplayer.TextDisplayerGroupRefresh();
    }
}
