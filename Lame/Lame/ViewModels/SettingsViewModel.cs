using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lame.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public List<string> Options = new List<string>() { "Option 1", "Option 2", "Option 3" };

    public ObservableCollection<Node> Nodes { get; }
    public ObservableCollection<Node> SelectedNodes { get; }
    
    public SettingsViewModel()
    {
        SelectedNodes = new ObservableCollection<Node>();
        Nodes = new ObservableCollection<Node>
        {
            new Node("General", new ObservableCollection<Node>
            {
                new Node("Appearance", new ObservableCollection<Node>
                {
                    new Node("Theme"),
                    new Node("Font Size"),
                    new Node("Color Scheme")
                }),
                new Node("Notifications", new ObservableCollection<Node>
                {
                    new Node("Email Alerts"),
                    new Node("Push Notifications"),
                    new Node("Sounds")
                }),
                new Node("Language", new ObservableCollection<Node>
                {
                    new Node("Default Language"),
                    new Node("Translation Settings"),
                    new Node("Input Modes")
                })
            }),
            new Node("Account", new ObservableCollection<Node>
            {
                new Node("Profile", new ObservableCollection<Node>
                {
                    new Node("Change Username"),
                    new Node("Update Password"),
                    new Node("Add Profile Picture")
                }),
                new Node("Security", new ObservableCollection<Node>
                {
                    new Node("Two-Factor Authentication"),
                    new Node("Firewall Settings"),
                    new Node("Trusted Devices")
                }),
                new Node("Preferences", new ObservableCollection<Node>
                {
                    new Node("Privacy Settings"),
                    new Node("Data Sharing"),
                    new Node("Ad Personalization")
                })
            }),
            new Node("Applications", new ObservableCollection<Node>
            {
                new Node("Installed Apps", new ObservableCollection<Node>
                {
                    new Node("List of Apps"),
                    new Node("Uninstall App"),
                    new Node("App Updates")
                }),
                new Node("Permissions", new ObservableCollection<Node>
                {
                    new Node("Camera"),
                    new Node("Microphone"),
                    new Node("Location")
                })
            }),
            new Node("Advanced", new ObservableCollection<Node>
            {
                new Node("Developer Settings", new ObservableCollection<Node>
                {
                    new Node("Debug Mode"),
                    new Node("Logs"),
                    new Node("Experimental Features")
                }),
                new Node("System Options", new ObservableCollection<Node>
                {
                    new Node("Reset Settings"),
                    new Node("Backup"),
                    new Node("Diagnostics")
                })
            })
        };
    }

    
}


public class Node
{
    public ObservableCollection<Node>? SubNodes { get; }
    public string Title { get; }

    public Node(string title)
    {
        Title = title;
    }

    public Node(string title, ObservableCollection<Node> subNodes)
    {
        Title = title;
        SubNodes = subNodes;
    }
}
