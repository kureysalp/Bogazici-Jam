using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpForce;
    public float fallMultiplier;
    public float lowJump;


    private Vector3 inputVector;
    public Vector3 lookDir;

    Camera mainCam;
    Rigidbody rigidbody;
    Animator animator;

    private bool isGrounded;
    private void Start()
    {
        mainCam = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UserInput();

    }

    private void FixedUpdate()
    {
        Movement();
        Rotation();
        BetterJump();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.velocity = Vector3.up * jumpForce;
            animator.SetTrigger("jump");
            // animator.ResetTrigger("land");
        }
    }

    private void UserInput()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");

        inputVector = new Vector3(_horizontal, 0, _vertical);


        float _forwardValue = Vector3.Dot(transform.forward, new Vector3(inputVector.x, 0, inputVector.z));
        float _rightValue = Vector3.Dot(transform.right, new Vector3(inputVector.x, 0, inputVector.z));
        animator.SetFloat("x", _rightValue);
        animator.SetFloat("y", _forwardValue);
    }

    private void Rotation()
    {
        Plane _playerPlane = new Plane(Vector3.up, transform.position);

        Vector2 _mousePos = Input.mousePosition;
        Ray _ray = mainCam.ScreenPointToRay(_mousePos);

        float _hitDistance = 0;

        if (_playerPlane.Raycast(_ray, out _hitDistance))
        {
            Vector3 _targetpoint = _ray.GetPoint(_hitDistance);
            lookDir = (_targetpoint - transform.position).normalized;
            Quaternion _targetRotation = Quaternion.LookRotation(_targetpoint - transform.position);
            _targetRotation.x = 0;
            _targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void Movement()
    {
        rigidbody.MovePosition(rigidbody.position + inputVector * movementSpeed * Time.deltaTime);
    }

    private void Jump()
    { }

    private void BetterJump()
    {
        if (rigidbody.velocity.y < 0)
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJump - 1) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetTrigger("land");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
            isGrounded = false;
    }
}
