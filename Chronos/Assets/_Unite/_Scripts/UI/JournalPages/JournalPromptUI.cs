using UnityEngine;
using System.Collections;

public class JournalPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiGameObject;
    [SerializeField] private float displayDuration = 5f;
    [SerializeField] private bool isDisplaying;

    void Start()
    {
        uiGameObject.SetActive(false);
    }

    public void ShowPromptUI(string eventName)
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
    }
}
