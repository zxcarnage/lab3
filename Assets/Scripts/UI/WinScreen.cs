using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonPressed);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonPressed);
    }

    private void OnRestartButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
