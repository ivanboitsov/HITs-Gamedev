using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [Header("Scripts")]
    public SceneTeleporter Teleporter;
    public CheckWin RoundWin;

    [Header("Movement")]
    public float speed = 0.5f;
    public float rotationSpeed = 500f;
    public float jumpCooldown = 0.5f;

    [Header("Other")]
    public float playerHeight = 0.1f;
    public float interractCooldown = 0.2f;

    [Header("Bools")]
    public bool flyingUp = false;
    public bool ventIsOn = true;
    public bool haveLeverDetail = false;
    public bool haveSvetlyachki = false;
    public bool haveLukKey = false;
    public bool haveRope = false;
    public bool haveBranch = false;
    public bool haveWorm = false;
    public bool fishAppeared = false;
    public bool haveFish = false;
    

    [Header("Binds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode interractKey = KeyCode.E;
    public KeyCode turnerKey = KeyCode.Q;

    private Animator ventAnimator;

    private float rotationAmount = 90f;
    private Quaternion targetRotation;
    private bool rotateClockwise;
    private bool rotateCounterClockwise;
    private bool isFacingRight = true;
    private int inputBoost = 1;
    private bool grounded;
    private bool jumpReady = true;
    public bool interractReady = true;
    private float leverFacing = -30f;
    private bool lukOpened = false;
    private float lukFacing = 0f;

    [Header("Interract Zones Checkers")]
    public bool inTurnZone = false;
    public bool inInterractZone = false;
    public bool inLeverZone = false;
    public bool inLeverPickerZone = false;
    public bool inSvetlyachkiPickerZone = false;
    public bool inLukOpenZone = false;
    public bool inKeyPickZone = false;
    public bool inRopeZone = false;
    public bool inBranchZone = false;
    public bool inWormZone = false;
    public bool inLakeZone = false;
    public bool inFishZone = false;

    [Header("Ground Layers")]
    public LayerMask groundLayer;
    public LayerMask groundNonJumpLayer;

    void Start()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("myVent");

        foreach (GameObject obj in objectsWithTag)
        {
            ventAnimator = obj.GetComponent<Animator>();
        }

        FixLevers();
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
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer) || Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundNonJumpLayer);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        inInterractZone = inLeverZone || inLeverPickerZone || inSvetlyachkiPickerZone || inLukOpenZone || inKeyPickZone || inBranchZone ||
            inRopeZone || inWormZone || inFishZone || inLakeZone;

        if (Input.GetKey(turnerKey) && inTurnZone && interractReady)
        {
            if (Quaternion.Angle(transform.rotation, Quaternion.Euler(0f, 90f, 0f)) <= 0.01f)
            {
                rotateCounterClockwise = true;
                inputBoost = 0;
            }
            else if (Quaternion.Angle(transform.rotation, Quaternion.identity) < 0.01f)
            {
                rotateClockwise = true;
                inputBoost = 0;
            }
            interractReady = false;
            Invoke(nameof(ResetInterraction), jumpCooldown);
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
            if (inLeverZone && haveLeverDetail)
            {
                if (!ventIsOn)
                {
                    ventIsOn = true;
                    leverFacing = -30f;
                }
                else
                {
                    ventIsOn = false;
                    leverFacing = 30f;
                }
                FixLevers();
            }
            else if (inLukOpenZone && haveLukKey)
            {
                haveLukKey = false;
                lukOpened = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("lukTurner");
                foreach (GameObject obj in objectsWithTag)
                {
                    GameObject luk_obj = obj.transform.GetChild(1).gameObject;
                    luk_obj.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else if (inLeverPickerZone && !haveLeverDetail)
            {
                haveLeverDetail = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("leverDetail");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y-1f, obj.transform.position.z);
                }
            }
            else if (inSvetlyachkiPickerZone && !haveSvetlyachki)
            {
                haveSvetlyachki = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("svetlyachki_picking");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 10f, obj.transform.position.z);
                }
            }
            else if (inKeyPickZone && !haveLukKey)
            {
                haveLukKey = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("keyPickZone");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 2f, obj.transform.position.z);
                }
            }
            else if (inRopeZone && !haveRope)
            {
                haveRope = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("ropePick");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 20f, obj.transform.position.z);
                }
            }
            else if (inBranchZone && !haveBranch)
            {
                haveBranch = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("branchPick");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 20f, obj.transform.position.z);
                }
            }
            else if (inWormZone && !haveWorm && RoundWin.winned)
            {
                haveWorm = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("wormPick");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 20f, obj.transform.position.z);
                }
            }
            else if (inLakeZone && !fishAppeared && haveBranch && haveRope && haveWorm)
            {
                fishAppeared = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("fishPick");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + 20f, obj.transform.position.z);
                }
            }
            else if (inFishZone && !haveFish)
            {
                haveFish = true;
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("fishPick");
                foreach (GameObject obj in objectsWithTag)
                {
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - 20f, obj.transform.position.z);
                }
            }
        }



        if (inTurnZone)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        Vector3 moveDirection = transform.right * horizontalInput * inputBoost;
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime * inputBoost);

        bool isRun = moveDirection.magnitude > 0.1f;
 
        if (!grounded)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isRun", isRun);
            animator.SetBool("isJump", false);
        }

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
        luckRotation();
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
            inTurnZone = true;
            rotationSpeed = 500f;
        }
        else if (other.CompareTag("WrongPlaceUp"))
        {
            transform.position = new Vector3(-4.85f, 0.165f, transform.position.z);
        }
        else if (other.CompareTag("RightPlaceUP"))
        {
            transform.position = new Vector3(-4.85f, transform.position.y+1.5f, transform.position.z);
        }
        else if (other.CompareTag("WrongPlaceDown"))
        {
            transform.position = new Vector3(2.34f, 0.165f, transform.position.z);
        }
        else if (other.CompareTag("RightPlaceDown"))
        {
            transform.position = new Vector3(2.34f, transform.position.y + 1.5f, transform.position.z);
        }
        else if (other.CompareTag("WrongPlaceRight"))
        {
            transform.position = new Vector3(transform.position.x, 0.165f, -5.45f);
        }
        else if (other.CompareTag("RightPlaceRight"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, -5.45f);
        }
        else if (other.CompareTag("WrongPlaceLeft"))
        {
            transform.position = new Vector3(transform.position.x, 0.165f, -12.55f);
        }
        else if (other.CompareTag("RightPlaceLeft"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, -12.55f);
        }
        else if (other.CompareTag("PushingUp"))
        {
            flyingUp = true;
        }
        else if (other.CompareTag("TurningPushingUp") && ventIsOn)
        {
            flyingUp = true;
        }
        else if (other.CompareTag("Vent_turner"))
        {
            inLeverZone = true;
            FixLevers();
        }
        else if (other.CompareTag("leverDetailPick"))
        {
            inLeverPickerZone = true;
        }
        else if (other.CompareTag("svetlyachki_picking"))
        {
            inSvetlyachkiPickerZone = true;
        }
        else if (other.CompareTag("lukOpenZone"))
        {
            inLukOpenZone = true;
        }
        else if (other.CompareTag("keyPickZone"))
        {
            inKeyPickZone = true;
        }
        else if (other.CompareTag("SceneTpTag"))
        {
            Teleporter.Teleport();
        }
        else if (other.CompareTag("ropePick"))
        {
            inRopeZone = true;
        }
        else if (other.CompareTag("branchPick"))
        {
            inBranchZone = true;
        }
        else if (other.CompareTag("wormPick"))
        {
            inWormZone = true;
        }
        else if (other.CompareTag("lakePick"))
        {
            inLakeZone = true;
        }
        else if (other.CompareTag("fishPick"))
        {
            inFishZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TurnZone"))
        {
            inTurnZone = false;
        }
        else if (other.CompareTag("PushingUp") || other.CompareTag("TurningPushingUp"))
        {
            flyingUp = false;
        }
        else if (other.CompareTag("Vent_turner"))
        {
            inLeverZone = false;
        }
        else if (other.CompareTag("leverDetailPick"))
        {
            inLeverPickerZone = false;
        }
        else if (other.CompareTag("svetlyachki_picking"))
        {
            inSvetlyachkiPickerZone = false;
        }
        else if (other.CompareTag("lukOpenZone"))
        {
            inLukOpenZone = false;
        }
        else if (other.CompareTag("ropePick"))
        {
            inRopeZone = false;
        }
        else if (other.CompareTag("branchPick"))
        {
            inBranchZone = false;
        }
        else if (other.CompareTag("wormPick"))
        {
            inWormZone = false;
        }
        else if (other.CompareTag("lakePick"))
        {
            inLakeZone = false;
        }
        else if (other.CompareTag("fishPick"))
        {
            inFishZone = false;
        }
    }

    void ventRotating()
    {
        if (ventAnimator)
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
    }

    private void Jump()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundNonJumpLayer))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * 5.5f, ForceMode.Impulse);
        }
    }

    private void ResetJump()
    {
        jumpReady = true;
    }

    private void ResetInterraction()
    {
        interractReady = true;
    }

    private void FixLevers()
    {
        if (!haveLeverDetail)
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("lever_turner_obj");
            foreach (GameObject obj in objectsWithTag)
            {
                obj.transform.rotation = Quaternion.Euler(180f, -90f, (-1f)*leverFacing);
            }
        }
        else
        {
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("lever_turner_obj");
            foreach (GameObject obj in objectsWithTag)
            {
                obj.transform.rotation = Quaternion.Euler(0f, -90f, leverFacing);
            }
        }
    }

    void luckRotation()
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("lukTurner");
        if (lukOpened)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                if (lukFacing > -75f)
                {
                    lukFacing = lukFacing - Math.Min(Math.Abs(lukFacing+75f), Math.Abs(lukFacing)) *0.008f - 0.008f;
                    obj.transform.rotation = Quaternion.Euler(0f, 0f, lukFacing);
                }
            }
        }
        
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
