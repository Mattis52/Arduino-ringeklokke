using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Maker.RemoteWiring;
using Microsoft.Maker.Firmata;
using Microsoft.Maker.Serial;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Arduino_ringeklokke
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page {
        UsbSerial connection;
        RemoteDevice arduino;
        // Arduino IDer: USB\VID_1A86&PID_7523

        public MainPage() {
            this.InitializeComponent();

            connection = new UsbSerial("VID_1A86", "PID_7523");
            arduino = new RemoteDevice(connection);
            arduino.DeviceReady += MyDeviceReadyCallback;
            arduino.DigitalPinUpdated += ButtonUpdateCallBack;
            //connection.ConnectionEstablished += OnConnectionEstablished;
            connection.begin(57600, SerialConfig.SERIAL_8N1);
        }

        private void OnConnectionEstablished() {
            arduino.pinMode(2, PinMode.INPUT);
            arduino.DigitalPinUpdated += ButtonUpdateCallBack;
        }

        private void MyDeviceReadyCallback() {
            arduino.pinMode(2, PinMode.INPUT);
            arduino.pinMode(3, PinMode.INPUT);
        }

        private void ButtonUpdateCallBack(byte pin, PinState state) {
            Debug.WriteLine("Button pressed");
            string title = "Ding dong!";
            string content = "Noen står bak deg!";
            //string image = "BILDEbilde";
            //string logo = "LOGObilde";

            ToastVisual visual = new ToastVisual()
            {
                BindingGeneric = new ToastBindingGeneric()
                {
                    Children = {
                        new AdaptiveText() {Text = title},
                        new AdaptiveText() {Text = content},
                    },

                    //AppLogoOverride = new ToastGenericAppLogo() {
                    //    Source = logo,
                    //    HintCrop = ToastGenericAppLogoCrop.Circle
                    //}
                }
            };
            ToastContent toastContent = new ToastContent()
            {
                Visual = visual,
                //Actions = actions,

                // Arguments when the user taps body of toast
                //Launch = new QueryString() {
                //    { "action", "viewConversation" },
                //    { "conversationId", conversationId.ToString() }
                //}.ToString()
            };

            // And create the toast notification
            ToastNotification notification = new ToastNotification(toastContent.GetXml());

            notification.ExpirationTime = DateTime.Now.AddMinutes(10);

            // And then send the toast
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            string title = "Ding dong!";
            string content = "Noen står bak deg!";
            //string image = "BILDEbilde";
            //string logo = "LOGObilde";

            ToastVisual visual = new ToastVisual() {
                BindingGeneric = new ToastBindingGeneric() {
                    Children = {
                        new AdaptiveText() {Text = title},
                        new AdaptiveText() {Text = content},
                    },

                    //AppLogoOverride = new ToastGenericAppLogo() {
                    //    Source = logo,
                    //    HintCrop = ToastGenericAppLogoCrop.Circle
                    //}
                }
            };
            ToastContent toastContent = new ToastContent() {
                Visual = visual,
                //Actions = actions,

                // Arguments when the user taps body of toast
                //Launch = new QueryString() {
                //    { "action", "viewConversation" },
                //    { "conversationId", conversationId.ToString() }
                //}.ToString()
            };

            // And create the toast notification
            ToastNotification notification = new ToastNotification(toastContent.GetXml());

            notification.ExpirationTime = DateTime.Now.AddMinutes(10);

            // And then send the toast
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }
}
