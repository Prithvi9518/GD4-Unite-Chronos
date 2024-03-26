using UnityEngine;
using System.Collections;
using Unite.EventSystem;
using Unite.JournalSystem;

public class JournalPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private float displayDuration = 5f;
    [SerializeField] private bool isDisplaying;
    [SerializeField] private GameEvent onJournalPromptHidden;

    void Start()
    {
        uiGameObject.SetActive(false);
    }

    public void ShowPromptUI()
    {
        isDisplaying = true;
        uiGameObject.SetActive(true);
        StartCoroutine(HidePromptUI());
    }

    private IEnumerator HidePromptUI()
    {
        yield return new WaitForSeconds(displayDuration);
        uiGameObject.SetActive(false);
        isDisplaying = false;
        onJournalPromptHidden.Raise();
    }
}
