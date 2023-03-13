using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchAnimation : MonoBehaviour
{
    private Light2D _light;
    private float _baseRadius;
    private float _baseIntensity;
    private int _toggle = 1;

    [SerializeField]
    private float _radiusRandomness = 1f;
    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _baseRadius = _light.pointLightOuterRadius;
    }
    private void Start()
    {
        StartShake();
    }
    private void StartShake()
    {
        float targetRadius = _baseRadius + _toggle * Random.Range(0, _radiusRandomness);
        float targetIntensity = _baseIntensity + _toggle * Random.Range(0, _radiusRandomness * 0.5f);
        _toggle *= -1;

        Sequence seq = DOTween.Sequence();

        //_light.pointLightInnerRadius

        transform.DOScale(10, 0.5f);

        var t1 = DOTween.To(() => _light.intensity,
            value => _light.intensity = value,
            targetIntensity,
            Random.Range(0.9f, 1f));

        var t2 = DOTween.To(
            () => _light.pointLightOuterRadius
            , value => _light.pointLightOuterRadius = value,
            targetRadius,
            Random.Range(0.9f, 1f));

        var t3 = DOTween.To(
            () => _light.pointLightInnerRadius
            , value => _light.pointLightInnerRadius= value,
            targetRadius,
            Random.Range(0.9f, 1f));

        seq.Append(t1);
        seq.Join(t2);
        //seq.Join(t3);
        seq.AppendCallback(() => StartShake());

    }
}
