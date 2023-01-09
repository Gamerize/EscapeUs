using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private GameObject m_StartMenuCanvas;
    [SerializeField] private GameObject m_GameplayUICanvas;
    [SerializeField] private GameObject m_CreditsCanvas;
    [SerializeField] private GameObject m_VictoryCanvas;
    [SerializeField] private GameObject m_GameOverCanvas;

    private void Start()
    {
        m_StartMenuCanvas.SetActive(true);
        m_GameplayUICanvas.SetActive(false);
        m_CreditsCanvas.SetActive(false);
        m_VictoryCanvas.SetActive(false);
        m_GameOverCanvas.SetActive(false);
        Time.timeScale = 0f;
    }

    public void GameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_StartMenuCanvas.SetActive(false);
        m_GameplayUICanvas.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ShowCredits()
    {
        m_StartMenuCanvas.SetActive(false);
        m_CreditsCanvas.SetActive(true);
    }

    public void GameOver(bool dead)
    {
        if (dead == true)
        {
            Cursor.lockState = CursorLockMode.None;
            m_GameplayUICanvas.SetActive(false);
            m_GameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Victory()
    {       
            Cursor.lockState = CursorLockMode.None;
            m_GameplayUICanvas.SetActive(false);
            m_VictoryCanvas.SetActive(true);
            Time.timeScale = 0f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quited Game");
    }
}
