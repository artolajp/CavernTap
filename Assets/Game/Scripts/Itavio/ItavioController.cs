using UnityEngine;
using itavio;

public class ItavioController : MonoBehaviour {
	public const bool SHOW_GET_APP_DIALOG = true;

	private static bool hasParentApp=false;

	[SerializeField]
    private string platform;



    void Start()
    {
        itavioManager.initialize(platform);
		#if UNITY_IOS
		itavioManager.OnLinkWithParentApp += OnLinkedWithParent;
		#endif
		itavioManager.OnCheckForParent += OnCheckForParent;

		itavioManager.OnStartDebit += itavioManager_OnStartDebit;
		itavioManager.OnCancelDebit += itavioManager_OnCancelDebit;
		itavioManager.OnCompleteDebit += itavioManager_OnCompleteDebit;
		itavioManager.OnError += itavioManager_OnError;
    }

    private static ItavioController instance;
    public static ItavioController Instance
    {
        get {
            if (instance == null)
                instance = FindObjectOfType<ItavioController>();
            return instance;
        }
    } 

	public delegate void IAPProcess(bool success);
	private IAPProcess iapListener;

	private bool alreadyTryLinkItavio;

	public void OpenTransaction(IAPProcess listener)
    {
		iapListener = listener;
        if (itavioManager.IsLinked)
        {
			itavioManager.checkForParent ();

        }
        else
        {
			if (!alreadyTryLinkItavio) {
				ReLinkWithItavio ();
				alreadyTryLinkItavio = true;
			} else {
				StartDebit ();
				alreadyTryLinkItavio = false;
			}
        }
    }

	private void StartDebit(){
		itavioManager.startDebit (0.99f, "USD", iapListener, true);
	}


	public void OnCheckForParent(bool success){
		if (success) {
			hasParentApp = true;
			StartDebit ();
		} else {
			Debug.Log ("no parent");
			iapListener (true);
		}
	}

	private void OnLinkedWithParent(bool success){
		if (success) {
			OpenTransaction (iapListener);
		} else
			iapListener (true);
	}
    private void LinkWithItavio()
    {
    #if UNITY_IOS
        itavioManager.linkWithParentApp(SHOW_GET_APP_DIALOG);
    #endif
    }

	private void ReLinkWithItavio(){
		#if UNITY_IOS
		itavioManager.linkWithParentApp(SHOW_GET_APP_DIALOG, true);
		#endif
	}

	public void CloseTranaction(bool success){
		itavioManager.finalizeDebit(success);

	}


	void itavioManager_OnStartDebit(bool result)
	{
		if (result)
		{
			// Debit has started ~ this is when the purchase delegate is called
		}
	}

	void itavioManager_OnCancelDebit(bool result)
	{
		if (result)
		{
			// Debit was cancelled
		}
	}

	void itavioManager_OnCompleteDebit(bool result)
	{
		if (result)
		{
			// Debit completed
		}
	}

	void itavioManager_OnError(int code, string message)
	{
		Debug.Log (code+" "+message);
	}
}
