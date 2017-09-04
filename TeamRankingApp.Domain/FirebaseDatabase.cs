using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System.Threading.Tasks;

namespace TeamRankingApp.Domain
{
    public class FirebaseDatabase
    {

        const string firebaseAPIKey = "AIzaSyDHPF-pa-9S6CDUbY3OZdoNmKxD7HLhqGs";
        const string firebaseDB = "https://team-ranking.firebaseio.com";

        private FirebaseAuthLink _auth = null;
        private FirebaseClient _client = null;

        public FirebaseDatabase() { }

        public bool Login(string username, string password)
        {
            try//do better??
            {
                _client = new FirebaseClient(firebaseDB);
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIKey));
                _auth = authProvider.SignInWithEmailAndPasswordAsync(username, password).Result;
                return true;
            }
            catch (Exception)
            {
                return false;
            }    
        }

        public T GetSingleAsync<T>(string path)
        {
            var fb = _client
              .Child(path)
              .OnceAsync<T>().Result;//.WithAuth(_auth.FirebaseToken)

            return fb.FirstOrDefault().Object;
            
            
            //.OrderByKey()
            //.LimitToFirst(2)
            //.OnceAsync<Object>().Result;

            
        }

        public async void InsertAsync(string path, object o)
        {
            if (_auth == null)
            {
                throw new InvalidOperationException("Must login first");
                return;
            }
            
            await _client.Child(path)
                .WithAuth(_auth.FirebaseToken)
                .PostAsync(o, false);
        }

        public async void UpdateAsync(string path, object o)
        {
            if (_auth == null) 
            {
                throw new InvalidOperationException("Must login first");
                return;
            }

            await _client.Child(path)
                .WithAuth(_auth.FirebaseToken)
                .PutAsync(o);
        }

        //public FirebaseDatabase(string username, string password)
        //{
        //    var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseAPIKey));

        //    var auth = authProvider.SignInWithEmailAndPasswordAsync(username,password).Result;
            
        //    //// The auth Object will contain auth.User and the Authentication Token from the request
        //    //System.Diagnostics.Debug.WriteLine(auth.FirebaseToken);

        //    var firebase = new FirebaseClient(firebaseDB);
        //    //var items = firebase
        //    //  .Child("")
        //    //  .WithAuth(auth.FirebaseToken)
        //    //  .OnceAsync<Firebase_Player>().Result;
        //    //  //.OrderByKey()
        //    //  //.LimitToFirst(2)
        //    //  //.OnceAsync<Object>().Result;

        //    //foreach (var item in items)
        //    //{
        //    //    Console.WriteLine($"{item.Key} name is {item.Object.Name}");
        //    //}

        //    var a = new Firebase_Player()
        //    {
        //        PlayerID = 0,
        //        ImagePath = "Guy.png",
        //        Name = "Guy"
        //    };
        //    var b = new Firebase_Player()
        //    {
        //        PlayerID = 1,
        //        ImagePath = "James.png",
        //        Name = "James"
        //    };

        //    //var auth2 = authProvider.SignInWithEmailAndPasswordAsync(username, password).Result;

        //    var f = firebase
        //      .Child("People")
        //      .WithAuth(auth.FirebaseToken)
        //      .PostAsync(a,false).Result;

        //    var g = firebase
        //        .Child("People")
        //        .WithAuth(auth.FirebaseToken)
        //        .PostAsync(b,false).Result;
        //}



    }


    public class Firebase_Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }

    }
}