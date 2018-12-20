using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GridController : MonoBehaviour
{
    public int selectedCell;
    public int selectedX;
    public int selectedY;
    List<int[]> g1 = new List<int[]>();
    List<int[]> g2 = new List<int[]>();
    List<int[]> g3 = new List<int[]>();
    Box[] boxes;
    int[,] data = new int[9,9];
    public Text Instruction;
    // Use this for initialization
    void Start()
    {
        boxes = GetComponentsInChildren<Box>();
        ParseJson();
        foreach (var item in boxes)
        {
            item.ResetBox();
        }

    }
    
    public void CreateGrid1()
    {
        foreach (var item in boxes)
        {
            item.FillBox(g1[item.x+item.y]);
        }
        Instruction.text = " Select a cell and then select the number to fill the cell.";
    }
    public void CreateGrid2()
    {
        foreach (var item in boxes)
        {
            item.FillBox(g2[item.x + item.y]);

        }
        Instruction.text = " Select a cell and then select the number to fill the cell.";

    }
    public void CreateGrid3()
    {
        foreach (var item in boxes)
        {
            item.FillBox(g3[item.x + item.y]);
        }
        Instruction.text = " Select a cell and then select the number to fill the cell.";

    }
    void ParseJson()
    {
        var path = Application.streamingAssetsPath + "/Sudoku.json";
        var jsonString = File.ReadAllText(path);
        JObject jdata = JObject.Parse(jsonString);
        List<JToken> grid1 = jdata["Grid_1"].ToList();
        List<JToken> grid2 = jdata["Grid_2"].ToList();
        List<JToken> grid3 = jdata["Grid_3"].ToList();
       
        foreach (var item in grid1)
        {
            int[] data = item.Select(u => (int)u).ToArray();
            g1.Add(data);
        }
        foreach (var item in grid2)
        {
            int[] data = item.Select(u => (int)u).ToArray();
            g2.Add(data);
        }
        foreach (var item in grid3)
        {
            int[] data = item.Select(u => (int)u).ToArray();
            g3.Add(data);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MakeSelection(int x, int y, int cell)
    {
        selectedX = x;
        selectedY = y;
        selectedCell = cell;
    }

    public void SelectOption(int option)
    {
        string match = selectedX + "," + selectedY;
        Box box = boxes.Where(a => a.transform.name == match).FirstOrDefault();
        box.EnterOption(selectedCell, option);
    }
    public void getData()
    {

        for (int i = 0; i < 3; i++)
        {
            //gives 3 boxes ata a time
            List<Box> tempBox = boxes.Where(a => a.x == i ).ToList();


            foreach (var item in boxes)
            {//looping each box for selected options
                for (int j = 0; j < 3; j++)
                {//each selected option has 3 rows of data thus i is multiple of 3 and j is 0-3 in that bunch
                   for (int k = 0; k < 3; k++)
                   {//k is for each element in selected options 
                        data[j + 3 * i, k + 3 * item.y] = item.selectedOptions[k + 3 *j];
                    }
                   
                  //  Debug.Log(data[j + 3 * i,0]);
                }
            }
            var l = 10;

        }


    }

    public void CheckSolution()
    {
        getData();
        bool allGood = true;
        //check if any of the cell is left
        allGood = AllCellsFilled();
        allGood = RowsValid();
        if (allGood)
        {
            Instruction.text = "you have solved the sudoku puzzle.";
        }
    }

    private bool AllCellsFilled()
    {
        bool returnVal = true;
        foreach (var item in boxes)
        {
            if (item.selectedOptions.Any(a => a == 0))
            {
                returnVal = false;
                Instruction.text = "Some cell(s) are empty.";
                break;
            }
        }
        return returnVal;
    }
    //values in rows is unique? use check value array for true or false
    public bool RowsValid()
    {
        bool isOk = true;
        for (int i = 0; i <(9) ; i++)
        {
            int[] row = new int[9];
            for (int j = 0; j < 9; j++)
            {
                row[j] = data[i, j];

            }
             if( row.Distinct().Count() != row.Count())
            {
                isOk = false;
                Instruction.text = "Invalid Rows";
                break;
            }
            else
            {
                Debug.Log("valid Row :" + i);
            }
        }

        return isOk;
    }
}
