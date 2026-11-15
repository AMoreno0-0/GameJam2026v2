using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionText;

    private Animator animator;
    private Transform dolley;
    private DolleyPickup pickup;

    void Start()
    {
        animator = GetComponent<Animator>();
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Let go of the dolley
        if (dolley != null && Input.GetKeyDown(KeyCode.E))
        {
            ReleaseDolley();
        }

        // Load a box
        if (dolley != null && pickup != null && pickup.NearbyBox != null
            && Input.GetKeyDown(KeyCode.R))
        {
            LoadBox(pickup.NearbyBox);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (dolley == null && other.gameObject.CompareTag("Dolley"))
        {
            GrabDolley(other.transform);
        }
    }

    void GrabDolley(Transform d)
    {
        dolley = d;
        pickup = d.GetComponent<DolleyPickup>();

        // Make the dolley follow the player instead of simulating on its own
        if (d.TryGetComponent(out Rigidbody2D dRb))
            dRb.bodyType = RigidbodyType2D.Kinematic;

        d.SetParent(transform);

        animator.SetBool("isPushing", true);
        animator.SetBool("isWalking", false);
        interactionText.gameObject.SetActive(true);
    }

    void ReleaseDolley()
    {
        dolley.SetParent(null);

        if (dolley.TryGetComponent(out Rigidbody2D dRb))
            dRb.bodyType = RigidbodyType2D.Dynamic;

        animator.SetBool("isPushing", false);
        interactionText.gameObject.SetActive(false);

        dolley = null;
        pickup = null;
    }

    void LoadBox(Transform box)
    {
        if (box.TryGetComponent(out Rigidbody2D boxRb))
        {
            boxRb.linearVelocity = Vector2.zero;
            boxRb.bodyType = RigidbodyType2D.Kinematic;
        }

        box.SetParent(dolley);
        box.localPosition = new Vector3(0.025f, 0.15f, 0f); // tweak so it sits on the dolley
        box.localRotation = Quaternion.identity;

        pickup.Clear();
    }
}