using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private GameObject hintTextContainer;
    [SerializeField] private Text hintText;
    public bool TimedHintCrRunning { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else Instance = this;
        SetHintUIActive(false);
    }

    public void StartShowingTimedHint(string msg, Color txtColor, float time)
    {
        if (time <= 0)
        {
            if (time <= 0)
                throw new System.ArgumentException($"'{nameof(time)}' debe ser mayor a 0");
            if (msg == "")
                Debug.LogWarning($"'{nameof(msg)}' es un string vacío");
        }
        else
        {
            StopAllCoroutines();
            TimedHintCrRunning = false;
            StartCoroutine(ShowTimedHint(msg, txtColor, time));
        }
    }

    public void SetHintText(string msg, Color txtColor)
    {
        hintText.text = msg;
        hintText.color = txtColor;
    }

    private IEnumerator ShowTimedHint(string msg, Color txtColor, float hintTime)
    {
        TimedHintCrRunning = true;
        float time = 0;
        while (time < hintTime)
        {
            time += Time.deltaTime;
            SetHintUIActive(true);
            SetHintText(msg, txtColor);
            yield return null;
        }
        TimedHintCrRunning = false;
    }

    public void SetHintUIActive(bool a)
    {
        hintTextContainer.SetActive(a);
    }
}
