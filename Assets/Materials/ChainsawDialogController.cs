using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainsawDialogControl : MonoBehaviour
{
    public PlayerControl player;
    public GameObject puzzle;

    private void Update()
    {
        if (player.brevnoCutted)
        {
            Destroy(gameObject);
            if (puzzle != null)
            {
                Destroy(puzzle);
            }
        }
    }
}
