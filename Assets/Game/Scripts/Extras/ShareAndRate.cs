using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;


public class ShareAndRate : MonoBehaviour {

	string subject = "Cavern Tap";
    string bundleName = "com.CyborgFish.CavernTap";//"ufo.funhub.rkkr.cavern";
    string body = "Cavern Tap https://play.google.com/store/apps/details?id=";
	string appleId="1230957291";
    private static ShareAndRate instance;

    public static ShareAndRate Instance
    {
        get
        {
            if (!instance) {
                instance = FindObjectOfType<ShareAndRate>();
                if (instance == null)
                {
                    instance = new GameObject("ShareAndRateController").AddComponent<ShareAndRate>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }

            return instance;
        }
    }



	/*
#if UNITY_IPHONE
	
	[DllImport("__Internal")]
	private static extern void sampleMethod (string iosPath, string message);
	
	[DllImport("__Internal")]
	private static extern void sampleTextMethod (string message);
	
#endif
*/
    public void OnAndroidTextSharingClick()
	{
		#if UNITY_ANDROID
		StartCoroutine(ShareAndroidText());
		#endif
	}
	IEnumerator ShareAndroidText()
	{
		yield return new WaitForEndOfFrame();
		//execute the below lines if being run on a Android device
		#if UNITY_ANDROID
		//Reference of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		//Reference of AndroidJavaObject class for intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");
		//call setAction method of the Intent object created
		intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
		//set the type of sharing that is happening
		intentObject.Call<AndroidJavaObject>("setType", "text/plain");
		//add data to be passed to the other activity i.e., the data to be sent
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject);
		//intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "Text Sharing ");
		intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body+bundleName);
		//get the current activity
		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
		//start the activity by sending the intent data
		AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via");
		currentActivity.Call("startActivity", jChooser);
		#endif
	}
	
	/*
	public void OniOSTextSharingClick()
	{
		
		#if UNITY_IPHONE || UNITY_IPAD
		string shareMessage = "Wow I Just Share Text ";
		sampleTextMethod (shareMessage);
		
		#endif
	}
*/
	public void RateUs()
	{
        PlayerPrefs.SetInt(PanelRate.CountShowedKey, PlayerPrefs.GetInt(PanelRate.CountShowedKey, 0) + 1);
        PlayerPrefs.Save();
#if UNITY_ANDROID
        Application.OpenURL("market://details?id="+bundleName);
		#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/id"+appleId);
		#endif
	}

}
