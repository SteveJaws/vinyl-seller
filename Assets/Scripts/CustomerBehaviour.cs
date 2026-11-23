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

    private GameObject wantedVinyl;

    // Awake i.p.v. Start: setSpawnAndTarget wordt vaak direct na Instantiate aangeroepen, vóór Start
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        AutoloadUnity.customer = gameObject;
    }

    void FixedUpdate()
    {
        print("test1");
        if (!moving || movingToVector == null) return;
        print("test2");

        Vector3 toTarget = movingToVector.Value - transform.position;
        toTarget.y = 0f; // alleen horizontaal bewegen

        if (toTarget.magnitude <= stopDistance)
        {
            print("reached destination");
            moving = false;
            return;
        }

        // Waarbij targetPosition de echte positie (Vector3) is waar je naartoe wilt
        transform.position = Vector3.MoveTowards(transform.position, (Vector3)movingToVector, moveSpeed * Time.fixedDeltaTime);
    }

    public void customerMoveToTarget()
    {
        movingToVector = target.transform.position;
        moving = true;
    }

    public void customerMoveToSpawn()
    {
        print("moving");
        movingToVector = spawn.transform.position;
        moving = true;
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
                wantedVinyl = chosenVinyl;
                chooseVinyl = false;
                wantedVinyl = chosenVinyl;
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
}
