using UnityEngine;

public class YSortingOrder : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // sorting is based on the y position of the object
    void LateUpdate()
    {
        sr.sortingOrder = Mathf.RoundToInt(transform.position.y * -100f);   
    }
}
