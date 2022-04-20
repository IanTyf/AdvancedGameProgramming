using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ProblemJsonRW : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        /*
        sudokuInfo s = new sudokuInfo();

        sudoku[] problems = new sudoku[3];
        problems[0].difficulty = "easy";
        problems[0].problem = "1,0,0,0,0,2,4,8,0,9,4,0,8,0,0,7,5,0,0,2,0,0,0,0,0,0,0,0,0,0,0,9," +
            "6,5,0,0,3,0,7,0,8,4,6,9,1,6,0,0,0,5,0,0,0,8,0,8,0,0,6,0,0,2,0,0,0,9,4,1,5,8,7,3,4,0,3,0,0,8,0,6,5";
        problems[1].difficulty = "medium";
        problems[1].problem = "0,0,0,0,0,0,6,0,9,1,0,0,0,0,4,0,0,0,0,0,5,3,0,6,8,2,1,0,0,4,6,7," +
            "0,0,5,0,0,0,7,0,0,0,9,0,0,0,0,0,5,4,0,0,0,0,3,7,0,4,0,5,2,0,6,0,0,0,0,0,0,5,1,0,0,6,0,0,2,0,0,3,7";
        problems[2].difficulty = "hard";
        problems[2].problem = "0,8,0,0,0,0,0,0,0,0,6,0,0,0,5,3,0,0,0,0,0,0,9,0,5,6,0,0,0,0,0,0," +
            "0,8,0,2,0,0,0,0,0,0,0,4,0,3,0,7,0,2,0,0,0,0,0,0,5,0,6,0,9,8,0,7,0,0,4,0,0,0,0,3,0,4,0,0,0,1,0,0,0";

        s.problems = problems;

        sudokuInfo.WriteToJSON("/JSON/SudokuDB.json", s);
        */
        //Debug.Log(GetRandomProblem("easy"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static string[] GetRandomProblem(string difficulty)
    {
        sudokuInfo info = sudokuInfo.CreateFromJSON("/JSON/SudokuDB.json");

        string ret = "";
        while (ret.Equals(""))
        {
            int randInd = Random.Range(0, info.problems.Length);
            if (difficulty.Equals(info.problems[randInd].difficulty))
            {
                ret = info.problems[randInd].problem;
            }
        }

        string[] r = ret.Split(',');

        return r;
    }

    public static void StoreNewProblem(string difficulty, GameObject[,] grid)
    {
        string newProblem = "";
        for (int i=0; i<9; i++)
        {
            for (int j=0; j<9; j++)
            {
                newProblem += grid[i, j].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text;

                if (!(i == 8 && j == 8)) newProblem += ",";
            }
        }

        sudokuInfo info = sudokuInfo.CreateFromJSON("/JSON/SudokuDB.json");
        sudoku[] sudokus = info.problems;
        sudoku newSudoku = new sudoku();
        newSudoku.difficulty = difficulty;
        newSudoku.problem = newProblem;
        sudoku[] newSudokus = new sudoku[sudokus.Length + 1];
        for (int i=0; i<sudokus.Length; i++)
        {
            newSudokus[i] = sudokus[i];
        }
        newSudokus[sudokus.Length] = newSudoku;

        info.problems = newSudokus;
        sudokuInfo.WriteToJSON("/JSON/SudokuDB.json", info);
    }
}

[System.Serializable]
public class sudokuInfo
{
    public sudoku[] problems;

    public static sudokuInfo CreateFromJSON(string path)
    {
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + path);
        return JsonUtility.FromJson<sudokuInfo>(jsonString);
    }
    public static void WriteToJSON(string path, sudokuInfo playerInfo)
    {
        string jsonString = JsonUtility.ToJson(playerInfo);
        File.WriteAllText(Application.streamingAssetsPath + path, jsonString);
    }
}

[System.Serializable]
public struct sudoku
{
    public string difficulty;
    public string problem;
}
