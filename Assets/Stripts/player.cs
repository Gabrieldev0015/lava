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

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- Rotação horizontal (Gira o corpo todo no eixo Y) ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, mouseX, 0f);

        // --- Rotação vertical (Gira apenas o container da câmera no eixo X) ---
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraContainer.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // --- Captura de Inputs ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = transform.right * h + transform.forward * v;
        transform.position += direction * moveSpeed * Time.deltaTime;
        //enquanto não funciona o axis
       

        // --- Pulo ---
        float jump = Input.GetAxis("Jump");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)//--- Quando o chão for detectado e o jogador apertar espaço, ele irá pular
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);

        }


    }


    // --- Detecção de Chão ---

    //---Qunando o jogador colidir com o chão, isGrounded se torna true
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    //---Quando o jogador sair do chão, isGrounded se torna false
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }

}