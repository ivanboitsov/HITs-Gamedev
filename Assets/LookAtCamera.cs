using System.Security.Cryptography;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GameObject[] objectsToLookAtCamera = GameObject.FindGameObjectsWithTag("PlayerStaring");

        foreach (GameObject obj in objectsToLookAtCamera)
        {
            obj.transform.LookAt(mainCamera.transform);
            Vector3 eulerAngles = obj.transform.rotation.eulerAngles;
            //float yAxis = (int)((eulerAngles.y + 180f + 45f) / 90f) * 90f;
            float yAxis = transform.rotation.eulerAngles.y;
            obj.transform.rotation = Quaternion.Euler(90f, yAxis, 180f);
        }
    }
}
