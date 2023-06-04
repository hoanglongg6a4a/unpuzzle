using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
    [SerializeField] Color backGroundActiveColor;
    Image backGroundImage;
    Color backGrounDefaultColor;
    Toggle toggle;
    Vector2 handlePosition;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        handlePosition = uiHandleRectTransform.anchoredPosition;
        backGroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
        Debug.Log(backGroundImage);
        //backGroundImage.color = backGroundActiveColor;
        backGrounDefaultColor = backGroundImage.color;
        toggle.onValueChanged.AddListener(OnSwitch);
        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }              
    void OnSwitch (bool on)
    {
        uiHandleRectTransform.anchoredPosition = on ? handlePosition*-1 : handlePosition;
        backGroundImage.color = on ? backGroundActiveColor : backGrounDefaultColor;
    }    
}
