using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GGM : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private Vector2 _destination;
    private Camera _mainCam;
    private Tween t = null;
    private SpriteRenderer _sr;
    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = new Color(1f, 1f, 1f, 0);
    }
    void Start()
    {
        _mainCam = Camera.main;
        _destination = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sequence seq = DOTween.Sequence();
            _destination = _mainCam.ScreenToWorldPoint(Input.mousePosition);


            seq.Prepend(_sr.DOFade(1f, 1f));
            seq.Append(transform.DOMove(_destination, 1f).SetEase(Ease.InBounce));
            seq.Join(transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360));
            seq.AppendInterval(1f);
            seq.AppendCallback(() =>
            {
                Debug.Log("완료");
            });
            //시퀀스는 스타트가 없고, 등록하면 다음 프레임에서 바로 실행된다.

        }
        //MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 dir = (Vector3)_destination - transform.position;
        if (dir.magnitude > 0.1f)
        {
            transform.Translate(dir.normalized * Time.deltaTime * _speed);
        }
    }
}
