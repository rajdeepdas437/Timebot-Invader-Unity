using Unity.VisualScripting;
using UnityEngine;

public class BrokenPot : MonoBehaviour
{
    [SerializeField] float movementSpeed=1f;
    private Vector3 movementDirection;
    [SerializeField] float haltingFactor=5f;
    [SerializeField] float lifetime=3f;
    private SpriteRenderer partSprite;
    [SerializeField] float fadeSpeed=2f;
    void Start()
    {
        partSprite = GetComponent<SpriteRenderer>();
        movementDirection.x = Random.Range(-movementSpeed, movementSpeed);
        movementDirection.y = Random.Range(-movementSpeed, movementSpeed);
    }

    void Update()
    {
        transform.position += movementDirection*Time.deltaTime;
        movementDirection = Vector3.Lerp(movementDirection, Vector3.zero, haltingFactor*Time.deltaTime);

        lifetime -= Time.deltaTime;

        if(lifetime<=0)
        {
            partSprite.color = new Color(
                partSprite.color.r,
                partSprite.color.g,
                partSprite.color.b,
                Mathf.MoveTowards(partSprite.color.a, 0f, fadeSpeed*Time.deltaTime)
            );
        }
        if(partSprite.color.a==0)
        {
            Destroy(gameObject);
        }
    }
}
