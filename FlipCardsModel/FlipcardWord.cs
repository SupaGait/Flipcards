using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FlipcardsModel
{
    [DataContract]
    public struct FlipcardWord : IComparable {

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public Dictionary<Language, string> Words { get; set; }

        [DataMember]
        public List<string> Tags { get; set; }

        public int CompareTo(object obj) {
            return string.Compare(obj as string, Key, StringComparison.Ordinal);
        }

        public override string ToString() {
            return $"Key: {Key}";
        }

        public bool Equals(FlipcardWord other)
        {
            return string.Equals(Key, other.Key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is FlipcardWord && Equals((FlipcardWord) obj);
        }

        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }
    }
}