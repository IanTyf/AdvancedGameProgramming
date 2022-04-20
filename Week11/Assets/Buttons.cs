using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Theme { white, yellow, black, darkBlue };

public class Buttons : MonoBehaviour
{
    public GameObject bg;
    public GameObject slotParent;

    public GameObject mainButtons;
    public TMP_Text diffText;

    public GameObject showOptionGO;

    public Image NewGameImg;
    public Image EasyImg;
    public Image MediumImg;
    public Image HardImg;

    public Image annotateImg;
    public bool annotate;

    public Sprite checkedSprite;
    public Sprite uncheckSprite;

    public Sprite checkedSpriteLight;
    public Sprite uncheckSpriteLight;

    public Image aarCheck;
    public bool aar;

    public Image dnCheck;
    public bool dn;

    public Image hbCheck;
    public bool hb;

    public Image seCheck;
    public bool se;

    public GameObject grid;

    public Theme theme;

    // Start is called before the first frame update
    void Start()
    {
        aar = true;
        se = true;

        theme = Theme.white;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToDifficulties()
    {
        mainButtons.GetComponent<Animator>().SetBool("selectDiff", true);
    }

    public void Easy()
    {
        mainButtons.GetComponent<Animator>().SetBool("selectDiff", false);
        diffText.text = "Easy";
        grid.GetComponent<InitializeGrid>().ShowEasyProblem();
    }

    public void Medium()
    {
        mainButtons.GetComponent<Animator>().SetBool("selectDiff", false);
        diffText.text = "Medium";
        grid.GetComponent<InitializeGrid>().ShowMediumProblem();
    }

    public void Hard()
    {
        mainButtons.GetComponent<Animator>().SetBool("selectDiff", false);
        diffText.text = "Hard";
        grid.GetComponent<InitializeGrid>().ShowHardProblem();
    }

    public void Back()
    {
        mainButtons.GetComponent<Animator>().SetBool("selectDiff", false);
        mainButtons.GetComponent<Animator>().SetBool("saveDiff", false);
    }

    public void SaveCustom()
    {
        mainButtons.GetComponent<Animator>().SetBool("saveDiff", true);
    }

    public void SaveEasy()
    {
        mainButtons.GetComponent<Animator>().SetBool("saveDiff", false);
        ProblemJsonRW.StoreNewProblem("easy", grid.GetComponent<InitializeGrid>().grid);
    }

    public void SaveMedium()
    {
        mainButtons.GetComponent<Animator>().SetBool("saveDiff", false);
        ProblemJsonRW.StoreNewProblem("medium", grid.GetComponent<InitializeGrid>().grid);
    }

    public void SaveHard()
    {
        mainButtons.GetComponent<Animator>().SetBool("saveDiff", false);
        ProblemJsonRW.StoreNewProblem("hard", grid.GetComponent<InitializeGrid>().grid);
    }

    public void ClearBoard()
    {
        grid.GetComponent<InitializeGrid>().clearAllNumbers();
    }

    public void anno()
    {
        if (annotate)
        {
            annotate = false;
            if (theme == Theme.white)
                annotateImg.color = new Color(0.8773585f, 0.8773585f, 0.8773585f);
            else if (theme == Theme.yellow)
                annotateImg.color = new Color(0.8793386f, 0.8962264f, 0.7228996f);
            else if (theme == Theme.black)
                annotateImg.color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
            else if (theme == Theme.darkBlue)
                annotateImg.color = new Color(0.3388216f, 0.4040475f, 0.509434f);
        }
        else
        {
            annotate = true;
            if (theme == Theme.white)
                annotateImg.color = new Color(0.7f, 0.7f, 0.7f);
            else if (theme == Theme.yellow)
                annotateImg.color = new Color(0.7793386f, 0.7962264f, 0.6228996f);
            else if (theme == Theme.black)
                annotateImg.color = new Color(0.4773585f, 0.4773585f, 0.4773585f);
            else if (theme == Theme.darkBlue)
                annotateImg.color = new Color(0.4388216f, 0.5040475f, 0.609434f);
        }
    }

    public void aarC()
    {
        if (aar)
        {
            aar = false;
            if (theme == Theme.white || theme == Theme.yellow)
                aarCheck.sprite = uncheckSprite;
            else
                aarCheck.sprite = uncheckSpriteLight;
        }
        else
        {
            aar = true;
            if (theme == Theme.white || theme == Theme.yellow)
                aarCheck.sprite = checkedSprite;
            else
                aarCheck.sprite = checkedSpriteLight;
        }
    }

    public void dnC()
    {
        if (dn)
        {
            dn = false;
            if (theme == Theme.white || theme == Theme.yellow)
                dnCheck.sprite = uncheckSprite;
            else
                dnCheck.sprite = uncheckSpriteLight;
        }
        else
        {
            dn = true;
            if (theme == Theme.white || theme == Theme.yellow)
                dnCheck.sprite = checkedSprite;
            else
                dnCheck.sprite = checkedSpriteLight;
        }
    }

    public void hbC()
    {
        if (hb)
        {
            hb = false;
            if (theme == Theme.white || theme == Theme.yellow)
                hbCheck.sprite = uncheckSprite;
            else
                hbCheck.sprite = uncheckSpriteLight;
        }
        else
        {
            hb = true;
            if (theme == Theme.white || theme == Theme.yellow)
                hbCheck.sprite = checkedSprite;
            else
                hbCheck.sprite = checkedSpriteLight;
        }
    }

    public void seC()
    {
        if (se)
        {
            se = false;
            if (theme == Theme.white || theme == Theme.yellow)
                seCheck.sprite = uncheckSprite;
            else
                seCheck.sprite = uncheckSpriteLight;
        }
        else
        {
            se = true;
            if (theme == Theme.white || theme == Theme.yellow)
                seCheck.sprite = checkedSprite;
            else
                seCheck.sprite = checkedSpriteLight;
        }
    }

    public void showMenu()
    {
        Debug.Log("show menu");
        bool o = showOptionGO.GetComponent<Animator>().GetBool("showOptions");
        showOptionGO.GetComponent<Animator>().SetBool("showOptions", !o);
    }

    public void white()
    {
        bg.GetComponent<SpriteRenderer>().color = Color.white;
        TMP_Text[] texts = transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in texts)
        {
            t.color = Color.black;
        }

        TMP_Text[] gridTexts = slotParent.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in gridTexts)
        {
            t.color = Color.black;
        }

        TMP_Text[] panelTexts = showOptionGO.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in panelTexts)
        {
            t.color = Color.black;
        }

        NewGameImg.color = new Color(0.8773585f, 0.8773585f, 0.8773585f);
        annotateImg.color = new Color(0.8773585f, 0.8773585f, 0.8773585f);
        EasyImg.color = new Color(0.8773585f, 0.8773585f, 0.8773585f);
        MediumImg.color = new Color(0.8773585f, 0.8773585f, 0.8773585f);
        HardImg.color = new Color(0.8773585f, 0.8773585f, 0.8773585f);
        showOptionGO.GetComponent<Image>().color = new Color(0.8773585f, 0.8773585f, 0.8773585f);

        theme = Theme.white;

        if (aarCheck.sprite == checkedSpriteLight) aarCheck.sprite = checkedSprite;
        else if (aarCheck.sprite == uncheckSpriteLight) aarCheck.sprite = uncheckSprite;

        if (dnCheck.sprite == checkedSpriteLight) dnCheck.sprite = checkedSprite;
        else if (dnCheck.sprite == uncheckSpriteLight) dnCheck.sprite = uncheckSprite;

        if (hbCheck.sprite == checkedSpriteLight) hbCheck.sprite = checkedSprite;
        else if (hbCheck.sprite == uncheckSpriteLight) hbCheck.sprite = uncheckSprite;

        if (seCheck.sprite == checkedSpriteLight) seCheck.sprite = checkedSprite;
        else if (seCheck.sprite == uncheckSpriteLight) seCheck.sprite = uncheckSprite;

        grid.GetComponent<InitializeGrid>().Reselect();
    }

    public void yellow()
    {
        bg.GetComponent<SpriteRenderer>().color = new Color(1, 0.9775798f, 0.8349056f);
        TMP_Text[] texts = transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in texts)
        {
            t.color = new Color(0.2f, 0.2f, 0.2f);
        }

        TMP_Text[] gridTexts = slotParent.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in gridTexts)
        {
            t.color = new Color(0.2f, 0.2f, 0.2f);
        }

        TMP_Text[] panelTexts = showOptionGO.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in panelTexts)
        {
            t.color = new Color(0.2f, 0.2f, 0.2f);
        }

        NewGameImg.color = new Color(0.8793386f, 0.8962264f, 0.7228996f);
        annotateImg.color = new Color(0.8793386f, 0.8962264f, 0.7228996f);
        EasyImg.color = new Color(0.8793386f, 0.8962264f, 0.7228996f);
        MediumImg.color = new Color(0.8793386f, 0.8962264f, 0.7228996f);
        HardImg.color = new Color(0.8793386f, 0.8962264f, 0.7228996f);
        showOptionGO.GetComponent<Image>().color = new Color(0.8793386f, 0.8962264f, 0.7228996f);

        theme = Theme.yellow;

        if (aarCheck.sprite == checkedSpriteLight) aarCheck.sprite = checkedSprite;
        else if (aarCheck.sprite == uncheckSpriteLight) aarCheck.sprite = uncheckSprite;

        if (dnCheck.sprite == checkedSpriteLight) dnCheck.sprite = checkedSprite;
        else if (dnCheck.sprite == uncheckSpriteLight) dnCheck.sprite = uncheckSprite;

        if (hbCheck.sprite == checkedSpriteLight) hbCheck.sprite = checkedSprite;
        else if (hbCheck.sprite == uncheckSpriteLight) hbCheck.sprite = uncheckSprite;

        if (seCheck.sprite == checkedSpriteLight) seCheck.sprite = checkedSprite;
        else if (seCheck.sprite == uncheckSpriteLight) seCheck.sprite = uncheckSprite;

        grid.GetComponent<InitializeGrid>().Reselect();
    }

    public void darkBlue()
    {
        bg.GetComponent<SpriteRenderer>().color = new Color(0.08392666f, 0.2860116f, 0.4339623f);
        TMP_Text[] texts = transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in texts)
        {
            t.color = new Color(0.8f, 0.8f, 0.8f);
        }

        TMP_Text[] gridTexts = slotParent.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in gridTexts)
        {
            t.color = new Color(0.8f, 0.8f, 0.8f);
        }

        TMP_Text[] panelTexts = showOptionGO.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in panelTexts)
        {
            t.color = new Color(0.8f, 0.8f, 0.8f);
        }

        NewGameImg.color = new Color(0.3388216f, 0.4040475f, 0.509434f);
        annotateImg.color = new Color(0.3388216f, 0.4040475f, 0.509434f);
        EasyImg.color = new Color(0.3388216f, 0.4040475f, 0.509434f);
        MediumImg.color = new Color(0.3388216f, 0.4040475f, 0.509434f);
        HardImg.color = new Color(0.3388216f, 0.4040475f, 0.509434f);
        showOptionGO.GetComponent<Image>().color = new Color(0.3388216f, 0.4040475f, 0.509434f);

        theme = Theme.darkBlue;

        if (aarCheck.sprite == checkedSprite) aarCheck.sprite = checkedSpriteLight;
        else if (aarCheck.sprite == uncheckSprite) aarCheck.sprite = uncheckSpriteLight;

        if (dnCheck.sprite == checkedSprite) dnCheck.sprite = checkedSpriteLight;
        else if (dnCheck.sprite == uncheckSprite) dnCheck.sprite = uncheckSpriteLight;

        if (hbCheck.sprite == checkedSprite) hbCheck.sprite = checkedSpriteLight;
        else if (hbCheck.sprite == uncheckSprite) hbCheck.sprite = uncheckSpriteLight;

        if (seCheck.sprite == checkedSprite) seCheck.sprite = checkedSpriteLight;
        else if (seCheck.sprite == uncheckSprite) seCheck.sprite = uncheckSpriteLight;

        grid.GetComponent<InitializeGrid>().Reselect();
    }

    public void black()
    {
        bg.GetComponent<SpriteRenderer>().color = new Color(0.2169811f, 0.2169811f, 0.2169811f);
        TMP_Text[] texts = transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in texts)
        {
            t.color = new Color(0.8f, 0.8f, 0.8f);
        }

        TMP_Text[] gridTexts = slotParent.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in gridTexts)
        {
            t.color = new Color(0.8f, 0.8f, 0.8f);
        }

        TMP_Text[] panelTexts = showOptionGO.transform.GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text t in panelTexts)
        {
            t.color = new Color(0.8f, 0.8f, 0.8f);
        }

        NewGameImg.color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
        annotateImg.color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
        EasyImg.color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
        MediumImg.color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
        HardImg.color = new Color(0.3773585f, 0.3773585f, 0.3773585f);
        showOptionGO.GetComponent<Image>().color = new Color(0.3773585f, 0.3773585f, 0.3773585f);

        theme = Theme.black;

        if (aarCheck.sprite == checkedSprite) aarCheck.sprite = checkedSpriteLight;
        else if (aarCheck.sprite == uncheckSprite) aarCheck.sprite = uncheckSpriteLight;

        if (dnCheck.sprite == checkedSprite) dnCheck.sprite = checkedSpriteLight;
        else if (dnCheck.sprite == uncheckSprite) dnCheck.sprite = uncheckSpriteLight;

        if (hbCheck.sprite == checkedSprite) hbCheck.sprite = checkedSpriteLight;
        else if (hbCheck.sprite == uncheckSprite) hbCheck.sprite = uncheckSpriteLight;

        if (seCheck.sprite == checkedSprite) seCheck.sprite = checkedSpriteLight;
        else if (seCheck.sprite == uncheckSprite) seCheck.sprite = uncheckSpriteLight;

        grid.GetComponent<InitializeGrid>().Reselect();
    }

    public void writeANumber(int i)
    {
        grid.GetComponent<InitializeGrid>().writeNumber(i);
    }
}
