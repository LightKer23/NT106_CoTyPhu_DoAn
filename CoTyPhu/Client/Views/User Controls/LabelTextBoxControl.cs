using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Client.Views.User_Controls
{
    public partial class LabelTextBoxControl : UserControl
    {
        public LabelTextBoxControl()
        {
            InitializeComponent();
        }

        [Category("Custom")]
        [Description("Text hiển thị trên label.")]
        public string LabelText
        {
            get => lblName.Text;
            set => lblName.Text = value;
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override string Text
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        [Category("Custom")]
        [Description("ReadOnly của TextBox.")]
        public bool TextBoxReadOnly
        {
            get => textBox1.ReadOnly;
            set => textBox1.ReadOnly = value;
        }

        [Category("Custom")]
        [Description("Ký tự hiển thị khi nhập password.")]
        public char PasswordChar
        {
            get => textBox1.PasswordChar;
            set => textBox1.PasswordChar = value;
        }

        [Category("Custom")]
        [Description("Sự kiện khi TextBox thay đổi nội dung.")]
        public event EventHandler TextBoxTextChanged
        {
            add => textBox1.TextChanged += value;
            remove => textBox1.TextChanged -= value;
        }
    }
}
