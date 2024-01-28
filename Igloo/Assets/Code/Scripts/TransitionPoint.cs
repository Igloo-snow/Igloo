using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TransitionPoint : MonoBehaviour
{
    [Tooltip("이동하려는 씬 이름과 동일해야함")]
    public string nextScene;

}
