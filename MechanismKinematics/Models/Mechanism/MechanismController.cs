using MechanismKinematicsWinFormsMVP;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MechanismKinematics
{
    public class MechanismController
    {
        private const int HeightIndent = 10;
        private readonly MainFormModel _mainFormModel;
        private readonly MechanismPainter _mechanismPainter;
        private List<Point> _pointsA = null;
        private List<Point> _pointsB = null;
        private Point _shadingPointOne = new Point();
        private Point _shadingPointTwo = new Point();
        private Rectangle _rectangle;
        private double _rotationAngle;
        private double _xCircleCoordianate;
        private double _yCircleCoordianate;
        private double _omega;

        public MechanismController(MainFormModel mainFormModel, MechanismPainter mechanismPainter)
        {
            _mainFormModel = mainFormModel;
            _mechanismPainter = mechanismPainter;
        }

        private Point Center { get => _mainFormModel.Center; }

        private int RadiusOne { get => _mainFormModel.RadiusOne; }

        private int RadiusTwo { get => _mainFormModel.RadiusTwo; }

        public Point ShadingPointOne { get => _shadingPointOne; }

        public Point ShadingPointTwo { get => _shadingPointTwo; }

        public double Time { get => _mainFormModel.Time; }

        private void SpecifyOmega()
        {
            const int timerOriginalInterval = 100;
            int timerIntervalPayback = timerOriginalInterval / MainFormModel.TimerInterval;
            _omega = _mainFormModel.Omega / timerIntervalPayback;
        }

        public void AddPointsToLists()
        {
            _pointsA.Clear();
            _pointsB.Clear();
            _pointsA.Add(GetTrajectoryPoint(RadiusOne));
            _pointsB.Add(GetTrajectoryPoint(RadiusTwo));
        }

        public void PointCheck()
        {
            _mechanismPainter.ClearDrawing();
            _mechanismPainter.RefreshPicture(clear: true, clearStable: false);
            CountTrajectory();
        }

        public void ClearPointsLists()
        {
            _pointsA = new List<Point>();
            _pointsB = new List<Point>();
        }

        public void CountTrajectory()
        {
            _pointsA.Add(GetTrajectoryPoint(RadiusOne));
            _pointsB.Add(GetTrajectoryPoint(RadiusTwo));
            _mechanismPainter.DrawTrajectory(_mainFormModel.PointAChecked, Color.Red, _pointsA);
            _mechanismPainter.DrawTrajectory(_mainFormModel.PointBChecked, Color.Blue, _pointsB);
        }

        private Point GetTrajectoryPoint(int radius)
        {
            var point = new Point();
            _xCircleCoordianate = OnCircleCoordinate(xAxis: true, radius);
            _yCircleCoordianate = OnCircleCoordinate(xAxis: false, radius);
            SetPointCoordinates(ref point);
            return point;
        }

        public void RefreshMechanism(bool clear, bool clearStable)
        {
            RefreshRectangle();
            RefreshWheelOne(clearStable);
            RefreshRotationAngle();
            SetPointsCoordinates(byDefault: true);
            _mechanismPainter.RefreshWheelOneShading(clear);
            RefreshBearing();
            RefreshBearingLines();
            RefreshShading(clearStable);
            RefreshWheelTwo();
            _mechanismPainter.RefreshWheelTwoShading(clear);
            RefreshWeightOne();
            RefreshWeightTwo();
        }

        private void RefreshRectangle()
        {
            int heightIndent = 4 * RadiusOne / 5;
            _rectangle = new Rectangle
                (Center.X - RadiusOne,
                 Center.Y + heightIndent,
                 2 * RadiusOne,
                 RadiusOne + HeightIndent - heightIndent);
        }

        private void RefreshRotationAngle()
        {
            _rotationAngle = _omega * (RadiusOne + RadiusTwo) * Time / RadiusTwo;
        }

        private void RefreshWheelOne(bool clearStable)
        {
            _omega = _mainFormModel.Omega;
            SpecifyOmega();
            _mechanismPainter.SetPenColor(!clearStable);
            _rectangle.Location = new Point(Center.X - RadiusOne, Center.Y - RadiusOne);
            _rectangle.Size = new Size(2 * RadiusOne, 2 * RadiusOne);
            _mechanismPainter.DrawEllipse(_rectangle);
        }

        private void RefreshBearing()
        {
            const int widthIndent = 4, heightIndent = 3, bearingSize = 8;
            _mechanismPainter.SetPenDashStyle(solid: true);
            _rectangle.Location = SetCenteredPoint(-widthIndent, -heightIndent);
            _rectangle.Size = new Size(bearingSize, bearingSize);
            _mechanismPainter.DrawEllipse(_rectangle);
        }

        private void RefreshBearingLines()
        {
            const int bearingLinesWidth = 7, bearingLinesHeight = 16;
            Point point = SetCenteredPoint(-bearingLinesWidth, bearingLinesHeight);
            _mechanismPainter.DrawLine(point, Center);
            point = SetCenteredPoint(bearingLinesWidth, bearingLinesHeight);
            _mechanismPainter.DrawLine(Center, point);
        }

        private void RefreshShading(bool clearStable)
        {
            const int shadingWidth = 15, shadingHeight = 16;
            _rectangle.Location = new Point(Center.X - shadingWidth, Center.Y + shadingHeight);
            _rectangle.Size = new Size(shadingWidth * 2, HeightIndent);
            _mechanismPainter.FillRectangle(clearStable, _rectangle);
            _mechanismPainter.DrawLine(
                Center.X + shadingWidth,
                Center.Y + shadingHeight,
                Center.X - shadingWidth,
                Center.Y + shadingHeight);
        }

        private void RefreshWheelTwo()
        {
            const int widthIndent = 4;
            int heightIndent = RadiusOne / 5;
            _mechanismPainter.SetPenDashStyle(solid: true);
            Rectangle rectangle = new Rectangle
                (Center.X - RadiusOne,
                 Center.Y + widthIndent * heightIndent,
                 2 * RadiusOne,
                 RadiusOne + HeightIndent - (widthIndent * heightIndent))
            {
                Location = SetCenteredPoint(-RadiusTwo, -RadiusTwo),
                Size = new Size(2 * RadiusTwo, 2 * RadiusTwo)
            };
            _mechanismPainter.DrawEllipse(rectangle);
        }

        public void SetPointsCoordinates(bool byDefault)
        {
            _xCircleCoordianate = -OnCircleCoordinate(byDefault, RadiusTwo);
            _yCircleCoordianate = OnCircleCoordinate(!byDefault, RadiusTwo);
            SignByBoolean(ref _yCircleCoordianate, byDefault, inverse: true);
            SetPointCoordinates(ref _shadingPointOne);
            _xCircleCoordianate *= -1; _yCircleCoordianate *= -1;
            SetPointCoordinates(ref _shadingPointTwo);
        }

        private void SetPointCoordinates(ref Point point)
        {
            point.X = Convert.ToInt32(Center.X + _xCircleCoordianate);
            point.Y = Convert.ToInt32(Center.Y + _yCircleCoordianate);
        }

        private Point SetCenteredPoint(int xOffset, int yOffset)
        {
            return new Point(Center.X + xOffset, Center.Y + yOffset);
        }

        private double OnCircleCoordinate(bool xAxis, int radius)
        {
            double axisCoordinate = xAxis ? Math.Sin(_rotationAngle) : Math.Cos(_rotationAngle);
            return axisCoordinate * radius;
        }

        private void RefreshWeightOne()
        {
            RefreshWeight(isRadiusOne: true); 
        }

        private void RefreshWeightTwo()
        {
            RefreshWeight(isRadiusOne: false);
        }

        private void DrawWeightLine(int radius, int weightLowestPossiblePoint)
        {
            _mechanismPainter.DrawLine
                (Convert.ToInt32(Center.X) + radius,
                 Convert.ToInt32(Center.Y),
                 Convert.ToInt32(Center.X) + radius,
                 weightLowestPossiblePoint);
        }

        private int WeightLowestHeightDelta(double weightLowestHeight, double linearDistance)
        {
            return Convert.ToInt32(weightLowestHeight + linearDistance);
        }

        private void RefreshWeight(bool isRadiusOne)
        {
            const int panelHeight = 139, weightHeightSize = 31, weightWidth = 15;
            int weightHeight = isRadiusOne ? 40 : 20,
                      radius = isRadiusOne ? RadiusOne : RadiusTwo,
                weightLowestHeight = Center.Y + radius + weightHeight,
                weightLowestHeightPossible = _mainFormModel.PictureBoxHeight - weightHeightSize,
                weightCurrentLowestHeight = _mainFormModel.PictureBoxHeight - panelHeight
                + (radius - weightHeight * 2) * 2;
            double linearSpeed = _omega * radius,
                   linearDistance = linearSpeed * Time;
            bool omegaPositive = _omega > 0;
            SignByBoolean(ref linearDistance, isRadiusOne, inverse: true);
            radius *= isRadiusOne ? 1 : -1;
            int weightHorizotnalDistance = Center.X + radius - 8;
            _mechanismPainter.SetPenDashStyle(solid: true);
            if (WeightLowestHeightDelta(weightLowestHeight, linearDistance) > Center.Y)
            {
                linearDistance = Math.Abs(linearDistance);
                SignByBoolean(ref linearDistance, omegaPositive);
                int weightLowestHeightDelta = WeightLowestHeightDelta(weightLowestHeight, linearDistance);
                bool weightWithinWheel = isRadiusOne ?
                    weightLowestHeightDelta > weightCurrentLowestHeight :
                    weightLowestHeightDelta < weightLowestHeightPossible;
                if (weightWithinWheel)
                {
                    SignByBoolean(ref linearDistance, isRadiusOne, inverse: true);
                    weightLowestHeightDelta = WeightLowestHeightDelta(weightLowestHeight, linearDistance);
                    SetWeightParameters(radius, weightLowestHeightDelta, weightHorizotnalDistance);
                }
                else SetWeightParameters(radius, weightLowestHeightPossible, weightHorizotnalDistance);
            }
            else _rectangle.Location = new Point(weightHorizotnalDistance, Convert.ToInt32(Center.Y));
            _rectangle.Size = new Size(weightWidth, weightWidth * 2);
            _mechanismPainter.DrawEllipse(_rectangle, isRectangle: true);
        }

        private void SetWeightParameters(int radius, int weightLowestPoint, 
            int weightHorizotnalDistance)
        {
            DrawWeightLine(radius, weightLowestPoint);
            _rectangle.Location = new Point(weightHorizotnalDistance, weightLowestPoint);
        }

        private void SignByBoolean(ref double digit, bool boolean, bool inverse = false)
        {
            digit *= boolean ? 1 : -1;
            digit *= inverse ? -1 : 1;
        }
    }
}