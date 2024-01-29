using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPage : MonoBehaviour
{

    public void ClosePage()
    {
        this.gameObject.SetActive(false);
        QuestPageButton.pageActive = false;
    }
}
