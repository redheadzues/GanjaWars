using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent (typeof(ArmoryPlayer))]
[RequireComponent(typeof(PlayerSkills))]
public abstract class PlayerControler : MonoBehaviour
{
    [SerializeField] protected float _takeDistance;

    public event UnityAction<float, float> CurrentThrowForceChanged;
    public event UnityAction<bool> IncreaseThrowForceActivated;
    public event UnityAction MenuOpened;
    public event UnityAction MainMenuOpened;

    public GameObject TakenObject => _takenObject;
    
    protected PlayerInput Controler;
    protected bool _isTaken = false;

    private GameObject _takenObject;
    private Player _player;
    private PlayerSkills _skills;
    private ArmoryPlayer _armory;
    private float _speed;
    private float _maxThrowForce;
    private float _currentThrowForce;
    private Vector2 _lastDirection;
    private Vector2 _takeDirection;
    private Coroutine _coroutine;
    private float delayTime = 0.2f;
    private int _stepsCount = 10;
    public Vector2 Direction { get; protected set; }

    private void Start()
    {
        _player = GetComponent<Player>();
        _armory = GetComponent<ArmoryPlayer>();
        _skills = GetComponent<PlayerSkills>();
    }

    private void OnEnable()
    {
        Controler.Enable();
    }

    private void OnDisable()
    {
        Controler.Disable();
    }

    private void Update()
    {
        ReadCurrentSkillsValue();
        SetDirections();
        Move(Direction);
    }

    protected abstract void SetMoveDirection();

    protected void ChooseAction()
    {
        if (_isTaken == true)
            Throw(true);
        else
            TryPickUp();
    }

    protected void Throw(bool isDrop = false)
    {
        _takenObject.transform.parent = null;
        _isTaken = false;

        if (isDrop == false)
        {
            var takeable = _takenObject.GetComponent<TakeableObject>();
            float targetPointX = _takenObject.transform.position.x + _lastDirection.x * _currentThrowForce;
            float targetPointY = _takenObject.transform.position.y + _lastDirection.y * _currentThrowForce;
            Vector3 targetPoint = new Vector3(targetPointX, targetPointY, 0);

            takeable.SetTarget(_player.Target);
            takeable.Throw(targetPoint);
        }

        StopCoroutine(OnIncreaseThrowForce());
        IncreaseThrowForceActivated?.Invoke(false);
        _takenObject = null;
    }

    protected void IncreaseThrowForce()
    {
        if(_isTaken == true)
        {
            _currentThrowForce = 0;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(OnIncreaseThrowForce());

            IncreaseThrowForceActivated?.Invoke(true);
        }
    }

    protected void OpenMenu()
    {
        MenuOpened?.Invoke();
    }

    protected void OpenMainMenu()
    {
        MainMenuOpened?.Invoke();
    }

    protected void NextWeapon()
    {
        _armory.NextWeapon();
    }

    protected void UseWeapon()
    {
        _armory.UseWeapon(_player.Target);
    }

    private void Move(Vector2 direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0) * _speed * Time.deltaTime;
    }

    private void TryPickUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _takeDirection, _takeDistance);

        if (hit && hit.collider.TryGetComponent(out TakeableObject takeable))
        {
            _isTaken = true;
            _takenObject = hit.collider.gameObject;

            _takenObject.transform.SetParent(transform, worldPositionStays: false);
        }
    }

    private void ReadCurrentSkillsValue()
    {
        _maxThrowForce = _skills.CurrentStrenght;
        _speed = _skills.CurrentMoveSpeed;
    }

    private void SetDirections()
    {
        SetMoveDirection();

        if (Direction != Vector2.zero)
            _lastDirection = Direction;

        if (Direction.x < 0)
            _takeDirection = Vector2.left;
        else if (Direction.x > 0)
            _takeDirection = Vector2.right;
    }

    private IEnumerator OnIncreaseThrowForce()
    {
        var waitingTime = new WaitForSeconds(delayTime);

        while (_currentThrowForce < _maxThrowForce)
        {
            _currentThrowForce += _maxThrowForce/_stepsCount;
            CurrentThrowForceChanged?.Invoke(_currentThrowForce, _maxThrowForce);

            yield return waitingTime;
        }
    }
}
