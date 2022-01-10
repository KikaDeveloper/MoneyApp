using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ReactiveUI;
using MoneyApp.Models;
using MoneyApp.Services;
using MoneyApp.Dialog;

namespace MoneyApp.ViewModels
{
    public class CategoryManagerViewModel : ViewModelBase
    {
        private int _walletId;
        private CategoryViewModel? _selectedCategoryViewModel;
        private ObservableCollection<CategoryViewModel>?  _categoryViewModels;

        public ObservableCollection<CategoryViewModel> CategoryViewModels
        {
            get => _categoryViewModels!;
            set => this.RaiseAndSetIfChanged(ref _categoryViewModels, value);
        }

        public CategoryViewModel SelectedCategoryViewModel
        {
            get => _selectedCategoryViewModel!;
            set => this.RaiseAndSetIfChanged(ref _selectedCategoryViewModel, value);
        }

        public IReactiveCommand AddCategoryCommand { get; }

        public CategoryManagerViewModel(int walletId){
            _walletId = walletId;

            AddCategoryCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Category>(
                    new AddCategoryWindow(){
                        DataContext = new AddCategoryViewModel()
                    }
                );
                result.WalletId = _walletId;
                await InsertCategory(result);
            });
        }

        public async Task InsertCategory(Category category){
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertCategoryAsync(category);

            var vm = new CategoryViewModel(){
                Category = category,
                RecordViewModels = new ObservableCollection<RecordViewModel>()
            };
            vm.DeleteCategoryEvent += DeleteCategoryEventHandler;

            CategoryViewModels.Add(vm);
        }

        private void DeleteCategoryEventHandler(object? sender, EventArgs e){
            var vm = (CategoryViewModel)sender!;
            CategoryViewModels.Remove(vm);
            Task.Run(async()=>{
                MoneyRepository repo = MoneyRepository.Instance;
                await repo.DeleteCategoryAsync(vm.Category);
            });
        }
    }
}