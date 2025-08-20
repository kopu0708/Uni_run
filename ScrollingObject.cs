using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float scrollSpeed = 10f;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
    }
}
