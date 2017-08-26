using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlipcardsModel {
    public class DataBaseInputOutput
    {
        private readonly FlipcardDatabase _database;

        public DataBaseInputOutput(FlipcardDatabase database)
        {
            _database = database;
        }

        public void Save()
        {
            // Since SortedSet is not serializable, change it to a list.
            IList<FlipcardWord> flipcardWords = new List<FlipcardWord>(_database.FlipcardsWords);

            DataContractSerializer serializer = new DataContractSerializer(typeof(IList<FlipcardWord>));
            using (Stream stream = File.Open("FlipCardDatabase.xml", FileMode.Create))
            {
                serializer.WriteObject(stream, flipcardWords);
            }           
        }

        public void Load() {
            IList<FlipcardWord> flipcardWords = new List<FlipcardWord>();

            DataContractSerializer serializer = new DataContractSerializer(typeof(IList<FlipcardWord>));
            using (Stream stream = File.Open("FlipCardDatabase.xml", FileMode.Open))
            {
                flipcardWords = (IList<FlipcardWord>)serializer.ReadObject(stream);
                foreach (var flipcardWord in flipcardWords)
                {
                    _database.FlipcardsWords.Add(flipcardWord);
                }
            }
        }
    }
}
