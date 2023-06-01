using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnPuzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject puzzlePrefab;
    private int rows;
    private int col;
    private float cellsize;
    private Vector2[,] tables;
    private List<Node> nodes;
    private Dictionary<Node, GameObject> nodesDictObj;
    private Action<int> SetCountMove;
    public bool UseHammer;
    public bool UseBomb;
    // Start is called before the first frame update
    private void Start()
    {
        nodesDictObj = new();
        //nodes = new();
    /*    nodes.Add(new Node(new Vector2Int(0, 0), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(1, 1), NodeType.Up));
        nodes.Add(new Node(new Vector2Int(2, 2), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(3, 3), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(4, 4), NodeType.Down));
        nodes.Add(new Node(new Vector2Int(0, 1), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(0, 2), NodeType.Left));
        nodes.Add(new Node(new Vector2Int(0, 3), NodeType.Saw));
        nodes.Add(new Node(new Vector2Int(1, 2), NodeType.Up));*/
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
                case NodeType.Saw:
                    quaternion = Quaternion.Euler(0, 0, 0);
                    break;
            } 
            GameObject NodeObj = Instantiate(puzzlePrefab, tables[node.position.x, node.position.y], quaternion);
            nodesDictObj.Add(node, NodeObj);
        }    
    }
    public void initTable(int rows, int col, float cellsize,List<Node> nodes, Action<int>SetCountMove)
    {
        this.rows = rows;
        this.col = col;
        this.cellsize = cellsize;
        this.nodes = nodes.Select(n => new Node(n.position, n.NodeType)).ToList();
        this.SetCountMove = SetCountMove;

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
                if(UseBomb && node.NodeType != NodeType.Saw && node.NodeType != NodeType.Rotate) 
                { 
                    HideAndRemoveSurroundingNodes(node);
                    nodesDictObj[node].SetActive(false);
                    nodes.Remove(node);
                    UseBomb = false;

                }
                if(UseHammer && node.NodeType != NodeType.Saw && node.NodeType != NodeType.Rotate)
                {
                    nodesDictObj[node].SetActive(false);
                    nodes.Remove(node);
                    UseHammer = false;
                }
                if (node.NodeType == NodeType.Rotate)
                {
                    RotateSurroundingNodesClockwise(node);
                }
                if (node.NodeType == NodeType.Saw)
                {
                    return;
                }
                Vector2Int movePos = NodeMovePosition(node);
                Vector2 destinition;
                if(movePos == node.position)
                {   
                    destinition = tables[movePos.x, movePos.y];                
                }
                else if (movePos.x <0 || movePos.x >100 || movePos.y <0 && movePos.y >100)
                {
                    destinition = CheckNode(node);
                    nodes.Remove(node);
                }
                else
                {            
                    destinition = tables[movePos.x, movePos.y];
                    nodes[nodes.IndexOf(node)].position = movePos;
                }
                StartCoroutine(MoveNode(nodesDictObj[node], destinition)); 
            }
        }    
    }
    private void RotateSurroundingNodesClockwise(Node node)
    {
        Vector2Int nodePosition = node.position;
        // Lưu trữ vị trí của các node xung quanh
        Vector2Int positionUp = nodePosition + Vector2Int.down;
        Vector2Int positionRight = nodePosition + Vector2Int.right;
        Vector2Int positionDown = nodePosition + Vector2Int.up;
        Vector2Int positionLeft = nodePosition + Vector2Int.left;
        Node nodeUp = nodes.Find(n => n.position == positionUp);
        Node nodeRight = nodes.Find(n => n.position == positionRight);
        Node nodeDown = nodes.Find(n => n.position == positionDown);
        Node nodeLeft = nodes.Find(n => n.position == positionLeft);

        // Xoá vị trí cũ của các node xung quanh (nếu tồn tại)
        if (nodeUp != null)
        {
            //nodesDictObj[nodeUp].transform.position = tables[positionRight.x, positionRight.y];
            nodes[nodes.IndexOf(nodeUp)].position = positionRight;
            StartCoroutine(MoveNode(nodesDictObj[nodeUp], tables[positionRight.x, positionRight.y]));         
        }
        if (nodeRight != null)
        {
            //nodesDictObj[nodeRight].transform.position = tables[positionDown.x, positionDown.y];
            nodes[nodes.IndexOf(nodeRight)].position = positionDown;
            StartCoroutine(MoveNode(nodesDictObj[nodeRight], tables[positionDown.x, positionDown.y]));         
        }
        if (nodeDown != null)
        {
            nodes[nodes.IndexOf(nodeDown)].position = positionLeft;
            StartCoroutine(MoveNode(nodesDictObj[nodeDown],tables[positionLeft.x, positionLeft.y]));
           // nodesDictObj[nodeDown].transform.position = tables[positionLeft.x, positionLeft.y];
           
        }
        if (nodeLeft != null)
        {
            nodes[nodes.IndexOf(nodeLeft)].position = positionUp;
            StartCoroutine(MoveNode(nodesDictObj[nodeLeft], tables[positionUp.x, positionUp.y]));
            //nodesDictObj[nodeLeft].transform.position = tables[positionUp.x, positionUp.y];       
        }
    }
    private void HideAndRemoveSurroundingNodes(Node node)
    {
        Vector2Int nodePosition = node.position;
        List<Vector2Int> positionsToRemove = new List<Vector2Int>();
        for (int i = nodePosition.x - 1; i <= nodePosition.x + 1; i++)
        {
            for (int j = nodePosition.y - 1; j <= nodePosition.y + 1; j++)
            {
                Vector2Int position = new Vector2Int(i, j);
                if (position != nodePosition)
                {
                    positionsToRemove.Add(position);
                }
            }
        }
        foreach (Vector2Int position in positionsToRemove)
        {
            Node surroundingNode = nodes.Find(n => n.position == position);
            if (surroundingNode != null)
            {
                nodesDictObj[surroundingNode].SetActive(false);
                nodes.Remove(surroundingNode);
            }
        }
    }
    private Vector2 CheckNode(Node node)
    {
        Vector2 direction = new();
        switch (node.NodeType)
        {
            case NodeType.Left:
              direction = new Vector2(-999f, 0f); ;
                break;
            case NodeType.Right:
              direction = new Vector2(999f, 0f); ;
                break;
            case NodeType.Up:
              direction = new Vector2(0f, 999f); ;
                break;
            case NodeType.Down:
               direction = new Vector2(0f, -999f); ;
                break;
        }
        return direction;
    }
    private IEnumerator MoveNode(GameObject node, Vector3 destination)
    {
        while (node.transform.position != destination)
        {
            Vector2 newPosition = Vector2.MoveTowards(node.transform.position, destination, 8 * Time.deltaTime);
            node.transform.position = newPosition;

            yield return null; 
        }
    }
    public Vector2Int NodeMovePosition(Node node)
    {      
        Vector2Int direction = new();
        switch (node.NodeType)
        {
            case NodeType.None:
                return Vector2Int.one * -1;
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
            case NodeType.Saw:
                direction = Vector2Int.zero; // Đặt giá trị direction thành Vector2Int.zero để đối tượng đứng yên
                break;
        }    
        Vector2Int curPos = node.position;
        while (curPos.x < rows && curPos.x >= 0  && curPos.y < col && curPos.y >= 0)
        {
            curPos += direction;
            Node temp = nodes.Find(node=>node.position == curPos);
            if (temp != null)
            {
                if (temp.NodeType == NodeType.Saw)
                {
                    nodesDictObj[node].SetActive(false);
                    nodes.Remove(node);
                    break;
                }
                return curPos -= direction;
            } 
        }
        return Vector2Int.one*999;
    }    
}
