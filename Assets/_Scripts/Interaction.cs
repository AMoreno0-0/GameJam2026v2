using UnityEngine;
using TMPro;

public class Interaction : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Collider collider;
    private Animator animator;
    private bool isInteracted;
    [SerializeField] private TextMeshProUGUI interactionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        animator = GetComponent<Animator>();
        isInteracted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInteracted == false && animator.GetBool("isPushing") == true && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 1;
            interactionText.gameObject.SetActive(false);
            isInteracted = true;
        }   
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!isInteracted && other.gameObject.CompareTag("Dolley"))
        {
            Debug.Log("isInteracted: " + isInteracted + ", Animator: " + animator.GetBool("isPushing"));
            Time.timeScale = 0;
            animator.SetBool("isPushing", true);
            animator.SetBool("isWalking", false);
            interactionText.gameObject.SetActive(true);

            other.transform.SetParent(transform);
            //other.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
    }
}
