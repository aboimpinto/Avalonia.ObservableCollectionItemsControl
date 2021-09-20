using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Avalonia.ObservableCollectionItemsControl.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private int _itemsCount = 0;

        public ObservableCollection<MyItemViewModel> MyItems { get; private set; }

        public ICommand AddItemCommand { get; private set; }

        public MainWindowViewModel()
        {
            this.MyItems = new ObservableCollection<MyItemViewModel>();

            this.MyItems
                .ToObservableChangeSet()
                .AutoRefreshOnObservable(x => x.DeleteCommand)
                .Select(_ => this.MyItems.SingleOrDefault(x => !x.IsActive))
                .Subscribe(x => 
                {
                    if (x != null)
                    {
                        this.MyItems.Remove(x);
                    }
                });


            this.AddItemCommand = ReactiveCommand.Create(this.HandleAddItem);
        }

        private void HandleAddItem()
        {
            this.MyItems.Add(new MyItemViewModel($"Item::{this._itemsCount}"));
            this._itemsCount ++;
        }
    }
}
