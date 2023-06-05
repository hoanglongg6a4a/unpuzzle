using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabActive;
    public void Subscribe(TabButton tabButton)
    {
        if(tabButtons == null) 
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(tabButton);
    }
    public void OnTabSelected(TabButton button)
    {
            
    }
    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons) 
        {
            button.backGround.sprite= tabIdle;
        }
    }
}
