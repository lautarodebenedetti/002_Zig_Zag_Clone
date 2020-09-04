using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Transform rayStart;
    public GameObject crystalEffect;

    private Rigidbody rb;
    private bool walkingRight = true;
    private Animator anim;
    private GameManager gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if (!gameManager.gameStarted)
        {
            return;
        } else
        {
            anim.SetTrigger("gameStarted");
        }
        rb.transform.position = transform.position + transform.forward * 2 * Time.deltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Switch();
        }
        if (!IsAFloorOrSomethingBelowUs(this.rayStart))
        {
            anim.SetTrigger("isFalling");
        } else
        {
            anim.SetTrigger("notFallingAnymore");
        }
        if(transform.position.y < -2)
        {
            this.gameManager.EndGame();
        }
    }

    private bool IsAFloorOrSomethingBelowUs(Transform rayStart)
    {
        RaycastHit hit;
        return Physics.Raycast(rayStart.position, - transform.up, out hit, Mathf.Infinity);
    }

    private void Switch()
    {
        if (!gameManager.gameStarted)
        {
            return;
        }
        walkingRight = !walkingRight;
        if (walkingRight)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Crystal")
        {
            this.gameManager.IncreaseScore();

            GameObject crystalE = Instantiate(this.crystalEffect, rayStart.transform.position, Quaternion.identity);
            Destroy(crystalE, 2);
            Destroy(other.gameObject);
        }
    }
}
