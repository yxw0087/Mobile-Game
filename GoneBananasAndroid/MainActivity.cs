using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CocosSharp;
using Microsoft.Xna.Framework;
using Game;

namespace Game
{
    [Activity(
        Label = "Game",
        AlwaysRetainTaskState = true,
        Icon = "@drawable/Icon",
        Theme = "@android:style/Theme.NoTitleBar",
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleInstance,
        MainLauncher = true,
        ConfigurationChanges =  ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)
    ]
    public class MainActivity : AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var application = new CCApplication();
            application.ApplicationDelegate = new GameApplicationDelegate();
            SetContentView(application.AndroidContentView);
            application.StartGame();
        }
    }
}