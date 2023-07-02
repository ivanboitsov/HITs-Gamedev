using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class moveRound : MonoBehaviour
{
    public float movedG = 0f;
    public float needG = 0f;
    private Vector3 myCenter;
    private float distation = 0f;
    public float startX = 0f;
    public bool moveUp = true;
    public bool moveRight = true;

    private void Update()
    {
        if (movedG != Math.Abs(needG))
        {
            Move();
            if (Math.Abs(needG - movedG) < 0.1f)
            {
                movedG = needG;
                Move();
            }
        }
    }

    public void MoveStart(float angle, Vector3 center, float startXx)
    {
        movedG = 0f;
        needG = angle;
        myCenter = center;
        distation = myCenter.x - startXx;
        startX = startXx;//transform.position.x;
        if (distation < 0f)
        {
            distation = Math.Abs(distation);
            moveUp = true;
            moveRight = true;
        }
        else
        {
            moveUp = false;
            moveRight = false;
        }
        UnityEngine.Debug.Log(startX);
    }

    private void Move()
    {
        if (needG > movedG)
        {
            movedG += 0.2f;
        }
        else
        {
            movedG -= 0.2f;
        }
        
        float placeX = (float)Math.Cos((double)(movedG * Math.PI / 180f)) * distation;
        
        if (moveRight)
        {
            placeX = startX - placeX - distation;
        }
        else
        {
            placeX = startX + placeX + distation;
        }

        float placeY = distation * (float)Math.Sin((double)(movedG * Math.PI / 180f));
        if (!moveUp)
        {
            placeY = placeY * (-1f);
        }

        placeY += myCenter.y;

        transform.position = new Vector3( placeX, placeY, transform.position.z );
    }
}
