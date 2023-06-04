using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Level",menuName="Levels")]
public class Level : ScriptableObject
{
    [SerializeField]
    private List<Node> nodeList;
    public List<Node> getList()
    {
        return nodeList;
    }    
}
