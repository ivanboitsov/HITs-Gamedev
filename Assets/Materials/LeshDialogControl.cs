using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeshDialogControl : MonoBehaviour
{
    public PlayerControl player;
    public GameObject puzzle;

    private void Update()
    {
        if (player.haveWorm)
        {
            Destroy(gameObject);
            Destroy(puzzle);
        }
    }
}
