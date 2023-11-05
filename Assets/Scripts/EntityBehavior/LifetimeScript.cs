using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeScript : MonoBehaviour
{
    [Header("Constant")]
    public float Lifetime;

    private void Start() {
        StartCoroutine(CoLifetime(Lifetime)); 
    }

    private IEnumerator CoLifetime(float lt) {
        yield return new WaitForSeconds(lt);
        Destroy(this.gameObject);
    }
}
