using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwaitTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Waiting...";
            DoAsyncAwait1("Lu");

            //var abc = await DoAsyncAwait("Lu");
            //label1.Text = abc;
        }

        public async Task<string> DoAsyncAwait(string name)
        {
            var abc = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Hello " + name;
            });

            return await abc;
        }

        public async void DoAsyncAwait1(string name)
        {
            var abc = await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Hello " + name;
            });

            label1.Text = abc;
        }

        private void DoChangedAsync(string name)
        {
            Task.Factory.StartNew(() => DoChanged(name)).ContinueWith(x => label1.Text = x.Result, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private string DoChanged(string name)
        {
            Thread.Sleep(2000);
            return "Hello " + name;
        }
    }
}
