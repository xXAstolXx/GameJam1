using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region Members
    #region Movement
    [Header("Movement" , order = 1)]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float movementSpeedLerp;
    [SerializeField]
    private float lerpTime = 0.1f;
    #endregion

    #region Toggle Lerp
    [Header("Use Lerp", order = 2)]
    [SerializeField]
    private Toggle toggle;
    private bool useLerp;
    #endregion
    #endregion

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;

        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        useLerp = toggle.isOn;
        Debug.Log($"toggle State: {toggle.isOn}");
    }

    void Update()
    {
        if(useLerp != true)
        {
            Movement();
        }
        MovementWithLerp();

    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }

    private void MovementWithLerp()
    {
        HandleInput();
        PlayerMovementLerp();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        targetPosition = new Vector2(transform.position.x + horizontal, transform.position.y + vertical);
    }

    private void PlayerMovementLerp()
    {
        Vector2 newPosition = Vector2.Lerp(rb.position, targetPosition, lerpTime * movementSpeedLerp);
        rb.MovePosition(newPosition);
    }
}
