using System.Collections.ObjectModel;
using ReactiveUI;

namespace MoneyApp.ViewModels
{
    public class CategoryManagerViewModel:ViewModelBase
    {
        private ObservableCollection<CategoryViewModel>?  _categoryViewModels;

        public ObservableCollection<CategoryViewModel> CategoryViewModels
        {
            get => _categoryViewModels!;
            set => this.RaiseAndSetIfChanged(ref _categoryViewModels, value);
        }

        public CategoryManagerViewModel(){

        }
    }
}