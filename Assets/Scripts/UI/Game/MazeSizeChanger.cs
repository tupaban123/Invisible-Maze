using UnityEngine;
using TMPro;
using InvisibleMaze.CodeBase;
using InvisibleMaze.SceneLoading;
using System;
using InvisibleMaze.GameConstants;

public class MazeSizeChanger : MonoBehaviour
{
    [SerializeField] private TMP_InputField widthInputField; 
    [SerializeField] private TMP_InputField heightInputField;
    [SerializeField] private TMP_Dropdown mazeTypeDropdown;

    private void Start()
    {
        int width = 6;
        int height = 6;
        int mazeType = 0;

        if(PlayerPrefs.HasKey(Constants.Width))
            width = PlayerPrefs.GetInt(Constants.Width);
        else
            PlayerPrefs.SetInt(Constants.Width, width);

        if(PlayerPrefs.HasKey(Constants.Height))
            height = PlayerPrefs.GetInt(Constants.Height);
        else
            PlayerPrefs.SetInt(Constants.Height, height);
        
        if(PlayerPrefs.HasKey(Constants.MazeType))
            mazeType = PlayerPrefs.GetInt(Constants.MazeType);
        else
            PlayerPrefs.SetInt(Constants.MazeType, mazeType);

        widthInputField.text = width.ToString();
        heightInputField.text = height.ToString();
        mazeTypeDropdown.value = mazeType;
    }

    public void ChangeSize()
    {
        var width = int.Parse(widthInputField.text);
        PlayerPrefs.SetInt(Constants.Width, width);

        var height = int.Parse(heightInputField.text);
        PlayerPrefs.SetInt(Constants.Height, height);
    }

    public void OnMazeTypeChange(Int32 value)
    {
        PlayerPrefs.SetInt(Constants.MazeType, value);
    }

    public void LoadGameScene()
    {
        ServiceLocator.Instance.Get<ISceneLoader>().LoadGame();
    }
}