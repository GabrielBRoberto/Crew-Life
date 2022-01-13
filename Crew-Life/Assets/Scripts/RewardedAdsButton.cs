using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Button))]
public class RewardedAdsButton : MonoBehaviour, IUnityAdsListener
{
    private string gameId;
    private string myPlacementId;

    Button myButton;

    [SerializeField]
    bool onAndroid;

    private void Awake()
    {
        if (onAndroid)
        {
            gameId = "4495565";
            myPlacementId = "Rewarded_Android";
        }
        else
        {
            gameId = "4495564";
            myPlacementId = "Rewarded_iOS";
        }
    }

    void Start()
    {
        myButton = GetComponent<Button>();

        //Set interactivity to be dependent on the Placement's status:
        myButton.interactable = Advertisement.IsReady(myPlacementId);

        //Map the ShowRewardedVideo function to the button's click listener:
        if (myButton)
        {
            myButton.onClick.AddListener(ShowRewardedVideo);
        }

        //Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, true);
    }

    //Implement a function for showing a rewarded video ad:
    void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    //Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        //If the ready Placement is rewarded, activate the button:
        if (placementId == myPlacementId)
        {
            myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            Time.timeScale = 1;
            //Reward the user for watching the ad to completion.
            FindObjectOfType<GameManager>().AdsRewardsSort();
        }
        else if (showResult == ShowResult.Skipped)
        {
            Time.timeScale = 1;
            //Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Time.timeScale = 1;
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Time.timeScale = 0;
    }
}
