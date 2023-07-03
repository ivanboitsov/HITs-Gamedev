using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tubesPuzzleController : MonoBehaviour
{
    public bool win = false;

    public void Activate()
    {
        int childID = 0;
        while (childID < transform.childCount)
        {
            if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>() != null)
            {
                transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().activated = false;
                if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().GetId() == 0)
                {
                    transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().activated = true;
                }
            }
            childID++;
        }
    }

    private void Update()
    {
        int childID = 0;
        int mainId = 0;
        int endId = 0;
        while (childID < transform.childCount)
        {
            if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>() != null)
            {
                transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().activated = false;
                if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().GetId() == 0)
                {
                    mainId = childID;
                }
                else if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().GetId() == 12)
                {
                    endId = childID;
                }
            }
            childID++;
        }
        if (transform.GetChild(mainId).gameObject.GetComponent<tubeBlock>().rotationState == 0f)
        {
            transform.GetChild(mainId).gameObject.GetComponent<tubeBlock>().activated = true;
            transform.GetChild(mainId).gameObject.GetComponent<tubeBlock>().GetNext();
        }
        if (transform.GetChild(endId).gameObject.GetComponent<tubeBlock>().activated)
        {
            win = true;
        }
        else
        {
            win = false;
        }
    }

    private void Start()
    {
        Activate();
    }

    public void ActivateSingle(int id, int prev)
    {
        bool flag = false;
        int childID = 0;
        while (!flag && childID < transform.childCount)
        {
            if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>() != null && childID < transform.childCount && transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().GetId() == id)
            {
                flag = true;
                break;
            }
            childID++;
        }
        if (flag && transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().activated == false)
        {
            transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().Activate(prev);
        }
    }

    public bool CheckState(int id, int prev)
    {
        bool flag = false;
        int childID = 0;
        while (!flag && childID < transform.childCount)
        {
            if (transform.GetChild(childID).gameObject.GetComponent<tubeBlock>() != null && childID < transform.childCount && transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().GetId() == id)
            {
                flag = true;
                break;
            }
            childID++;
            UnityEngine.Debug.Log("bruh1s");
        }
        if (flag)
        {
            return transform.GetChild(childID).gameObject.GetComponent<tubeBlock>().myNexts(prev);
        }
        return false;
    }

    public void Win()
    {
        win = true;
    }
}
