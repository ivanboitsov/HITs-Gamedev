using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public string targetTag;
    public string materialName;

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
            Debug.Log("zone1 green");
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone2), materialName))
        {
            Debug.Log("zone2 green");
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone3), materialName))
        {
            Debug.Log("zone3 green");
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone4), materialName))
        {
            Debug.Log("zone4 green");
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone5), materialName))
        {
            Debug.Log("zone5 green");
            winCounter++;
        }
        if (CheckMaterial(getObject(targetTag, zone6), materialName))
        {
            Debug.Log("zone6 green");
            winCounter++;
        }

        if (winCounter == 6)
        {
            Debug.Log("win");
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
