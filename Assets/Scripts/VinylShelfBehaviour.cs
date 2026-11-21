using System.Collections.Generic;
using UnityEngine;

public class VinylShelfBehaviour : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private float spawnAreaWidth = 0;
    private float spawnAreaHeight = 0;

    [SerializeField] private GameObject vinylPrefab;

    [SerializeField] private int vinylSlots = 5;

    private List<GameObject> vinylSlotList = new List<GameObject>();

    private VinylController vinylController = new VinylController();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnVinylSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void spawnVinylSlots()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnAreaWidth = spriteRenderer.bounds.size.x;
        spawnAreaHeight = spriteRenderer.bounds.size.y;

        for(int i = 0; i < vinylSlots; i++)
        {
            GameObject vinylSlot = new GameObject("viylSlot" + i.ToString());
            // vinylSlot.transform.SetParent(transform);
            vinylSlotList.Add(vinylSlot);

            float spawnPointX = spawnAreaWidth / vinylSlots * i;

            Vector3 newPos = transform.position; 

            newPos.x = spawnAreaWidth / 2;
            newPos.x += spawnPointX;

            vinylSlot.transform.position = newPos;
            placeVinylInEmptySlot();
        }
    }

    public void placeVinylInEmptySlot(GameObject vinylParam = null)
    {
        GameObject vinylToPlace = null;

        if(vinylParam == null)
        {
            GameObject spawnedVinyl = Instantiate(vinylPrefab);
            spawnedVinyl.GetComponent<VinylBehaviour>().setVinylController(vinylController);
            vinylToPlace = spawnedVinyl;
        }
        else
        {
            vinylToPlace = vinylParam;
        }

        GameObject vinylSlotToPlaceVinylIn = vinylSlotList.Find(it => it.transform.childCount == 0);
        vinylToPlace.transform.SetParent(vinylSlotToPlaceVinylIn.transform);
        vinylToPlace.transform.position = vinylSlotToPlaceVinylIn.transform.position;
        vinylToPlace.GetComponent<VinylBehaviour>().setSlotLocation(vinylSlotToPlaceVinylIn.transform.position);
        vinylToPlace.GetComponent<VinylBehaviour>().setVinylShelf(gameObject);
    }
}
