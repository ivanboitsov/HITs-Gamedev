using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public CubeController[] cubes;
    private bool isGameStarted = false;
    private Vector3[] initialPositions;

    private void Start()
    {
        initialPositions = new Vector3[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.position;
        }

        ShuffleCubes();
        isGameStarted = true;
    }

    private void Update()
    {
        if (isGameStarted && CheckWinCondition())
        {
            Debug.Log("pyatnashki win");
            isGameStarted = false;
        }
    }

    private void ShuffleCubes()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            int randomIndex = Random.Range(i, cubes.Length);
            SwapCubes(i, randomIndex);
        }
    }

    private void SwapCubes(int indexA, int indexB)
    {
        Vector3 tempPosition = cubes[indexA].transform.position;
        cubes[indexA].transform.position = cubes[indexB].transform.position;
        cubes[indexB].transform.position = tempPosition;
    }

    private bool CheckWinCondition()
    {
        float epsilon = 0.01f;

        for (int i = 0; i < cubes.Length; i++)
        {
            float distance = Vector3.Distance(cubes[i].transform.position, initialPositions[i]);
            if (distance > epsilon)
            {
                return false;
            }
        }
        return true;
    }
}
