using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class Puzzle : MonoBehaviour
{
    protected bool isTouched = false;
    protected Vector3 targetPosition;
    protected float rightScreenLimit;
    protected float leftScreenLimit;
    protected float upScreenLimit;
    protected float downScreenLimit;
    protected float speed;
    private void Start()
    {
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;
        float maxX = worldWidth / 2f;
        float maxY = worldHeight / 2f;
        rightScreenLimit = maxX;
        leftScreenLimit = -maxX;
        upScreenLimit = maxY;
        downScreenLimit = -maxY;
    }
    public void init(float speed)
    {
        this.speed = speed;
    }    
    private void Update()
    {
        CheckOutScreen();
    }
    public void CheckOutScreen()
    {
        if(transform.position.x > rightScreenLimit || transform.position.x < leftScreenLimit)
        {
            gameObject.SetActive(false);
        }
        if(transform.position.y > upScreenLimit || transform.position.y < downScreenLimit)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Puzzle")
        {
            isTouched = false;
        }
    }
    private void OnMouseDown()
    {
       Move();
    }
    public abstract void Move();
}
