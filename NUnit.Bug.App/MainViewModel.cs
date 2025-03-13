using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace NUnit.Bug.App {
    public class MainViewModel : INotifyPropertyChanged {
        private string _message;
        public string Message {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand ShowMessageCommand { get; }

        public MainViewModel() {
            ShowMessageCommand = new RelayCommand(param => ShowMessage(param?.ToString()));
        }

        private void ShowMessage(string message) {
            Message = string.IsNullOrEmpty(message) ? "Hello, World!" : message;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}