using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject baseCube;

    private bool isClickable = true;

    private void OnMouseDown()
    {
        if (!isClickable)
            return;

        CubeController baseCubeController = baseCube.GetComponent<CubeController>();

        if (IsAdjacentCube(baseCubeController))
        {
            StartCoroutine(SwapCubes(baseCubeController));
        }
    }

    private bool IsAdjacentCube(CubeController baseCubeController)
    {
        Vector3 currentPosition = transform.position;
        Vector3 basePosition = baseCube.transform.position;

        return Mathf.Abs(currentPosition.x - basePosition.x) + Mathf.Abs(currentPosition.y - basePosition.y) < 0.7;
    }

    private System.Collections.IEnumerator SwapCubes(CubeController baseCubeController)
    {
        isClickable = false;

        Vector3 basePosition = baseCube.transform.position;
        Vector3 nowPosition = transform.position;

        yield return StartCoroutine(MoveObjectToPosition(transform, basePosition, 0.2f));
        yield return StartCoroutine(MoveObjectToPosition(baseCube.transform, nowPosition, 0.2f));

        yield return new WaitForSeconds(0.5f);

        baseCubeController.isClickable = true;
        isClickable = true;
    }

    private System.Collections.IEnumerator MoveObjectToPosition(Transform obj, Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = obj.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            obj.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        obj.position = targetPosition;
    }
}
