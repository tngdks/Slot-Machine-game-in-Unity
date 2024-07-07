using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate{ };

    [SerializeField]
    private Text prizetext;

    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;

    private bool resultsChecked = false;

    // Update is called once per frame
    void Update()
    {
        // we are checking if the row is still spinning 
       if(!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
       {
        prizeValue = 0;
        prizetext.enabled = false;
        resultsChecked = false;
       } 

        //if the rows have stopped spinning and rows are not checked then CheckResults is invoked
       if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
       {
        CheckResults();
        prizetext.enabled = true;
        prizetext.text = "Prize: " + prizeValue;
       }
    }
    // slot machine handle is clicked by left click mouse button to pull
    private void OnMouseDown()
    {
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            StartCoroutine("PullHandle");
    }

    // animation for the handle
    private IEnumerator PullHandle()
    {
        //pull towards us
        for(int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        HandlePulled(); // Handle pulled event is triggered in the middle of these both events

        //handle goes away from us 
        for(int i=0; i < 15; i+=5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    //prize values for the different types of combinations we get 
    private void CheckResults()
    {
        //checking for the items with same value
        if(rows[0].stoppedSlot == "Cherry"
            && rows[1].stoppedSlot == "Cherry"
            && rows[2].stoppedSlot == "Cherry")

            prizeValue = 200;

        else if(rows[0].stoppedSlot == "Bar"
            && rows[1].stoppedSlot == "Bar"
            && rows[2].stoppedSlot == "Bar")

            prizeValue = 1000;

        else if(rows[0].stoppedSlot == "Bell"
            && rows[1].stoppedSlot == "Bell"
            && rows[2].stoppedSlot == "Bell")

            prizeValue = 600;

        else if(rows[0].stoppedSlot == "Seven"
            && rows[1].stoppedSlot == "Seven"
            && rows[2].stoppedSlot == "Seven")

            prizeValue = 5000;

        //checking for the items value in different combinations
        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot)
            && (rows[0].stoppedSlot == "Cherry"))
            
            || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Cherry"))
            
            ||((rows[1].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Cherry")))

            prizeValue = 100;

        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot)
            && (rows[0].stoppedSlot == "Bar"))
            
            || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Bar"))
            
            ||((rows[1].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Bar")))

            prizeValue = 300;

        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot)
            && (rows[0].stoppedSlot == "Bell"))
            
            || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Bell"))
            
            ||((rows[1].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Bell")))

            prizeValue = 500;

        else if(((rows[0].stoppedSlot == rows[1].stoppedSlot)
            && (rows[0].stoppedSlot == "Seven"))
            
            || ((rows[0].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Seven"))
            
            ||((rows[1].stoppedSlot == rows[2].stoppedSlot)
            && (rows[1].stoppedSlot == "Seven")))

            prizeValue = 700;

        resultsChecked = true;
    }
}
