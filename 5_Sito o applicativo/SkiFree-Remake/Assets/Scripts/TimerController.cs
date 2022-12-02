using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerController : MonoBehaviour
{
    private TextMeshProUGUI tempoTesto;

    DateTime dt = DateTime.Now;
    private double tempoIniziale;
    private double tempoGioco;

    void Start()
    {
        //salva il tempo dell'inizio della partita
        DateTime vuota = new DateTime();
        TimeSpan time = dt - vuota;
        tempoIniziale = time.TotalSeconds;

        //prende il campo di testo per il tempo di gioco
        tempoTesto = GameObject.FindGameObjectWithTag("TempoGioco").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //salva il tempo attuale
        DateTime actualTime = DateTime.Now;

        //calcola il tempo di gioco
        tempoGioco = actualTime.Second - tempoIniziale;

        //converte il tempo di gioco
        TimeSpan time = TimeSpan.FromSeconds(tempoGioco);

        //formatta il tempo di gioco in hh:mm:ss
        string str = time .ToString(@"hh\:mm\:ss");

        //scrive il tempo di gioco nel suo campo a schermo
        tempoTesto.text = "Tempo: " + str;
    }
}
