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
            // Нацеливаем объект на камеру
            obj.transform.LookAt(mainCamera.transform);

            // Сбрасываем поворот по осям X и Z
            Vector3 eulerAngles = obj.transform.rotation.eulerAngles;
            obj.transform.rotation = Quaternion.Euler(90f, eulerAngles.y + 180f, 180f);
        }
    }
}
