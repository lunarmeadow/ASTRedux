using ASTRedux.Services;
using ASTRedux.Utils;
using ASTRedux.Utils.Logging;
using ASTRedux.ViewModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using ManagedBass;
using Microsoft.Extensions.DependencyInjection;

namespace ASTRedux;

public partial class App : Application
{
    private ServiceProvider _ServiceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void RegisterDependencies()
    {
        ServiceCollection services = new ServiceCollection();

        // viewmodels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<ASTViewModel>();
        services.AddTransient<RSoundViewModel>();

        // vm factory function
        services.AddSingleton<Func<Type, ObservableObject>>(serviceProvider => viewModelType => (ObservableObject)serviceProvider.GetRequiredService(viewModelType));

        // services
        services.AddSingleton<NavService>();

        _ServiceProvider = services.BuildServiceProvider();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        RegisterDependencies();

        var vm = _ServiceProvider.GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
            desktop.Exit += FinishProcess;

            desktop.MainWindow.Opened += async (_, _) =>
            {
                await DependencyGrabber.GrabBassLibrary();

                Bass.Init();
                Logger.Message("BASS initialized!");  
                PluginLoader.LoadPlugins(AppContext.BaseDirectory);
            };
        }
        base.OnFrameworkInitializationCompleted();
    }
    private void FinishProcess(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        // clear all resources here
        Bass.Free();

        Logger.Message("BASS freed!", LogType.INFO);

        // finish by writing log
        if (Logger.VerbosityLevel > LogDetail.LOW)
            File.WriteAllText($"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.log", Logger.LogOut.ToString());
    }
}