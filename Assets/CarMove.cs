using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private Vector3 initialPosition;
    public Vector3 finalPosition;

    private bool isMoving = false;

    public Image screenOverlay;
    public PlayerControl playerControl;

    private void Start()
    {
        initialPosition = transform.position;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (finalPosition - transform.position).normalized;
        float distance = speed * Time.deltaTime;

        if (distance >= Vector3.Distance(transform.position, finalPosition))
        {
            // Если объект достиг цели, телепортировать его на начальную позицию
            transform.position = initialPosition;
        }
        else
        {
            // Если объект еще не достиг цели, продолжить движение
            transform.position += direction * distance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerControl.isDead = true;

            Time.timeScale = 0.1f; // Замедляем время в 10 раз
            StartCoroutine(RestartScene());
        }
    }

    private System.Collections.IEnumerator RestartScene()
    {
        // Затемняем экран
        float duration = 0.5f; // Длительность анимации затемнения
        float targetAlpha = 1f; // Целевая прозрачность (полностью непрозрачный)

        Color initialColor = screenOverlay.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            screenOverlay.color = Color.Lerp(initialColor, targetColor, t);

            yield return null;
        }

        // Перезапускаем сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerControl.isDead = false;
        Time.timeScale = 1f;
    }
}
