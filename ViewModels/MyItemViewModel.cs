using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace Avalonia.ObservableCollectionItemsControl.ViewModels
{
    public class MyItemViewModel : ReactiveObject
    {
        private bool _isActive;

        public string Name { get; private set; }

        public bool IsActive 
        { 
            get => this._isActive; 
            set => this.RaiseAndSetIfChanged(ref this._isActive, value); 
        }

        public ReactiveCommand<Unit, Unit> DeleteCommand { get; private set; }
    
        public MyItemViewModel(string name)
        {
            this.Name = name;
            this.IsActive = true;
            this.DeleteCommand = ReactiveCommand.Create(this.HandleDelete);
        }

        private void HandleDelete()
        {
            this.IsActive = false;
        }
    }
}