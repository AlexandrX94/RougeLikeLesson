using GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSystem : MonoBehaviour, IActivate
{

    public Action OnLevelChanged;
    [SerializeField] private List<Level> _levels = new List<Level>();
    private GameTimer _gameTimer;
    private DiContainer _diContainer;


    private void Awake()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            _diContainer.Inject(_levels[i]);
        }

    }

    private void Start()
    {
        Activate();
    }

    private void LevelUp()
    {
        _levels[_gameTimer.Minutes - 1].Deactivate();
        Activate();
    }

    private void OnEnable()
    {
        OnLevelChanged += LevelUp;
    }

    private void OnDisable()
    {
        OnLevelChanged -= LevelUp;
    }

    public void Activate()
    {
        _levels[_gameTimer.Minutes].Activate();
    }

    public void Deactivate()
    {
        _levels[_gameTimer.Minutes].Deactivate();
    }

    [Inject] private void Construct(GameTimer gameTimer, DiContainer diContainer)
    {  
        _gameTimer = gameTimer; 
        _diContainer = diContainer;
    }

}
