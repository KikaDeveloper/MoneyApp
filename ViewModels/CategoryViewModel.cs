using ReactiveUI;
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

        public CategoryViewModel(){
            AddRecordCommand = ReactiveCommand.CreateFromTask(async()=>{
                var result = await DialogService.ShowDialogAsync<Record>(
                    new AddRecordWindow()
                    {
                        DataContext = new AddRecordViewModel()
                    }
                );
            });
        }

    }
}
