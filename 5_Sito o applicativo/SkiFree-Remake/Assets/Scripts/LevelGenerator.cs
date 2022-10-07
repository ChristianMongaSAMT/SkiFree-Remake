using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPart_Start;
    [SerializeField]private Transform levelPart_1;
    private void Awake(){
        Transform lastLevelPartTransform;
        lastLevelPartTransform = SpawnLevelPart(levelPart_Start.Find("PosizioneFinale").position);
        lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("PosizioneFinale").position);
        lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("PosizioneFinale").position);
        lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("PosizioneFinale").position);
        lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("PosizioneFinale").position);
        lastLevelPartTransform = SpawnLevelPart(lastLevelPartTransform.Find("PosizioneFinale").position);
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition){
        Transform levelPartTransform = Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
