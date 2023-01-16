using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

class MenuState : State
{
    private MenuViews _menuView;
   // private GameObject _menu;

    public override void OnEnter()
    {
        base.OnEnter();

        _menuView = GameObject.FindObjectOfType<MenuViews>(true);

        if (_menuView != null) 
        {
            _menuView.gameObject.SetActive(true);
            _menuView.PlayClicked += OnPlayClicked;
        }
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        StateMachine.MoveTo(States.Playing);
    }

    public override void OnExit()
    {
        base.OnExit();
        Debug.Log("Exit MenuState");

        if (_menuView != null)
        {
            _menuView.PlayClicked -= OnPlayClicked;
            _menuView.gameObject.SetActive(false);
        }       
        //  SceneManager.UnloadSceneAsync("Menu");
    }
}
