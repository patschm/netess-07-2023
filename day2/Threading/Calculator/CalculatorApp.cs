namespace Calculator
{
    public partial class CalculatorApp : Form
    {
        private readonly SynchronizationContext mainThread;
        public CalculatorApp()
        {
            InitializeComponent();
            mainThread = SynchronizationContext.Current!;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var main = SynchronizationContext.Current;
            if (int.TryParse(txtA.Text, out int a) && int.TryParse(txtB.Text, out int b))
            {
                //int result = LongAdd(a, b);
                //UpdateAnswer(result);

                //Task.Run(() => LongAdd(a, b))
                //    .ContinueWith(pt => {
                //        mainThread.Post(UpdateAnswer, pt.Result);
                //    });

                //int result = await Task.Run(()=>LongAdd(a,b));
                //int result = await LongAddAsync(a, b);

                int result = DoeIetsAsync(a, b).Result;  // Dead lock
                UpdateAnswer(result);

            }    
        }

        private async Task<int> DoeIetsAsync(int a, int b)
        {
            return await LongAddAsync(a, b);
        }

        private void UpdateAnswer(object result)
        {
            lblAnswer.Text = result.ToString();
        }
        private int LongAdd(int a, int b)
        {
            Task.Delay(10000).Wait();
            return a + b;
        }
        private Task<int> LongAddAsync(int a, int b)
        {
            return Task.Run(() => LongAdd(a,b));
        }
    }
}