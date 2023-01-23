using System;
using System.Windows.Forms;

namespace MechanismKinematics
{
    public partial class KinematicForm : Form, IKinematicFormView
    {
        public KinematicForm()
        {
            InitializeComponent();
        }

        #region Interface implementation

        public string MaskedTextBoxText { get; set; }
        public event EventHandler<EventArgs> ButtonOkClick;

        #endregion

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            MaskedTextBoxText = maskedTextBox.Text;
            ButtonOkClick?.Invoke(this, EventArgs.Empty);
            Close();
        }
    }
}