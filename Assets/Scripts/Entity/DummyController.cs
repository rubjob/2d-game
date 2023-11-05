using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [Header("Dependecy")]
    public Rigidbody2D Rb;

    [Header("Constant")]
    public float BackToOriginAfter = 3f;

    private Vector2 initialPosition;

    private void Start()
    {
        initialPosition = Rb.position;
    }

    public void OnTakingDamage()
    {
        StopAllCoroutines();
        StartCoroutine(CoOnTakingDamage());
    }

    private IEnumerator CoOnTakingDamage()
    {
        yield return new WaitForSeconds(BackToOriginAfter);

        Rb.position = initialPosition;
    }


}
