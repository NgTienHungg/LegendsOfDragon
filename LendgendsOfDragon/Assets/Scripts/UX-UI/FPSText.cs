using UnityEngine;
using TMPro;
using System.Collections;

public class FPSText : MonoBehaviour
{
    private TextMeshProUGUI fpsText;
    private string prefix = "FPS: ";
    private bool needUpdate = true;

    private void Awake()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (needUpdate)
            StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        fpsText.text = prefix + ((int)(1f / Time.deltaTime)).ToString();
        needUpdate = false;

        yield return new WaitForSeconds(1f);

        needUpdate = true;
    }
}