using UnityEngine;
using System.Collections;

public class OTO_Click : MonoBehaviour
{
    public idCreator indexCreate;
    public OTO_Main buttonsScript;

    private void OnMouseDown()
    {
        if (!buttonsScript.win)
        {
            buttonsScript.ClickOnButton(indexCreate.GetId());
        }
    }
}