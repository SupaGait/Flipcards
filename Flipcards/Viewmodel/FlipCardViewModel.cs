using System.Windows.Input;
using Flipcards.Utils;
using FlipcardsModel;

namespace Flipcards.Viewmodel{
    class FlipCardViewModel : ObservableObject
    {
        public Flipcard FlipcardModel { get; set;}
        public string TestText { get; } = "TestText";

        private ICommand _getProductCommand;
        private ICommand _saveProductCommand;
        public ICommand SaveProductCommand
        {
            get
            {
                if (_saveProductCommand == null)
                {
                    _saveProductCommand = new RelayCommand(
                        param => SaveProduct(),
                        param => (FlipcardModel != null)
                    );
                }
                return _saveProductCommand;
            }
        }
        public ICommand GetProductCommand
        {
            get
            {
                if (_getProductCommand == null)
                {
                    _getProductCommand = new RelayCommand(
                        param => GetProduct(),
                        param => (TestText.Length > 0)
                    );
                }
                return _getProductCommand;
            }
        }

        private void SaveProduct() {
            throw new System.NotImplementedException();
        }
        private void GetProduct()
        {
            throw new System.NotImplementedException();
        }
    }
}
