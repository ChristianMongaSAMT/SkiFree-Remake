using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : MonoBehaviour
{
    private GameObject player;
    private float velocita = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, velocita * Time.deltaTime);
    }
}
