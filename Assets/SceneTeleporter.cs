using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [Header("Scene Id")]
    public int teleportScene = 0;

    public void Teleport()
    {
        SceneManager.LoadScene(teleportScene);
    }
}
