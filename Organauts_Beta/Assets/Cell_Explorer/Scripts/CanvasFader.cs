using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;
    [SerializeField] private float fadeTime = 0.7f;

    //Provisional parameters
    [SerializeField] private float visibleTime = 10;

    // Start is called before the first frame update
    private void OnEnable()
    {
        panel.alpha = 0;
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (panel.alpha < 1)
        {
            panel.alpha += fadeTime * Time.deltaTime;
            yield return null;
        }

        panel.alpha = 1;

        yield return new WaitForSeconds(visibleTime);

        while (panel.alpha > 0)
        {
            panel.alpha -= fadeTime * Time.deltaTime;
            yield return null;
        }
        panel.alpha = 0;

    }
}
