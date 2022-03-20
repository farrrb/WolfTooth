using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.ApplicationModel.Core;
using Windows.Devices.Radios;
using Windows.UI.Popups;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WolfTooth
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static async void ShowError(string error)
        {
            MessageDialog messageDialog = new MessageDialog(error, "Error!");
            await messageDialog.ShowAsync();
        }

        static async void GetRadios()
        {
            var accessLevel = await Radio.RequestAccessAsync();

            if (accessLevel != RadioAccessStatus.Allowed)
            {
                ShowError("Insufficient application rights (access to bluetooth radio is missing)!");
                return;
            }

            var radios = await Radio.GetRadiosAsync();

            for (int i = 0; i < radios.Count; i++)
            {
                var radio = radios[i];
                if (radio.Name.Equals("Bluetooth"))
                {
                    // toggle bluetooth
                    await radio.SetStateAsync(RadioState.Off);
                    await radio.SetStateAsync(RadioState.On);

                    // kill the app
                    CoreApplication.Exit();
                    return;
                }
            }
            ShowError("Bluetooth radio button not found!");
        }

        public MainPage()
        {
            this.InitializeComponent();
            GetRadios();
        }
    }
}
