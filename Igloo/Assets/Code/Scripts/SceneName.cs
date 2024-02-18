using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNames
{
    MainMenu,
    Home,
    WorldMap,
    Univ,
    TempHome,
    YR,
    Dungeon = 6,
    DigitalDesign,
    DiscreteMath
}

public class SceneName : MonoBehaviour
{
    public string SwitchName(SceneNames name)
    {
        switch ((int)name)
        {
            case 0:
                return "메인 메뉴";
            case 1:
                return "집";
            case 2:
                return "월드맵";
            case 3:
                return "학교";
            case 4:
            case 5:
                return "";
            case 6:
                return "던전";
            case 7:
                return "디논 던전";
            case 8:
                return "컴수 던전";
            default:
                return "";
        }
    }
}

