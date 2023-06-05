using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour , IPointerClickHandler , IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image backGround;

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    private void Start()
    {
        backGround= GetComponent<Image>();
        tabGroup.Subscribe(this);
    }
}

