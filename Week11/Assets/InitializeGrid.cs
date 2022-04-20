using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InitializeGrid : MonoBehaviour
{
    public Buttons btnManager;
    public GameObject slot;
    public GameObject slotHolder;

    public GameObject[,] grid;

    public string[] easyProblem;
    public string[] mediumProblem;
    public string[] hardProblem;

    public bool ShowEasy;
    public bool ShowMedium;
    public bool ShowHard;

    private float timer;
    private int row;
    private int col;

    public GameObject currentlySelectedSlot;


    // Start is called before the first frame update
    void Start()
    {
        // create 9x9 slots
        grid = new GameObject[9,9];
        for (int i=0; i<9; i++)
        {
            for (int j=0; j<9; j++)
            {
                GameObject newSlot = Instantiate(slot, slotHolder.transform);
                if (j < 3)
                    //newSlot.transform.localPosition = new Vector3((j - 4) * 0.4825f, (i - 4) * 0.4825f, 0);
                    newSlot.transform.localPosition = newSlot.transform.localPosition + new Vector3((j - 2) * 0.48f - 0.97f, 0f, 0f);
                else if (j < 6)
                    newSlot.transform.localPosition = newSlot.transform.localPosition + new Vector3((j - 4) * 0.48f, 0f, 0f);
                else
                    newSlot.transform.localPosition = newSlot.transform.localPosition + new Vector3((j - 6) * 0.48f + 0.97f, 0f, 0f);

                if (i < 3)
                    //newSlot.transform.localPosition = new Vector3((j - 4) * 0.4825f, (i - 4) * 0.4825f, 0);
                    newSlot.transform.localPosition = newSlot.transform.localPosition + new Vector3(0f, (i - 2) * 0.48f - 0.97f, 0f);
                else if (i < 6)
                    newSlot.transform.localPosition = newSlot.transform.localPosition + new Vector3(0f, (i - 4) * 0.48f, 0f);
                else
                    newSlot.transform.localPosition = newSlot.transform.localPosition + new Vector3(0f, (i - 6) * 0.48f + 0.97f, 0f);
                grid[i, j] = newSlot;
                
            }
        }

        /*
        easyProblem = new int[9, 9] { {1,0,0,0,0,2,4,8,0},
                                      {9,4,0,8,0,0,7,5,0},
                                      {0,2,0,0,0,0,0,0,0},
                                      {0,0,0,0,9,6,5,0,0},
                                      {3,0,7,0,8,4,6,9,1},
                                      {6,0,0,0,5,0,0,0,8},
                                      {0,8,0,0,6,0,0,2,0},
                                      {0,0,9,4,1,5,8,7,3},
                                      {4,0,3,0,0,8,0,6,5} };
        mediumProblem = new int[9, 9] { {0,0,0,0,0,0,6,0,9},
                                      {1,0,0,0,0,4,0,0,0},
                                      {0,0,5,3,0,6,8,2,1},
                                      {0,0,4,6,7,0,0,5,0},
                                      {0,0,7,0,0,0,9,0,0},
                                      {0,0,0,5,4,0,0,0,0},
                                      {3,7,0,4,0,5,2,0,6},
                                      {0,0,0,0,0,0,5,1,0},
                                      {0,6,0,0,2,0,0,3,7} };
        hardProblem = new int[9, 9] { {0,8,0,0,0,0,0,0,0},
                                      {0,6,0,0,0,5,3,0,0},
                                      {0,0,0,0,9,0,5,6,0},
                                      {0,0,0,0,0,0,8,0,2},
                                      {0,0,0,0,0,0,0,4,0},
                                      {3,0,7,0,2,0,0,0,0},
                                      {0,0,5,0,6,0,9,8,0},
                                      {7,0,0,4,0,0,0,0,3},
                                      {0,4,0,0,0,1,0,0,0} };
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (ShowEasy)
        {
            timer += Time.deltaTime;
            if (timer >= 0.02)
            {
                timer = 0;

                writeNumber(row, col, easyProblem[row * 9 + col]);
                col++;
                if (col == 9)
                {
                    col = 0;
                    row--;
                }
                if (row == -1)
                {
                    ShowEasy = false;
                }
            }
        }
        if (ShowMedium)
        {
            timer += Time.deltaTime;
            if (timer >= 0.02)
            {
                timer = 0;

                writeNumber(row, col, mediumProblem[row * 9 + col]);
                col++;
                if (col == 9)
                {
                    col = 0;
                    row--;
                }
                if (row == -1)
                {
                    ShowMedium = false;
                }
            }
        }
        if (ShowHard)
        {
            timer += Time.deltaTime;
            if (timer >= 0.02)
            {
                timer = 0;

                writeNumber(row, col, hardProblem[row * 9 + col]);
                col++;
                if (col == 9)
                {
                    col = 0;
                    row--;
                }
                if (row == -1)
                {
                    ShowHard = false;
                }
            }
        }
    }

    public void writeNumber(int i, int j, string val)
    {
        if (!val.Equals("0")) 
            grid[i, j].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = val;
        else
            grid[i, j].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";

        if (btnManager.theme == Theme.white)
        {
            grid[i, j].GetComponent<Animator>().SetTrigger("whiteFlash");
        }
        else if (btnManager.theme == Theme.yellow)
        {
            grid[i, j].GetComponent<Animator>().SetTrigger("yellowFlash");
        }
        else if (btnManager.theme == Theme.black)
        {
            grid[i, j].GetComponent<Animator>().SetTrigger("blackFlash");
        }
        else if (btnManager.theme == Theme.darkBlue)
        {
            grid[i, j].GetComponent<Animator>().SetTrigger("darkBlueFlash");
        }
    }

    public void writeNumber(int val)
    {
        if (currentlySelectedSlot == null) return;
        slot = currentlySelectedSlot;

        if (val != 0)
            slot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = val.ToString();
        else
            slot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";

        if (btnManager.theme == Theme.white)
        {
            slot.GetComponent<Animator>().SetTrigger("whiteFlash");
        }
        else if (btnManager.theme == Theme.yellow)
        {
            slot.GetComponent<Animator>().SetTrigger("yellowFlash");
        }
        else if (btnManager.theme == Theme.black)
        {
            slot.GetComponent<Animator>().SetTrigger("blackFlash");
        }
        else if (btnManager.theme == Theme.darkBlue)
        {
            slot.GetComponent<Animator>().SetTrigger("darkBlueFlash");
        }

        SelectSlot(currentlySelectedSlot);
    }

    public void ShowEasyProblem()
    {
        clearAllSlots();
        ShowEasy = true;
        ShowMedium = false;
        ShowHard = false;
        timer = 0;
        row = 8;
        col = 0;
        easyProblem = ProblemJsonRW.GetRandomProblem("easy");
    }

    public void ShowMediumProblem()
    {
        clearAllSlots();
        ShowEasy = false;
        ShowMedium = true;
        ShowHard = false;
        timer = 0;
        row = 8;
        col = 0;
        mediumProblem = ProblemJsonRW.GetRandomProblem("medium");
    }

    public void ShowHardProblem()
    {
        clearAllSlots();
        ShowEasy = false;
        ShowMedium = false;
        ShowHard = true;
        timer = 0;
        row = 8;
        col = 0;
        hardProblem = ProblemJsonRW.GetRandomProblem("hard");
    }

    public void SelectSlot(GameObject slot)
    {
        if (slot == null) return;

        currentlySelectedSlot = slot;
        string slotNum = slot.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text;

        clearAllSlots();

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject s = grid[i, j];
                if (!slotNum.Equals("") && s.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text.Equals(slotNum))
                {
                    if (btnManager.theme == Theme.white)
                    {
                        //slot.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                        s.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
                        //Debug.Log("white!");
                    }
                    else if (btnManager.theme == Theme.yellow)
                    {
                        s.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.95f, 1f, 0.88f, 1f);

                    }
                    else if (btnManager.theme == Theme.black)
                    {
                        s.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.58f, 1f);
                    }
                    else if (btnManager.theme == Theme.darkBlue)
                    {
                        s.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.53f, 0.55f, 0.64f, 1f);
                    }
                }
            }
        }

        if (btnManager.theme == Theme.white)
        {
            //slot.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
            slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1f);
            Debug.Log("white!");
        }
        else if (btnManager.theme == Theme.yellow)
        {
            slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.95f, 1f, 0.88f, 1f);

        }
        else if (btnManager.theme == Theme.black)
        {
            slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.58f, 0.58f, 0.58f, 1f);
        }
        else if (btnManager.theme == Theme.darkBlue)
        {
            slot.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.53f, 0.55f, 0.64f, 1f);
        }
    }

    public void clearAllSlots()
    {
        for (int i=0; i<9; i++)
        {
            for (int j=0; j<9; j++)
            {
                grid[i, j].transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    public void clearAllNumbers()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grid[i, j].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = "";
            }
        }
    }

    public void Reselect()
    {
        SelectSlot(currentlySelectedSlot);
    }

    
}
