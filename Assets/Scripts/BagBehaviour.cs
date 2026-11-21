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
        if (!onBagDispenser && !onItemCounter)
        {
            bagDispenserScript.spawnNewBag();
            Destroy(gameObject);
        }
        else if (onItemCounter)
        {
            if(hasVinyl == false)
            {
                bool canBag = itemCounter.GetComponent<ItemCounterBehaviour>().bagVinyl(gameObject);
                if (!canBag)
                {
                    Destroy(gameObject);
                    bagDispenserScript.spawnNewBag();
                }
                if (canBag)
                {
                    hasVinyl = true;
                }
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
    }
}
