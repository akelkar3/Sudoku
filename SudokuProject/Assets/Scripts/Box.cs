using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour {
    public int x,y;
    int[] options = new int[9] { 1,2,3,4,5,6,7,8,9}; //array that contains all the possible numbers in this box
    bool[] available = new bool[10] { true,true,true,true,true,true, true, true,true,true };
    public int[] selectedOptions = new int[10];
    Dictionary<int, int> filled = new Dictionary<int, int>(); //containing the options filled inside this box and the number of time they are filled
    int selectedCell;
    GridController gridController;
    public Text Instruction;
    /// <summary>
    /// method to fill box
    /// </summary>
  public void ResetBox()
    {
        
        Text[] cellsText = GetComponentsInChildren<Text>();
        //initialize the available places
        
        foreach (var item in cellsText)
        {
            int index = Convert.ToInt32(item.transform.parent.gameObject.name);
           
            selectedOptions[index]= 0;
            //get the button in parent
            Button cellButton = item.GetComponentInParent<Button>();
        
                //set no text and add a listener to the parent
                item.text = "";
            cellButton.interactable = true;
            
        }

    }
    public void FillBox(int[] input)
    {
        ResetBox();
        for (int i = 0; i < input.Length; i++)
        {
            available[input[i]] = input[i] > 0 ? false : true;
            selectedOptions[i] = input[i];
        }

        //UI part
        Text[] cellsText = GetComponentsInChildren<Text>();
       
        foreach (var item in cellsText)
        {   int index = Convert.ToInt32(item.transform.parent.gameObject.name);
            int valueForCell = input[index];
            //get the button in parent
            Button cellButton = item.GetComponentInParent<Button>();
            if (  valueForCell > 0)
            {
                item.text = valueForCell.ToString();
                cellButton.interactable = false; //disable the button
            }
            else
            {
                //set no text and add a listener to the parent
                item.text = "";
                cellButton.onClick.AddListener(() =>gridController.MakeSelection(x,y,index));
            }
        }
    }
    
    /// <summary>
    /// method to call when an option for a particular cell is selected
    /// </summary>
    /// <param name="option"></param>
    public void EnterOption(int cell,int option)
    {//check if the selected option is allowed to enter in this box
        if (available[option])
        {
            //replace the earlier number selected
            if (selectedOptions[cell] > 0)
            {
                available[selectedOptions[cell]] = true;
                selectedOptions[cell] = 0;
                
            }
            selectedOptions[cell] = option;
            available[option] = false;
            Text[] cellsTexts = GetComponentsInChildren<Text>();
          Text cellText=  cellsTexts.Where(a => a.transform.parent.name == cell.ToString()).FirstOrDefault();
            cellText.text = option.ToString();
            Debug.Log(selectedOptions.ToString());
        }
        else
        {
            Debug.LogError("Number is already available in the box.");
            showMessage("number is alreay used in this box");

        }
    }
	// Use this for initialization
	void Awake () {
      
        var splitted = transform.name.Split(',');
        x = Convert.ToInt32(splitted[0]);
        y = Convert.ToInt32(splitted[1]);
        gridController = GetComponentInParent<GridController>();
	}
    void Start()
    {
        //for testing only
     //   FillBox(new int[] { 0, 0, 1, 0, 2, 0, 4, 5, 6 });
    }
    /// <summary>
    /// method to check whether this block is unique or not
    /// </summary>
    /// <returns></returns>
	public bool IsUnique()
    {
        for (int i = 0; i < selectedOptions.Length; i++)
        {
            if (selectedOptions[i] > 0)
            {
                filled[selectedOptions[i]] += 1;
            }
        }
        bool returnValue = true;
        foreach (var item in filled)
        {
           if(item.Value > 1)
            {
                returnValue = false;
               break; //break and  return false
            }
        }
        return returnValue;
        
    }
 
	// Update is called once per frame
	void Update () {
        //foreach (var item in selectedOptions)
        //{
        //    if (item > 0)
        //    {
        //        available[item] = false;
        //    }
        //    else
        //    {
        //        available[item] = true;
        //    }
        //}
	}
    void showMessage(string message)
    {
        Instruction.text = message;
    }
    
}
