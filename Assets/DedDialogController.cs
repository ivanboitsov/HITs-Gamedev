using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DedDialogControl : MonoBehaviour
{
    public PlayerControl player;

    private void Update()
    {
        if (player.lampPlaced)
        {
            Destroy(gameObject);
        }
    }
}
