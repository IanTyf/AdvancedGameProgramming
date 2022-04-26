using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplayer_Menu : TextDisplayer
{

    [SerializeField]
    Text tX_MenuTitle;
    [SerializeField]
    Text tX_Resume;
    [SerializeField]
    Text tX_Options;
    [SerializeField]
    Text tX_BackToMenu;
    [SerializeField]
    Text tX_Quit;
    [SerializeField]
    Text tX_RestartLevel;
    [SerializeField]
    Text tX_ChangeLanguage;
    [SerializeField]
    Text tX_LanguageButton;


    // Update is called once per frame
    public override void LanguageCheck()
    {
        //Example 1: Dynamic variable defined by a string
        var excelItem = languageData.GetExcelItemByID("T001");
        //targetObj.GetType().GetField("Variable Name String").GetValue(targetObj) as variableType
        string exampleDynamicVariable = excelItem.GetType().GetField(CURRENT_LANGUAGE).GetValue(excelItem) as string;

        tX_MenuTitle.text = exampleDynamicVariable;

        tX_Resume.text = GetTranslation("Button_Resume");
        tX_Options.text = GetTranslation("Button_Options");
        tX_BackToMenu.text = GetTranslation("Button_BackToMenu");
        tX_Quit.text = GetTranslation("Button_Quit");
        tX_RestartLevel.text = GetTranslation("Button_RestartLevel");
        tX_ChangeLanguage.text = GetTranslation("Button_ChangeLanguage") + ":";
        tX_LanguageButton.text = GetTranslation("Button_" + CURRENT_LANGUAGE);
    }

    string GetTranslation(string name)
    {
        var excelItem = languageData.GetExcelItemByName(name);
        if(CURRENT_LANGUAGE == "English")
		{
            return excelItem.English;
        }
        else
        {
            return excelItem.SimplifiedChinese;
        }
    }

}
