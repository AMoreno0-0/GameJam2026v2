using UnityEngine;

public class SceneStartPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupUI;

    private void Start()
    {
        if (popupUI != null)
        {
            popupUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        bool popupOpen = popupUI != null && popupUI.activeInHierarchy;
        Time.timeScale = popupOpen ? 0f : 1f;
    }

    // Public so a UI Button's OnClick can call it too
    public void ClosePopup()
    {
        if (popupUI != null) popupUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        // Safety net so leaving the scene never leaves the game frozen
        Time.timeScale = 1f;
    }
}