using UnityEngine;

public class player : MonoBehaviour
{
    [Header("Movimentação")]
    public float moveSpeed = 18f;
    public float jumpForce = 5f;

    [Header("Mouse")]
    public float mouseSensitivity = 2f; // Sensibilidade costuma ser menor que 5
    public float verticalClamp = 50f;

    [Header("Referências")]
    public Transform cameraContainer;

    private float verticalRotation = 0f;
    public bool isGrounded;

    private Rigidbody rb;

    public float forcaPulo = 10f;
    float multiplicadorQueda = 3f;
    float multiplicadorPulo = 2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       
    }

}