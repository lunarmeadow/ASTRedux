using ASTRedux.Utils;
using ASTRedux.Utils.Logging;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using ManagedBass;

namespace ASTRedux;
public partial class MainWindow : Window
{
    private const int SFX_MODE = 0;
    private const int MUSIC_MODE = 1;

    private FilePickerFileType soundType = new("Dead Rising Sound Files")
    {
        Patterns = new[] { "*.rSoundSnd", "*.snd" },
        AppleUniformTypeIdentifiers = null,
        MimeTypes = null
    };

    private FilePickerFileType musicType = new("Dead Rising Music Files")
    {
        Patterns = new[] { "*.rSoundAst", "*.ast" },
        AppleUniformTypeIdentifiers = null,
        MimeTypes = null
    };

    public MainWindow()
    {
        InitializeComponent();

        Logger.OnLogMessage += WriteLog;
        Logger.VerbosityLevel = LogDetail.LOW;

        Logger.Message("Welcome to ASTRedux!");
    }
    private void WriteLog(string log)
    {
        // https://docs.avaloniaui.net/docs/guides/development-guides/accessing-the-ui-thread
        Dispatcher.UIThread.Post(() =>
        {
            LogWindow.Text += log + Environment.NewLine;
        });
    }

    private void ClearLog(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.Post(() =>
        {
            LogWindow.Text = String.Empty;
        });
    }
}