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
        private static readonly FlipcardDatabase _flipcardDatabase = new FlipcardDatabase();
        private DeckStatus _deckStatus = null;
        protected FlipcardDeck _flipcardDeckShown = null;
        private DataBaseInputOutput _dataBaseInputOutput = null;

        private ICommand _showFlipCardsCommand;
        private ICommand _addContentCommand;
        private ICommand _newDeckCommand;
        private ICommand _saveDeckCommand;
        private ICommand _loadDeckCommand;
        private ICommand _closeWindowCommand;
        private ICommand _loadedWindowCommand;
        #endregion

        #region properties

        /// <summary>
        /// A Collection of flipcards shown in the view
        /// </summary>
        public ObservableCollection<FlipCardViewModel> FlipcardsVm { get; set; } = new ObservableCollection<FlipCardViewModel>();

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
                           param => _dataBaseInputOutput.Save(),
                           param => true)
                       );
            }
        }

        public ICommand LoadedWindowCommand
        {
            get {
                return _loadedWindowCommand ??
                       (_loadedWindowCommand = new RelayCommand(
                           param => _dataBaseInputOutput.Load(),
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
            _dataBaseInputOutput = new DataBaseInputOutput(_flipcardDatabase);
            _deckStatus = new DeckStatus(Language.Dutch, Language.German);

            // If no decks loaded, create a new one.
            if(_flipcardDatabase.FlipcardDecks.Count <= 0) {
                _flipcardDatabase.AddDeck(new FlipcardDeck(_flipcardDatabase, _deckStatus) { Name = "newdeck" });
            }


            // Register for events on the deck
            _flipcardDeckShown = _flipcardDatabase.FlipcardDecks.First().Value;
            _flipcardDeckShown.Flipcards.CollectionChanged += Flipcards_CollectionChanged;

            // Show the available decks
            foreach (var deck in _flipcardDatabase.FlipcardDecks.Values)
            {
                DecksAvailable.Add(deck.Name);
            }
            DeckSelected = DecksAvailable.First();
        }

        private void Flipcards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var flipcard in e.NewItems.OfType<Flipcard>())
                    {
                        FlipcardsVm.Add(new FlipCardViewModel(flipcard));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    // TODO
                    break;
                case NotifyCollectionChangedAction.Reset:
                    FlipcardsVm.Clear();
                    break;
            }
        }

        private void OpenAddContentWindow() {
            AddContentViewModel viewModel = new AddContentViewModel(_flipcardDatabase, _flipcardDeckShown, _deckStatus);
            AddContentView view = new AddContentView {DataContext = viewModel};
            view.Show();
        }

        /// <summary>
        /// Fill with data
        /// </summary>
        private void PopulateModel()
        {
            foreach (var flipcard in _flipcardDeckShown.Flipcards)
            {
                FlipcardsVm.Add(new FlipCardViewModel(flipcard));
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

                // Create and select first deck
                var flipcardDeck = new FlipcardDeck(_flipcardDatabase, _deckStatus) {Name = name };
                _flipcardDatabase.AddDeck(flipcardDeck);
                _flipcardDeckShown = flipcardDeck;

                DecksAvailable.Add(name);
                DeckSelected = DecksAvailable.Last();
            }
        }
    }
}
