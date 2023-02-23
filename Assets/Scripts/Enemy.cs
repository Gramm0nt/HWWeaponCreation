using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string DefeatName = "Defeat";
    private const string HurtName = "Hurt";

    [SerializeField] private int _health;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.Play(HurtName);

        if (_health <= 0)
        {
            _animator.Play(DefeatName);
        }
    }
}