using UnityEngine;

public class ItemCounterBehaviour : MonoBehaviour
{
    private GameObject placedVinyl = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeVinyl(GameObject vinyl)
    {
        placedVinyl = vinyl;
    }

    public void removeVinyl()
    {
        placedVinyl = null;
    }

    public bool bagVinyl(GameObject bag)
    {
        if (!placedVinyl)
        {
            return false;
        }

        placedVinyl.transform.SetParent(bag.transform);
        placedVinyl.transform.position = bag.transform.position;
        placedVinyl.gameObject.GetComponent<VinylBehaviour>().bagVinyl();
        placedVinyl = null;
        return true;
    }
}
