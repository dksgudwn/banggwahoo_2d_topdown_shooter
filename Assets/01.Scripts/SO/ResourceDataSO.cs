using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Items/ResourcesData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate;
    public GameObject ItemPrefab;

    public ItemType _itemType;

    [SerializeField]
    private int _minAmount = 1, _maxAmount = 5;

    public Color PopupTextColor;
    public AudioClip UseSound;
    public int GetAmount()
    {
        return Random.Range(_minAmount, _maxAmount);
    }
}
