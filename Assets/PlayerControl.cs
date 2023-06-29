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
    public float playerHeight = 0.1f;
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpCooldown = 0.5f;
    public KeyCode interractKey = KeyCode.E;
    public float interractCooldown = 0.2f;
    public bool ventIsOn = true;
    
    private Animator ventAnimator;

    private float rotationAmount = 90f;
    private Quaternion targetRotation;
    private bool rotateClockwise;
    private bool rotateCounterClockwise;
    private bool inTurnZone = false;
    private bool isFacingRight = true;
    private int inputBoost = 1;
    private bool flyingUp = false;
    private bool grounded;
    private bool jumpReady = true;
    private bool interractReady = true;
    private bool inInterractZone = false;
    private bool inLeverZone = false;
    

    public LayerMask groundLayer;

    void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("myVent");

        foreach (GameObject obj in objectsWithTag)
        {
            // Получаем компонент Animator
            ventAnimator = obj.GetComponent<Animator>();
            if (ventAnimator != null)
            {
                UnityEngine.Debug.Log("Победа");
            }
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetRotation = transform.rotation;
        ResetJump();
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //сложение всех зон взаимодействия, потому что  потому
        inInterractZone = inLeverZone;

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
        if (Input.GetKey(jumpKey) && grounded && jumpReady)
        {
            Jump();
            jumpReady = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKey(interractKey) && inInterractZone && interractReady)
        {
            interractReady = false;
            Invoke(nameof(ResetInterraction), interractCooldown);
            if (inLeverZone)
            {
                if (!ventIsOn)
                {
                    ventIsOn = true;
                }
                else
                {
                    ventIsOn = false;
                }
            }
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

        if (flyingUp)
        {
            Vector3 upperDirection = new Vector3(0f, 1f, 0f);
            rb.AddForce(upperDirection.normalized * 7f, ForceMode.Force);
        }

        ventRotating();
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
        else if (other.CompareTag("PushingUp"))
        {
            UnityEngine.Debug.Log("Персонаж вошёл в зону воздушной струи");
            flyingUp = true;
        }
        else if (other.CompareTag("TurningPushingUp") && ventIsOn)
        {
            UnityEngine.Debug.Log("Персонаж вошёл в зону выключаемой воздушной струи");
            flyingUp = true;
        }
        else if (other.CompareTag("Vent_turner"))
        {
            UnityEngine.Debug.Log("Персонаж вошёл в зону взаимодействия с рычагом");
            inLeverZone = true;
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
        else if (other.CompareTag("PushingUp") || other.CompareTag("TurningPushingUp"))
        {
            UnityEngine.Debug.Log("Персонаж вышел из зоны воздушной струи");
            flyingUp = false;
        }
        else if (other.CompareTag("Vent_turner"))
        {
            UnityEngine.Debug.Log("Персонаж вышел из зоны взаимодействия с рычагом");
            inLeverZone = false;
        }
    }

    void ventRotating()
    {
        if (ventIsOn)
        {
            ventAnimator.speed = ventAnimator.speed + 0.001f;
        }
        else
        {
            ventAnimator.speed = ventAnimator.speed * 0.999f;
        }

        if (ventAnimator.speed > 1f)
        {
            ventAnimator.speed = 1f;
        }
        else if (ventAnimator.speed <= 0.0001f)
        {
            ventAnimator.speed = 0f;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * 5.5f, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
    }

    private void ResetInterraction()
    {
        interractReady = true;
    }

    public AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
