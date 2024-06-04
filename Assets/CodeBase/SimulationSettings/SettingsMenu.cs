using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider _fieldSizeSlider;
    [SerializeField] private TMP_Text _fieldSizeText;
    [SerializeField] private Slider _animalsCountSlider;
    [SerializeField] private TMP_Text _animalsCountText;
    [SerializeField] private Slider _animalsSpeedSlider;
    [SerializeField] private TMP_Text _animalsSpeedText;
    [SerializeField] private Button _startSimulationButton;


    private void Awake()
    {
        Subscribe();
        SetStartValues();
    }

    private void Subscribe()
    {
        _fieldSizeSlider.onValueChanged.AddListener(ChangeFieldSize);
        _animalsCountSlider.onValueChanged.AddListener(ChangeAnimalsCount);
        _animalsSpeedSlider.onValueChanged.AddListener(ChangeAnimalsSpeed);
        _startSimulationButton.onClick.AddListener(StartSimulation);
    }

    private void StartSimulation()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ChangeAnimalsSpeed(float animalsSpeed)
    {
        int intAnimalsSpeed = (int)animalsSpeed;
        _animalsSpeedText.text = intAnimalsSpeed.ToString();
        Constants.AnimalsSpeed = intAnimalsSpeed;
    }

    private void ChangeAnimalsCount(float animalsCount)
    {
        int intAnimalsCount = (int)animalsCount;
        _animalsCountText.text = intAnimalsCount.ToString();
        Constants.AnimalsCount = intAnimalsCount;

    }

    private void ChangeFieldSize(float fieldSize)
    {
        int intFieldSize = (int)fieldSize;
        _fieldSizeText.text = intFieldSize.ToString();
        Constants.FieldSize = intFieldSize;
        _animalsCountSlider.maxValue = (int)(Constants.FieldSize * Constants.FieldSize / 2);
    }

    private void SetStartValues()
    {
        _fieldSizeSlider.value = Constants.FieldSize;
        _fieldSizeText.text = Constants.FieldSize.ToString();

        _animalsCountSlider.value = Constants.AnimalsCount;
        _animalsCountText.text = Constants.AnimalsCount.ToString();

        _animalsSpeedSlider.value = Constants.AnimalsSpeed;
        _animalsSpeedText.text = Constants.AnimalsSpeed.ToString();
    }
}
