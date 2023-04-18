using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private float _magneticRange = 2f, _magneticPower = 2f;

    [SerializeField]
    private LayerMask _whatIsItem;

    private List<ItemScript> _collectList = new List<ItemScript>();

    private void FixedUpdate()
    {
        Collider2D[] resources = Physics2D.OverlapCircleAll(transform.position, _magneticRange, _whatIsItem);

        foreach (Collider2D r in resources)
        {
            if (r.TryGetComponent<ItemScript>(out ItemScript item))
            {
                _collectList.Add(item);
                item.gameObject.layer = 0;
            }
        }
        for (int i = 0; i < _collectList.Count; i++)
        {
            ItemScript item = _collectList[i];

            Vector2 dir = (transform.position - item.transform.position).normalized;
            item.transform.Translate(dir * _magneticPower * Time.fixedDeltaTime);

            if (Vector2.Distance(transform.position, item.transform.position) < 0.1f)
            {
                int value = item.ItemData.GetAmount();

                PopupText text = PoolManager.Instance.Pop("PopupText") as PopupText;
                text.SetUp(value.ToString(), transform.position + new Vector3(0, 0.5f, 0), item.ItemData.PopupTextColor);

                ProcessItem(item.ItemData._itemType, value);
                item.PickUpResource(); //아이템 줍기
                _collectList.RemoveAt(i);
                i--;
            }
        }
    }


    public UnityEvent<int> OnAmmoAdded = null;
    public UnityEvent<int> OnHealthAdded = null;
    private void ProcessItem(ItemType type, int value)
    {
        switch (type)
        {
            case ItemType.Ammo:
                OnAmmoAdded?.Invoke(value);
                break;
            case ItemType.Health:
                OnHealthAdded?.Invoke(value);
                break;
        }
    }
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject) //오브젝트 클릭시 표시
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _magneticRange);
            Gizmos.color = Color.white;
        }
    }
}
