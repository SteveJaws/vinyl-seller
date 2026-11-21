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
        isDragging = !isDragging;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("itemCounter"))
        {
            onPlaceableSurface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("itemCounter"))
        {
            onPlaceableSurface = false;
        }
    }

    public void bagVinyl()
    {
        isBagged = true;
    }
}
