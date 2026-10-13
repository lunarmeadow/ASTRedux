using System;
using System.Net;
using ASTRedux.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASTRedux.ViewModel;

enum VMSelect : int
{
    RSound,
    AST
}

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private NavService _nav;

    [ObservableProperty]
    private int _selectedTab;

    private int defaultVM = (int)VMSelect.RSound;

    partial void OnSelectedTabChanged(int value)
    {
        switch((VMSelect)value)
        {
            case VMSelect.RSound:
                Nav.NavigateTo<RSoundViewModel>();
                break;
            case VMSelect.AST:
                Nav.NavigateTo<ASTViewModel>();
                break;
        }
    }

    public MainViewModel(NavService nav)
    {
        _nav = nav;
        SelectedTab = defaultVM;

        switch((VMSelect)defaultVM)
        {
            case VMSelect.RSound:
                Nav.NavigateTo<RSoundViewModel>();
                break;
            case VMSelect.AST:
                Nav.NavigateTo<ASTViewModel>();
                break;
        }
    }
}
