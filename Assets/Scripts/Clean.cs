using TMPro;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;
    public float dirtyness = 0f;
    public bool hasBeenCleaned = false;
    public static Clean Instance;
    [SerializeField]private float timeSinceCleaned = 0;
     

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Club.Instance == null)
        {
            Debug.Log("Club instance was null, returning");
            return;
        }


        timeSinceCleaned += Time.deltaTime;
        if (timeSinceCleaned > 20f)
        {
            Club.Instance.IncreaseDirtyness();
            Debug.Log("Dirtyness increased!");
            timeSinceCleaned = 0f;
        }

        if (!hasBeenCleaned && dirtyness >= 1f)
        {

            if (!_clubNeedsCleaningText.enabled)
            {
                _clubNeedsCleaningText.enabled = true;
                Club.Instance.IncreaseDirtyness();
            }
        }
    }

    public void CleanTheClub()
    {
        if (Club.Instance == null) return;

        dirtyness = 0f;
        hasBeenCleaned = true;
        _clubNeedsCleaningText.enabled = false;

        Club.Instance.ResetDirtyness();

        Debug.Log("Cleaned succesfully!");
    }
}