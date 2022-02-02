using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ReactiveUI;
using MoneyApp.Models;
using MoneyApp.Services;
using MoneyApp.Dialog;

namespace MoneyApp.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {

        #region Private variables

        private string? _name;
        private int _amount;
        private Category? _category;
        private int _availableAmount;
        private ObservableCollection<RecordViewModel>? _recordViewModels;

        #endregion

        #region Public fields

        public Category Category
        {
            get => _category!;
        }

        public int AvailableAmount
        {
            get => _availableAmount;
            set => this.RaiseAndSetIfChanged(ref _availableAmount, value);
        }

        public string Name
        {
            get => _category!.Name;
            set {
                this.RaiseAndSetIfChanged(ref _name, value);
                _category!.Name = value;
            }
        }

        public int Amount
        {
            get => _category!.Amount;
            set {
                this.RaiseAndSetIfChanged(ref _amount, value);
                _category!.Amount = value;
            }
        }

        public ObservableCollection<RecordViewModel> RecordViewModels
        {
            get => _recordViewModels!;
            set => this.RaiseAndSetIfChanged(ref _recordViewModels, value);
        }
        #endregion

        public event EventHandler? DeleteCategoryEvent;
        public IReactiveCommand OpenDialogCommand { get; }
        public IReactiveCommand DeleteCategoryCommand { get; }

        public CategoryViewModel(Category category,IEnumerable<RecordViewModel> recordViewModels)
        {
            _category = category;
            RecordViewModels = new ObservableCollection<RecordViewModel>(recordViewModels);
            RecordViewModels.CollectionChanged += RecordViewModelsCollectionChanged;

            //обновление остатка суммы категории после инициализации
            UpdateAvailableAmount();

            // подписка на событие удаления записи
            SubscribeToDeleteRecordEvent();

            OpenDialogCommand = ReactiveCommand.
                CreateFromTask(async () => await OpenRecordWindow());

            DeleteCategoryCommand = ReactiveCommand.Create(
                () => DeleteCategoryEvent?.Invoke(this, new EventArgs()));
        }

        // добавление записи и RecordVM в бд
        public async Task InsertRecord(Record record)
        {
            MoneyRepository repo = MoneyRepository.Instance;
            await repo.InsertRecordAsync(record);

            var vm = new RecordViewModel(record);
            vm.DeleteRecordEvent += DeleteRecordEventHandler;
            RecordViewModels.Add(vm);
        }

        // удаление записи и RecordVM в бд
        private void DeleteRecordEventHandler(object? sender, EventArgs e)
        {
            var vm = (RecordViewModel)sender!;
            RecordViewModels.Remove(vm);
            Task.Run(async()=>{
                MoneyRepository repo = MoneyRepository.Instance;
                await repo.DeleteRecordAsync(vm.Record);
            });
        }

        private async Task OpenRecordWindow()
        {
            Record record = await DialogService.ShowDialogAsync<Record>(
                new RecordDialogWindow()
                {
                    DataContext = new RecordDialogViewModel(AvailableAmount, "New Record")
                }
            );
            
            if(record != null)
            {
                record.CategoryId = _category!.Id;
                await InsertRecord(record);
            }
        }

        private void UpdateAvailableAmount()
        {
            int remainingAmount = _category!.Amount;
            foreach(var recordVM in RecordViewModels)
            {
                remainingAmount -= recordVM.Record.Amount;
            }
            AvailableAmount = remainingAmount;
        }

        private void RecordViewModelsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
            => UpdateAvailableAmount();

        private void SubscribeToDeleteRecordEvent(){
            if(RecordViewModels.Count > 0)
                foreach(var recordVM in RecordViewModels)
                {
                    recordVM.DeleteRecordEvent += DeleteRecordEventHandler;
                }
        }

    }
}
