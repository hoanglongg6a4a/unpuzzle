using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfigButtonController : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonExitConfig;
    [SerializeField] private UnityEvent onButtonShowConfig;
    [SerializeField] private UnityEvent onButtonShowSkinPanel;
    [SerializeField] private UnityEvent onButtonExitSkinPanel;
    public void ExitConfig()
    {
        onButtonExitConfig?.Invoke();
    }     
    public void ShowConfig()
    {
        onButtonShowConfig.Invoke();
    }
    public void ShowSkinPanel()
    {
        onButtonShowSkinPanel?.Invoke();
    }
    public void ExitSkinPanel()
    {
        onButtonExitSkinPanel?.Invoke();
    }
}
