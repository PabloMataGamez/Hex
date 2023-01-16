using System;
using UnityEngine;

class MenuState : State
{
    private MenuViews _menuView;

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
    }
}
