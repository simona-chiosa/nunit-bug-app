namespace NUnit.Bug.App.xUnit.Test {
    public class MainViewModelTests {
        [Theory]
        [InlineData("Hello, World!")]
        [InlineData("Test Message")]
        [InlineData("Another Example")]
        [InlineData("")]
        public void ShowMessageCommand_UpdatesMessage(string input) {
            var viewModel = new MainViewModel();
            viewModel.ShowMessageCommand.Execute(input);
            var expected = string.IsNullOrEmpty(input) ? "Hello, World!" : input;
            Assert.Equal(expected, viewModel.Message);
        }
    }
}