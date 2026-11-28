using System;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject customerSpawn;
    [SerializeField] private GameObject customerTarget;
    [SerializeField] private List<BgItem> bgItemList;
    private Dictionary<BgItem, GameObject> spawnedBgItemList = new Dictionary<BgItem, GameObject>();

    void Awake()
    {
        AutoloadUnity.gameArea = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(BgItem item in bgItemList)
        {
            Color? savedColor = AutoloadUnity.chosenBgColors.TryGetValue(item.prefab, out Color color) ? color : (Color?)null;
            spawnBgItemCopyAtLocation(item.prefab, savedColor);
        }
        GameObject spawnedCustomer = Instantiate(customer, transform);
        CustomerBehaviour spawnedCustomerScript = spawnedCustomer.GetComponent<CustomerBehaviour>();
        spawnedCustomerScript.setSpawnAndTarget(customerSpawn, customerTarget);
        spawnedCustomerScript.customerMoveToTarget();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBgItemCopyAtLocation(GameObject prefab, Color? color = null)
    {
        foreach(BgItem item in bgItemList)
        {
            if(item.prefab == prefab)
            {
                if (spawnedBgItemList.TryGetValue(item, out GameObject oldItem))
                {
                    Destroy(oldItem);
                    spawnedBgItemList.Remove(item);
                }

                GameObject spawnedItem = Instantiate(prefab, transform);
                spawnedBgItemList.Add(item, spawnedItem);
                if(color != null) spawnedItem.GetComponent<SpriteRenderer>().color = (Color)color;
                spawnedItem.transform.position = item.location.transform.position;
            }
        }
    }
}
