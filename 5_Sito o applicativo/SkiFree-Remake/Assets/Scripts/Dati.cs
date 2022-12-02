
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dati : MonoBehaviour
{
    private TextMeshProUGUI metriPercorsiTesto;
    private TextMeshProUGUI punteggioTesto;

    private GameObject player;
    private int metriPercorsi;
    private int punteggio;
    void Start()
    {
        metriPercorsiTesto = GameObject.FindGameObjectWithTag("MetriPercorsi").GetComponent<TextMeshProUGUI>();

        punteggioTesto = GameObject.FindGameObjectWithTag("Punti").GetComponent<TextMeshProUGUI>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       scriviMetriPercorsi();
    }

    private void scriviMetriPercorsi(){
        metriPercorsi = Mathf.Abs((int) player.transform.position.y);
        metriPercorsiTesto.text = metriPercorsi + "m";
    }

    public void scriviPunteggio(int punti){
        punteggio += punti;
        punteggioTesto.text = punteggio + "pt";
    }
}
