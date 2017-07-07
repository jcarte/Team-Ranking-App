using Android.Content.PM;
using Android.App;
using Android.OS;
using Dropbox.CoreApi.Android;
using Dropbox.CoreApi.Android.Session;
using Android.Widget;
using System;
using Android.Content;
using System.IO;
using System.Threading.Tasks;

namespace TeamRankingApp.Android
{
    [Activity(Label = "DropboxSync", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class DropboxSync : Activity
    {
        //UI
        TextView txtAccessToken;
        EditText editApiKey;
        Button btnLink;
        Button btnDownload;
        Button btnUpload;
        TextView txtFileStatus;

        //Dropbox API
        string AppKey = "oxm5fu0vw2l41he";
        
        DropboxApi dropboxApi;


        //File Info
        private static readonly string fileName = "Jicola.png";
        private static string filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), fileName);


        private string AccessToken
        {
            get
            {
                ISharedPreferences sharedprefs = GetSharedPreferences("prefs_file", FileCreationMode.Private);
                return sharedprefs.GetString("accessToken", string.Empty);
            }
            set
            {
                ISharedPreferences sharedprefs = GetSharedPreferences("prefs_file", FileCreationMode.Private);
                sharedprefs.Edit().PutString("accessToken", value).Commit();
            }
        }

        private string AppSecret
        {
            get
            {
                ISharedPreferences sharedprefs = GetSharedPreferences("prefs_file", FileCreationMode.Private);
                return sharedprefs.GetString("apiSecret", string.Empty);
            }
            set
            {
                ISharedPreferences sharedprefs = GetSharedPreferences("prefs_file", FileCreationMode.Private);
                sharedprefs.Edit().PutString("apiSecret", value).Commit();
            }
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DropBoxSync);

            txtAccessToken = FindViewById<TextView>(Resource.Id.dbs_AccessToken);
            editApiKey = FindViewById<EditText>(Resource.Id.dbs_SecretKey);
            btnLink = FindViewById< Button > (Resource.Id.dbs_Link);
            btnDownload = FindViewById< Button > (Resource.Id.dbs_Download);
            btnUpload = FindViewById< Button > (Resource.Id.dbs_Upload);
            txtFileStatus = FindViewById< TextView > (Resource.Id.dbs_FileStatus);


            //When link button clicked, redirect to dropbox login, get back access code
            btnLink.Click += (s, e) =>
            {
                if (!string.IsNullOrEmpty(editApiKey.Text))
                {
                    AppSecret = editApiKey.Text;
                    AppKeyPair appKeys = new AppKeyPair(AppKey, AppSecret);
                    AndroidAuthSession session = new AndroidAuthSession(appKeys);
                    dropboxApi = new DropboxApi(session);
                    (dropboxApi.Session as AndroidAuthSession).StartOAuth2Authentication(this);
                }
                else
                {
                    Toast.MakeText(this, "No Key Given", ToastLength.Short).Show();
                }
            };

            //Download file from dropbox, overwrite
            btnDownload.Click += (s, e) =>
            {
                Task.Run(() =>
                {
                    try
                    {
                        using (var output = File.OpenWrite(filePath))
                        {
                            // Gets the file from Dropbox and saves it to the local folder
                            var info = dropboxApi.GetFile(fileName, null, output, null);
                            RunOnUiThread(() => txtFileStatus.Text = info.Metadata.Modified);
                            RunOnUiThread(() => Toast.MakeText(this, fileName + " Downloaded", ToastLength.Long).Show());
                        }

                    }
                    catch (Exception x)
                    {
                        RunOnUiThread(() => Toast.MakeText(this, fileName + "Failed: " + x.Message, ToastLength.Long).Show());
                    }
                });
            };

            //Upload current file to dropbox, overwrite
            btnUpload.Click += (s, e) =>
            {
                Task.Run(() =>
                {
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            using (var input = File.OpenRead(filePath))
                            {
                                // Gets the file from Dropbox and saves it to the local folder
                                var info = dropboxApi.PutFileOverwrite(fileName, input, input.Length, null);
                                RunOnUiThread(() => txtFileStatus.Text = info.Modified);
                                RunOnUiThread(() => Toast.MakeText(this, fileName + " Uploaded", ToastLength.Long).Show());
                            }
                        }
                        catch (Exception x)
                        {
                            RunOnUiThread(() => Toast.MakeText(this, fileName + "Failed: " + x.Message, ToastLength.Long).Show());
                        }
                    }
                    else
                    {
                        RunOnUiThread(() => Toast.MakeText(this, "File Not Found", ToastLength.Long).Show());
                    }

                });
            };
        }

        protected override void OnStart()
        {
            base.OnStart();

            //Has api secret key been stored already?
            string key = AppSecret;
            if(key != string.Empty)
            {
                editApiKey.Text = key;
            }

            //Has access token been stored previously?
            string token = AccessToken;
            if (token != string.Empty)
            {
                //restore key and login to api using it
                txtAccessToken.Text = token;
                AppKeyPair appKeys = new AppKeyPair(AppKey, AppSecret);
                AndroidAuthSession session = new AndroidAuthSession(appKeys);
                session.OAuth2AccessToken = token;
                dropboxApi = new DropboxApi(session);

                btnDownload.Enabled = true;
                btnUpload.Enabled = true;
            }
            else
            {
                btnDownload.Enabled = false;
                btnUpload.Enabled = false;
            }

            //Update the file status text
            if(File.Exists(filePath))
            {
                FileInfo fi = new FileInfo(filePath);
                txtFileStatus.Text = fi.LastWriteTime.ToLongDateString() + fi.LastWriteTime.ToLongTimeString();
            }
            else
            {
                txtFileStatus.Text = "File Not Found";
            }
        }


        protected override void OnResume()
        {
            base.OnResume();

            var session = dropboxApi.Session as AndroidAuthSession;

            //I think will be false if already have access token so exit before finishing
            if (!session.AuthenticationSuccessful())
                return;

            //Come back from logging in to dropbox, session now has auth token, finish process and store token
            try
            {
                session.FinishAuthentication();
                string token = session.OAuth2AccessToken;
                AccessToken = token;//save
                txtAccessToken.Text = token;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
            
        }

        

    }
}






