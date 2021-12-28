using ReactiveUI;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class RecordViewModel:ViewModelBase
    {
        private Record? _record;

        public Record Record
        {
            get => _record!;
            set => this.RaiseAndSetIfChanged(ref _record, value);
        }

        public RecordViewModel(){
            
        }
    }
}