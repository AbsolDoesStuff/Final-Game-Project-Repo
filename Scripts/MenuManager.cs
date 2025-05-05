using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Menu UI Elements")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button exitToMenuButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button settingsFromPauseButton;
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject settingsCanvas;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundEffectsSource;

    [Header("UI Sliders")]
    [SerializeField] private Slider musicVolumeSlider;

    private bool isPaused = false;
    private GameObject lastActiveCanvas;

    private SoundManager soundManager;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();

        // Set initial volume from music slider
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = musicSource.volume;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        // Button click listeners
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayButtonClicked);

        if (resumeButton != null)
            resumeButton.onClick.AddListener(OnResumeButtonClicked);

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsButtonClicked);

        if (settingsFromPauseButton != null)
            settingsFromPauseButton.onClick.AddListener(OnSettingsButtonClicked);

        if (exitButton != null)
            exitButton.onClick.AddListener(OnExitButtonClicked);

        if (exitToMenuButton != null)
            exitToMenuButton.onClick.AddListener(OnExitToMenuButtonClicked);

        if (backButton != null)
            backButton.onClick.AddListener(OnBackButtonClicked);

        if (pauseButton != null)
            pauseButton.onClick.AddListener(OnPauseButtonClicked);

        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            OpenPauseMenu();
        }
    }

    private void OnPlayButtonClicked()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnResumeButtonClicked()
    {
        ClosePauseMenu();
        Time.timeScale = 1f;
    }

    private void OnSettingsButtonClicked()
    {
        lastActiveCanvas = pauseCanvas.activeSelf ? pauseCanvas : menuCanvas;
        settingsCanvas.SetActive(true);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");
    }

    private void OnExitToMenuButtonClicked()
    {
        ResetGame();
        menuCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    private void OnBackButtonClicked()
    {
        if (lastActiveCanvas != null)
        {
            settingsCanvas.SetActive(false);
            lastActiveCanvas.SetActive(true);
        }
    }

    private void OnPauseButtonClicked()
    {
        if (!isPaused)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }

    private void OpenPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    private void ClosePauseMenu()
    {
        isPaused = false;
        pauseCanvas.SetActive(false);
    }

    private void ResetGame()
    {
        Time.timeScale = 1f;
    }

    // Method to adjust the music volume
    public void SetMusicVolume(float volume)
    {
        if (soundManager != null)
        {
            soundManager.SetMusicVolume(volume);
        }
    }
}
