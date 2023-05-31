using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
public enum NodeType
{
    None , Left , Right , Up , Down
}
public class GameController : MonoBehaviour
{
    [Header("MVC")]
    [SerializeField] GameConfig model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] SpawnPuzzle spawnPuzzle;
    private Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;
        //spawnPuzzle.init(5,5,1);
    }
    private void Update()
    {
 
    }
}
