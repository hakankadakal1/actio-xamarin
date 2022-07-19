
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinalProject.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomDialog 
    {
        public CustomDialog(string header,string content)
        {
            InitializeComponent();
            dialogHeader.Text = header;
            dialogContent.Text = content;
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}