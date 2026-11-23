using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class BagBehaviour : MonoBehaviour
{
    private BagDispenserBehaviour bagDispenserScript;

    private bool isDragging = false;

    private bool onBagDispenser = false;

    private bool onItemCounter = false;
    private bool onCustomer = false;

    private GameObject itemCounter = null;

    private bool hasVinyl = false;

    private Vector3? lastLocation = null;

    private GameObject vinyl;

    private bool onSpawnArea = false;

    private bool isBaggedVinylWanted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void loadBagDispenserObject(GameObject bagDispenser)
    {
        bagDispenserScript = bagDispenser.GetComponent<BagDispenserBehaviour>();
    }

    private void OnMouseDown()
    {
        isDragging = !isDragging;
        if (!onBagDispenser && !onItemCounter && !onSpawnArea && !onCustomer)
        {
            if(lastLocation != null)
            {
                transform.position = (Vector3)lastLocation;
            }
            else
            {
                bagDispenserScript.spawnNewBag();
                Destroy(gameObject);
            }
        }
        else if (onItemCounter)
        {
            if(hasVinyl == false)
            {
                bool canBag = itemCounter.GetComponent<ItemCounterBehaviour>().bagVinyl(gameObject);
                if (!canBag)
                {
                    destroyAndNewBag();
                }
                if (canBag)
                {
                    hasVinyl = true;
                    bagDispenserScript.spawnNewBag();
                }
            }
            else
            {
                lastLocation = transform.position;
            }
        }
        else if(onSpawnArea){
            if (hasVinyl)
            {
                vinyl.GetComponent<VinylBehaviour>().setVinylIntoVinylSlot();
                vinyl.GetComponent<VinylBehaviour>().unBagVinyl();
                hasVinyl = false;
                Destroy(gameObject);
            }
            else
            {
                destroyAndNewBag();
            }
        }
        else if (onCustomer)
        {
            if (hasVinyl && isBaggedVinylWanted)
            {
                isBaggedVinylWanted = false;
                Destroy(gameObject);
                AutoloadUnity.shelf.GetComponent<VinylShelfBehaviour>().placeVinylInEmptySlot();
                AutoloadUnity.shelf.GetComponent<VinylShelfBehaviour>().removeVinylFromList(vinyl);
                AutoloadUnity.customer.GetComponent<CustomerBehaviour>().customerMoveToSpawn();
                AutoloadUnity.cash += 10;
            }
            else
            {
                transform.position = (Vector3)lastLocation;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bagDispenser"))
        {
            onBagDispenser = true;
        }

        if (other.CompareTag("itemCounter"))
        {
            itemCounter = other.gameObject;
            onItemCounter = true;
        }

        if (other.CompareTag("spawnArea"))
        {
            onSpawnArea = true;
        }

        if (other.CompareTag("customer"))
        {
            onCustomer = true;
            CustomerBehaviour customerScript = other.GetComponent<CustomerBehaviour>();
            isBaggedVinylWanted = customerScript.isBaggedVinylWantedVinyl(vinyl);
            print(isBaggedVinylWanted);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("bagDispenser"))
        {
            onBagDispenser = false;
        }

        if (other.CompareTag("itemCounter"))
        {
            itemCounter = null;
            onItemCounter = false;
        }

        if (other.CompareTag("spawnArea"))
        {
            onSpawnArea = false;
        }

        if (other.CompareTag("customer"))
        {
            onCustomer = false;
            isBaggedVinylWanted = false;
        }
    }

    public void setVinyl(GameObject vinylparam)
    {
        vinyl = vinylparam;
    }

    private void destroyAndNewBag()
    {
        Destroy(gameObject);
        bagDispenserScript.spawnNewBag();
    }
}
