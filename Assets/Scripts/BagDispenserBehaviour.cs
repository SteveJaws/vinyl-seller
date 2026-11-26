using UnityEngine;

public class BagDispenserBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject bag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnNewBag();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnNewBag()
    {
        GameObject spawnedBag = Instantiate(bag, AutoloadUnity.gameArea.transform);
        spawnedBag.transform.position = transform.position;

        BagBehaviour bagScript = spawnedBag.GetComponent<BagBehaviour>();
        bagScript.loadBagDispenserObject(gameObject);
    }
}
