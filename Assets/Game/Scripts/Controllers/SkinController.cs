using UnityEngine;

public class SkinController  {

    public enum Skins { Cocodrilos, Aliens, Panchos,Dragones,Retro,Topos,NotDefined,Fingers,Jelly }
    private static Skins actualSkin;
    private static string nameActualSkin="";

    public static Skins ActualSkin
    {
        get
        {
            if (string.IsNullOrEmpty(nameActualSkin))
            {
                nameActualSkin = PlayerPrefs.GetString("ActualSkin", Skins.Cocodrilos.ToString());
                actualSkin = GetSkinFrom(nameActualSkin);
            }
            return actualSkin;
        }

        set
        {
            actualSkin = value;
            nameActualSkin = actualSkin.ToString();
            PlayerPrefs.SetString("ActualSkin",actualSkin.ToString());
			PlayerPrefs.Save ();
        }
    }

    private static Skins GetSkinFrom(string s) {
        if (s.Equals(Skins.Cocodrilos.ToString())) return Skins.Cocodrilos;
        else if (s.Equals(Skins.Aliens.ToString())) return Skins.Aliens;
        else if (s.Equals(Skins.Panchos.ToString())) return Skins.Panchos;
        else if (s.Equals(Skins.Dragones.ToString())) return Skins.Dragones;
        else if (s.Equals(Skins.Retro.ToString())) return Skins.Retro;
        else if (s.Equals(Skins.Topos.ToString())) return Skins.Topos;
        else if (s.Equals(Skins.Fingers.ToString())) return Skins.Fingers;
        else if (s.Equals(Skins.Jelly.ToString())) return Skins.Jelly;
        else return Skins.NotDefined;
    }

    public static bool IsOwned(Skins skin) {

        if (skin == Skins.Cocodrilos)
        { if (PlayerPrefs.GetInt("Skin-" + skin.ToString(), 1) == 1) return true; }
        else
            if (PlayerPrefs.GetInt("Skin-" + skin.ToString(), 0) == 1) return true;

        return false;
    }

    public static void BuySkin(Skins skin) {
        Debug.Log(skin.ToString());
        PlayerPrefs.SetInt("Skin-" + skin.ToString(), 1);
        PlayerPrefs.SetInt("SkinFree",0);
		PlayerPrefs.Save ();
    }

    public static bool SkinFree {
        get { return !PlayerPrefs.HasKey("SkinFree"); }
    }
}
