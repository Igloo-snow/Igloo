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
    CutsceneDD,
    TutorialHome,
    TutorialWorldMap,
    CutsceneDM,
    CutsceneLx,
    Linux
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
                return "명신관";
            case 4:
                return "던전";
            case 5:
                return "디지털 논리회로 던전";
            case 6:
                return "컴퓨터 수학 던전";
            case 7:
                return "디지털 논리회로 던전";
            case 8:
                return "집";
            case 9:
                return "월드맵";
            case 10:
                return "컴퓨터 수학 던전";
            case 11:
                return "리눅스 던전";
            case 12:
                return "리눅스 던전";
            default:
                return "";
        }
    }
}

