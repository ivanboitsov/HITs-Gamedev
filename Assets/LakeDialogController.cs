using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeDialogControl : MonoBehaviour
{
    public PlayerControl player;
    public GameObject puzzle;

    private void Update()
    {
        if (player.haveFish)
        {

            Destroy(gameObject);
            if (puzzle != null)
            {
                Destroy(puzzle);
            }
        }
    }
}
