using UnityEngine;
using UnityEngine.UIElements;

public class BackGroundLoop : MonoBehaviour
{
    public float width;

    private void Awake()
    {
     BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;
    }

    private void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
       
    }

    private void Reposition()
    {
        Vector2 offset = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
