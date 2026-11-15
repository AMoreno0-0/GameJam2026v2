using UnityEngine;
using UnityEngine.Events;

public class BuildingChoice : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private KeyCode chooseKey = KeyCode.E;
    [SerializeField] private GameObject interactHint;   // optional "Press E" label
    [SerializeField] private UnityEvent onChosen;       // load a scene, show UI, etc.

    private bool playerInRange;

    private void Start()
    {
        if (interactHint != null) interactHint.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange || ChoiceState.HasChosen) return;
        if (Time.timeScale == 0f) return;                                  // a prompt is open
        if (Time.frameCount == ChoiceState.LastPromptCloseFrame) return;   // E just closed a prompt
        if (!Input.GetKeyDown(chooseKey)) return;

        ChoiceState.HasChosen = true;
        if (interactHint != null) interactHint.SetActive(false);
        onChosen?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInRange = true;
        if (interactHint != null && !ChoiceState.HasChosen) interactHint.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        playerInRange = false;
        if (interactHint != null) interactHint.SetActive(false);
    }
}