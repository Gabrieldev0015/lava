using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Configurações de Vida e Checkpoints")]
    [Tooltip("Coloque aqui a coordenada Y do mar.")]
    public float alturaDoMar = -5f;

    // Armazena a posição de onde o jogador deve renascer
    private Vector3 ultimoCheckpoint;

    // Referência ao CharacterController do seu script Playermover
    private CharacterController controller;

    void Start()
    {
        // Pega o CharacterController que já está no jogador
        controller = GetComponent<CharacterController>();

        // Define que o checkpoint inicial é o exato local onde ele nasce no mapa
        ultimoCheckpoint = transform.position;
    }

    void Update()
    {
        // Verifica se o jogador caiu na água (passou da altura do mar)
        if (transform.position.y <= alturaDoMar)
        {
            Respawnar();
        }
    }

    void Respawnar()
    {
        // O SEGREDO DO CHARACTER CONTROLLER:
        // Precisamos desligá-lo rapidamente para que ele permita o teleporte
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Teleporta o jogador de volta para a posição salva
        transform.position = ultimoCheckpoint;

        // Liga o Character Controller de volta para ele voltar a andar e cair
        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Caiu no mar! Voltando ao último checkpoint...");
    }

    // Essa função é chamada automaticamente quando o jogador encosta em uma Trigger
    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Checkpoint"))
        {
            // Salva a posição exata deste checkpoint
            ultimoCheckpoint = outro.transform.position;
            Debug.Log("Novo checkpoint salvo!");

            // Desativa o collider do checkpoint para ele não ser pego duas vezes
            outro.enabled = false;
        }
        else if (outro.CompareTag("Finish"))
        {
            VencerJogo();
        }
    }

    void VencerJogo()
    {
        Debug.Log("Parabéns! Você alcançou o 4º checkpoint e venceu!");
        // Futuramente você pode colocar a tela de vitória aqui
    }
}
