using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Input.Inking;
using Windows.UI.Core;
using Windows.UI;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MNIST.Client.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CNTKGraphModel model;
        public MainPage()
        {
            this.InitializeComponent();

            Inker.InkPresenter.InputDeviceTypes =
            CoreInputDeviceTypes.Pen |
            CoreInputDeviceTypes.Touch |
            CoreInputDeviceTypes.Mouse;

            InkDrawingAttributes drawingAttributes = new InkDrawingAttributes();
            drawingAttributes.Color = Colors.Black;
            drawingAttributes.Size = new Size(20, 20);
            drawingAttributes.IgnorePressure = false;
            drawingAttributes.FitToCurve = true;
            drawingAttributes.PenTip = PenTipShape.Circle;
            //ida = InkDrawingAttributes.CreateForPencil();

            
            //drawingAttributes.PencilProperties.Opacity = 1;
            Inker.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);

            this.Loaded += MainPage_Loaded;
            
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///mnist.onnx"));
            model = await CNTKGraphModel.CreateCNTKGraphModel(file);
        }

        private async void Recognize_Click(object sender, RoutedEventArgs e)
        {
            var bitmap = Inker.GetCropedSoftwareBitmap(newWidth: 227, newHeight: 227, keepRelativeSize: true);
            var frame = VideoFrame.CreateWithSoftwareBitmap(bitmap);
            var input = new CNTKGraphModelInput() { Input3 = frame };

            var output = await model.EvaluateAsync(input);
            var highestValue = output.Plus258_Output_0.OrderByDescending(x => x).ToArray();
            var guessedValue = "N/A";
            for (int i = 0; i < output.Plus258_Output_0.Count; i++)
            {
                if (output.Plus258_Output_0[i] == highestValue[0])
                    guessedValue = i.ToString();
            }
            //var guessedTag = output.Plus258_Output_0.OrderBy(x => x).First();
            //var guessedPercentage = output.Plus258_Output_0..loss.OrderByDescending(kv => kv.Value).First().Value;
            RecognizedInk.Text = guessedValue;
        }



        private void Erase_Click(object sender, RoutedEventArgs e)
        {
            RecognizedInk.Text = "";
            Inker.InkPresenter.StrokeContainer.Clear();
        }
    }
}
