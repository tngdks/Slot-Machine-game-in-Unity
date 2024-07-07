using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    private int randomValue;//holds the value for how long the row will keep spinning until it stops
    private float timeInterval;//used for slowing the movement of wheel 

    public bool rowStopped;//it will set to true when row stops spinning
    public string stoppedSlot;//holds the value of perticular item when the row stops

    //invoking the start rotating method when the handle is pulled
    void Start()
    {
        rowStopped = true;
        GameControl.HandlePulled += StartRotating;
    }

    //moving the row
    private void StartRotating()
    {
        stoppedSlot = "";
        StartCoroutine("Rotate");//used for the rotation
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;//row is spinning now
        timeInterval = 0.025f;//represents time interval in which the item is switched when the row is spinning

        //making the wheel stop spinning
        for(int i=0; i<30; i++)
        {
            if(transform.position.y <= -0.95f)
                transform.position = new Vector2(transform.position.x, -0.55f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.2f);

            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(60, 100);

        //putting the row in the correct position
        switch(randomValue % 3)
        {
            case 1:
                randomValue +=2;
                break;
            case 2:
                randomValue +=1;
                break;
        }

    //spinning the row depending on the how man steps row has passed
    for(int i=0; i<randomValue; i++)
    {
        //making the effect of slowing down of wheel
        if(transform.position.y <= 0.49f)
            transform.position = new Vector2(transform.position.x, 1.75f);

        transform.position = new Vector2(transform.position.x, transform.position.y - 0.25f);

        if(i > Mathf.RoundToInt(randomValue * 0.25f))
            timeInterval = 0.05f;
        if(i > Mathf.RoundToInt(randomValue * 0.50f))
            timeInterval = 0.10f;
        if(i > Mathf.RoundToInt(randomValue * 0.75f))
            timeInterval = 0.15f;
        if(i > Mathf.RoundToInt(randomValue * 0.95f))
            timeInterval = 0.20f;
        
        yield return new WaitForSeconds(timeInterval);
    }

        //checking the final position of item
        if(transform.position.y == -0.25f)
            stoppedSlot = "Bar";
        else if(transform.position.y == -0.90f)
            stoppedSlot = "Bell";
        else if(transform.position.y == -1.55f)
            stoppedSlot = "Seven";
        else if(transform.position.y == 0.49f)
            stoppedSlot = "Cherry";

        rowStopped = true;
    }

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
    }
}


