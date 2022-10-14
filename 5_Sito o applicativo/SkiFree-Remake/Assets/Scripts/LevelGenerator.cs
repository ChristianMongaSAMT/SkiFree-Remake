using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField]private Transform levelPart_1;
    
    [SerializeField]private PlayerMovement player;

    private Vector3[] lastEndPositionsBottom = new Vector3[3];
    private Vector3[] lastEndPositionsRight = new Vector3[2];       //0 è sotto, 1 è destra

    private void Awake(){

        lastEndPositionsBottom[0] = levelPart_Start.Find("PosizioneFinaleSinistra").position;       //0 --> sx
        lastEndPositionsBottom[1] = levelPart_Start.Find("PosizioneFinaleSotto").position;          //1 --> dw
        lastEndPositionsBottom[2] = levelPart_Start.Find("PosizioneFinaleDestra").position;         //2 --> dx
        SpawnNewPartBottomPrincipal();
    }

    private void Update(){
        //quando il player si trova ad una distanza minore di quella definita della costante allora spawna una nuova parte
        if(Vector3.Distance(player.getPosition(), lastEndPositionsRight[1]) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            //Aggiunge un'altra parte
            SpawnNewPartRightPrincipal();
        }else if(Vector3.Distance(player.getPosition(), lastEndPositionsBottom[1]) < PLAYER_DISTANCE_SPAWN_LEVEL_PART){
            SpawnNewPartBottomPrincipal();
        }
    }

    private void SpawnNewPartBottomPrincipal(){
        //crea le prime tre parti intorno a quella di riferimento
        Transform lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionsBottom[1]);
        Transform lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionsBottom[2]);
        Transform lastLevelPartTransformLeft = SpawnLevelPart(lastEndPositionsBottom[0]);

        //salva le posizioni della parte inferiore
        lastEndPositionsBottom[1] = lastLevelPartTransformBottom.Find("PosizioneFinaleSotto").position;
        lastEndPositionsBottom[2] = lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position;
        lastEndPositionsBottom[0] = lastLevelPartTransformBottom.Find("PosizioneFinaleSinistra").position;
        
        //crea altre 3 parti come le prime intorno alla parte inferiore
        lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionsBottom[1]);
        lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionsBottom[2]);
        lastLevelPartTransformLeft = SpawnLevelPart(lastEndPositionsBottom[0]);

        //salva le posizioni in caso di percorrimento verso destra
        lastEndPositionsRight[0] = lastLevelPartTransformRight.Find("PosizioneFinaleSotto").position;
        lastEndPositionsRight[1] = lastLevelPartTransformRight.Find("PosizioneFinaleDestra").position;

        //salva le posizioni dell'ultima parte inferiore creata
        lastEndPositionsBottom[1] = lastLevelPartTransformBottom.Find("PosizioneFinaleSotto").position;
        lastEndPositionsBottom[2] = lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position;
        lastEndPositionsBottom[0] = lastLevelPartTransformBottom.Find("PosizioneFinaleSinistra").position;
    }

    private void SpawnNewPartRightPrincipal(){                                      //NON FUNZIONA lavorare sull'ultima parte in diagonale creata, crea la curva molto più in basso e una volta entrato non può più tornare dritto
        //Crea le parti in caso di percorrimento verso destra
        Transform lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionsRight[0]);
        Transform lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionsRight[1]);
        //crea la parte nell'angolo che farà da riferimento per la prossima
        Transform lastLevelPartTransformDiagonal = SpawnLevelPart(lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position);

        //salva le posizioni di dx e dw della parte di riferimento
        lastEndPositionsRight[0] = lastLevelPartTransformDiagonal.Find("PosizioneFinaleSotto").position;
        lastEndPositionsRight[1] = lastLevelPartTransformDiagonal.Find("PosizioneFinaleDestra").position;
        
        //crea a destra e sotto di quella altre due parti
        lastLevelPartTransformBottom = SpawnLevelPart(lastEndPositionsRight[0]);
        lastLevelPartTransformRight = SpawnLevelPart(lastEndPositionsRight[1]);
        //crea una nuova parte di riferimento
        lastLevelPartTransformDiagonal = SpawnLevelPart(lastLevelPartTransformBottom.Find("PosizioneFinaleDestra").position);

        //salvo posizioni dell'ultima creata
        lastEndPositionsRight[0] = lastLevelPartTransformDiagonal.Find("PosizioneFinaleSotto").position;
        lastEndPositionsRight[1] = lastLevelPartTransformDiagonal.Find("PosizioneFinaleDestra").position;
    }

    private void SpawnLevelPartVoid(Vector3 spawnPosition){
        Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition){
        Transform levelPartTransform = Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
