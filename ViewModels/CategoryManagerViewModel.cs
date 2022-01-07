using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Models;
using MoneyApp.Services;
using MoneyApp.Dialog;

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

        public IReactiveCommand AddCategoryCommand { get; }

        public CategoryManagerViewModel(){

            AddCategoryCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Category>(
                    new AddCategoryWindow(){
                        DataContext = new AddCategoryViewModel()
                    }
                );
            });

        }
    }
}