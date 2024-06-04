using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationMenu : MonoBehaviour
{
    [SerializeField] private Slider _simulationSpeedSlider;
    [SerializeField] private TMP_Text _simulationSpeedText;
    [SerializeField] private Button _stopSimulationButton;

    public void Init()
    {
        Subscribe();
        SetStartValues();
    }

    private void SetStartValues()
    {
        _simulationSpeedSlider.value = Time.timeScale;
        _simulationSpeedText.text = Time.timeScale.ToString();
    }

    private void Subscribe()
    {
        _simulationSpeedSlider.onValueChanged.AddListener(ChangeSimulationSpeed);
        _stopSimulationButton.onClick.AddListener(StopSimulation);
    }

    private void StopSimulation()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void ChangeSimulationSpeed(float value)
    {
        int intValue = (int) value;
        _simulationSpeedText.text = intValue.ToString();
        Time.timeScale = intValue;
    }
}
