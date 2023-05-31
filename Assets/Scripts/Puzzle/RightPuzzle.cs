using UnityEngine;
public class RightPuzzle : Puzzle
{
    public override void Move()
    {
        isTouched= true;
        //MoveRight();
    }
    private void Update()
    {
        if(isTouched)
        {
            MoveRight();           
        }
      /*  if (isTouched)
        {
            targetPosition = transform.position + Vector3.right;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * base.speed);
        }*/
        if (transform.position.x > rightScreenLimit + 0.8f)
        {
            isTouched = false;
            gameObject.SetActive(false);
        }
    }
    public void MoveRight()
    {
        int currentRow = base.row;
        int currentColumn = base.column;
        for (int column = currentColumn + 1; column < 4; column++)
        {
            if (IsPositionEmpty(currentRow, column)==1)
            {
                SetRowColumn(currentRow, column);
                //SetPos(this, currentRow, column);
                MoveToPosition(base.GetCellPosition(currentRow, column)); 
            }
            if (IsPositionEmpty(currentRow, column) == 2)
            {
                int targetColumn = column-1 ;
                SetRowColumn(currentRow, targetColumn);
                //SetPos(this,currentRow, targetColumn);
                MoveToPosition(GetCellPosition(currentRow, targetColumn));
                break;
            }
            else
            {
                Debug.Log("Vào");
                DeletePos(currentRow, currentColumn);
                targetPosition = transform.position + Vector3.right;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * base.speed);
            }    
        }
        isTouched = false;
    }
    public void MoveToPosition(Vector3 position)
    {
        transform.position = position;
    }
}
