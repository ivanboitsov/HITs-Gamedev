using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeDialogControl : MonoBehaviour
{
    public PlayerControl player;

    private void Update()
    {
        if (player.haveFish)
        {

            Destroy(gameObject);
        }
    }
}
