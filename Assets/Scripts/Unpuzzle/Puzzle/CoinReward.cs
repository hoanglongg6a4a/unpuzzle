using UnityEngine;
public class CoinReward : MonoBehaviour
{
    [SerializeField] 
    private float speed = 100f; 
    private Transform target; 
    private bool isMoving = false; 
    private Vector2 oldPos;
    public void Start()
    {
        oldPos = transform.position;
    }
    private void Update()
    {
        if (isMoving)
        {
            Vector2 newPos = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
            transform.position = newPos;
            if (transform.position == target.position)
            {
                transform.position = oldPos;
                gameObject.SetActive(false);
                isMoving = false;
            }
        }
    }
    public void ShowReward(Transform targetPosition)
    {
        target = targetPosition;
        isMoving = true;
        gameObject.SetActive(true);
    }
}

