using System.Collections;
using System.Collections.Generic;
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rotateClockwise = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rotateCounterClockwise = true;
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
        isFacingRight = !isFacingRight; // Инвертируем флаг направления
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Инвертируем масштаб по оси X
        transform.localScale = scale;
    }
}
