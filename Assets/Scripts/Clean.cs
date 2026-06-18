using TMPro;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;
    public float dirtyness = 0f;
    public bool hasBeenCleaned = false;
    public static Clean Instance;
    private float timeSinceCleaned = 0;

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
        // For time since cleanded, it should be based on how many customers have visited.
        timeSinceCleaned += Time.deltaTime;
        if (timeSinceCleaned > 60)
        {
            Club.Instance.IncreaseDirtyness();
            timeSinceCleaned = 0;
        }

        if (!hasBeenCleaned && dirtyness >= 1f)
        {
            _clubNeedsCleaningText.enabled = true;
            Club.Instance.IncreaseDirtyness();
        }
        else if (hasBeenCleaned && dirtyness <= 0f || hasBeenCleaned)
        {
            _clubNeedsCleaningText.enabled = false;
            Club.Instance.hasBeenCleaned = true;
            Club.Instance.ResetDirtyness();
        }
    }
}
