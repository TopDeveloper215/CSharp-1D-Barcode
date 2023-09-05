using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace GenBarcode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var formatList = new List<KeyValuePair<string, BarcodeFormat>>();
            formatList.Add(new KeyValuePair<string, BarcodeFormat>(BarcodeFormat.CODE_128.ToString(), BarcodeFormat.CODE_128));
            formatList.Add(new KeyValuePair<string, BarcodeFormat>(BarcodeFormat.CODE_39.ToString(), BarcodeFormat.CODE_39));
            formatList.Add(new KeyValuePair<string, BarcodeFormat>(BarcodeFormat.CODE_93.ToString(), BarcodeFormat.CODE_93));
            // formatList.Add(new KeyValuePair<string, BarcodeFormat>(BarcodeFormat.QR_CODE.ToString(), BarcodeFormat.QR_CODE));

            cboFormat.DisplayMember = "key";
            cboFormat.ValueMember = "value";
            cboFormat.DataSource = formatList;
            cboFormat.SelectedIndex = 0;
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtEncode.Text)) return;

                BarcodeWriter writter = new BarcodeWriter()
                {
                    Format = (BarcodeFormat)cboFormat.SelectedValue //  BarcodeFormat.CODE_128
                };

                // 填入Options，當有設定的話。
                if (!String.IsNullOrWhiteSpace(txtWidth.Text))
                    writter.Options.Width = int.Parse(txtWidth.Text.Trim());

                if (!String.IsNullOrWhiteSpace(txtHeight.Text))
                    writter.Options.Height = int.Parse(txtHeight.Text.Trim());

                // if (!String.IsNullOrWhiteSpace(txtMargin.Text))
                    // writter.Options.Margin = int.Parse(txtMargin.Text.Trim());

                writter.Options.PureBarcode = chkPureBarcode.Checked;

                // 產生barcode
                pictureBox1.Image = writter.Write(txtEncode.Text);
                pictureBox1.Size = pictureBox1.Image.Size;
                statusLabel1.Text = "Barcode image size:" + pictureBox1.Image.Size.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null) return;

                BarcodeReader reader = new BarcodeReader();
                var result = reader.Decode((Bitmap)pictureBox1.Image);
               // if (result != null)
                   //  txtDecode.Text = result.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
