using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    public static TimerController timerController;
    
    //contiene il testo formattato
    private TextMeshProUGUI tempoTesto;

    //salva il tempo per poterlo formattare
    private TimeSpan tempoGioco;

    private bool timerAttivo;
    
    //tempo
    private float tempo;

    void Awake(){
        timerController = this;
    }

    void Start()
    {
        //prende il text dove poter scrivere il tempo
        tempoTesto = GameObject.FindGameObjectWithTag("TempoGioco").GetComponent<TextMeshProUGUI>();

        //scrive un testo iniziale
        tempoTesto.text = "Tempo: 00:00:00";
        timerAttivo = false;

        InizioGioco();
    }

    void InizioGioco(){
        //avvia il timer
        TimerController.timerController.AvvioTimer();
    }

    void AvvioTimer(){
        //resetta variabili
        timerAttivo =  true;
        tempo = 0f;
        
        //avvia couroutine per l'aggiornamento del timer
        StartCoroutine(AggiornaTimer());
    }

    private IEnumerator AggiornaTimer(){
        while(timerAttivo){

            //Time.deltaTime è il tempo impiegato dal fotogramma precedente al fotogramma successivo
            tempo += Time.deltaTime;

            
            tempoGioco = TimeSpan.FromSeconds(tempo);

            //formatta il tempo in hh:mm:ss
            string tempoGiocoStringa = "Time: " + tempoGioco.ToString("hh':'mm':'ss");

            //salva all'interno del text nel gioco il tempo di gioco 
            tempoTesto.text = tempoGiocoStringa;

            yield return null;
        }
    }
}
