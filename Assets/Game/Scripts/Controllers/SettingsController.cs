using UnityEngine;
using System.Collections;

public class SettingsController : MonoBehaviour {

    public enum Options { Music,Sfx}
    private static bool music, sfx,mload,sload;

    public static bool Music
    {
        get
        {
            if (!mload)
            {
                music = PlayerPrefs.GetInt("SettingsMusic", 1) == 1;
                mload = true;
            }

            return music;
        }

        set
        {
            music = value;
            PlayerPrefs.SetInt("SettingsMusic", value? 1:0);
        }
    }

    public static bool Sfx
    {
        get
        {
            if (!sload) {
                sfx =PlayerPrefs.GetInt("SettingsSfx", 1) == 1;
                sload = true;
            }
            return sfx; 
        }

        set
        {
            sfx = value;
            PlayerPrefs.SetInt("SettingsSfx", value ? 1 : 0);
        }
    }
}
