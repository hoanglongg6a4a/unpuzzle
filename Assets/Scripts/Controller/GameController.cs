using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
public class GameController : MonoBehaviour
{
    [Header("MVC")]
    [SerializeField] GameConfig model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] SpawnPuzzle spawnPuzzle;
    private Camera mainCamera;
    private Puzzle puzzle;
    private GameObject puzzleObject;
    private void Awake()
    {
        mainCamera = Camera.main;
        spawnPuzzle.init(3,3);
    }
    private void Update()
    {
        Puzzle puzzleClick = CheckClick();
        if (puzzleClick != null)
        {
            puzzleObject = puzzleClick.gameObject; // Gán game object của đối tượng Puzzle cho puzzleObject
        }
        if (puzzleObject != null)
        {
            puzzle = puzzleObject.GetComponent<Puzzle>();
        }
    }
    private Puzzle CheckClick()
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
                    return puzzle; // Trả về đối tượng Puzzle được click vào
                }
            }
        }
        return null; // Trả về null nếu không có đối tượng Puzzle được click vào
    }

}
