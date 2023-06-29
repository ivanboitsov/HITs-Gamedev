using UnityEngine;

public class WayPoint : MonoBehaviour
{
public float rotationAmount = 90f;
    public float rotationSpeed = 5f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isRotating = false;
    private GameObject player;
    private bool isBackward = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && !isRotating)
        {
            initialRotation = player.transform.rotation;

            float targetAngle = initialRotation.eulerAngles.y + rotationAmount;
            
            if (isBackward)
            {
                targetAngle = initialRotation.eulerAngles.y - rotationAmount;
                isBackward = false;
            }
            else
            {
                isBackward = true;
            }

            targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            isRotating = true;
            
        }
    }

    private void Update()
    {
        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, targetRotation, step);

            if (Quaternion.Angle(player.transform.rotation, targetRotation) < 0.1f)
            {
                isRotating = false;
            }
        }
    }
}
