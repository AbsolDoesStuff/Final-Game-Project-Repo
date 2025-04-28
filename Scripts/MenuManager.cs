using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject menuCanvas;

    void Start()
    {
        Time.timeScale = 0f;

        if (playButton != null && menuCanvas != null)
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }
        else
        {
            Debug.LogError("Play Button or Menu Canvas not assigned in MenuManager!");
        }
    }

    private void OnPlayButtonClicked()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
}