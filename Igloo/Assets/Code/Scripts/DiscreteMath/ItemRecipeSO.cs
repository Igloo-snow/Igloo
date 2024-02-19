using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemRecipeSO", menuName = "ScriptableObjects/ItemRecipeSO", order = 2)]
public class ItemRecipeSO : ScriptableObject
{
    [field: SerializeField]
    public string id { get; private set; }

    public Item[] requirements;
    public GameObject resultItem;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
}
