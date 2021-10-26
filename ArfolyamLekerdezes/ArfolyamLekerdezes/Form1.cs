using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArfolyamLekerdezes.MnbServiceReference;
using ArfolyamLekerdezes.Entities;
using System.Xml;

namespace ArfolyamLekerdezes
{
    
    
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
                
        public Form1()
        {
            InitializeComponent();
            CallWebservice();
            dataGridView1.DataSource = Rates;
            ProcessXml();
        }

        private void ProcessXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(CallWebservice());

            foreach (XmlElement item in xml.DocumentElement)
            {
                var Rate = new RateData();
                Rate.Date = DateTime.Parse(item.GetAttribute("date"));

                var childElement = (XmlElement)item.ChildNodes[0];
                Rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0) Rate.Value = value / unit;

                Rates.Add(Rate);
            }
        }

        private string CallWebservice()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
            return result.ToString();
        }
    }
}
