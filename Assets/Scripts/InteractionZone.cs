using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public GameObject keyIcon; // key icon displayed when near interaction zone area

    private bool playerNearby = false;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Interact.performed += ctx => OnInteract();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyIcon.SetActive(true);
            playerNearby = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            keyIcon.SetActive(false);
            playerNearby = false;
        }
    }
    
    void OnInteract()
    {
        if (playerNearby)
        {
            Debug.Log("cooking mode entered!");
            // trigger area transition or cooking logic here
        }
    }
}
