using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
public class SpawnPuzzle : MonoBehaviour
{
    [SerializeField]   
    private int rows;
    [SerializeField]
    private int col;
    [SerializeField]
    private GameObject puzzlePrefab;
    [SerializeField]
    private float cellsize;
    private Vector2[,] tables;

    private List<Node> nodes;
    private Dictionary<Node, GameObject> nodesDictObj;
    // Start is called before the first frame update
    private void Awake()
    {
        nodesDictObj = new();
        nodes = new();
        nodes.Add(new Node(new Vector2Int(0, 0), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(1, 1), NodeType.Up));
        nodes.Add(new Node(new Vector2Int(2, 2), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(3, 3), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(4, 4), NodeType.Down));
        nodes.Add(new Node(new Vector2Int(0, 1), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(0, 2), NodeType.Left));
        tables = new Vector2[rows, col];
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < col; j++)
            {
                float PosX = (-rows / 2 + i) *cellsize;
                float PosY = (col / 2 - j) *cellsize;
               
                tables[i,j] = new Vector2(PosX, PosY);
            }    
        }
        foreach (var node in nodes)
        {
            Quaternion quaternion = Quaternion.identity;
            switch (node.NodeType)
            {
                case NodeType.Left:
                    quaternion = Quaternion.Euler(0, 0,90);
                    break;
                case NodeType.Right:
                    quaternion = Quaternion.Euler(0, 0,-90);
                    break;
                case NodeType.Up:
                    quaternion = Quaternion.Euler(0, 0, 0);
                    break;
                case NodeType.Down:
                    quaternion = Quaternion.Euler(0, 0, 180);
                    break;
            } 
            GameObject NodeObj = Instantiate(puzzlePrefab, tables[node.position.x, node.position.y], quaternion);
            nodesDictObj.Add(node, NodeObj);
        }    
    }
    private void Update()
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Node node;
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider2D = Physics2D.OverlapPoint(touchPos); 
            if(collider2D != null)
            {
                Vector2Int temp = new Vector2Int(Mathf.RoundToInt(collider2D.transform.position.x/cellsize + rows/2), Mathf.RoundToInt(-collider2D.transform.position.y/cellsize + col/2));
                node = nodes.Find(n => n.position == new Vector2 (temp.x,temp.y));
                Vector2Int movePos = NodeMovePosition(node);
                Vector2 destinition;
                if(movePos == node.position)
                {
                    destinition = tables[movePos.x, movePos.y];
                }
                else if (movePos.x <0 || movePos.x >100 || movePos.y <0 && movePos.y >100)
                {
                    destinition = new Vector2(999f, 999f);
                    nodes.Remove(node);
                }
                else
                {
                    destinition = tables[movePos.x, movePos.y];
                    nodes[nodes.IndexOf(node)].position = movePos;
                }
                MoveNode(nodesDictObj[node], destinition);

            }
        }    
    }
    private void MoveNode(GameObject node , Vector2 destinition)
    {
        node.transform.position = destinition;
    }
    public void init(int rows , int col , float cellsize)
    {
        this.rows = rows;
        this.col = col;
        this.cellsize = cellsize;
    }
    public Vector2Int NodeMovePosition(Node node)
    {
        Vector2Int direction = new();
        switch (node.NodeType)
        {
            case NodeType.None:
                return Vector2Int.one * -1;
                break;
            case NodeType.Left:
                direction = Vector2Int.left;
                break;
            case NodeType.Right:
                direction = Vector2Int.right;
                break;
            case NodeType.Up:
                direction = Vector2Int.down;
                break;
            case NodeType.Down:
                direction = Vector2Int.up;
                break;
        }    
        Vector2Int curPos = node.position;
        while (curPos.x < rows && curPos.x >= 0  && curPos.y < col && curPos.y >= 0)
        {
            curPos += direction;
            Node temp = nodes.Find(node=>node.position == curPos);
            if (temp != null) return curPos-=direction;
            Debug.Log(curPos);
        }    
        return Vector2Int.one*999;
     
    }    
}
