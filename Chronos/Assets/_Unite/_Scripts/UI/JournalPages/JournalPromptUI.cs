using UnityEngine;
using System.Collections;
using Unite.EventSystem;
using Unite.JournalSystem;

public class JournalPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private float displayDuration = 5f;
    [SerializeField] private GameEvent onJournalPromptHidden;
    
    private bool isDisplaying;
    private Coroutine hideCoroutine;

    void Start()
    {
        uiGameObject.SetActive(false);
    }

    public void OnPromptRaised()
    {
        ShowUI();
    }

    public void OnJournalOpened()
    {
        HideUI();
    }

    private IEnumerator HideCoroutine()
    {
        yield return new WaitForSeconds(displayDuration);
        uiGameObject.SetActive(false);
        isDisplaying = false;
        onJournalPromptHidden.Raise();
    }

    private void ShowUI()
    {
        if (isDisplaying) return;
        isDisplaying = true;
        uiGameObject.SetActive(true);

        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideCoroutine());
    }

    private void HideUI()
    {
        if (!isDisplaying) return;
        isDisplaying = false;
        uiGameObject.SetActive(false);
        onJournalPromptHidden.Raise();

        if (hideCoroutine == null) return;
        StopCoroutine(hideCoroutine);
        hideCoroutine = null;
    }
}
