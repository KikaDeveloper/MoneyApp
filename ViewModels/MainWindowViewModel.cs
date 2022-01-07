﻿using ReactiveUI;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using MoneyApp.Models;

namespace MoneyApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private WalletViewModel? _walletViewModels;

        public WalletViewModel WalletViewModel
        {
            get => _walletViewModels!;
            set => this.RaiseAndSetIfChanged(ref _walletViewModels, value);
        }

        public MainWindowViewModel(){

            WalletViewModel = new WalletViewModel(new List<WalletAdapter>(){
                new WalletAdapter(){
                    Wallet = new Wallet(){
                        Id = 1,
                        Name = "Wallet1",
                        Amount = 10000,
                        AmountRatio = AmountRatio.Day
                    },
                    ViewModel = new CategoryManagerViewModel(){
                        CategoryViewModels = new ObservableCollection<CategoryViewModel>(){
                            new CategoryViewModel(){
                                Category = new Category(){
                                    Id = 1,
                                    Name = "Home",
                                    Amount = 1000,
                                    WalletId = 1,
                                },
                                RecordViewModels = new ObservableCollection<RecordViewModel>(){
                                    new RecordViewModel(){
                                        Record = new Record(){
                                            Id = 1,
                                            Text = "food",
                                            Amount = 400,
                                            CategoryId = 1
                                        }
                                    }
                                }
                            },
                            new CategoryViewModel(){
                                Category = new Category(){
                                    Id = 2,
                                    Name = "Shop",
                                    Amount = 1000,
                                    WalletId = 1
                                },
                                RecordViewModels = new ObservableCollection<RecordViewModel>(){
                                    new RecordViewModel(){
                                        Record = new Record(){
                                            Id = 2,
                                            Text = "Car",
                                            Amount = 1500,
                                            CategoryId = 2
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new WalletAdapter(){
                    Wallet = new Wallet(){
                        Id = 2,
                        Name = "Wallet2",
                        Amount = 10500,
                        AmountRatio = AmountRatio.Day
                    },
                    ViewModel = new CategoryManagerViewModel()
                }
            });
            
        }
    }
}
