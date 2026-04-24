using TMPro;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;
    public float dirtyness = 0f;
    public bool hasBeenCleaned = false;
    public static Clean Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // If want to save club between levels:
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!hasBeenCleaned && dirtyness >= 1f)
        {
            _clubNeedsCleaningText.enabled = true;
        }
        else if (hasBeenCleaned && dirtyness <= 0f || hasBeenCleaned)
        {
            _clubNeedsCleaningText.enabled = false;
        }
    }
}
