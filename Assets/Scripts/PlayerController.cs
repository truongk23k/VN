using Cinemachine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
	[SerializeField] float _speed;
	public Vector2 newPos;

	Vector2 _input;
	Vector2 _lastMoveDir;
	Vector2 _dirSave;

	[SerializeField] bool _isAttack = false;
	[SerializeField] public int _live = 3;
	bool _isDeath = false;

	public bool isBusy;

	[Header("Components")]
	Animator _anim;
	Rigidbody2D _rb;

	[Header("Attack info")]
	[SerializeField] List<Vector2> posAttack;
	public Transform attackCheck;
	public float attackCheckRadius;

	public GameObject currentItem = null;
	int itemIndex = 0;

	//fx
	FXController _fx;


	// Start is called before the first frame update
	void Start()
	{
		DontDestroyOnLoad(this);
		_anim = GetComponent<Animator>();
		_rb = GetComponent<Rigidbody2D>();
		_fx = GetComponent<FXController>();

		/*this.gameObject.SetActive(false);*/

		_lastMoveDir = new Vector2(0, -1);
	}

	public void Init(Vector2 spawnPos)
	{
		this.gameObject.SetActive(true);
		this.GetComponent<FXController>().SetOriginMat();
		this.transform.position = spawnPos;
		this.transform.localScale = Vector3.one;

		currentItem = null;
		_live = 3;
		_isDeath = false;
		isBusy = false;
		currentItem = null;
		itemIndex = 0;
		SetPosPlayer();

	}
    public  void SetPosPlayer()
    {
        CamController.instance.GetComponent<CinemachineVirtualCamera>().Follow = PlayerController.instance.gameObject.transform;
        
    }

    public void InitMoveScene()
	{
		this.gameObject.SetActive(true);
		this.GetComponent<FXController>().SetOriginMat();
		this.transform.position = newPos;
		this.transform.localScale = Vector3.one;

	}

	public void ColorOK()
	{
		this.GetComponent<SpriteRenderer>().color = new Color(this.GetComponent<SpriteRenderer>().color.r, this.GetComponent<SpriteRenderer>().color.g, this.GetComponent<SpriteRenderer>().color.b, 255f);
	}

	public void LoadingScene()
	{
		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (GameManager.instance._isPause)
			return;

		ProcessInputs();
		ProcessAnim();

	}


	private void FixedUpdate()
	{
		if (_isAttack)
			_rb.velocity = new Vector2(0, 0);
		else
			_rb.velocity = _speed * _input;
	}

	void ProcessInputs()
	{
		if (isBusy)
		{
			_input = new Vector2(0, 0);
			return;
		}

		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");

		if ((moveX == 0 && moveY == 0) && (_input.x != 0 || _input.y != 0))
		{
			_lastMoveDir = _input;
		}

		_input.x = Input.GetAxisRaw("Horizontal");
		_input.y = Input.GetAxisRaw("Vertical");
		if (_input.x * this.transform.localScale.x < 0)
		{
			Flip();
		}

		if (_input.x == 0 && _input.y == 0)
			_dirSave = _lastMoveDir;
		else
			_dirSave = new Vector2(_input.x, _input.y);

		_input.Normalize();

		if (Input.GetMouseButtonDown(1) && itemIndex != 0)
			_isAttack = true;


		if (Input.GetKeyDown(KeyCode.G))
			DropCurrentItem();

	}


	/*	void ProcessInteract()
		{
			interactRadius = Mathf.Abs(_col.radius * this.transform.localScale.x) / 2 + Mathf.Abs(this.transform.localScale.x) * 0.4f;

			Vector2 direction = _input.normalized;
			if (direction == Vector2.zero)
			{
				direction = _lastMoveDir;
			}
			interactPosition = (Vector2)this.transform.position + direction * interactRadius;


			Collider2D[] colliders = Physics2D.OverlapCircleAll(interactPosition, interactRadius);

			foreach (Collider2D collider in colliders)
			{
				IInteractable interactable = collider.GetComponent<IInteractable>();
				if (interactable != null)
				{
					_interactable = interactable;
					_interactable.ShowCanBeInteract();
					Debug.Log("Có th? týõng tác");

					if (Input.GetKeyDown(KeyCode.E))
					{
						if (_currentItem != null && ((_interactable is FireExtinguisher) || (_interactable is GunBase)))
						{
							DropCurrentItem();
						}

						_interactable.Interact();

						if ((_interactable is FireExtinguisher) || (_interactable is GunBase))
						{
							_currentItem = collider.gameObject;
							_currentItem.SetActive(false);
						}
					}
					break;
				}
			}
		}*/


	public void DropCurrentItem()
	{
		if (currentItem != null)
		{
			currentItem.SetActive(true);
			currentItem.transform.position = this.transform.position;

			currentItem = null;
			itemIndex = 0;
		}
	}


	void Flip()
	{
		this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
	}

	void ProcessAnim()
	{
		if (_input.x == 0 && _input.y == 0)
		{
			_anim.SetFloat("MoveX", _lastMoveDir.x);
			_anim.SetFloat("MoveY", _lastMoveDir.y);
		}
		else
		{
			_anim.SetFloat("MoveX", _input.x);
			_anim.SetFloat("MoveY", _input.y);
		}

		_anim.SetFloat("MoveMag", _input.magnitude);
		_anim.SetFloat("LastMoveX", _lastMoveDir.x);
		_anim.SetFloat("LastMoveY", _lastMoveDir.y);

		_anim.SetFloat("ItemIndex", itemIndex);
		_anim.SetBool("Attack", _isAttack);
		_anim.SetFloat("DirSaveX", _dirSave.x);
		_anim.SetFloat("DirSaveY", _dirSave.y);

	}

	public void TriggerExitAttack()
	{
		_isAttack = false;
	}

	public void TriggerUpdatePosAttack()
	{
		if (_dirSave == new Vector2(1, 0))
			attackCheck.localPosition = posAttack[0];
		else if (_dirSave == new Vector2(-1, 0))
			attackCheck.localPosition = posAttack[1];
		else if (_dirSave == new Vector2(0, 1))
			attackCheck.localPosition = posAttack[2];
		else
			attackCheck.localPosition = posAttack[3];
	}

	public void AttackTrigger()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

		foreach (var hit in colliders)
		{
			if (hit.GetComponent<EnemyController>() != null)
				hit.GetComponent<EnemyController>().Damage();
		}
	}

	public void Damage()
	{
		Debug.Log("Nguoi choi bi dam");
		_live--;
		_fx.StartCoroutine("FlashFX");

		if (_live <= 0)
		{
			Death();
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
	}

	public void GetItem(GameObject g, int index)
	{
		currentItem = g;
		itemIndex = index;
		g.gameObject.SetActive(false);
	}

	public void Death()
	{
		if (_isDeath)
			return;
		_isDeath = true;
		GameManager.instance.ChangeState(GameManager.GAME_STATE.MENU);
		this.gameObject.SetActive(false);
	}
}
