using UnityEngine;

public class SmoothMovement : MonoBehaviour
{
    public string targetTag1; // Тег целевых объектов
    public string targetTag2; // Тег целевых объектов

    public Collider zone1;
    public Collider zone2;
    public Collider zone3;
    public Collider zone4;
    public Collider zone5;
    public Collider zone6;

    public float movementSpeed = 1f; // Скорость перемещения

    private bool isMoving;

    void OnMouseDown()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveObjectsBetweenZones1());
            StartCoroutine(MoveObjectsBetweenZones2());
            StartCoroutine(MoveObjectsBetweenZones3());
            StartCoroutine(MoveObjectsBetweenZones4());
            StartCoroutine(MoveObjectsBetweenZones5());
            StartCoroutine(MoveObjectsBetweenZones6());
        }
    }

    private System.Collections.IEnumerator MoveObjectsBetweenZones1()
    {
        GameObject sp1 = getObject(targetTag1, zone1);
        yield return StartCoroutine(MoveObjectToZone(sp1, zone2));
    }

    private System.Collections.IEnumerator MoveObjectsBetweenZones2()
    {
        GameObject sp2 = getObject(targetTag1, zone2);
        yield return StartCoroutine(MoveObjectToZone(sp2, zone3));
    }

    private System.Collections.IEnumerator MoveObjectsBetweenZones3()
    {
        GameObject sp3 = getObject(targetTag1, zone3);
        yield return StartCoroutine(MoveObjectToZone(sp3, zone4));
    }

    private System.Collections.IEnumerator MoveObjectsBetweenZones4()
    {
        GameObject sp4 = getObject(targetTag1, zone4);
        yield return StartCoroutine(MoveObjectToZone(sp4, zone5));
    }

    private System.Collections.IEnumerator MoveObjectsBetweenZones5()
    {
        GameObject sp5 = getObject(targetTag1, zone5);
        yield return StartCoroutine(MoveObjectToZone(sp5, zone6));
    }

    private System.Collections.IEnumerator MoveObjectsBetweenZones6()
    {
        GameObject sp6 = getObject(targetTag1, zone6);
        yield return StartCoroutine(MoveObjectToZone(sp6, zone1));

        isMoving = false;
    }

    private System.Collections.IEnumerator MoveObjectToZone(GameObject obj, Collider toZone)
    {
        if (obj != null)
        {
            Vector3 startPosition = obj.transform.position;
            Vector3 targetPosition = toZone.transform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float duration = distance / movementSpeed;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                obj.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            obj.transform.position = targetPosition;
        }
    }

    private GameObject getObject(string _targetTag, Collider fromZone)
    {
        GameObject[] objectsToMove = GameObject.FindGameObjectsWithTag(_targetTag);

        foreach (GameObject obj in objectsToMove)
        {
            if (fromZone.bounds.Contains(obj.transform.position))
            {
                return obj;
            }
        }

        return null;
    }
}
