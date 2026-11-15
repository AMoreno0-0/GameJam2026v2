using UnityEngine;
using UnityEngine.SceneManagement;
public class ActivationZone : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private KeyCode closeKey = KeyCode.E;
    [SerializeField] private string sceneToLoad;

    private bool isPromptOpen;

    private void Start()
    {
        if (promptUI != null) promptUI.SetActive(false);
    }

    private void Update()
    {
        if (isPromptOpen && Input.GetKeyDown(closeKey)) //get key pressed
        {
            ClosePrompt();
            if (gameObject.CompareTag("toOutside")) SceneManager.LoadScene("Exterior");
            // if (gameObject.CompareTag("toOffice")) SceneManager.LoadScene("CorpoInterior");
            // if (gameObject.CompareTag("toCharity")) SceneManager.LoadScene("CharityInterior");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag)) OpenPrompt();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag)) OpenPrompt();
    }

    private void OpenPrompt()
    {
        // Keep coming back until a building has been chosen
        if (isPromptOpen || ChoiceState.HasChosen) return;

        isPromptOpen = true;
        Time.timeScale = 0f;
        if (promptUI != null) promptUI.SetActive(true);
    }

    // Hook to the X button's OnClick. E calls it too.
    public void ClosePrompt()
    {
        if (!isPromptOpen) return;

        isPromptOpen = false;
        ChoiceState.LastPromptCloseFrame = Time.frameCount;
        if (promptUI != null) promptUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        if (isPromptOpen) Time.timeScale = 1f;
    }

}