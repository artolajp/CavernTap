/*
 * Copyright (c) 2015. KinderGuardian Inc.
 */

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using itavio;
using itavio.Utilities;

public class itavioSample : MonoBehaviour
{
    private bool m_showGetAppDialog = false;
    //private bool m_canCompleteDebit = false;
	private double m_balance = -1.0;
    private double m_limit = -1.0;
    private string m_purchaseValue = "1.00";
    private List<string> m_messages = new List<string>() { "", "", "" };
	private string m_currency = "CAD";
    private string m_limitCurrency = "";
	private string m_balanceCurrency = "";
	private string m_emailAddress = "email";
	private GUIStyle m_buttonStyle;
	private GUIStyle m_labelStyle;
	private GUIStyle m_fieldStyle;
	private GUIStyle m_windowStyle;
    private GUIStyle m_toggleStyle;
	private bool m_showResult;
	private string m_result;

    public delegate void SampleTransactionCompleted();
    public event SampleTransactionCompleted OnSampleTransactionCompleted;

    // Use this for initialization
    void Start()
    {
        itavioDbg.IsEnabled = true;	// Enable Itavio Debug Messages
        itavioManager.initialize("itavioSampleStaging");
        //itavioManager.initialize("itavioSampleIntegration");

        OnSampleTransactionCompleted += itavioSample_OnSampleTransactionCompleted;

		itavioManager.OnGetBalance += itavioManager_OnGetBalance;
		itavioManager.OnGetLimit += itavioManager_OnGetLimit;
        itavioManager.OnStartDebit += itavioManager_OnStartDebit;
        itavioManager.OnCancelDebit += itavioManager_OnCancelDebit;
        itavioManager.OnCompleteDebit += itavioManager_OnCompleteDebit;
        itavioManager.OnCheckForParent += itavioManager_OnCheckForParent;
        itavioManager.OnError += itavioManager_OnError;

#if UNITY_IOS
        itavioManager.OnLinkWithParentApp += itavioManager_OnLinkWithParentApp;
#endif

    }

    private void Log(string message)
    {
		m_result = message;
		m_showResult = true;
    }

    private void SampleTransaction(string item, int cost)
    {
        Log("Transaction Started");
        if (OnSampleTransactionCompleted != null)
        {
            OnSampleTransactionCompleted();
        }
    }

    private void itavioSample_OnSampleTransactionCompleted()
    {
        Log("Transaction Completed");
        //m_canCompleteDebit = true;
    }

	void itavioManager_OnGetBalance(double result, string currencyCode)
    {
		Log("Retrieved Balance: " + result + currencyCode);
		m_balance = result;
		m_balanceCurrency = currencyCode;
    }

	void itavioManager_OnGetLimit(double result, string currencyCode)
	{
		Log("Retrieved Balance: " + result + currencyCode);
		m_limit = result;
		m_limitCurrency = currencyCode;
		m_currency = currencyCode;
	}

    void itavioManager_OnStartDebit(bool result)
    {
        if (result)
        {
            Log("Started Debit");
        }
    }

    void itavioManager_OnCancelDebit(bool result)
    {
        if (result)
        {
            Log("Debit Cancelled");
            //m_canCompleteDebit = false;
        }
    }

    void itavioManager_OnCompleteDebit(bool result)
    {
        if (result)
        {
            Log("Debit Completed");
            //m_canCompleteDebit = false;
        }
    }

    void itavioManager_OnCheckForParent(bool result)
    {
        Log("Has Parent App: " + result);
    }

    void itavioManager_OnError(int code, string message)
    {
        m_messages.Add("OnError " + code + ": " + message);
        itavioDbg.LogError("Error " + code + ": " + message);
    }

#if UNITY_IOS
    void itavioManager_OnLinkWithParentApp(bool result)
    {
        Log("Linked Parent: " + result);
    }
#endif

	private void ResultWindowFunc(int windowID)
	{
		GUILayout.Label (m_result, m_labelStyle);
		GUILayout.FlexibleSpace ();
		if(GUILayout.Button("Ok", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.MinHeight(150)))
			m_showResult = false;
	}

    void OnGUI()
    {
		m_buttonStyle = new GUIStyle (GUI.skin.button);
		m_labelStyle = new GUIStyle (GUI.skin.label);
		m_fieldStyle = new GUIStyle (GUI.skin.textArea);
		m_toggleStyle = new GUIStyle (GUI.skin.toggle);
		m_windowStyle = new GUIStyle (GUI.skin.window);
        m_buttonStyle.fontSize = 32;
		m_labelStyle.fontSize = 32;
		m_fieldStyle.fontSize = 32;
		m_toggleStyle.fontSize = 32;
        m_windowStyle.fontSize = 32;

        GUILayout.BeginArea (new Rect (0f, 0f, Screen.width, Screen.height));

		if (m_showResult) {
			GUI.Window (0, new Rect (0, 0, Screen.width, Screen.height), ResultWindowFunc, "Results", m_windowStyle);
		} else {

		

			GUILayout.Label (string.Format("Balance: {0} {1}", m_balance, m_balanceCurrency), m_labelStyle);
			GUILayout.Label (string.Format("Limit: {0} {1}", m_limit, m_limitCurrency), m_labelStyle);
			GUILayout.BeginHorizontal ();
			m_currency = GUILayout.TextField (m_currency, m_fieldStyle);
			m_purchaseValue = GUILayout.TextField (m_purchaseValue, m_fieldStyle);
			GUILayout.EndHorizontal ();
			m_emailAddress = GUILayout.TextField (m_emailAddress, m_fieldStyle);

			m_showGetAppDialog = GUILayout.Toggle (m_showGetAppDialog, "Show Get App Dialog", m_toggleStyle);

			if (GUILayout.Button ("Check For Parent App", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.checkForParent (m_showGetAppDialog);
			}

			if (GUILayout.Button ("Get Balance", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.getBalance ();
			}

			if (GUILayout.Button ("Get Limit", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.getLimit ();
			}

			if (GUILayout.Button ("Purchase", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.startDebit<Action<string, int>> (Convert.ToDouble (m_purchaseValue), m_currency, (string item, int cost) => {
					itavioManager.finalizeDebit (true);
					SampleTransaction (item, cost);
				}, "item_id", 1);
			}

			if (GUILayout.Button ("Purchase (Cancel)", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.startDebit<Action<string, int>> (Convert.ToDouble (m_purchaseValue), m_currency, (string item, int cost) => {
					itavioManager.finalizeDebit (false);
					SampleTransaction (item, cost);
				}, "item_id", 1);

			}

			if (GUILayout.Button ("Show uninstall dialog", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.showUninstallDialog (m_emailAddress);
			}

#if UNITY_IOS
			GUILayout.BeginHorizontal ();
			if (GUILayout.Button ("Link with Parent", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.linkWithParentApp (m_showGetAppDialog);
			}

			if (GUILayout.Button ("Re-Link with Parent", m_buttonStyle, GUILayout.ExpandWidth (true), GUILayout.ExpandHeight (true))) {
				itavioManager.linkWithParentApp (m_showGetAppDialog, true);
			}
			GUILayout.EndHorizontal ();
#endif

			GUI.enabled = true;
		}
		GUILayout.EndArea ();

    }

}
