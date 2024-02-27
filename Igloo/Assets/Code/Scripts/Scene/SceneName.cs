using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneNames
{
    MainMenu,
    Home,
    WorldMap,
    Univ,
    Dungeon,
    DigitalDesign,
    DiscreteMath,
    TutorialHome,
    TutorialWorldMap
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
                return "던전";
            case 5:
                return "디지털 논리회로 던전";
            case 6:
                return "컴퓨터 수학 던전";
            case 7:
                return "집";
            case 8:
                return "월드맵";
            default:
                return "";
        }
    }
}

