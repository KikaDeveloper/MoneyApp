using ReactiveUI;
using System.Collections.ObjectModel;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class CategoryViewModel:ViewModelBase
    {
        private Category? _category;
        private ObservableCollection<RecordViewModel>? _recordViewModels;

        public Category Category
        {
            get => _category!;
            set => this.RaiseAndSetIfChanged(ref _category, value);
        }

        public ObservableCollection<RecordViewModel> RecordViewModels
        {
            get => _recordViewModels!;
            set => this.RaiseAndSetIfChanged(ref _recordViewModels, value);
        }

        public CategoryViewModel(){
            
        }

    }
}
