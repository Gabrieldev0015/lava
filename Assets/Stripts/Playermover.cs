using UnityEngine;

public class Playermover : MonoBehaviour
{

    [Header("Configurações de Movimento")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -20f; // Aumentei um pouco para a queda ser mais firme
    public float jumpHeight = 3f;

    [Header("Detecção de Chão")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Animator anim;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Pega o Animator que está no modelo (filho do objeto Player)
        anim = GetComponentInChildren<Animator>();

        // Removemos o Rigidbody daqui, pois o CharacterController faz o trabalho físico.
    }

    void Update()
    {
        // 1. Checagem de Chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. Input de Movimento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // 3. Executa o Movimento
        controller.Move(move * speed * Time.deltaTime);

        // 4. Lógica de Animação
        if (anim != null)
        {
            // move.magnitude retorna a "força" do movimento (0 quando parado, ~1 andando)
            anim.SetFloat("Velocidade", move.magnitude);
        }

        // 5. Pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Fórmula física para altura de pulo: v = sqrt(h * -2 * g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 6. Aplicar Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}