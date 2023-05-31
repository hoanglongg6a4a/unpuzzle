using System;
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
    protected int row;
    protected int column;
    protected Func<int,int,int> IsPositionEmpty;
    protected Func<int, int, Vector3> GetCellPosition;
    protected Action<Puzzle, int, int> SetPos;
    protected Action<int, int> DeletePos;
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
    public void SetRowColumn(int row,int column)
    {
        this.row = row;
        this.column = column;
    }  
    public int GetColumn()
    {
        return this.column;
    }  
    public int GetRow()
    {
        return this.row;
    }    
    public void init(float speed,Func<int, int, int> IsPositionEmpty,Func<int, int, Vector3>GetCellPosition, Action<Puzzle, int, int> SetPos, Action<int, int> DeletePos)
    {
        this.speed = speed;
        this.IsPositionEmpty = IsPositionEmpty;
        this.GetCellPosition = GetCellPosition;   
        this.SetPos = SetPos;
        this.DeletePos = DeletePos;
    }    
    private void Update()
    {
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
