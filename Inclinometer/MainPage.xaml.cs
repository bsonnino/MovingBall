using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MovingBall
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var inclinometer = Inclinometer.GetDefault();
            if (inclinometer == null)
            {
                return;
            }
            inclinometer.ReadingChanged += async (s1, e1) =>
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    var position = Translation.X;
                    var newPosition = position + e1.Reading.RollDegrees;
                    if (newPosition < -(ActualWidth - 80) / 2)
                        newPosition = -(ActualWidth - 80) / 2;
                    if (newPosition > (ActualWidth - 80) / 2)
                        newPosition = (ActualWidth - 80) / 2;
                    Translation.X = newPosition;
                });
            };

        }
    }
}
