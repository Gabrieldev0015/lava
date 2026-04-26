using UnityEngine;

public class Playermover : MonoBehaviour
{
    private Transform myCamera;

    [Header("Configurações de Movimento")]
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -20f; // Aumentei um pouco para a queda ser mais firme
    public float jumpHeight = 3f;

    [Header("Detecção de Chão")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform Pejogador;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Pega o Animator que está no modelo (filho do objeto Player)
        animator = GetComponentInChildren<Animator>();

        myCamera = Camera.main.transform;
    }

    void Update()
    {
        // 1. Checagem de chão (PRIMEIRO)
        isGrounded = Physics.CheckSphere(Pejogador.position, 0.3f, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
        }

        // 2. Input de Movimento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);

        move = myCamera.TransformDirection(move);
        move.y = 0;

        controller.Move(move * Time.deltaTime * speed);
        controller.Move(new Vector3(0, -9.81f,  0) * Time.deltaTime);


        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * 10);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = 20f;
            animator.SetTrigger("Saltar");
        }

        animator.SetBool("Mover", move != Vector3.zero);

        animator.SetBool("NoChao", isGrounded);


        // 6. Aplicar Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}