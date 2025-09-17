using System.Collections.Generic;

namespace Lame.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    public List<string> Options = new List<string>() { "Option 1", "Option 2", "Option 3" };
}