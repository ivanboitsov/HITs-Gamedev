using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public float speed = 0.5f;
    public float rotationSpeed = 500f;
    private float rotationAmount = 90f;
    private Quaternion targetRotation;
    private bool rotateClockwise;
    private bool rotateCounterClockwise;
    private bool inTurnZone = false;
    private bool isFacingRight = true;
    private int inputBoost = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow) && Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, 90f, 0f)) < 0.01f && inTurnZone)
        {
            rotateCounterClockwise = true;
            inputBoost = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Quaternion.Angle(transform.rotation, Quaternion.identity) < 0.01f && inTurnZone)
        {
            rotateClockwise = true;
            inputBoost = 0;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 moveDirection = transform.right * horizontalInput * inputBoost;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime * inputBoost);

        bool isRun = moveDirection.magnitude > 0.1f;
        animator.SetBool("isRun", isRun);

        if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput == 0)
        {
            inputBoost = 1;
        }
    }

    void FixedUpdate()
    {
        if (rotateClockwise)
        {
            targetRotation *= Quaternion.Euler(0f, rotationAmount, 0f);
            rotateClockwise = false;
        }
        else if (rotateCounterClockwise)
        {
            targetRotation *= Quaternion.Euler(0f, -rotationAmount, 0f);
            rotateCounterClockwise = false;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TurnZone"))
        {
            // Персонаж находится в зоне поворота, разрешаем поворот
            inTurnZone = true;
            rotationSpeed = 500f;
            UnityEngine.Debug.Log("Персонаж вошел в зону поворота.");
        }
        else if (other.CompareTag("WrongPlaceUp"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(-4.85f, 0.165f, transform.position.z);
        }
        else if (other.CompareTag("RightPlaceUP"))
        {
            UnityEngine.Debug.Log("Персонаж вошел туда");
            transform.position = new Vector3(-4.85f, transform.position.y+1.5f, transform.position.z);
        }
        else if (other.CompareTag("WrongPlaceDown"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(2.34f, 0.165f, transform.position.z);
        }
        else if (other.CompareTag("RightPlaceDown"))
        {
            UnityEngine.Debug.Log("Персонаж вошел туда");
            transform.position = new Vector3(2.34f, transform.position.y + 1.5f, transform.position.z);
        }
        else if (other.CompareTag("WrongPlaceRight"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(transform.position.x, 0.165f, -5.45f);
        }
        else if (other.CompareTag("RightPlaceRight"))
        {
            UnityEngine.Debug.Log("Персонаж вошел туда");
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, -5.45f);
        }
        else if (other.CompareTag("WrongPlaceLeft"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(transform.position.x, 0.165f, -12.55f);
        }
        else if (other.CompareTag("RightPlaceLeft"))
        {
            UnityEngine.Debug.Log("Персонаж вошел туда");
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, -12.55f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TurnZone"))
        {
            // Персонаж вышел из зоны поворота, запрещаем поворот
            inTurnZone = false;

            UnityEngine.Debug.Log("Персонаж вышел из зоны поворота.");
        }
    }
}
