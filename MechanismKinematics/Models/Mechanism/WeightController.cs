using System;
using System.Drawing;

namespace MechanismKinematics
{
    public class WeightController
    {
        private readonly MainFormModel _mainFormModel;
        private readonly MechanismPainter _mechanismPainter;
        private Rectangle _rectangle;
        private readonly Point _center;

        public WeightController(
            MainFormModel mainFormModel, 
            MechanismPainter mechanismPainter,
            Rectangle rectangle)
        {
            _mainFormModel = mainFormModel;
            _mechanismPainter = mechanismPainter;
            _rectangle = rectangle;
            _center = _mainFormModel.Center;
        }

        public void RefreshWeightOne(double omega) => RefreshWeight(isRadiusOne: true, omega);

        public void RefreshWeightTwo(double omega) => RefreshWeight(isRadiusOne: false, omega);

        private int GetWeightHeight(bool isRadiusOne) => isRadiusOne ? 40 : 160; // problem with second radius height

        // mb some class having radius and stuff in the future used like Weight One = new Weight(height: 40, goingUp: true);
        private int GetWeightLowestHeight(int radius, bool isRadiusOne) => _center.Y + radius + GetWeightHeight(isRadiusOne);

        private int GetWeightCurrentLowestHeight(int radius, bool isRadiusOne)
        {
            const int panelHeight = 139;

            return _mainFormModel.PictureBoxHeight - panelHeight + (radius - GetWeightHeight(isRadiusOne) * 2) * 2;
        }

        private int GetWeightHorizontalDistance(int radius) => _center.X + radius - 8;

        private int GetWeightLowestHeightDelta(double weightLowestHeight, double linearDistance)
        {
            return Convert.ToInt32(weightLowestHeight + linearDistance);
        }

        private void DrawWeightLine(int radius, int weightLowestPossiblePoint)
        {
            _mechanismPainter.DrawLine
                (Convert.ToInt32(_center.X) + radius,
                 Convert.ToInt32(_center.Y),
                 Convert.ToInt32(_center.X) + radius,
                 weightLowestPossiblePoint);
        }

        private void RefreshWeight(bool isRadiusOne, double omega)
        {
            const int weightHeightSize = 31, weightWidth = 15;

            int radius = isRadiusOne ? _mainFormModel.RadiusOne : _mainFormModel.RadiusTwo,
                weightLowestHeightPossible = _mainFormModel.PictureBoxHeight - weightHeightSize;

            double linearSpeed = omega * radius,
                   linearDistance = linearSpeed * _mainFormModel.Time;

            bool omegaPositive = omega > 0;

            MechanismController.SignByBoolean(ref linearDistance, isRadiusOne, inverse: true);
            radius *= isRadiusOne ? 1 : -1;

            _mechanismPainter.SetPenDashStyle(solid: true);
            SetWeight(isRadiusOne, radius, weightLowestHeightPossible, linearDistance, omegaPositive);

            _rectangle.Size = new Size(weightWidth, weightWidth * 2);
            _mechanismPainter.DrawEllipse(_rectangle, isRectangle: true);
        }

        private void SetWeight(bool isRadiusOne, int radius, int weightLowestHeightPossible,
            double linearDistance, bool omegaPositive)
        {
            int weightLowestHeight = GetWeightLowestHeight(radius, isRadiusOne);

            if (GetWeightLowestHeightDelta(weightLowestHeight, linearDistance) > _center.Y)
            {
                linearDistance = Math.Abs(linearDistance);
                MechanismController.SignByBoolean(ref linearDistance, omegaPositive);

                SetWeightParameters(isRadiusOne, radius, weightLowestHeightPossible, weightLowestHeight, ref linearDistance);
            }
            else _rectangle.Location = new Point(GetWeightHorizontalDistance(radius), Convert.ToInt32(_center.Y));
        }

        private void SetWeightParameters(bool isRadiusOne, 
            int radius, int weightLowestHeightPossible, int weightLowestHeight,
            ref double linearDistance)
        {

            int weightLowestHeightDelta = GetWeightLowestHeightDelta(weightLowestHeight, linearDistance);

            bool weightWithinWheel = isRadiusOne ?
                    weightLowestHeightDelta > GetWeightCurrentLowestHeight(radius, isRadiusOne) :
                    weightLowestHeightDelta < weightLowestHeightPossible;

            if (weightWithinWheel)
            {
                MechanismController.SignByBoolean(ref linearDistance, isRadiusOne, inverse: true);
                weightLowestHeightDelta = GetWeightLowestHeightDelta(GetWeightLowestHeight(radius, isRadiusOne), linearDistance);

                SetWeightDrawing(radius, weightLowestHeightDelta);
            }
            else SetWeightDrawing(radius, weightLowestHeightPossible);
        }

        private void SetWeightDrawing(int radius, int weightLowestPoint)
        {
            DrawWeightLine(radius, weightLowestPoint);
            _rectangle.Location = new Point(GetWeightHorizontalDistance(radius), weightLowestPoint);
        }
    }
}
