using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton.Translates existing Obstacles and creates additional Obstacles.  
// The world moves around the player.

// When this object is collided into, the level pieces are deactivated.
// Level ends when all level pieces are deactivated

public class LevelTranslator : MonoBehaviour
{
    public static LevelTranslator instance = null;

    // FIELDS
    public bool automaticallyStart = false;
    public bool isEndless = false;

    // Are all of the level pieces used?
    public bool levelOver
    {
        get
        {
            if (allLevelObjects.Count == 0)
                return true;
            else return false;
        }
    }

    // Where all of the transforms are attached to
    public Transform levelParent = null;

    // Prefab to add to the level 
    public List<Transform> endlessLevelObject_Prefabs = null;

    // Where the level starts. ENDLESS!
    public Vector3 levelObjectOrigin = new Vector3();

    // The last created object. ENDLESS!
    public Transform lastCreatedObject = null;

    // Contains all of the transforms for the level
    public List<Transform> allLevelObjects = new List<Transform>();

    // Game Values
    public float speed = 5;
    public float objectSpacing = 5;                 // ENDLESS. How much object spacing there is between the current level piece and the going-to-spawn level piece
    public Vector3 direction = Vector3.forward;
    public bool stopMoving = false;

    // Starts the level
    public void BeginMovingLevel()
    {
        StartCoroutine(BeginMovingLevel_Coroutine());
    }

    IEnumerator BeginMovingLevel_Coroutine()
    {
        while (!levelOver)
        {
            if (stopMoving)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                continue;
            }

            // Moves the Transform Parent. All Objects will be attached to the parent, so it's moving the objects...
            levelParent.Translate(speed * direction * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);

            //if (isEndless)
            //{
            //if (Vector3.Distance(lastCreatedObject.position, levelObjectOrigin) >= objectSpacing + Time.deltaTime)
            //{
                    
                Transform t = Instantiate(endlessLevelObject_Prefabs[Random.Range(0, endlessLevelObject_Prefabs.Count)], levelObjectOrigin, Quaternion.identity) as Transform;
                t.parent = levelParent;
                lastCreatedObject = t;
                allLevelObjects.Add(t);
                levelObjectOrigin = new Vector3(Random.Range(levelParent.position.x, levelParent.position.x+Random.Range(-6.0f, 6.0f)), Random.Range(levelParent.position.y-3, levelParent.position.y - 6), 0);

            //}
            //}
            /*else
            {
                if (Vector3.Distance(lastCreatedObject.position, levelObjectOrigin) >= objectSpacing + Time.deltaTime)
                {
                    Transform t = allLevelObjects[0];
                    t.position = levelObjectOrigin;
                    t.rotation = Quaternion.identity;

                    t.parent = levelParent;
                    t.gameObject.SetActive(true);

                    lastCreatedObject = t;
                    allLevelObjects.Remove(t);
                }
            }*/
        }
    }

    void Start()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        if (isEndless)
        {
            if (lastCreatedObject == null)
            {
                Debug.LogError("ERROR! It's endless mode, but you didn't assign the lastCreatedObject to a starting object! You fucked up! (Well, it's okay, I did...)");
                return;
            }

            allLevelObjects.Add(lastCreatedObject);
            levelObjectOrigin = lastCreatedObject.position;
        }
        /*else
        {
            if (allLevelObjects.Count > 0)
                Debug.LogWarning("WARNING! There are no Objects in the allLevelObjects list! Place some there in order to avoid this error!");

            lastCreatedObject = allLevelObjects[0];
            allLevelObjects.Remove(lastCreatedObject);
            levelObjectOrigin = lastCreatedObject.position;
        }*/

        // Apply all of the Transforms to the parent
        if (automaticallyStart)
            BeginMovingLevel();
    }

    // Whenever an Obstacle collides with this... deactivate the colliding object.
    void OnCollisionEnter(Collision c)
    {
        Transform obstacleT = c.transform;
        if (allLevelObjects.Contains(obstacleT))
        {
            allLevelObjects.Remove(obstacleT);

            // "Destroys" the object
            obstacleT.gameObject.SetActive(false);
        }
    }
}
