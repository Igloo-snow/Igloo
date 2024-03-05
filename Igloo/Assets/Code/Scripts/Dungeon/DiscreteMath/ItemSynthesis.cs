using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ItemSynthesis : Interactable
{
    private Animator anim;

    private List<ItemRecipeSO> recipeList = new List<ItemRecipeSO>();
    [SerializeField] private Transform spawnPoint;
    private bool done = false;



    private void Awake()
    {
        CreateRecipeList();
        anim = GetComponent<Animator>();
    }

    private void CreateRecipeList()
    {
        ItemRecipeSO[] allRecipes = Resources.LoadAll<ItemRecipeSO>("Items");

        foreach (ItemRecipeSO recipe in allRecipes)
        {
            if (recipeList.Contains(recipe))
            {
                Debug.Log("중복 : " + recipe.id);
            }
            recipeList.Add(recipe);
        }
    }

    protected override void Interact()
    {
        done = false;

        foreach(ItemRecipeSO recipe in recipeList)
        {
            bool achived = true;
            foreach(Item item in recipe.requirements)
            {
                if (!InventoryManager.Instance.CheckInventory(item.id))
                {
                    achived = false;
                }
            }
            if(achived)
            {
                anim.SetTrigger("ItemSpawn");
                Instantiate(recipe.resultItem, spawnPoint.position, Quaternion.identity, spawnPoint.transform);
                for(int i = 0; i < recipe.requirements.Length; i++)
                {
                    InventoryManager.Instance.Remove(recipe.requirements[i]);
                    
                }
                done = true;
            }
        }
        if (!done)
            promptMessage = "합성 가능한 아이템 없음";

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            promptMessage = "아이템 합성";

        }
    }

}
