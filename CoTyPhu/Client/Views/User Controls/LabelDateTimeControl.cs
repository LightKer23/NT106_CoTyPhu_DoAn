using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Views.User_Controls
{
    public partial class LabelDateTimeControl : UserControl
    {
        public LabelDateTimeControl()
        {
            InitializeComponent();
        }

        [Category("Custom")]
        [Description("Text hiển thị trên label.")]
        public string LabelText
        {
            get => label1.Text;
            set => label1.Text = value;
        }
    }
}
