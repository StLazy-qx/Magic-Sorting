using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StoreScrollViewFiller : MonoBehaviour
{
    [SerializeField] private Store _store;
    [SerializeField] private ScrollRect _scrollRect;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => _store.IsInitialized);
        yield return new WaitForEndOfFrame();

        IReadOnlyList<ItemSO> items = _store.GetItemsSO();

        foreach (ItemSO storeItem in items)
        {
            ItemView itemView = Instantiate(storeItem.ItemView, _scrollRect.content);

            itemView.Initialize(storeItem);
        }

        Canvas.ForceUpdateCanvases();
        _scrollRect.verticalNormalizedPosition = 1;
    }
}
