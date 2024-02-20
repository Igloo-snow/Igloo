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
    DiscreteMath
}

public class SceneName : MonoBehaviour
{
    public string SwitchName(SceneNames name)
    {
        switch ((int)name)
        {
            case 0:
                return "���� �޴�";
            case 1:
                return "��";
            case 2:
                return "�����";
            case 3:
                return "�б�";
            case 4:
            case 5:
                return "";
            case 6:
                return "����";
            case 7:
                return "��� ����";
            case 8:
                return "�ļ� ����";
            default:
                return "";
        }
    }
}

