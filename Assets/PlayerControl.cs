using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public float speed = 0.5f;
    public float rotationSpeed = 200f;
    private float rotationAmount = 90f;
    private Quaternion targetRotation;
    private bool rotateClockwise;
    private bool rotateCounterClockwise;
    private bool inTurnZone = false;
    private bool isFacingRight = true;

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
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Quaternion.Angle(transform.rotation, Quaternion.identity) < 0.01f && inTurnZone)
        {
            rotateClockwise = true;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 moveDirection = transform.right * horizontalInput;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);

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
            rotationSpeed = 200f;
            UnityEngine.Debug.Log("Персонаж вошел в зону поворота.");
        }
        else if (other.CompareTag("WrongPlaceUp"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(-4.84f, -0.085f, transform.position.z);
        }
        else if (other.CompareTag("WrongPlaceDown"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(2.25f, -0.085f, transform.position.z);
        }
        else if (other.CompareTag("WrongPlaceRight"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(transform.position.x, -0.085f, -5.4f);
        }
        else if (other.CompareTag("WrongPlaceLeft"))
        {
            UnityEngine.Debug.Log("Персонаж вошел не туда");
            transform.position = new Vector3(transform.position.x, -0.085f, -12.6f);
            
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
