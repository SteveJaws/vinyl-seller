using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;

public class VinylBehaviour : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;

    public int testVar = 1;

    private Vector3? lastLocation = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {
        print(testVar);
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(!isDragging && lastLocation != null)
        {
            if(transform.position != lastLocation)
            {
                transform.position = (Vector3)lastLocation;
            }
        }
    }

    private void OnMouseDrag() {
        lastLocation = transform.position;
        isDragging = !isDragging;
    }
}
