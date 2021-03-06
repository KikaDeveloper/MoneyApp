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
        
        public int WalletAvailableAmount { get; set; }

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

        public IReactiveCommand OpenDialogCommand { get; }
        public IReactiveCommand UpdateDialogCommand { get; }

        public CategoryManagerViewModel(int walletId, IEnumerable<CategoryViewModel> categoryViewModels){
            _walletId = walletId;
            
            CategoryViewModels = new ObservableCollection<CategoryViewModel>(categoryViewModels);

            SubcribeToDeleteCategoryEvent();

            OpenDialogCommand = ReactiveCommand.
                CreateFromTask(async () => await OpenAddCategoryWindow());

            UpdateDialogCommand = ReactiveCommand.
                CreateFromTask(async () => await OpenUpdateCategoryWindow());
        }

        // добавление категории и categoryVM в бд
        public async Task InsertCategory(Category category){
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertCategoryAsync(category);

            var viewModel = new CategoryViewModel
            (
                category,
                new List<RecordViewModel>()
            );
            
            viewModel.DeleteCategoryEvent += DeleteCategoryEventHandler;
            CategoryViewModels.Add(viewModel);
        }

        // удаление категории и categoryVM в бд
        private void DeleteCategoryEventHandler(object? sender, EventArgs e){
            var viewModel = (CategoryViewModel)sender!;
            CategoryViewModels.Remove(viewModel);

            Task.Run(async()=>{
                MoneyRepository repo = MoneyRepository.Instance;
                await repo.DeleteCategoryAsync(viewModel.Category);
            });
        }
   
        private async Task OpenAddCategoryWindow()
        {
            var category = await DialogService.ShowDialogAsync<Category>(
                new CategoryDialogWindow(){
                    DataContext = new CategoryDialogViewModel(WalletAvailableAmount, "New category")
                }
            );

            if(category != null)
            {
                category.WalletId = _walletId;
                await InsertCategory(category);
            }
        }

        private async Task OpenUpdateCategoryWindow()
        {
            var category = await DialogService.ShowDialogAsync<Category>(
                new CategoryDialogWindow(){
                    DataContext = new CategoryDialogViewModel(WalletAvailableAmount, "Edit category")
                }
            );

            if(category != null)
            {
                category.WalletId = _walletId;
                await UpdateCategory(category);
            }
        }

        private async Task UpdateCategory(Category category)
        {
            MoneyRepository repo = MoneyRepository.Instance;
            category.Id = SelectedCategoryViewModel.Category.Id;
            category.WalletId = SelectedCategoryViewModel.Category.WalletId;
            await repo.UpdateCategoryAsync(category);

            var nameIsValid = !string.IsNullOrEmpty(category.Name);
            var amountIsValid = category.Amount > 0;

            if(nameIsValid && amountIsValid)
            {
                SelectedCategoryViewModel.Category.Name = category.Name;
                SelectedCategoryViewModel.Category.Amount = category.Amount;
            } else if (!nameIsValid && amountIsValid)
            {
                SelectedCategoryViewModel.Category.Amount = category.Amount;
            } else if(nameIsValid && !amountIsValid)
            {
                SelectedCategoryViewModel.Category.Name = category.Name;
            }
        }

        // подписка на событие удаления категории
        private void SubcribeToDeleteCategoryEvent()
        {
            if(CategoryViewModels.Count > 0)
                foreach(var categoryViewModel in CategoryViewModels)
                {
                   categoryViewModel.DeleteCategoryEvent += DeleteCategoryEventHandler;
                }
        }

    }
}