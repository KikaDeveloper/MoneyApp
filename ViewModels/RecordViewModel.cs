using System;
using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class RecordViewModel : ViewModelBase
    {
        public event EventHandler? DeleteRecordEvent;
        private Record? _record;

        public Record Record
        {
            get => _record!;
            set => this.RaiseAndSetIfChanged(ref _record, value);
        }

        public IReactiveCommand DeleteRecordCommand { get; }

        public RecordViewModel(Record record){
            Record = record;
            DeleteRecordCommand = ReactiveCommand.Create(()=>{
                DeleteRecordEvent?.Invoke(this, new EventArgs());
            });
        }
    }
}