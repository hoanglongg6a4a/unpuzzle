using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
public class GameController : MonoBehaviour
{
    [Header("MVC")]
    [SerializeField] GameConfig model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] Puzzle puzzelPrefab;
    private Camera mainCamera;
    //[SerializeField] private SpawnBullet spawnBullet;
    private Puzzle puzzle;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        CheckClick();
    }
    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0)) // Kiểm tra nút chuột trái được nhấn xuống
        {
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // Thực hiện Raycast từ vị trí chuột
            if (hit.collider != null) // Kiểm tra nếu Raycast chạm vào collider
            {
                Puzzle puzzle = hit.collider.GetComponent<Puzzle>(); // Lấy tham chiếu đến component Puzzle của game object được chạm

                if (puzzle != null) // Kiểm tra nếu component Puzzle tồn tại
                {
                    puzzle.init(model.Speed); // Gọi phương thức init để bắt đầu di chuyển
                }
            }
        }
    }
  
}
