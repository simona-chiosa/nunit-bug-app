namespace NUnit.Bug.App.Test {
    [TestFixture]
    public class MainViewModelTests {
        [TestCase("Hello, World!")]
        [TestCase("Test Message")]
        [TestCase("Another Example")]
        [TestCase("")]
        public void ShowMessageCommand_UpdatesMessage(string input) {
            var viewModel = new MainViewModel();
            viewModel.ShowMessageCommand.Execute(input);
            var expected = string.IsNullOrEmpty(input) ? "Hello, World!" : input;
            Assert.Equals(expected, viewModel.Message);
        }
    }
}