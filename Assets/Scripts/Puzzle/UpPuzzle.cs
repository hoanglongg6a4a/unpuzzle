using UnityEngine;

public class UpPuzzle : Puzzle
{
    public override void Move()
    {  
        isTouched = true;
    }
    private void Update()
    {
        if (isTouched)
        {
            targetPosition = transform.position + Vector3.up;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * base.speed);   
        }
        if (transform.position.x > rightScreenLimit + 0.8f)
        {
            isTouched = false;
        }
    }
}
