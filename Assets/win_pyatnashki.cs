using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public CubeController[] cubes;
    private bool isGameStarted = false;
    private Vector3[] initialPositions;
    public bool Win = false;
    public bool Mercy = false;
    public float moveTimeout = 0.01f;
    public float moved = 0f;


    private void Start()
    {
        initialPositions = new Vector3[cubes.Length];
        for (int i = 0; i < cubes.Length; i++)
        {
            initialPositions[i] = cubes[i].transform.position;
        }

        ShuffleCubes();
        isGameStarted = true;
        moved = transform.position.y;
    }

    private void Update()
    {
        if (isGameStarted && CheckWinCondition())
        {
            Win = true;
            isGameStarted = false;
            
        }
        if (Win)
        {
            MoveAfterWin();
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

    private void MoveAfterWin()
    {
        moved -= 0.1f;
        transform.position = new Vector3(transform.position.x, moved, transform.position.z);
        if (moved < -39.06f)
        {
            Invoke(nameof(MoveAfterWin), moveTimeout);
        }
    }

    private bool CheckWinCondition()
    {
        float epsilon = 0.01f;
        int errors = 0;

        for (int i = 0; i < cubes.Length; i++)
        {
            float distance = Vector3.Distance(cubes[i].transform.position, initialPositions[i]);
            if (distance > epsilon)
            {
                errors++;
            }
        }

        if (errors == 0)
        {
            return true;
        }
        else if (errors == 2)
        {
            Mercy = true;

            return true;
        }
        else
        {
            return false;
        }
    }
}
