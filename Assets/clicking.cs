using UnityEngine;
using System.Collections;

public class ClickingButton : MonoBehaviour
{
    private int myId;
    public ButtonsMain buttonsScript;
    private Renderer _renderer;

    private void Start()
    {
        myId = int.Parse(this.tag.Split("_")[1]);
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (buttonsScript.buttons[myId])
        {
            _renderer.material.SetColor("_EmissionColor", Color.white);
        }
        else
        {
            _renderer.material.SetColor("_EmissionColor", Color.black);
        }
    }

    private void OnMouseDown()
    {
        if (!buttonsScript.win)
        {
            buttonsScript.ClickOnButton(myId);
            print("poeshGovna");
        }
    }
}