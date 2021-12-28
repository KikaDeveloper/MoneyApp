using System.Collections.ObjectModel;
using ReactiveUI;

namespace MoneyApp.ViewModels
{
    public class CategoryManagerViewModel:ViewModelBase
    {
        private ObservableCollection<CategoryViewModel>?  _caftegoryViewModels;

        public ObservableCollection<CategoryViewModel> CategoryViewModels
        {
            get => _caftegoryViewModels!;
            set => this.RaiseAndSetIfChanged(ref _caftegoryViewModels, value);
        }

        public CategoryManagerViewModel(){

        }
    }
}