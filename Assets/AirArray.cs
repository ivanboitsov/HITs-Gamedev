using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirArray : MonoBehaviour
{
#nullable enable
    public Air[]? AirsArrayObjects;

    public void SwitchStates()
    {
        if (AirsArrayObjects != null)
        {
            foreach (var obj in AirsArrayObjects)
            {
                obj.SwitchState();
            }
        }
    }

    public float GetForce(int id)
    {
        if (AirsArrayObjects != null)
        {
            if (id < AirsArrayObjects.Length)
            {
                return AirsArrayObjects[id].GetForce();
            }
        }
        return 0f;
    }
}
