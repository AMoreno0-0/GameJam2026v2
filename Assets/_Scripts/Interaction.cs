using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;

    private Animator animator;
    private Transform dolley;
    private DolleyPickup pickup;
    private Transform loadedBox;   // the box currently on the dolley
    private bool isPromptOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        interactionText.SetActive(false);
    }

    void Update()
    {
        if (isPromptOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
                ClosePrompt();
            return;
        }

        if (dolley != null && Input.GetKeyDown(KeyCode.Q))
        {
            ReleaseDolley();
        }

        // Load a box, only if the dolley is empty
        if (dolley != null && loadedBox == null && pickup != null
            && pickup.NearbyBox != null && Input.GetKeyDown(KeyCode.R))
        {
            LoadBox(pickup.NearbyBox);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (dolley == null && other.gameObject.CompareTag("Dolley"))
        {
            GrabDolley(other.transform);
            OpenPrompt();
        }
    }

    void OpenPrompt()
    {
        isPromptOpen = true;
        Time.timeScale = 0f;
        interactionText.SetActive(true);
    }

    void ClosePrompt()
    {
        isPromptOpen = false;
        Time.timeScale = 1f;
        interactionText.SetActive(false);
    }

    void GrabDolley(Transform d)
    {
        dolley = d;
        pickup = d.GetComponent<DolleyPickup>();

        if (d.TryGetComponent(out Rigidbody2D dRb))
            dRb.bodyType = RigidbodyType2D.Kinematic;

        d.SetParent(transform);

        animator.SetBool("isPushing", true);
        animator.SetBool("isWalking", false);
    }

    void ReleaseDolley()
    {
        dolley.SetParent(null);

        if (dolley.TryGetComponent(out Rigidbody2D dRb))
            dRb.bodyType = RigidbodyType2D.Dynamic;

        animator.SetBool("isPushing", false);

        dolley = null;
        pickup = null;
        // loadedBox stays parented to the dolley and keeps riding on it
    }

    void LoadBox(Transform box)
    {
        if (box.TryGetComponent(out Rigidbody2D boxRb))
        {
            boxRb.linearVelocity = Vector2.zero; // .velocity on older Unity versions
            boxRb.bodyType = RigidbodyType2D.Kinematic;
        }

        box.SetParent(dolley);
        box.localPosition = new Vector3(0.015f, 0.15f, 0f);
        box.localRotation = Quaternion.identity;

        loadedBox = box;
        pickup.Clear();
    }

    void OnDisable()
    {
        if (isPromptOpen) Time.timeScale = 1f;
    }
}