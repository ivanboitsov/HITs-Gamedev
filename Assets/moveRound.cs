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
    public float distation = 0f;
    public float startX = 0f;
    public bool moveUp = true;
    public bool moveRight = true;

    private void FixedUpdate()
    {
        if (movedG != Math.Abs(needG))
        {
            Move();
            if (Math.Abs(needG - movedG) < 5f)
            {
                movedG = needG;
                Move();
            }
        }
    }

    public void MoveStart(float angle, Vector3 center, float startXx)
    {
        movedG = 0f;
        needG = angle*(181f/180f);
        myCenter = center;
        distation = Math.Abs(startXx - myCenter.x) - 7.62f;
        startX = startXx;//transform.position.x;
        
        if (startXx < myCenter.x)
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
    }

    private void Move()
    {
        if (Math.Abs(needG) > Math.Abs(movedG))
        {
            if (needG > movedG)
            {
                movedG += 5f;
            }
            else
            {
                movedG -= 5f;
            }
        }
        
        
        
        float placeX = (float)Math.Cos((double)(movedG * Math.PI / 180f)) * distation;
        
        if (moveRight)
        {
            placeX = myCenter.x - placeX;
        }
        else
        {
            placeX = myCenter.x + placeX;
        }

        float placeY = distation * (float)Math.Sin((double)(movedG * Math.PI / 180f));
        if (!moveUp)
        {
            placeY = placeY * (-1f);
        }

        placeY += myCenter.y;

        transform.position = new Vector3( placeX, placeY, transform.position.z);
    }
}
