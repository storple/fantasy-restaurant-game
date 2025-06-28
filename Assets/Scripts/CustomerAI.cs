using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPos;
    private bool isWalking = false;
    private int tableIndex;

    public void MoveTo(Vector3 destination, int tableIndex)
    {
        targetPos = destination;
        this.tableIndex = tableIndex; // Store table index
        isWalking = true;
    }

    void Update()
    {
        if (!isWalking) return;

        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            transform.position = targetPos; // snap to target position
            isWalking = false;
            SitDown();
        }
    }

    void SitDown()
    {
        // sit down logic and animation here
    }


}
