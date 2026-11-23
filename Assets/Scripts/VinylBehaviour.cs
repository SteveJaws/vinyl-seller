using System;
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

    private SpriteRenderer spriteRenderer;

    private Vector3 originalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        lastLocation = transform.position;

        // In Awake i.p.v. Start: Awake draait direct tijdens Instantiate,
        // zodat overrideColor() daarna de random kleur kan overschrijven
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = UnityEngine.Random.ColorHSV();
    }

    void Start()
    {
        originalScale = transform.localScale;
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
        if (isDragging == false && vinylController.isAVinylBeingDragged == false)
        {
            isDragging = true;
            vinylController.isAVinylBeingDragged = true;
            transform.SetParent(null);
        }
        else if (isDragging == true)
        {
            isDragging = false;
            vinylController.isAVinylBeingDragged = false;

            if (onItemCounter)
            {
                lastLocation = transform.position;
            }
            else if (onSpawnArea)
            {
                setVinylIntoVinylSlot();
            }
            else
            {
                transform.position = lastLocation;
            }
        }
    }

    private void OnMouseEnter()
    {
        transform.localScale = transform.localScale * 1.2f;
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale;
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

    public void unBagVinyl()
    {
        isBagged = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;
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

    public void setVinylIntoVinylSlot()
    {
        VinylShelfBehaviour shelf = vinylShelf.GetComponent<VinylShelfBehaviour>();
        shelf.placeVinylInEmptySlot(gameObject);
    }

    public void overrideColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public Color getColor()
    {
        return spriteRenderer.color;
    }
}
