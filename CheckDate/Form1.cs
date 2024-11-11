using System.Globalization;

namespace CheckDate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CheckDateShamsi(maskedTextBox1.Text))
                {
                    label1.Text = CheckDateShamsi(maskedTextBox1.Text).ToString();
                }
                else
                {
                    label1.Text = "تاریخ نامعتبر است";
                }
            }
        }

        private static bool CheckDateShamsi(string shamsi)
        {
            string year, month, day;

            if (!TryGetDateShamsi(shamsi, out year, out month, out day))
            {
                //label1.Text = "فرمت تاریخ نامعتبر است.";
                return false;
            }

            PersianCalendar pc = new PersianCalendar();
            try
            {
                DateTime dt = pc.ToDateTime(int.Parse(year), int.Parse(month), int.Parse(day), 0, 0, 0, 0);
                //label1.Text = "تاریخ صحیح است";
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                //label1.Text = "تاریخ نامعتبر است";
                return false;
            }
        }

        // تابع جدید برای استخراج اجزای تاریخ با بررسی خطا
        private static bool TryGetDateShamsi(string shamsi, out string year, out string month, out string day)
        {
            if (shamsi.Length != 10 || !int.TryParse(shamsi.Substring(0, 4), out int y) || !int.TryParse(shamsi.Substring(5, 2), out int m) || !int.TryParse(shamsi.Substring(8, 2), out int d))
            {
                year = month = day = null;
                return false;
            }

            year = y.ToString();
            month = m.ToString("00"); // اطمینان از دو رقمی بودن ماه
            day = d.ToString("00");   // اطمینان از دو رقمی بودن روز

            return true;
        }
    }
}
