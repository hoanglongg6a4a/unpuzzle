using UnityEngine;
[System.Serializable]
public class Node
{
    public Vector2Int position;
    public NodeType NodeType;

    public Node(Vector2Int position, NodeType nodeType)
    {
        this.position = position;
        NodeType = nodeType;
    }
}
