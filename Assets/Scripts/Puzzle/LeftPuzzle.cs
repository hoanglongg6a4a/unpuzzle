using UnityEngine;

public class LeftPuzzle : Puzzle
{
    public override void Move()
    {  
        isTouched = true;
    }
    private void Update()
    {
        if (isTouched)
        {
            targetPosition = transform.position + Vector3.left;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * base.speed);   
        }
        if (transform.position.x > rightScreenLimit + 0.8f)
        {
            isTouched = false;
        }
    }
}
