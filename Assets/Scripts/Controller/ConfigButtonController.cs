using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConfigButtonController : MonoBehaviour
{
    [SerializeField] private UnityEvent onButtonExitConfig;
    [SerializeField] private UnityEvent onButtonShowConfig;
    public void ExitConfig()
    {
        onButtonExitConfig?.Invoke();
    }     
    public void ShowConfig()
    {
        onButtonShowConfig.Invoke();
    }    
}
