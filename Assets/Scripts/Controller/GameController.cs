using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NodeType
{
    None , Left , Right , Up , Down , Rotate , Saw
}
public class GameController : MonoBehaviour
{
    [Header("MVC")]
    [SerializeField] GameConfig model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] SpawnPuzzle spawnPuzzle;
    [SerializeField] private List<Level> levels;
    private void Awake()
    {
        spawnPuzzle.initTable(model.Row, model.Col, model.Cellsize, levels[0].getList(),view.SetMoveCount);
    }
    public void UseHammer()
    {
        spawnPuzzle.UseHammer = true;
    }
    public void UseBomb()
    {
        spawnPuzzle.UseBomb = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }    
    private void Update()
    {
    }
}
