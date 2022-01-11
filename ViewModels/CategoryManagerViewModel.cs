using System;
using System.Collections.Generic;
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

        public CategoryManagerViewModel(int walletId, IEnumerable<CategoryViewModel> categoryViewModels){
            _walletId = walletId;
            
            CategoryViewModels = new ObservableCollection<CategoryViewModel>(categoryViewModels);

            // подписка на событие удаления категории
            foreach(var categoryVM in CategoryViewModels)
            {
                categoryVM.DeleteCategoryEvent += DeleteCategoryEventHandler;
            }

            AddCategoryCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Category>(
                    new AddCategoryWindow(){
                        DataContext = new AddCategoryViewModel()
                    }
                );
                if(result != null)
                {
                    result.WalletId = _walletId;
                    await InsertCategory(result);
                }
            });
        }

        // добавление категории и categoryVM в бд
        public async Task InsertCategory(Category category){
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertCategoryAsync(category);

            var vm = new CategoryViewModel
            (
                category,
                new List<RecordViewModel>()
            );
            
            vm.DeleteCategoryEvent += DeleteCategoryEventHandler;
            CategoryViewModels.Add(vm);
        }

        // удаление категории и categoryVM в бд
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