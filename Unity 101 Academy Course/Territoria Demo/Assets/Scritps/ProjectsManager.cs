using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ProjectsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] projectsModels;
    [SerializeField] private Transform projectsGrid;
    [SerializeField] private GameObject projectOption;

    private GameObject _projectToShow;
    private ScenesManager scenesManager;
    private bool projectShowed = false;

    public GameObject projectToShow
    {
        get => _projectToShow;
        set => _projectToShow = value;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        scenesManager = GameObject.Find("ScenesManager").GetComponent<ScenesManager>();

        if (projectsModels.Length > 0)
        {
            foreach (var project in projectsModels)
            {
                GameObject option = Instantiate(projectOption, projectsGrid);
                Button button = option.transform.Find("ProjectButton").GetComponent<Button>();
                button.onClick.AddListener(() => OptionOnClick(project));
            }
        }
    }

    private void Update()
    {
        ShowProject();
    }

    private void OptionOnClick(GameObject project)
    {
        _projectToShow = project;
        scenesManager.LoadProject();
    }

    private void ShowProject()
    {
        if (projectToShow && !projectShowed && SceneManager.GetActiveScene().buildIndex == 2)
        {
            Instantiate(projectToShow);
        }
    }
}