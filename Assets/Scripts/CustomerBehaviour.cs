using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    private GameObject spawn = null;
    private GameObject target = null;

    private bool moving = false;

    private Vector3? movingToVector = null;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 20f;        // units per seconde
    [SerializeField] private float stopDistance = 0.1f;

    private bool moveToSpawn = false;

    private GameObject wantedVinyl;

    [SerializeField] private GameObject vinylPrefab;
    private GameObject vinylProp;

    [SerializeField] private GameObject vinylPosition;
    [SerializeField] private GameObject speechBubble;

    // Awake i.p.v. Start: setSpawnAndTarget wordt vaak direct na Instantiate aangeroepen, vóór Start
    void Awake()
    {
        spawnWantedVinylProp();
        rb = GetComponent<Rigidbody2D>();
        AutoloadUnity.customer = gameObject;
    }

    void Start()
    {
        GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
        speechBubble.SetActive(false);
        vinylProp.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!moving || movingToVector == null) return;

        Vector3 toTarget = movingToVector.Value - transform.position;
        toTarget.y = 0f; // alleen horizontaal bewegen

        if (toTarget.magnitude <= stopDistance)
        {
            moving = false;
            if (moveToSpawn)
            {
                moveToSpawn = false;
                customerMoveToTarget();
                chooseVinyl();
                GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
            }
            else{
                speechBubble.SetActive(true);
                vinylProp.SetActive(true);
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, (Vector3)movingToVector, moveSpeed * Time.fixedDeltaTime);
    }

    public void customerMoveToTarget()
    {
        movingToVector = target.transform.position;
        moving = true;
    }

    public void customerMoveToSpawn()
    {
        movingToVector = spawn.transform.position;
        moving = true;
        moveToSpawn = true;
    }

    public void setSpawnAndTarget(GameObject spawnParam, GameObject targetParam)
    {
        spawn = spawnParam;
        target = targetParam;

        transform.position = spawn.transform.position;
        chooseVinyl();
    }

    public void chooseVinyl()
    {
        bool chooseVinyl = true;

        while (chooseVinyl)
        {
            GameObject chosenVinyl = AutoloadUnity.shelf.GetComponent<VinylShelfBehaviour>().customerChooseVinyl();
            if(chosenVinyl != null)
            {
                chooseVinyl = false;
                wantedVinyl = chosenVinyl;

                vinylProp.GetComponent<SpriteRenderer>().color = wantedVinyl.GetComponent<SpriteRenderer>().color;
            }
        }
    }

    private void setTargetVinyl()
    {
        List<GameObject> vinylList = AutoloadUnity.shelf.GetComponent<VinylShelfBehaviour>().vinylList;
        
        if (vinylList.Count > 0)
        {
            // Kies een willekeurige index van 0 tot het aantal items
            int randomIndex = Random.Range(0, vinylList.Count);
            GameObject randomItem = vinylList[randomIndex];

            Debug.Log("Willekeurig item: " + randomItem);
        }
    }

    public bool isBaggedVinylWantedVinyl(GameObject baggedVinyl)
    {
        if(baggedVinyl == wantedVinyl)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void spawnWantedVinylProp()
    {
        vinylProp = Instantiate(vinylPrefab);
        vinylProp.GetComponent<Collider2D>().enabled = false;
        vinylProp.GetComponent<VinylBehaviour>().enabled = false;
        vinylProp.transform.position = transform.position;
        vinylProp.transform.SetParent(transform);
        vinylProp.transform.localPosition = vinylPosition.transform.localPosition;
    }

    public void disableSpeechBubble()
    {
        speechBubble.SetActive(false);
        vinylProp.SetActive(false);
    }
}
