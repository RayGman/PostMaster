using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Client : MonoBehaviour, IPauseHandler
{
	private Animator _animator;

	private ProductData _productData;
	private OrderTable _orderTable;
	private ToRoad _toRoad;

	private Vector3 _target;
	private float _speed;
	private bool _isPaused;

	#region States
	private StateMachine _stateMachine;
	private Idle _idle;
	private Walk _walk;
	#endregion

    private void Update()
	{
        if (_isPaused != true)
        {
			if (_stateMachine.CurrentState == _walk)
			{
				float step = _speed * Time.deltaTime;
				_target = new Vector3(_target.x, transform.position.y, _target.z);
				transform.position = Vector3.MoveTowards(transform.position, _target, step);

				if (Vector3.Distance(transform.position, _target) < 0.01f)
				{
					_stateMachine.ChangeState(_idle);
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out OrderTable orderTable) == true)
		{
			_orderTable = orderTable;		
			_orderTable.OrderComplited.AddListener(MyOrderComplited);
			_orderTable.TakeProduct(_productData);
		}

		if (other.TryGetComponent(out ToRoad toRoad) == true)
        {
			_toRoad = toRoad;
		}
	}

	public void Init(ClientData data)
	{
		_productData = data.Product;
		_speed = 1.0f;
		_animator = GetComponent<Animator>();
		_stateMachine = new StateMachine();
		_idle = new Idle(_animator);
		_walk = new Walk(_animator);
		_stateMachine.Init(_idle);

		_isPaused = PauseManager.pauseManager.IsPaused;
		PauseManager.pauseManager.Register(this);
	}

	public void SetPause(bool isPaused)
	{
		_isPaused = isPaused;

		if (isPaused == false)
		{
			if (Vector3.Distance(transform.position, _target) < 0.01f)
			{
				_stateMachine.ChangeState(_idle);
			}
            else
            {
				RotateTo(_target);
				_stateMachine.ChangeState(_walk);
			}
		}
		else
		{
			_stateMachine.ChangeState(_idle);
		}
	}

	#region Moving
	public void GoTo(Transform to)
	{
		_target = to.position;
		RotateTo(_target);
		_stateMachine.ChangeState(_walk);
	}
	
	private void RotateTo(Vector3 target)
	{
		Vector3 direction = (target - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 1f);
	}
    #endregion

	private void MyOrderComplited(float cash)
	{
		_orderTable.OrderComplited.RemoveListener(MyOrderComplited);
		if (_toRoad != null)
		{
			GoTo(_toRoad.WhereNext);
			_toRoad = null;
		}
	}

    private void OnDestroy()
    {
		PauseManager.pauseManager.UnRegister(this);
	}
}
