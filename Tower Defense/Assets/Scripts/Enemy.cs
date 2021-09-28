using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private SpriteRenderer _healthBar;
    [SerializeField] private SpriteRenderer _healthFill;

    public Vector3 TargetPosition
    {
        get; private set;
    }

    public int CurrentPathIndex
    {
        get; private set;
    }

    private int _currentHealth;

    private void OnEnable()
    {
        _currentHealth = _maxHealth;
        _healthFill.size = _healthBar.size;
    }

    public void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _moveSpeed * Time.deltaTime);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        TargetPosition = targetPosition;
        _healthBar.transform.parent = null;

        Vector3 distance = targetPosition - transform.position;
        if(Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
        {
            if(distance.y > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
            }
        }
        else
        {
            if(distance.x > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
            _healthBar.transform.parent = transform;
        }
    }

    public void ReduceEnemyHealth(int damage)
    {
        _currentHealth -= damage;
        AudioPlayer.Instance.PlaySFX("hit-enemy");

        if(_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            AudioPlayer.Instance.PlaySFX("enemy-die");
        }
    }

    public void SetCurrentPathIndex(int currentIndex)
    {
        CurrentPathIndex = currentIndex;
    }

}