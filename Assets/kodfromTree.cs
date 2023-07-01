using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsMain : MonoBehaviour
{
    public bool[] buttons = {false, false, false, false, false, false, false, false, false};
    public int nowButton = 0;
    public int toWin = 6;
    public bool win = false;

    void Start()
    {
        //material = GetComponent<Renderer>().materal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickOnButton(int buttonId)
    {
        print("poel");
        if (buttonId == nowButton)
        {
            if (!buttons[buttonId])
            {
                SwithEmission(buttonId);
                nowButton += 1;
            }
            else
            {
                SwithEmission(buttonId);
                nowButton = 0;
            }
            
        }
        else
        {
            SwithEmission(buttonId);
            nowButton = 0;
        }
        CheckWin();
    }

    void CheckWin()
    {
        if(nowButton == toWin)
        {
            if (!win)
            {
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("magicBarrierTag");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 100f, obj.transform.position.z);
                }
            }
            win = true;
        }
    }

    void SwithEmission(int index)
    {
        if (!buttons[index])
        {
            buttons[index] = true;
        }
        else{
            buttons[index] = false;
        }
    }

    void OffEmission(int index)
    {
        buttons[index] = false;
    }

    void CheckEmmision ()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("button_"+i.ToString());
            foreach (GameObject obj in objectsWithTag)
            {   
                /*
                Renderer renderer = objectsWithTag.GetComponent<Renderer>();
                Material myMat = renderer.material;
                if (buttons[i])
                {
                    //enble emission
                    myMat.SetColor("_EmissionColor", Color.white);
                }
                else
                {
                    //disable emission
                    myMat.SetColor("_EmissionColor", Color.black);
                }
                */
            }
        }
    }
}
