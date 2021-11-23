using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gyartosor.Entities;

namespace Gyartosor
{
    public partial class Form1 : Form
    {
        List<Ball> _balls = new List<Ball>();

        public BallFactory _factory;
        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }



        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
