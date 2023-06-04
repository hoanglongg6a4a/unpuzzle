using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReward : MonoBehaviour
{ 
    private float speed = 10f; // T?c ?? bay c?a ??ng xu
    private Transform target; // V? tr� ?�ch m� ??ng xu bay t?i
    private bool isMoving = false; // Bi?n x�c ??nh xem ??ng xu ?ang di chuy?n hay kh�ng
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
            // N?u ??ng xu ?� ??n v? tr� target, x? l� s? ki?n nh?n th??ng
            if (transform.position == target.position)
            {
                transform.position = oldPos;
                isMoving = false;
                gameObject.SetActive(false);
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

