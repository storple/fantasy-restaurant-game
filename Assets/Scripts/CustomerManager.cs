using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customerSprites; // array of possible customer sprites
    public Transform spawnPoint;
    public Transform[] tableTargets;
    private HashSet<int> occupiedTables = new HashSet<int>();

    public GameObject takeOrderPrefab; // prefab for the "take order" button icon (also could be used for other interactions, but just for now)

    private PlayerControls controls;
    private Transform currentCustomersParent;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Debug.SpawnCustomer.performed += ctx => SpawnCustomer();

        currentCustomersParent = GameObject.Find("Current Customers").transform;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    // spawns a new customer at the spawn point and assigns them to a random unoccupied table
    public void SpawnCustomer()
    {
        if (occupiedTables.Count >= tableTargets.Length)
        {
            Debug.Log("All tables are occupied. Cannot spawn new customer.");
            return; // all tables are occupied
        }

        GameObject newCustomer = new GameObject("Customer");

        // render sprite
        SpriteRenderer spriteRenderer = newCustomer.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = customerSprites[Random.Range(0, customerSprites.Length)].GetComponent<SpriteRenderer>().sprite; // assign random sprite
        spriteRenderer.sortingLayerName = "Environment";

        // set position and parent
        newCustomer.transform.position = spawnPoint.position;
        newCustomer.transform.SetParent(currentCustomersParent);

        // assign a random unoccupied table
        int randomTableIndex;
        do
        {
            randomTableIndex = Random.Range(0, tableTargets.Length);
        } while (occupiedTables.Contains(randomTableIndex));

        Transform randomTable = tableTargets[randomTableIndex];

        // add CustomerAI component and move customer to the table
        CustomerAI customerAI = newCustomer.AddComponent<CustomerAI>();
        customerAI.MoveTo(randomTable.position, randomTableIndex);
        occupiedTables.Add(randomTableIndex);

        // assign glyph prefabs
        customerAI.takeOrderPrefab = takeOrderPrefab;
    }

    // method to free a table when a customer leaves or is destroyed
    public void FreeTable(int tableIndex)
    {
        occupiedTables.Remove(tableIndex);
    }
}
