using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DependencyService
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TextToSpeechDemo : ContentPage
    {
        public TextToSpeechDemo()
        {
            InitializeComponent();
            var stack = new StackLayout();
            var speak = new Button
            {
                Text = "Hello,Valdez Forms!",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            speak.Clicked += (sender, e) =>
            {
                Xamarin.Forms.DependencyService.Get<ITextToSpeech>().Speak("Hello Valdez from Xamarin Forms");
            };

            stack.Children.Add(speak);
            Content = speak;
        }
    }
}