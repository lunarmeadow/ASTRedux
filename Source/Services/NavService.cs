using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASTRedux.Services;

public interface INavService
{
    ObservableObject CurrentView { get; }    
    void NavigateTo<T>() where T : ObservableObject;
}

public partial class NavService : ObservableObject, INavService
{
    private readonly Func<Type, ObservableObject> _vmFactory;

    [ObservableProperty]
    private ObservableObject _currentView;

    public void NavigateTo<T>() where T : ObservableObject
    {
        ObservableObject vm = _vmFactory.Invoke(typeof(T));
        CurrentView = vm;
    }

    public NavService(Func<Type, ObservableObject> vmFactory)
    {
        _vmFactory = vmFactory;
    }
}
