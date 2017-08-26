using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Flipcards.View;
using FlipcardsModel;
using ProjectUtils.Binding;

namespace Flipcards.Viewmodel {
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region fields
        private static FlipcardDatabase _flipcardDatabase = new FlipcardDatabase();
        protected FlipcardDeck _flipcardDeck = new FlipcardDeck(_flipcardDatabase);
        private DataBaseInputOutput dataBaseInputOutput = new DataBaseInputOutput(_flipcardDatabase);
        private ICommand _showFlipCardsCommand;
        private ICommand _addContentCommand;
        private ICommand _newDeckCommand;
        private ICommand _saveDeckCommand;
        private ICommand _loadDeckCommand;
        private ICommand _closeWindowCommand;
        private ICommand _openWindowCommand;

        #endregion

        #region properties

        /// <summary>
        /// A Collection of flipcards shown in the view
        /// </summary>
        public ObservableCollection<FlipCardViewModel> Flipcards { get; set; } = new ObservableCollection<FlipCardViewModel>();

        /// <summary>
        /// Show the cards
        /// </summary>
        public ICommand ShowFlipCardsCommand {
            get {
                return _showFlipCardsCommand ??
                       (_showFlipCardsCommand = new RelayCommand(
                           param => PopulateModel(),
                           param => true)
                       );
            }
        }

        public ICommand AddContentCommand
        {
            get {
                return _addContentCommand ??
                       (_addContentCommand = new RelayCommand(
                           param => OpenAddContentWindow(),
                           param => true)
                       );
            }
        }

        public ICommand SaveDeckCommand
        {
            get {
                return _saveDeckCommand ??
                       (_saveDeckCommand = new RelayCommand(
                           param => SaveDeck(),
                           param => false)
                       );
            }
        }

        public ICommand LoadDeckCommand
        {
            get {
                return _loadDeckCommand ??
                       (_loadDeckCommand = new RelayCommand(
                           param => LoadDeck(),
                           param => false)
                       );
            }
        }

        public ICommand NewDeckCommand
        {
            get {
                return _newDeckCommand ??
                       (_newDeckCommand = new RelayCommand(
                           param => NewDeck(),
                           param => true)
                       );
            }
        }
        public IList<string> DecksAvailable { get; set; } = new List<string>();
        public string DeckSelected { get; set; }

        public ICommand CloseWindowCommand
        {
            get {
                return _closeWindowCommand ??
                       (_closeWindowCommand = new RelayCommand(
                           param => dataBaseInputOutput.Save(),
                           param => true)
                       );
            }
        }

        public ICommand OpenWindowCommand
        {
            get {
                return _openWindowCommand ??
                       (_openWindowCommand = new RelayCommand(
                           param => dataBaseInputOutput.Load(),
                           param => true)
                       );
            }
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainViewModel()
        {
            DecksAvailable.Add("test1");
            DecksAvailable.Add("test2");
            DeckSelected = DecksAvailable.First();

            _flipcardDeck.Flipcards.CollectionChanged += Flipcards_CollectionChanged;
        }

        private void Flipcards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var flipcard in e.NewItems.OfType<Flipcard>())
                    {
                        Flipcards.Add(new FlipCardViewModel(flipcard));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    // TODO
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Flipcards.Clear();
                    break;
            }
        }

        private void OpenAddContentWindow() {
            AddContentViewModel viewModel = new AddContentViewModel(_flipcardDatabase, _flipcardDeck);
            AddContentView view = new AddContentView {DataContext = viewModel};
            view.Show();
        }

        /// <summary>
        /// Fill with data
        /// </summary>
        private void PopulateModel()
        {
            foreach (var flipcard in _flipcardDeck.Flipcards)
            {
                Flipcards.Add(new FlipCardViewModel(flipcard));
            }
        }

        private void SaveDeck() {
            throw new NotImplementedException();
        }
        private void LoadDeck() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create a new deck
        /// </summary>
        private void NewDeck() {
            NewDeck newDeckView = new NewDeck();
            
            if (newDeckView.ShowDialog() == true)
            {
                var name = newDeckView.DeckName;
                DecksAvailable.Add(name);
                DeckSelected = DecksAvailable.Last();
            }
        }
    }
}
