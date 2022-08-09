using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    [SerializeField] float timeUntilDestruction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeUntilDestruction);
        Destroy(gameObject);
    }
}
