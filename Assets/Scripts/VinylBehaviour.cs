using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class VinylBehaviour : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;

    public Vector3 lastLocation;

    private bool onPlaceableSurface = false;

    private bool isBagged = false;

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
        else if(!isDragging)
        {
            if (onPlaceableSurface)
            {
                lastLocation = transform.position;
            }
            else
            {
                transform.position = (Vector3)lastLocation;
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isBagged)
        {
            print("test");
            isDragging = !isDragging;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("itemCounter"))
        {
            other.gameObject.GetComponent<ItemCounterBehaviour>().placeVinyl(gameObject);
            onPlaceableSurface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isBagged) return;

        if (other.CompareTag("itemCounter"))
        {
            other.gameObject.GetComponent<ItemCounterBehaviour>().removeVinyl();
            onPlaceableSurface = false;
        }
    }

    public void bagVinyl()
    {
        isBagged = true;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

}
