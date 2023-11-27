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

        private Point Center => _mainFormModel.Center;

        private int RadiusOne => _mainFormModel.RadiusOne;

        private int RadiusTwo => _mainFormModel.RadiusTwo;

        public Point ShadingPointOne => _shadingPointOne;

        public Point ShadingPointTwo => _shadingPointTwo;

        public double Time => _mainFormModel.Time;

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

            _xCircleCoordianate = GetOnCircleCoordinate(xAxis: true, radius);
            _yCircleCoordianate = GetOnCircleCoordinate(xAxis: false, radius);
            SetPointCoordinates(ref point);

            return point;
        }

        public void RefreshMechanism(bool clear, bool clearStable)
        {
            RefreshRectangle();
            RefreshWheelOne(clearStable);
            RefreshRotationAngle();
            SetPointsCoordinates(xAxis: true);
            _mechanismPainter.RefreshWheelOneShading(clear);

            RefreshBearing();
            RefreshBearingLines();
            RefreshShading(clearStable);
            RefreshWheelTwo();
            _mechanismPainter.RefreshWheelTwoShading(clear);

            var weightController = new WeightController(_mainFormModel, _mechanismPainter, _rectangle);
            weightController.RefreshWeightOne(_omega);
            weightController.RefreshWeightTwo(_omega);
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

        public void SetPointsCoordinates(bool xAxis)
        {
            _xCircleCoordianate = -GetOnCircleCoordinate(xAxis, RadiusTwo);
            _yCircleCoordianate = GetOnCircleCoordinate(!xAxis, RadiusTwo);
            SignByBoolean(ref _yCircleCoordianate, xAxis, inverse: true);

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
            => new Point(Center.X + xOffset, Center.Y + yOffset);

        private double GetOnCircleCoordinate(bool xAxis, int radius)
        {
            double axisCoordinate = xAxis ? Math.Sin(_rotationAngle) : Math.Cos(_rotationAngle);
            return axisCoordinate * radius;
        }

        public static void SignByBoolean(ref double digit, bool boolean, bool inverse = false)
        {
            digit *= boolean ? 1 : -1;
            digit *= inverse ? -1 : 1;
        }

        public static void SetRadius(ref int radius, int value)
        {
            if (value >= 0)
            {
                radius = value;
            }
        }
    }
}