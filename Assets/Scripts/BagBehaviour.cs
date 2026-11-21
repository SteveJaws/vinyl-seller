using Unity.VisualScripting;
using UnityEngine;

public class BagBehaviour : MonoBehaviour
{
    private BagDispenserBehaviour bagDispenserScript;

    private bool isDragging = false;

    private bool onBagDispenser = false;

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
        if (!onBagDispenser)
        {
            bagDispenserScript.spawnNewBag();
            Destroy(gameObject);
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bagDispenser"))
        {
            onBagDispenser = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("bagDispenser"))
        {
            onBagDispenser = false;
        }
    }
}
