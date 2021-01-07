using Google.Cloud.Firestore;
using Newtonsoft.Json;
using SampleAPIProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleAPIProject.FirebaseContext
{
    public class BaseRepository
    {
        private string collectionName;
        public FirestoreDb fireStoreDb;
        public BaseRepository(string CollectionName)
        {
            var fileName = $"sample-firebase-apps-87731-firebase-adminsdk-ishjc-d7f481603f.json";
            string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", filePath);
            var x = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
            fireStoreDb = FirestoreDb.Create("sample-firebase-apps-87731");
            collectionName = CollectionName;
        }


        public T Add<T>(T record) where T : Base
        {
            CollectionReference colRef = fireStoreDb.Collection(collectionName);
            DocumentReference doc = colRef.AddAsync(record).GetAwaiter().GetResult();
            record.Id = doc.Id;
            return record;
        }

        public bool Update<T>(T record) where T : Base
        {
            DocumentReference recordRef = fireStoreDb.Collection(collectionName).Document(record.Id);
            recordRef.SetAsync(record, SetOptions.MergeAll).GetAwaiter().GetResult();
            return true;
        }

        public bool Delete<T>(T record) where T : Base
        {
            DocumentReference recordRef = fireStoreDb.Collection(collectionName).Document(record.Id);
            recordRef.DeleteAsync().GetAwaiter().GetResult();
            return true;
        }
        public T Get<T>(T record) where T : Base
        {
            DocumentReference docRef = fireStoreDb.Collection(collectionName).Document(record.Id);
            DocumentSnapshot snapshot = docRef.GetSnapshotAsync().GetAwaiter().GetResult();
            if (snapshot.Exists)
            {
                T target = snapshot.ConvertTo<T>();
                target.Id = snapshot.Id;
                return target;
            }
            else
            {
                return null;
            }
        }

        public List<T> GetAll<T>() where T : Base
        {
            Query query = fireStoreDb.Collection(collectionName);
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> currentDoc = documentSnapshot.ToDictionary();
                    // TODO: Consider adding JSON error handler since Firestore returns DateTime as Timestamp
                    if (currentDoc.Values.Any(val => val.GetType().Name == "Timestamp"))
                    {
                        var targetEntry = currentDoc.FirstOrDefault(val => val.Value.GetType().Name == "Timestamp");
                        currentDoc[targetEntry.Key] = ((Timestamp)targetEntry.Value).ToDateTime();
                    }
                    string json = JsonConvert.SerializeObject(currentDoc);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }
            return list;
        }

        public List<T> QueryRecords<T>(Query query) where T : Base
        {
            QuerySnapshot querySnapshot = query.GetSnapshotAsync().GetAwaiter().GetResult();
            List<T> list = new List<T>();
            foreach (DocumentSnapshot documentSnapshot in querySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {
                    Dictionary<string, object> currentDoc = documentSnapshot.ToDictionary();
                    string json = JsonConvert.SerializeObject(currentDoc);
                    T newItem = JsonConvert.DeserializeObject<T>(json);
                    newItem.Id = documentSnapshot.Id;
                    list.Add(newItem);
                }
            }
            return list;
        }

    }
}
