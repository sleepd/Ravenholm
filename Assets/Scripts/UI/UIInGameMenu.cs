using UnityEngine;
using UnityEngine.UIElements;

public class UIInGameMenu : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    private VisualElement rootPanel;
    private Button button_resume;
    private Button button_restart;
    private Button button_quit;
    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rootPanel = document.rootVisualElement.Q<VisualElement>("Root");
        button_resume = document.rootVisualElement.Q<Button>("BackBtn");
        button_restart = document.rootVisualElement.Q<Button>("RestartBtn");
        button_quit = document.rootVisualElement.Q<Button>("QuitBtn");
        button_resume.clicked += () =>
        {
            CloseMenu();
        };
        button_restart.clicked += () =>
        {
            GameManager.Instance.RespawnPlayer();
            CloseMenu();
        };
        button_quit.clicked += () =>
        {
            Application.Quit();
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    private void OpenMenu()
    {
        rootPanel.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
        isOpen = true;
        
    }

    private void CloseMenu()
    {
        rootPanel.style.display = DisplayStyle.None;
        Time.timeScale = 1;
        isOpen = false;
    }
}
