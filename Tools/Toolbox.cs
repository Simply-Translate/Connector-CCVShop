using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public partial class Toolbox : Form
    {
        public Toolbox()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            string jsonUrl = InputTextBox.Text;
            if(!CheckWebsiteIsOk(jsonUrl))
                return;
            string json = RetrieveWebsite(jsonUrl);
            string codeResult = ConvertJson(json);
            OutputTextBox.Text = codeResult;
        }

        private string ConvertJson(string jsonResult)
        {
            var json = JsonConvert.DeserializeObject<Convert.Data.RootObject>(jsonResult);
            var v = new Convert.GenerateFileFromJson(json);
            return v.Convert();
        }

        private string RetrieveWebsite(string jsonUrl)
        {
            Uri jsonUri = new Uri(jsonUrl);

            using (var client = new HttpClient())
            {
                var httpResponse = client.GetAsync(jsonUri).Result;
                var response = httpResponse.Content.ReadAsStringAsync().Result;
                return response;
            }
        }

        private bool CheckWebsiteIsOk(string jsonUrl)
        {
            if(!jsonUrl.Contains("securearea.eu"))
            {
                MessageBox.Show("Wrong host, must be from securearea.eu");
                return false;
            }

            if(!jsonUrl.EndsWith(".json"))
            {
                MessageBox.Show("The url is not a json file.");
                return false;
            }

            Uri url;
            if(!Uri.TryCreate(jsonUrl, UriKind.Absolute, out url))
            {
                MessageBox.Show("The url is not in a good format, cannot be converted to an Uri object.");
                return false;
            }
            return true;
        }

        private void CopyToClipboardButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(OutputTextBox.Text);
        }
    }
}
