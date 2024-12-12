using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	[SerializeField] string _nameKnowledge;
	[SerializeField] Transform _target;
	public Transform attackCheck;
	public float attackCheckRadius;
	[SerializeField] float _attackRange;
	float _attackRangeReal;

    NavMeshAgent _agent;
	Animator _anim;

	bool isAttacking = false;

	[SerializeField] int _live;
	bool isDeath = false;

	//fx
	FXController _fx;

	float _speedDefault = 0;

	// Start is called before the first frame update
	void Start()
	{
		_fx = GetComponent<FXController>();
		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		_agent.updateUpAxis = false;
		_target = PlayerController.instance.transform;

		_anim = GetComponent<Animator>();
		_speedDefault = _agent.speed;


	}

	public void Init(Vector2 pos, string nameKnowleage)
	{
		this.transform.position = pos;
		this._nameKnowledge = nameKnowleage;
		isAttacking = false;
		_live = 3;
		isDeath = false;
        _attackRangeReal = _attackRange;
        this.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
	{
		if (GameManager.instance._isPause)
		{
			_agent.speed = 0;
            return;
		}
		_agent.speed = _speedDefault;

		ProcessAnim();

        if (isDeath)
			return;

		_agent.SetDestination(_target.position);
		ProcessMovement();

	}

	void ProcessMovement()
	{
		if (_agent.velocity.x * this.transform.localScale.x > 0)
		{
			Flip();
		}
		if (Vector2.Distance(this.transform.position, _target.position) <= _attackRangeReal)
		{
			isAttacking = true;
			_agent.velocity = Vector3.zero;
		}
		else
		{
			isAttacking = false;
		}
	}

	void ProcessAnim()
	{
		_anim.SetBool("Attack", isAttacking);
		_anim.SetBool("Death", isDeath);
	}

	void Flip()
	{
		this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
	}


	public void Damage()
	{
		Debug.Log("Chuot bi dam");
		_live--;
		_fx.StartCoroutine("FlashFX");

		if (_live <= 0)
		{
			Death();
		}
	}

	public void AttackTrigger()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

		foreach (var hit in colliders)
		{
			if (hit.GetComponent<PlayerController>() != null)
				hit.GetComponent<PlayerController>().Damage();
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
	}



	void Death()
	{
		PlayerKnowledge.instance.AddeKnowledge(_nameKnowledge);
		this.gameObject.SetActive(false);
	}
}