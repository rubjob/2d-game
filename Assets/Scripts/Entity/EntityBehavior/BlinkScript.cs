using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    [Header("Dependency")]
    public SpriteRenderer SpriteRenderer;
    public Material BlinkMaterial;

    [Header("Constant")]
    public float BlinkDuration = 0.1f;

    private Coroutine coBlink;
    private Material originalMat;
    private Color originalColor;

    public void Blink() {
        if (coBlink != null) {
            StopAllCoroutines();
        } else {
            originalMat = SpriteRenderer.material;
            originalColor = SpriteRenderer.color;
        }

        coBlink = StartCoroutine(CoBlink());
    }

    private IEnumerator CoBlink() {
        SpriteRenderer.material = BlinkMaterial;
        SpriteRenderer.color = Color.white;

        yield return new WaitForSeconds(BlinkDuration);

        SpriteRenderer.material = originalMat;
        SpriteRenderer.color = originalColor;

        coBlink = null;
    }
}
