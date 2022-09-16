using UnityEngine;

public class Player : MonoBehaviour, IPauseHandler
{
    private GameData _gameData;

    [SerializeField] private Joystick _joystick;
    private float _joystickValueLimit;

    [SerializeField] private float _normalSpeed;
    private float _speed;
    private float _strength;
    private float _charisma;

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _hand;

    #region States
    private StateMachine _stateMachine;
    private Idle _idle;
    private Run _run;
    private Hold _hold;
    private Carry _carry;
    #endregion

    private Product _product;
    private Check _check;

    private bool _isPaused;

    private void Start()
    {        
        _joystickValueLimit = 0.05f;
        _speed = _normalSpeed;

        _gameData = GameData.gameData;
        Stats myStats = _gameData.PlayerStats;
        _strength = myStats.Strength;
        _charisma = myStats.Charisma;

        _stateMachine = new StateMachine();
        _idle = new Idle(_animator);
        _run = new Run(_animator);
        _carry = new Carry(_animator);
        _hold = new Hold(_animator);
        _stateMachine.Init(_idle);

        _isPaused = PauseManager.pauseManager.IsPaused;
        PauseManager.pauseManager.Register(this);
    }

    private void Update()
    {
        if (_isPaused != true)
        {
            if (Mathf.Abs(_joystick.Horizontal) >= _joystickValueLimit || Mathf.Abs(_joystick.Vertical) >= _joystickValueLimit)
            {
                Move();
            }
            else if (_product == null && _stateMachine.CurrentState != _idle)
            {
                _stateMachine.ChangeState(_idle);
            }
            else if (_product != null && _stateMachine.CurrentState != _hold)
            {
                _stateMachine.ChangeState(_hold);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out OrderTable orderTable) == true)
        {
            switch (orderTable.Order)
            {
                case OrderStatus.HasProduct:
                    TakeProduct(orderTable.GiveProduct());
                    break;
                case OrderStatus.WaitCheck:
                    if (_check != null)
                    {
                        orderTable.TakeCheck(_check);
                        _product = null;
                        _check = null;
                    }
                    break;
                default: 
                    break;
            }
        }

        if (_product != null && other.TryGetComponent(out PostBox postBox) == true)
        {
            CreateCheck(postBox.BoxType);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out OrderTable orderTable) == true)
        {
            if (_product == null && orderTable.Order == OrderStatus.HasProduct)
            {
                TakeProduct(orderTable.GiveProduct());
            }
        }
    }

    private void Move()
    {
        if (_product == null && _stateMachine.CurrentState != _run)
        {
            _stateMachine.ChangeState(_run);           
        }
        else if (_product != null && _stateMachine.CurrentState != _carry)
        {
            _stateMachine.ChangeState(_carry);          
        }

        transform.position += new Vector3(_joystick.Horizontal, 0.0f, _joystick.Vertical) * _speed * Time.deltaTime;
        transform.LookAt(Vector3.forward * _joystick.Vertical * 180 + Vector3.right * _joystick.Horizontal * 180);
    }

    private void TakeProduct(Product product)
    {
        if(product != null)
        {
            _product = product;
            _product.gameObject.transform.SetParent(_hand);
            _product.ToHand();

            switch (_product.Weight)
            {
                case WeightType.Light:
                    _speed = _normalSpeed - (1 - (_strength/10) + 0.4f);
                    break;
                case WeightType.Medium:
                    _speed = _normalSpeed - (1 - (_strength / 10) + 1.2f);
                    break;
                case WeightType.Heavy:
                    _speed = _normalSpeed - (1 - (_strength / 10) + 2.0f);
                    break;
                default:
                    _speed = _normalSpeed - (1 - (_strength / 10) + 0.4f);
                    break;
            }
        }
    }

    private void CreateCheck(ProductType boxType)
    {
        _speed = _normalSpeed;
        Check check = new Check(boxType, _product.type, _product.price, _charisma);
        _check = check;
        Destroy(_product.gameObject);
    }

    public void SetPause(bool isPaused)
    {
        _isPaused = isPaused;
        _stateMachine.ChangeState(_idle);
    }
}
