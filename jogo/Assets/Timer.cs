using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private FimJogo TelaFimJogo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject fimJogoGameObject = GameObject.FindGameObjectWithTag("TelaFimJogo");
        this.TelaFimJogo = fimJogoGameObject.GetComponent<FimJogo>();
        this.TelaFimJogo.Esconder();
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;
            this.gameObject.SetActive(false);
            //Exibir tela de fim de jogo
            TelaFimJogo.Exibir();


        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
