using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject sprintTrail;
    [SerializeField] GameObject sprintSmoke;

    [SerializeField] GameObject emote1;
    [SerializeField] GameObject emote2;
    [SerializeField] GameObject emote3;
    [SerializeField] GameObject emote4;
    [SerializeField] Transform emoteSpawnPos;

    private float currentV = 0;
    private float currentH = 0;
    private float targetSprintTime = 0;
    private bool isGrabbing = false;

    private void Update()
    {
        move();
        emote();
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            interact();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            grab();
        }
    }

    private void interact()
    {

    }

    public void grab()
    {
        isGrabbing = !isGrabbing;
        animator.SetBool("isGrabbing", isGrabbing);

    }

    private void emote()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(emote1, emoteSpawnPos, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Instantiate(emote2, emoteSpawnPos, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Instantiate(emote3, emoteSpawnPos, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Instantiate(emote4, emoteSpawnPos, false);
        }
    }

    private void move()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (v < 0)
        {
            v *= BalanceSheet.backwardsRunMultiplier;
        }

        currentV = Mathf.Lerp(currentV, v, Time.deltaTime *
            BalanceSheet.characterInterpolation);
        currentH = Mathf.Lerp(currentH, h, Time.deltaTime *
            BalanceSheet.characterInterpolation);

        transform.position += transform.forward * currentV *
            BalanceSheet.characterMoveSpeed * Time.deltaTime;
        transform.Rotate(0, currentH *
            BalanceSheet.characterTurnSpeed * Time.deltaTime, 0);

        animator.SetFloat("MoveSpeed", currentV);

        if (currentV > 0 && Input.GetKeyDown(KeyCode.LeftShift)
            && Time.time > targetSprintTime)
        {
            Instantiate(sprintSmoke, transform.position, transform.rotation);
            Instantiate(sprintTrail, transform, false);
            currentV = BalanceSheet.sprintDistance;
            targetSprintTime += BalanceSheet.sprintCooldown;
        }
    }
}
