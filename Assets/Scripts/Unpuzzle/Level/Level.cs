using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Level",menuName="Levels")]
public class Level : ScriptableObject
{
    [SerializeField]
    private List<Node> nodeList;
    [SerializeField]
    private int moveCount;
    [SerializeField]
    private int coinReward;
    public Level(List<Node> nodeList, int moveCount)
    {
        this.nodeList = nodeList;
        this.moveCount = moveCount;
    }
    public List<Node> getList()
    {
        return nodeList;
    }
    public int getMoveCount()
    {
        return moveCount;
    }
    public int getCoinReward()
    {
        return coinReward;
    }
}
