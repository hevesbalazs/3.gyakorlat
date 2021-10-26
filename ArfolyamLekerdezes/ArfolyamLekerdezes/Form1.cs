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
using System.Windows.Forms.DataVisualization.Charting;

namespace ArfolyamLekerdezes
{
    
    
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        
                
        public Form1()
        {
            InitializeComponent();
            RefreshData();            
        }

        private void RefreshData()
        {
            Rates.Clear();
            CallWebservice();
            dataGridView1.DataSource = Rates;
            ProcessXml();
            CreateChart();
        }

        private void CreateChart()
        {
            chartRateData.DataSource = Rates;
            var elsoelem = chartRateData.Series[0];
            elsoelem.ChartType = SeriesChartType.Line;
            elsoelem.XValueMember = "Date";
            elsoelem.YValueMembers = "Value";
            elsoelem.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
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
                //currencyNames = comboBox1.SelectedItem.ToString(),
                // startDate = "2020-01-01",
                startDate = dateTimePicker1.Value.ToString(),
                // endDate = "2020-06-30"
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;
            return result.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();            
        }

        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();           
        }
    }
}
