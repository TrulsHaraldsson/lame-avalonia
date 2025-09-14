using Lame.ViewModels;

namespace Lame.Test.ViewModels;

public class TestMainViewModel
{
    [Test]
    public void After_Initialization_LoginViewModel_Is_Set_To_Current_View()
    {
        // Arrange
        var vm = new MainWindowViewModel();
        
        // Assert
        Assert.That(vm.CurrentViewModel, Is.TypeOf<LoginViewModel>(), 
            "Expected the LoginViewModel to be set after initialization.");
    }
}