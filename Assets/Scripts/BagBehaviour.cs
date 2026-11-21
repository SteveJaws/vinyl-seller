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

    private GameObject itemCounter = null;

    private bool hasVinyl = false;

    private Vector3? lastLocation = null;

    private GameObject vinyl;

    private bool onSpawnArea = false;

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
        if (!onBagDispenser && !onItemCounter && !onSpawnArea)
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
                hasVinyl = false;
                Destroy(gameObject);
            }
            else
            {
                destroyAndNewBag();
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
