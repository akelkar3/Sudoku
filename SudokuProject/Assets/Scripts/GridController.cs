using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int selectedCell;
    public int selectedX;
    public int selectedY;
    Box[] boxes;
    int[,] data = new int[9,9];
    // Use this for initialization
    void Start()
    {
        boxes = GetComponentsInChildren<Box>();
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
          //  var l = 10;

        }


    }
}
