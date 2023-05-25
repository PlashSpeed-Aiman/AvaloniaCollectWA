using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.NETCoreMVVMApp1.Data;
using Avalonia.NETCoreMVVMApp1.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace Avalonia.NETCoreMVVMApp1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase,IActivatableViewModel

    {
        private CollectionContext dbContext;
        private string _phoneNumber ="";
        private string _description ="";
        private string _whatsAppLink = "https://wa.me/";
        private ObservableCollection<CollectionEntity> _collectionEntities;
        private string[] _strings = { "Any", "60", "61", "62" };
        private string _countryCode = "";
        private string _category = "";
        public MainWindowViewModel()
        {
            Activator = new ViewModelActivator();
            this.WhenActivated((CompositeDisposable disposables) =>
            {
                /* handle activation */
                Disposable
                    .Create(() => { /* handle deactivation */ })
                    .DisposeWith(disposables);
            });
            _collectionEntities = new ObservableCollection<CollectionEntity>();
            dbContext = new CollectionContext();
            var x = dbContext
                .Entities
                .Include( x => x.Category)
                .ToList();
            foreach(var elem in x)
            {
                _collectionEntities.Add(elem);
            }

            var evnt = Observable
                .FromEventPattern<SavedChangesEventArgs>(dbContext, "SavedChanges")
                .Select(change => change.EventArgs.AcceptAllChangesOnSuccess).Where(x => x)
                .Subscribe(x => { }
                    //     CollectionEntities.Add((new CollectionEntity
                // {
                //     PhoneNum = _countryCode+PhoneNumber,
                //     Description = Description,
                //     Category = new CollectionCategory
                //     {
                //         Category = "All"
                //     }
                // }))
                    );

        }

        public string Category
        {
            get => _category;
            set => this.RaiseAndSetIfChanged(ref _category, value);
        }

        public string WhatsAppLink
        {
            get
            {
                _phoneNumber = Regex.Replace(_phoneNumber, "[^0-9.]", "");
                return _whatsAppLink + "6"+ _phoneNumber;
            }
            // set
            // {
            //     _whatsAppLink = value;
            //     this.RaisePropertyChanged();
            // }
        }

        public string[] Strings
        {
            get => _strings;
            set => this.RaiseAndSetIfChanged(ref _strings, value);
        }

        public ObservableCollection<CollectionEntity> CollectionEntities
        {
            get => _collectionEntities;
            set => this.RaiseAndSetIfChanged(ref _collectionEntities, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                this.RaisePropertyChanged();
            }

        }

        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }


        public void ListOpenWhatsApp(string phoneNumber = null)
        {
            //whatsapp://send?text=Hello&phone=+60172182325
            if (phoneNumber != null)
            {
                Process.Start("explorer", $"https://wa.me/{phoneNumber}");
                
            }

          
        }
        public void DeleteEntry(object entity)
        {
            var x = entity as CollectionEntity;
            CollectionEntities.Remove(x);
            dbContext.Entities.Remove(x);
            dbContext.SaveChanges();
        }
        public void OpenWhatsApp(object code)
        {
            var strCode = code as string;
            //whatsapp://send?text=Hello&phone=+60172182325
            if (PhoneNumber != null)
            {
                if(strCode.Contains("Any") != true)
                    Process.Start("explorer", $"https://wa.me/{strCode}{PhoneNumber}");
                else
                    Process.Start("explorer", $"https://wa.me/{PhoneNumber}");

            }
            

        }
        
        public async void AddToCollection(object code)
        {
            var x = code as string;
            if (x.Contains("Any"))
                _countryCode = "";
            else
            {
                _countryCode = x;
            }
            var body = new CollectionEntity
            {
                PhoneNum = $"{_countryCode + PhoneNumber}",
                Description = Description,
                Category = new CollectionCategory
                {
                    Category = Category
                }
            };
            Task.Run(() => { 
                dbContext.AddAsync(body);
                dbContext.SaveChangesAsync();});
            
            CollectionEntities.Add(body);
        }

        public ViewModelActivator Activator { get; }
    }
}