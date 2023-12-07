using System;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private WinScreen _winScreen;
    private void Awake()
    {
        ServiceLocator.GameUI = this;
    }

    public void ShowWinScreen()
    {
        _winScreen.gameObject.SetActive(true);
    }
}