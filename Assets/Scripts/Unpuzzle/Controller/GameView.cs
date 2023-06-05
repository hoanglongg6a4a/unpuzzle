using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject config;
    [SerializeField] private GameObject topButtonPanel;
    [SerializeField] private GameObject skinPanel;
    [SerializeField] private GameObject panelWhenUseSkill;
    [SerializeField] private GameObject Tab1;
    [SerializeField] private GameObject Tab2;
    [SerializeField] private TextMeshProUGUI coinNum;
    [SerializeField] private TextMeshProUGUI moveCount;
    [SerializeField] private TextMeshProUGUI textWhenUseSkill;
    [SerializeField] private TextMeshProUGUI levelNum;
    [SerializeField] private TextMeshProUGUI coinText;
    private const string Level = "Level";
    private const string TotalCoin = "TotalCoin";
    private int toTalCoin;
    private void Awake()
    {
        int levelGame = PlayerPrefs.GetInt(Level);
        toTalCoin = PlayerPrefs.GetInt(TotalCoin);
        levelNum.text = "Level: " + levelGame.ToString(); 
        coinText.text = toTalCoin.ToString();
    }
    public void ShowTab1()
    {
        
    }
    public void SetTopPanel(bool statusPanel,bool statusText,string text)
    {
        topButtonPanel.SetActive(statusPanel);
        textWhenUseSkill.text = text;
        panelWhenUseSkill.SetActive(statusText);      
    }
    public void ShowSkinPanel()
    {
        skinPanel.SetActive(true);
    }
    public void ExitSkinPanel()
    {
        skinPanel.SetActive(false);
    }
    public void SetMoveCount(int count)
    {
        moveCount.text = count.ToString()+" Moves";
    }
    public void SetCoin()
    {
        toTalCoin++;
        PlayerPrefs.SetInt(TotalCoin, toTalCoin);
        coinNum.text = toTalCoin.ToString();
    }
    public void ShowEndPanel()
    {
        endPanel.SetActive(true);
    }
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }
    public void ShowConfig()
    {
        config.SetActive(true);
    }  public void ExitConfig()
    {
        config.SetActive(false);
    }
 
}
