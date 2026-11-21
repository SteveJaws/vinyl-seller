using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VinylSpawnAreaBehaviour : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float spawnAreaWidth = 0;
    private float spawnAreaHeight = 0;

    [SerializeField] GameObject vinyl;
    [SerializeField] GameObject gameArea;

    [SerializeField] float timeBetweenVinylSpawns = 3f;

    private List<GameObject> vinylList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnAreaWidth = spriteRenderer.bounds.size.x;
        spawnAreaHeight = spriteRenderer.bounds.size.y;

        for(int i = 0; i < 10; i++)
        {
            vinylList.Add(vinyl);
        }

        spawnVinylList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnVinylList()
    {
        int vinylListCount = vinylList.Count;

        for(int i = 0; i < vinylListCount; i++)
        {   
            float spawnPointX = 0;
            // if(i == 0)
            // {
            //     spawnPointX = spawnAreaWidth / vinylListCount;
            // }
            // else
            // {
            //     spawnPointX = spawnAreaWidth / vinylListCount * i;
            // }

            spawnPointX = spawnAreaWidth / vinylListCount * i;

            GameObject currentVinyl = vinylList[i];

            Vector3 newPos = transform.position; 

            newPos.x = spawnAreaWidth / 2;
            newPos.x += spawnPointX;
            newPos.x -= vinyl.GetComponent<SpriteRenderer>().bounds.size.x / 2;

            currentVinyl.transform.position = newPos;

            GameObject spawnedVinyl = Instantiate(currentVinyl, gameArea.transform);

            spawnedVinyl.GetComponent<VinylBehaviour>().testVar = 15;
        }
    }
}
