using System;
using System.Windows.Forms;

namespace MechanismKinematics
{
    public partial class GeometricForm : Form, IGeometricFormView
    {
        public GeometricForm() => InitializeComponent();

        #region Interface implementation

        public int RadiusOne { get; set; }
        public int RadiusTwo { get; set; }
        public event EventHandler<EventArgs> GeometricFormLoad;
        public event EventHandler<EventArgs> ButtonOKClick;

        #endregion

        private void GeometricForm_Load(object sender, EventArgs e)
        {
            GeometricFormLoad?.Invoke(this, EventArgs.Empty);
            numericUpDownFirstWheelRadius.Value = RadiusOne;
            numericUpDownSecondWheelRadius.Value = RadiusTwo;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            RadiusOne = (int)numericUpDownFirstWheelRadius.Value;
            RadiusTwo = (int)numericUpDownSecondWheelRadius.Value;
            ButtonOKClick?.Invoke(this, EventArgs.Empty);
            Close();
        }
    }
}