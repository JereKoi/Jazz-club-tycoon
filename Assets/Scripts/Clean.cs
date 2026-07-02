using TMPro;
using UnityEditor.Toolbars;
using UnityEngine;

public class Clean : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _clubNeedsCleaningText;
    [SerializeField] private GameObject _cleanButton;
    public bool hasBeenCleaned = false;
    public static Clean Instance;
    [SerializeField]private float timeSinceCleaned = 0;
     
    // TODO: determine if this function/method is needed anymore
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //Destroy(gameObject);
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
    }

    public void CleanTheClub()
    {
        if (Club.Instance == null) return;

        hasBeenCleaned = true;
        _clubNeedsCleaningText.enabled = false;
        _cleanButton.SetActive(false);

        Club.Instance.ResetDirtyness();

        Debug.Log("Cleaned succesfully!");
    }
}