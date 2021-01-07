using Google.Cloud.Firestore;
using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;

namespace SampleAPIProject.Models
{
    [FirestoreData]
    public class Cliente: Base
    {
        /// <summary>
        /// Client name
        /// </summary>
        [FirestoreProperty]
        public string Nombres { get; set; }

        /// <summary>
        /// Client surname
        /// </summary>
        [FirestoreProperty]
        public string Apellidos { get; set; }

        /// <summary>
        /// Client age
        /// </summary>        
        [FirestoreProperty]
        public int Edad { get; set; }

        /// <summary>
        /// Client birthday
        /// </summary>
        [FirestoreProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Nacimiento { get; set; }

    }

    public class Base
    {
        public string Id { get; set; }
    }
}
