using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class VinylBehaviour : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;

    public Vector3 lastLocation;

    private bool onItemCounter = false;
    private bool onSpawnArea = false;

    private bool isBagged = false;
    private bool isStationary = true;

    private GameObject spawnArea;

    public static bool isBeingDragged = false;

    private VinylController vinylController;

    private Vector3 lastPosition;

    private GameObject vinylShelf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastLocation = transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (isBagged) return;

        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseDown()
    {
        if(isDragging == false && vinylController.isAVinylBeingDragged == false)
        {
            isDragging = true;
            vinylShelf.GetComponent<VinylShelfBehaviour>().toggleCollider();
        }
        else if(isDragging == true)
        {
            isDragging = false;
            vinylShelf.GetComponent<VinylShelfBehaviour>().toggleCollider();
        }
        vinylController.isAVinylBeingDragged = isDragging;

        if (isDragging)
        {
            transform.SetParent(null);
        }
        else
        {
            if (onItemCounter)
            {
                lastLocation = transform.position;
            }
            else if (onSpawnArea)
            {
                vinylShelf.GetComponent<VinylShelfBehaviour>().placeVinylInEmptySlot(gameObject);
            }
            else
            {
                transform.position = lastLocation;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("itemCounter"))
        {
            other.gameObject.GetComponent<ItemCounterBehaviour>().placeVinyl(gameObject);
            onItemCounter = true;
        }
        else if (other.CompareTag("spawnArea"))
        {
            print("on spawn Areaq");
            onSpawnArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isBagged) return;

        if (other.CompareTag("itemCounter"))
        {
            other.gameObject.GetComponent<ItemCounterBehaviour>().removeVinyl();
            onItemCounter = false;
        }
        else if (other.CompareTag("spawnArea"))
        {
            onSpawnArea = false;
        }
    }

    public void bagVinyl()
    {
        isBagged = true;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    public void setVinylController(VinylController controller)
    {
        vinylController = controller;
    }

    public void setSlotLocation(Vector3 lastPos)
    {
        lastLocation = lastPos;
    }

    public void setVinylShelf(GameObject shelf)
    {
        vinylShelf = shelf;
    }
}
