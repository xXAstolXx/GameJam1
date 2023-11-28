using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        transform.Translate(movement * movementSpeed * Time.deltaTime);
    }
}
