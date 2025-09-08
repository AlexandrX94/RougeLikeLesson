using Player;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.IU
{
    public class HealthUIUpdate : MonoBehaviour
    {
        [SerializeField] private Image _playerHealthImage;
        private PlayerHealth _playerHealth;
       

        private void OnEnable()
        {
            _playerHealth.OnHealthChange += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _playerHealth.OnHealthChange -= UpdateHealthBar;
        }

        [Inject] private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;

        }

        private void UpdateHealthBar()
        {
            _playerHealthImage.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
            _playerHealthImage.fillAmount = Mathf.Clamp01(_playerHealthImage.fillAmount);

        }

     
    }
}

