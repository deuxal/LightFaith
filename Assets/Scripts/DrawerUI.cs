using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DrawerUI : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<int> currentItems;
    [SerializeField] List<GameObject> slots;

    System.Action<int> onItemUsed;
    // Start is called before the first frame update
    void OnEnable()
    {
        InventorySystem invS = FindObjectOfType<InventorySystem>();
        Debug.Log("H", this.gameObject);
        for (int i = 0; i < slots.Count; i++)
        {
            if (currentItems.Count > i && currentItems[i] >= 0)
            {
                Debug.Log(currentItems[i]);
                int currentID = i;
                slots[i].SetActive(true);
                slots[i].GetComponent<Image>().sprite = sprites[currentItems[i]];
                slots[i].GetComponent<Button>().onClick.RemoveAllListeners();
                slots[i].GetComponent<Button>().onClick.AddListener(() => { invS.GetItem(currentItems[currentID]); });
                slots[i].GetComponent<Button>().onClick.AddListener(() => { onItemUsed(currentID); });
                slots[i].GetComponent<Button>().onClick.AddListener(() => { slots[currentID].SetActive(false); });
                
            }
            else
            {
                slots[i].SetActive(false);
            }
        }
    }


    public void Setup(List<int> items, System.Action<int> ev)
    {
        currentItems = items;
        onItemUsed = ev;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
