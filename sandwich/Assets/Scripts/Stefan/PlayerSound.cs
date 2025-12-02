using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private ThirdPersonMovement _playerMovement;
    [SerializeField] private PlayerIngredients _playerIngredients;
    [SerializeField] private AudioClip _dashSound;
    [SerializeField] private List<AudioClip> _walkSound;
    [SerializeField] private AudioClip _pickupIngredientSound;
    private Coroutine _walkingCoroutine;
    private bool _isWalking;

    void OnEnable()
    {
        _playerMovement.OnDash += DashSound;
        _playerMovement.OnMoveChange += WalkSound;
        _playerIngredients.PickupIngredientEvent += PickupSound;
    }

    void OnDisable()
    {
        _playerMovement.OnDash -= DashSound;
        _playerMovement.OnMoveChange -= WalkSound;
        _playerIngredients.PickupIngredientEvent -= PickupSound;
    }

    private void WalkSound(bool isWalking)
    {
        _isWalking = isWalking;
        if (isWalking)
        {
            if (_walkingCoroutine == null)
            {
                _walkingCoroutine = StartCoroutine(RepeatWalkSound());
            }
        }
    }

    private IEnumerator RepeatWalkSound()
    {
        while (_isWalking)
        {
            SoundManager.Instance.PlaySound(_walkSound[Random.Range(0,_walkSound.Count)]);
            yield return new WaitForSeconds(0.3f);
        }
        _walkingCoroutine = null;
    }

    private void DashSound(bool play)
    {
        SoundManager.Instance.PlaySound(_dashSound);
    }

    private void PickupSound()
    {
        SoundManager.Instance.PlaySound(_pickupIngredientSound);
    }
}