using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject[] objects;

    void Start(){
        int percentualeSpawn = Random.Range(0, 101);
        if(percentualeSpawn < 75){
            int rand = Random.Range(0, objects.Length);
            var newObject = Instantiate(objects[rand], transform.position, Quaternion.identity);
            //newObject.transform.parent = GameObject.;
        }
    }
}
