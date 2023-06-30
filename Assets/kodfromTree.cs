using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kodfromTree : MonoBehaviour
{
    public []bool buttons = {false, false, false, false, false, false, false, false, false};
    public int nowButton = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Activate(this.gameObject);
    }

    void Activate(GameObject obj)
    {
        if (obj.layer == "kodLayer")
        {
            int myId = (int)obj.tag.Split("_")[0]
            if (myId == nowButton)
            {
                buttons[myId] = true;
                nowButton += 1;
            }
            else
            {
                nowButton = 0;
                for (int i = 0; i < 9; i++)
                {
                    buttons[i] = false;
                }
            }
        }
    }

    void CheckEmmision ()
    {
        for (int i = 0; i < 9; i++)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("button_"+i.ToString());
            foreach (GameObject obj in objectsWithTag)
            {
                Renderer renderer = myGameObject.GetComponent<Renderer>();
                if (buttons[i])
                {
                    //enble emission
                    renderer.emission = true;
                }
                else
                {
                    //disable emission
                    renderer.emission = false;
                }
            }
        }
        
    }
}
