using UnityEngine;

public class GroundController : MonoBehaviour
{
    Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayerArea")
        {
            RepositionTilemap(collision.gameObject.transform.position);
        }
    }

    private void RepositionTilemap(Vector3 playerPosition)
    {
        float xGap = transform.position.x - playerPosition.x;
        float yGap = transform.position.y - playerPosition.y;


        if (xGap > 24)
        {
            transform.position = new Vector2(transform.position.x - (2 * 24), transform.position.y);
        }
        if (xGap < -24)
        {
            transform.position = new Vector2(transform.position.x + (2 * 24), transform.position.y);
        }
        if (yGap > 20)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (2 * 20));
        }
        if (yGap < -20)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + (2 * 20));
        }

    }
}
