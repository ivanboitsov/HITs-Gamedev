using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public string targetTag;
    public string materialName;
    public bool winned = false;

    public Collider zone1;
    public Collider zone2;
    public Collider zone3;
    public Collider zone4;
    public Collider zone5;
    public Collider zone6;

    // Update is called once per frame
    void Update()
    {
        int winCounter = 0;

        if (CheckMaterial(getObject(targetTag, zone1), materialName))
        {
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone2), materialName))
        {
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone3), materialName))
        {
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone4), materialName))
        {

            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone5), materialName))
        {
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone6), materialName))
        {
            winCounter++;
        }

        if (winCounter == 6)
        {
            win();
        }
    }

    private void win()
    {
        if (!winned)
        {
            winned = true;

            GameObject[] boxWall = GameObject.FindGameObjectsWithTag("boxTurner");

            foreach (GameObject obj in boxWall)
            {
                obj.transform.rotation = Quaternion.Euler(0f, -172f, 0f);
            }
        }
    }

    private bool CheckMaterial(GameObject obj, string materialName)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;

                if (material != null && material.name.Equals(materialName))
                {
                    return true;
                }
            }
        }
        return false;
    }


    private GameObject getObject(string _targetTag, Collider fromZone)
    {
        GameObject[] objectsToMove = GameObject.FindGameObjectsWithTag(_targetTag);

        foreach (GameObject obj in objectsToMove)
        {
            if (fromZone.bounds.Contains(obj.transform.position))
            {
                return obj;
            }
        }

        return null;
    }
}
