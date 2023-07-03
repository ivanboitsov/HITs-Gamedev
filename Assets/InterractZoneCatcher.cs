using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class InterractZoneCatcher : MonoBehaviour
{
    public bool showed = false;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SwitchState(bool state)
    {
        showed = state;
        if (!showed)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
    }
}
