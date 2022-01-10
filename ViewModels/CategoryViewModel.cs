using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MoneyApp.Models;
using MoneyApp.Services;
using MoneyApp.Dialog;

namespace MoneyApp.ViewModels
{
    public class CategoryViewModel:ViewModelBase
    {
        private Category? _category;
        private ObservableCollection<RecordViewModel>? _recordViewModels;
        public event EventHandler? DeleteCategoryEvent;

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

        public IReactiveCommand AddRecordCommand { get; }
        public IReactiveCommand DeleteCategoryCommand { get; }

        public CategoryViewModel()
        {
            AddRecordCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Record>(
                    new AddRecordWindow()
                    {
                        DataContext = new AddRecordViewModel()
                    }
                );
                if(result != null)
                {
                    result.CategoryId = Category.Id;
                    await InsertRecord(result);
                }
            });

            DeleteCategoryCommand = ReactiveCommand.Create(
                () => DeleteCategoryEvent?.Invoke(this, new EventArgs()));
        }

        public async Task InsertRecord(Record record)
        {
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertRecordAsync(record);

            var vm = new RecordViewModel(record);
            vm.DeleteRecordEvent += DeleteRecordEventHandler;
            RecordViewModels.Add(vm);
        }

        private void DeleteRecordEventHandler(object? sender, EventArgs e)
        {
            var vm = (RecordViewModel)sender!;
            RecordViewModels.Remove(vm);
            Task.Run(async()=>{
                MoneyRepository repo = MoneyRepository.Instance;
                await repo.DeleteRecordAsync(vm.Record);
            });
        }
    }
}
