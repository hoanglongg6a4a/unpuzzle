using UnityEngine;

public class DownPuzzle : Puzzle
{
    public override void Move()
    {  
        isTouched = true;
    }
    private void Update()
    {
        if (isTouched)
        {
            targetPosition = transform.position + Vector3.down;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * base.speed);   
        }
        if (transform.position.y < downScreenLimit - 0.8f)
        {
            isTouched = false;
            gameObject.SetActive(false);
        }
    }
}
