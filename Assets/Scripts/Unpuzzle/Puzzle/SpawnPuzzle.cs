using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class SpawnPuzzle : MonoBehaviour
{
    [SerializeField]
    private GameObject puzzlePrefab,hammerPrefab;
    [SerializeField]
    private Sprite sawSprite, defaultSprite , roTateSprite,bombSprite;
    private int rows;
    private int col;
    private float cellsize;
    private Vector2[,] tables;
    private List<Node> nodes;
    private Dictionary<Node, GameObject> nodesDictObj;
    private Action<int> SetCountMoveView;
    private Action<bool,bool,string> setTopPanel;
    private bool isDesTroy=false;
    private int countMove;
    private bool endGame=false;
    private float maxX,minX,maxY,minY;
    public bool useHammer;  // Dang Test nen chua chinh lai private 
    public bool useBomb;    // Dang Test nen chua chinh lai private 
    // Start is called before the first frame update
    private void Start()
    {
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;
        maxX = worldWidth / 2;
        maxY = worldHeight / 2;
        minX = -maxX;
        minY = -maxY;
        endGame= false;
        nodesDictObj = new();
        tables = new Vector2[rows, col];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < col; j++)
            {
                float PosX = (-rows / 2 + i) * cellsize;
                float PosY = (col / 2 - j) * cellsize;
                tables[i, j] = new Vector2(PosX, PosY);
            }
        }
        foreach (var node in nodes)
        {
            Quaternion quaternion = Quaternion.identity;
            Sprite sprite = defaultSprite;
            switch (node.NodeType)
            {
                case NodeType.Left:
                    quaternion = Quaternion.Euler(0, 0, 90);
                    break;
                case NodeType.Right:
                    quaternion = Quaternion.Euler(0, 0, -90);
                    break;
                case NodeType.Up:
                    quaternion = Quaternion.Euler(0, 0, 0);
                    break;
                case NodeType.Down:
                    quaternion = Quaternion.Euler(0, 0, 180);
                    break;
                case NodeType.Saw:
                    quaternion = Quaternion.Euler(0, 0, 0);
                    sprite = sawSprite;
                    break;
                case NodeType.Rotate:
                    quaternion = Quaternion.Euler(0, 0, 0);
                    sprite = roTateSprite;
                    break;
            }
            GameObject NodeObj = Instantiate(puzzlePrefab, tables[node.position.x, node.position.y], quaternion);
            SpriteRenderer spriteRenderer = NodeObj.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            nodesDictObj.Add(node, NodeObj);
        }
    }
    public void InitTable(int rows, int col, float cellsize, List<Node> nodes, int countMove, Action<int> SetCountMoveView, Action<bool, bool, string> setTopPanel)
    {
        this.rows = rows;
        this.col = col;
        this.cellsize = cellsize;
        this.nodes = nodes.Select(n => new Node(n.position, n.NodeType)).ToList();
        this.countMove = countMove;
        this.SetCountMoveView = SetCountMoveView;
        this.setTopPanel = setTopPanel;
    }
    public void SetUseHammer(bool status)
    {
        this.useHammer = status;
    }
    public void SetUseBomb(bool status)
    {
        this.useBomb = status;
    }
    public int GetCountMove()
    {
        return countMove;
    }    
    public bool GetEndGame()
    {
        return this.endGame;
    }    
    public void SetEndGame(bool endGame)
    {
        this.endGame = endGame;
    }    
    public Dictionary<Node, GameObject> GetNodeDict()
    {
        return nodesDictObj;
    }    
    private void ChecKOutScreen(GameObject node)
    {
        Vector2 nodePos = node.transform.position;   
        if (nodePos.x > maxX || nodePos.y > maxY || nodePos.x < minX || nodePos.y < minY) 
        {
            node.SetActive(false);
        }
    }
    private void Update()
    {
        //CheckMoveCount(countMove);
        /*  if (CheckAllNodesBlocked(nodes))
          {
              Debug.Log("Không thể di chuyển tất cả các node!");
          }*/
    }
    public void TouchAction(Collider2D collider2D)
    {
        if (endGame) return;
        if (collider2D != null)
        {
            Node node;
            Vector2Int temp = new Vector2Int(Mathf.RoundToInt(collider2D.transform.position.x / cellsize + rows / 2), Mathf.RoundToInt(-collider2D.transform.position.y / cellsize + col / 2));
            node = nodes.Find(n => n.position == new Vector2(temp.x, temp.y));
            if (node.NodeType == NodeType.Saw)
            {
                return;
            }  
            else
            {
                countMove--;
                SetCountMoveView(countMove);
                if (useBomb && node.NodeType != NodeType.Saw && node.NodeType != NodeType.Rotate)
                {
                    SpriteRenderer spriteRenderer = nodesDictObj[node].GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = bombSprite;
                    StartCoroutine(HideAndRemoveSurroundingNodes(node));
                    //HideAndRemoveSurroundingNodes(node);                                
                    return;
                }
                if (useHammer && node.NodeType != NodeType.Saw && node.NodeType != NodeType.Rotate)
                {        
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0;
                    StartCoroutine(SpawnHammerAndDestroy(mousePosition));            
                    nodesDictObj[node].SetActive(false);
                    nodes.Remove(node);
                    useHammer = false;
                    return;
                }
                if (node.NodeType == NodeType.Rotate)
                {
                    RotateSurroundingNodesClockwise(node);
                }
                Vector2Int movePos = NodeMovePosition(node);
                Vector2 destinition;
                if (movePos == node.position)
                {
                    destinition = tables[movePos.x, movePos.y];
                }
                else if (movePos.x < 0 || movePos.x > 100 || movePos.y < 0 && movePos.y > 100)
                {
                    destinition = CheckNode(node);
                    nodes.Remove(node);
                }
                else
                {
                    if (isDesTroy)
                    {
                        StartCoroutine(DelayedActions(node));
                    }
                    destinition = tables[movePos.x, movePos.y];
                    nodes[nodes.IndexOf(node)].position = movePos;
                }
                StartCoroutine(MoveNode(nodesDictObj[node], destinition));
            }        
        }       
    }
    private IEnumerator SpawnHammerAndDestroy(Vector3 position)
    {
        GameObject hammer = Instantiate(hammerPrefab, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(hammer);
        setTopPanel(true, false, "");
    }
    private void RotateSurroundingNodesClockwise(Node node)
    {
        Vector2Int nodePosition = node.position;
        // Save Position around node
        Vector2Int positionUp = nodePosition + Vector2Int.down;
        Vector2Int positionRight = nodePosition + Vector2Int.right;
        Vector2Int positionDown = nodePosition + Vector2Int.up;
        Vector2Int positionLeft = nodePosition + Vector2Int.left;
        Node nodeUp = nodes.Find(n => n.position == positionUp);
        Node nodeRight = nodes.Find(n => n.position == positionRight);
        Node nodeDown = nodes.Find(n => n.position == positionDown);
        Node nodeLeft = nodes.Find(n => n.position == positionLeft);
        // Delete position node around and move
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
            StartCoroutine(MoveNode(nodesDictObj[nodeDown], tables[positionLeft.x, positionLeft.y]));
            // nodesDictObj[nodeDown].transform.position = tables[positionLeft.x, positionLeft.y];

        }
        if (nodeLeft != null)
        {
            nodes[nodes.IndexOf(nodeLeft)].position = positionUp;
            StartCoroutine(MoveNode(nodesDictObj[nodeLeft], tables[positionUp.x, positionUp.y]));
            //nodesDictObj[nodeLeft].transform.position = tables[positionUp.x, positionUp.y];       
        }
    }
    private IEnumerator HideAndRemoveSurroundingNodes(Node node)
    {
        yield return new WaitForSeconds(0.5f);
        nodesDictObj[node].SetActive(false);
        nodes.Remove(node);
        setTopPanel(true, false, "");
        useBomb = false;
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
        Vector3 nodePosition = nodesDictObj[node].transform.position;
        Vector2 direction = Vector2.zero;
        switch (node.NodeType)
        {
            case NodeType.Left:
                direction = new Vector2(minX-2f, nodePosition.y);
                break;
            case NodeType.Right:
                direction = new Vector2(maxY+2f, nodePosition.y);
                break;
            case NodeType.Up:
                direction = new Vector2(nodePosition.x, maxY+2f);
                break;
            case NodeType.Down:
                direction = new Vector2(nodePosition.x, minY-2f);
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
        ChecKOutScreen(node);
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
        }
        Vector2Int curPos = node.position;
        while (curPos.x < rows && curPos.x >= 0 && curPos.y < col && curPos.y >= 0)
        {
            curPos += direction;
            Node temp = nodes.Find(node => node.position == curPos);
            if (temp != null)
            {
                if (temp.NodeType == NodeType.Saw)
                {
                    isDesTroy= true;              
                    //return curPos -= direction;
                }
                return curPos -= direction;
            }
        }
        return Vector2Int.one * 999;
    }
    IEnumerator DelayedActions(Node node)
    {
        isDesTroy = false;
        yield return new WaitForSeconds(0.5f); 
        nodesDictObj[node].SetActive(false);
        nodes.Remove(node);
    }   
    public bool CheckAllNodesBlocked(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            if (node.NodeType != NodeType.Saw && node.NodeType != NodeType.Rotate)
            {
                Vector2Int movePosition = NodeMovePosition(node);
                if (movePosition == Vector2Int.one * 999)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public bool CheckAllObjectsHidden(Dictionary<Node, GameObject> dictionary)
    {
        foreach (var pair in dictionary)
        {
            Node node = pair.Key;
            GameObject obj = pair.Value;
            if (node.NodeType != NodeType.Saw && node.NodeType != NodeType.Rotate && obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
