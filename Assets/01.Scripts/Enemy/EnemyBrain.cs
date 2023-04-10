using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged; //마우스방향전환을 

    //공격 은 어짜피 오늘 못하니까 일단 쌩까고


    public Transform BasePosition; //이게 거리측정을 몬스터의 바닥에서 

    public AIState CurrentState;

    private EnemyRenderer _enemyRenderer;

    [SerializeField]
    private bool _isActive = false;

    private void Awake()
    {
        _enemyRenderer = transform.Find("VisualSprite").GetComponent<EnemyRenderer>();
    }
    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        CurrentState?.SetUp(transform);
    }

    public void ChangeState(AIState nextState)
    {
        CurrentState = nextState;
        CurrentState?.SetUp(transform); //이 부분은 최적화 필요해
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            ShowEnemy();
        }
        if (_isActive == false) return;//업데이트 수행 안함
        if (Target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            CurrentState.UpdateState(); //현재 상태를 갱신한다.
        }
    }

    public void ShowEnemy()
    {
        _isActive = false;
        _enemyRenderer.ShowProgress(2f, () => _isActive = true);
    }

    public void Move(Vector2 moveDirection, Vector2 targetPosition)
    {
        OnMovementKeyPress?.Invoke(moveDirection);
        OnPointerPositionChanged?.Invoke(targetPosition);
    }

    public override void Reset()
    {
        _isActive = false;
    }
}
