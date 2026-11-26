using UnityEngine;

public class GameAreaBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private GameObject customerSpawn;
    [SerializeField] private GameObject customerTarget;

    void Awake()
    {
        AutoloadUnity.gameArea = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject spawnedCustomer = Instantiate(customer, transform);
        CustomerBehaviour spawnedCustomerScript = spawnedCustomer.GetComponent<CustomerBehaviour>();
        spawnedCustomerScript.setSpawnAndTarget(customerSpawn, customerTarget);
        spawnedCustomerScript.customerMoveToTarget();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
