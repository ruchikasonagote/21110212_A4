namespace Activity2
{
    public partial class Form1 : Form
    {
        private DateTime targetTime;
        private Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000; // 1 second
            timer1.Tick += timer1_Tick;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(timeInput.Text, out _))
            {
                timeInput.BackColor = Color.LightGreen; // Valid time
            }
            else
            {
                timeInput.BackColor = Color.LightCoral; // Invalid time
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(timeInput.Text, out DateTime parsedTime))
            {
                if (parsedTime <= DateTime.Now)
                {
                    MessageBox.Show("Please enter a future time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                targetTime = parsedTime;
                timer1.Start();

            }
            else
            {
                MessageBox.Show("Invalid time format. Use HH:MM:SS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256));

            if (DateTime.Now.ToLongTimeString() == targetTime.ToLongTimeString())
            {
                timer1.Stop();
                MessageBox.Show("⏰⏰ Alarm ringing! Wake up! ⏰⏰", "Alarm");
            }
        }
    }
}
