using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    [Header("Interaction Settings")]
    public GameObject keyIconPrefab; // key icon image
    public Vector3 keyIconOffset = new Vector3(0, 1, 0); // offset for key icon position
    private GameObject keyIconInstance; // instance of the key icon

    public delegate void PlayerInteractHandler();
    public event PlayerInteractHandler OnPlayerInteract;

    private bool playerNearby = false;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => TriggerInteraction();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyIconPrefab != null && keyIconInstance == null)
            {
                keyIconInstance = Instantiate(keyIconPrefab, transform.position + keyIconOffset, Quaternion.identity, transform);
            }

            if (keyIconInstance != null)
            {
                keyIconInstance.SetActive(true); 
            }

            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (keyIconInstance != null)
            {
                keyIconInstance.SetActive(false);
            }

            playerNearby = false;
        }
    }

    void TriggerInteraction()
    {
        if (playerNearby)
        {
            OnPlayerInteract?.Invoke();
        }
    }
}
