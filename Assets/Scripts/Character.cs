using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject sprintTrail;
    [SerializeField] GameObject sprintSmoke;

    private float currentV = 0;
    private float currentH = 0;
    private float targetSprintTime = 0;

    private void Update()
    {
        move();
        emote();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            interact();
        }
    }

    private void interact()
    {

    }

    private void emote()
    {

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
