using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    [SerializeField]
    private HeartUI _heartUIPrefab;

    [SerializeField]
    private Sprite _fullHeart, _emptyHeart;

    private List<HeartUI> _childHearts = null;

    public void InitSetup(int count)
    {
        _childHearts = new List<HeartUI>();

        for (int i = 0; i < count; i++)
        {
            HeartUI heart = Instantiate<HeartUI>(_heartUIPrefab, transform);
            _childHearts.Add(heart);
        }
    }
    public void ChangeHeartUI(int current, int max)
    {
        for (int i = 0; i < _childHearts.Count; i++)
        {
            _childHearts[i].SetSprite(i < current ? _fullHeart : _emptyHeart);
        }
    }
}
