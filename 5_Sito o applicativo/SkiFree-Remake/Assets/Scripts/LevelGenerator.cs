using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField]private Transform levelPart_1;
    
    [SerializeField]private PlayerMovement player;

    private Vector3 lastEndPositionRight;
    private Vector3 lastEndPositionBottom;
    private Vector3 lastEndPositionLeft;

    private void Awake(){

        lastEndPositionRight = levelPart_Start.Find("PosizioneFinaleDestra").position;
        lastEndPositionBottom = levelPart_Start.Find("PosizioneFinaleSotto").position;
        lastEndPositionLeft = levelPart_Start.Find("PosizioneFinaleSinistra").position;
    }

    private void Update(){
        //quando il player si trova ad una distanza minore di quella definita della costante allora spawna una nuova parte
        if(Vector3.Distance(player.getPosition(), lastEndPositionRight) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            //Aggiunge un'altra parte
            SpawnNewPartRightPrincipal();
        }else if(Vector3.Distance(player.getPosition(), lastEndPositionBottom) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            SpawnNewPartBottomPrincipal();
        }
    }

    private void SpawnNewPartBottomPrincipal(){
                Transform lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionBottom);
                Transform lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionRight);
                Transform lastLevelPartTransformLeft = SpawnLevelPart(lastEndPositionLeft);

                lastEndPositionRight = lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position;
                lastEndPositionBottom = lastLevelPartTransformBottom.Find("PosizioneFinaleSotto").position;
                lastEndPositionLeft = lastLevelPartTransformBottom.Find("PosizioneFinaleSinistra").position;
                
                lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionRight);
                lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionBottom);
                lastLevelPartTransformLeft = SpawnLevelPart(lastEndPositionLeft);

                lastEndPositionRight = lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position;
                lastEndPositionBottom = lastLevelPartTransformBottom.Find("PosizioneFinaleSotto").position;
                lastEndPositionLeft = lastLevelPartTransformBottom.Find("PosizioneFinaleSinistra").position;
    }

    private void SpawnNewPartRightPrincipal(){
                Transform lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionBottom);
                Transform lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionRight);
                Transform lastLevelPartTransformLeft = SpawnLevelPart(lastEndPositionLeft);

                lastEndPositionRight = lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position;
                lastEndPositionBottom = lastLevelPartTransformBottom.Find("PosizioneFinaleSotto").position;
                lastEndPositionLeft = lastLevelPartTransformBottom.Find("PosizioneFinaleSinistra").position;
                
                lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionRight);
                lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionBottom);
                lastLevelPartTransformLeft = SpawnLevelPart(lastEndPositionLeft);

                lastEndPositionRight = lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position;
                lastEndPositionBottom = lastLevelPartTransformBottom.Find("PosizioneFinaleSotto").position;
                lastEndPositionLeft = lastLevelPartTransformBottom.Find("PosizioneFinaleSinistra").position;
    }

    private void SpawnLevelPartVoid(Vector3 spawnPosition){
        Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition){
        Transform levelPartTransform = Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
