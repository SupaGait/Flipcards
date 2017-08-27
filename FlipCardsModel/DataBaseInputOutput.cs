using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace FlipcardsModel {
    public class DataBaseInputOutput
    {
        private readonly FlipcardDatabase _database;

        public DataBaseInputOutput(FlipcardDatabase database)
        {
            _database = database;
        }

        private string GetObjectName<T>()
        {
            return "FlipCardDatabase_ " + typeof(T) + ".xml";
        }

        private void SaveObject<T>(T obj)
        {
            try
            {
                using (Stream stream = File.Open(GetObjectName<T>(), FileMode.Create)) {
                    new DataContractSerializer(typeof(T)).WriteObject(stream, obj);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private T LoadObject<T>() where T : new()
        {
            try
            {
                using (Stream stream = File.Open(GetObjectName<T>(), FileMode.Open)) {
                    return (T)new DataContractSerializer(typeof(T)).ReadObject(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new T();
            }
        }

        public void Save()
        {
            // Since dictonary is not serializable, change it to a list.
            IList<FlipcardWord> flipcardWords = new List<FlipcardWord>(_database.FlipcardsWords.Values);
            SaveObject(flipcardWords);

            IList<FlipcardDeck> flipcardDecks = new List<FlipcardDeck>(_database.FlipcardDecks.Values);
            SaveObject(flipcardDecks);
        }

        public void Load() {
            DataContractSerializer serializer = new DataContractSerializer(typeof(IList<FlipcardWord>));

            var flipcardWords = LoadObject<List<FlipcardWord>>();
            foreach (var flipcardWord in flipcardWords)
            {
                _database.FlipcardsWords.Add(flipcardWord.Key, flipcardWord);
            }

            var flipcardDecks = LoadObject<List<FlipcardDeck>>();
            foreach (var flipcardDeck in flipcardDecks)
            {
                _database.FlipcardDecks.Add(flipcardDeck.Name, flipcardDeck);
            }


            /*
            using (Stream stream = File.Open("FlipCardDatabase.xml", FileMode.Open))
            {
                var flipcardWords = (IList<FlipcardWord>)serializer.ReadObject(stream);
                foreach (var flipcardWord in flipcardWords)
                {
                    _database.FlipcardsWords.Add(flipcardWord.Key, flipcardWord);
                }
            }
            */
        }
    }
}
