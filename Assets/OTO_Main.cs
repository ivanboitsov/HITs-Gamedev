using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTO_Main : MonoBehaviour
{
    public bool clickReady = true;
    public float clickTimeout = 1f;
    public bool win = false;
    private int changing = 0;
    private bool ch = false;

    private int[] poryadok = { 4, 2, 1, 3, 0 };

    public void ClickOnButton(int buttonId)
    {
        if (clickReady)
        {
            clickReady = false;
            int idInP = 0;
            while (poryadok[idInP] != buttonId)
            {
                idInP++;
            }
            int idInObj = 0;
            while (transform.GetChild(idInObj).gameObject.GetComponent<idCreator>().GetId() != poryadok[idInP])
            {
                idInObj++;
            }
            if (idInP > 0 && idInP < 4)
            {
                ch = true;
                changing = idInP;
                Vector3 theCenter = transform.GetChild(idInObj).gameObject.transform.position;
                int k = 0;
                while (transform.GetChild(k).gameObject.GetComponent<idCreator>().GetId() != poryadok[idInP - 1])
                {
                    k++;
                }

                //UnityEngine.Debug.Log( (idInP-1).ToString() + " " + k.ToString());

                transform.GetChild(k).gameObject.GetComponent<moveRound>().MoveStart(180f, theCenter, 0.205f * (idInP - 3));

                k = 0;
                while (transform.GetChild(k).gameObject.GetComponent<idCreator>().GetId() != poryadok[idInP + 1])
                {
                    k++;
                }

                //UnityEngine.Debug.Log( (idInP + 1).ToString() + " " + k.ToString());

                transform.GetChild(k).gameObject.GetComponent<moveRound>().MoveStart(180f, theCenter, 0.205f * (idInP - 1));
                //UnityEngine.Debug.Log(theCenter.y.ToString() + " tis is Y");
                //UnityEngine.Debug.Log(theCenter.x);
            }
            else
            {
                ch = false;
            }
            Invoke(nameof(changePoryadok), clickTimeout);
        }
    }

    private void changePoryadok()
    {
        if (ch)
        {
            int res = poryadok[changing - 1];
            poryadok[changing - 1] = poryadok[changing + 1];
            poryadok[changing + 1] = res;
        }
        clickReady = true;
        string gay = "";
        foreach (var p in poryadok)
        {
            gay += " " + p.ToString();
        }
        UnityEngine.Debug.Log(gay);
    }
}
