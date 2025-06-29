using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPos;
    private bool isWalking = false;
    private int tableIndex;

    public enum CustomerState
    {
        WalkingIn,
        WaitingToTakeOrder,
        TakingOrder,
        WaitingToEat,
        Eating,
        Leaving
    }

    private CustomerState currentState = CustomerState.WalkingIn;

    private GameObject interactionZone;

    public GameObject takeOrderPrefab;
    private GameObject takeOrderInstance;

    public void MoveTo(Vector3 destination, int tableIndex)
    {
        targetPos = destination;
        this.tableIndex = tableIndex; // store table index
        isWalking = true;
        currentState = CustomerState.WalkingIn;
    }

    void Update()
    {
        switch (currentState)
        {
            case CustomerState.WalkingIn:
                HandleWalkingIn();
                break;
            case CustomerState.WaitingToTakeOrder:
                HandleWaitingToTakeOrder();
                break;
            case CustomerState.TakingOrder:
                HandleTakingOrder();
                break;
            case CustomerState.WaitingToEat:
                HandleWaitingToEat();
                break;
            case CustomerState.Eating:
                HandleEating();
                break;
            case CustomerState.Leaving:
                HandleLeaving();
                break;
        }
    }

    void HandleWalkingIn()
    {
        if (!isWalking) return;

        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            transform.position = targetPos; // snap to target position
            isWalking = false;
            currentState = CustomerState.WaitingToTakeOrder; // transition to next state
            Debug.Log($"Customer at table {tableIndex} is now waiting to take order.");
        }
    }

    void HandleWaitingToTakeOrder()
    {
        if (interactionZone == null)
        {
            // create interaction zone
            interactionZone = new GameObject("InteractionZone");
            interactionZone.transform.SetParent(transform);
            interactionZone.transform.localPosition = Vector3.zero;

            // add collider for interaction zone
            BoxCollider2D collider = interactionZone.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;
            collider.size = new Vector2(1f, 1f);
            
            // initialize interaction zone script
            InteractionZone zoneScript = interactionZone.AddComponent<InteractionZone>();
            zoneScript.OnPlayerInteract += TakeOrder;
            zoneScript.keyIconPrefab = takeOrderPrefab; // assign the take order button icon prefab

        }
    }

    void TakeOrder()
    {
        Debug.Log($"Customer at table {tableIndex} is now taking order.");
        currentState = CustomerState.TakingOrder; // transition to next state

        // destroy interaction zone and button icon after taking order
        if (interactionZone != null)
        {
            Destroy(interactionZone);
        }
        if (takeOrderInstance != null)
        {
            Destroy(takeOrderInstance);
        }
    }

    void HandleTakingOrder()
    {
        // logic for taking order
    }

    void HandleWaitingToEat()
    {
        // logic for waiting to eat
    }

    void HandleEating()
    {
        // logic for eating
    }

    void HandleLeaving()
    {
        // logic for leaving
    }
}
