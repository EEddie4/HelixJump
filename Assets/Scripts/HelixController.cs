using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector2 lastTapPos;
    private Vector3 startRotation;
    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;

    public List<Stage> allStages = new List<Stage>();
    private float helixDistance;
    private  List<GameObject> spawnedLevels = new List<GameObject>();


    void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + 0.1f);
        LoadStage(0);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos;
            }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if(Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }

    }

    public void LoadStage(int stageNumber)
    {
        Stage stage = allStages [Mathf.Clamp(stageNumber, 0 , allStages.Count - 1)];

        if (stage == null)
        {
            Debug.Log("No stage" + stageNumber + " found in all stages list. Are all stages assigned in the list");
        }

        //Change color of stage background
        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;

        //Change stage ball color
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;

        //Reset helix rotation
        transform.localEulerAngles = startRotation;

        //Destroy Old Levels
        foreach (GameObject go in spawnedLevels)
            Destroy(go);

        //Create New levels/ platforms
        float levelDistance = helixDistance/stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i =0; i<stage.levels.Count; i++)
        {
            spawnPosY -= levelDistance;
            //Creates level within scene 
            GameObject level = Instantiate(helixLevelPrefab, transform);
            Debug.Log("Levels spawned");
            level.transform.localPosition = new Vector3 (0, spawnPosY, 0);
            spawnedLevels.Add(level);

            //Creating the gaps
            int partsToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while(disabledParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))           
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }
            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;
                if(t.gameObject.activeInHierarchy)
                    leftParts.Add(t.gameObject);
            }
             
             //Creating Death Parts
             List<GameObject> deathParts = new List<GameObject>();

             while (deathParts.Count < stage.levels[i]. deathPartCount)
             {
                    GameObject randomPart = leftParts[(Random.Range (0, leftParts.Count))];
                    if (!deathParts.Contains(randomPart))
                    {
                        randomPart.gameObject.AddComponent<DeathPart>();
                        deathParts.Add(randomPart);
                    }
             }
        } 

    }



}
