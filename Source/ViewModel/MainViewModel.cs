using System;
using ASTRedux.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASTRedux.ViewModel;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private NavService _nav;

    public MainViewModel(NavService nav)
    {
        _nav = nav;
    }
}
