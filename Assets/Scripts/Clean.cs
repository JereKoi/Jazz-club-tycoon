using TMPro;
using UnityEngine;

public class Clean : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;

    private void OnEnable()
    {
        Club.OnDirtynessChanged += UpdateUI;
    }

    private void OnDisable()
    {
        Club.OnDirtynessChanged -= UpdateUI;
    }

    private void UpdateUI(float newDirtyness)
    {
        int percentage = Mathf.RoundToInt(newDirtyness * 30f);
        _clubNeedsCleaningText.text = "Dirtyness: " + percentage + " %";

        Debug.Log("UI got new info! New dirtyness is: " + percentage + " %");
    }
}
