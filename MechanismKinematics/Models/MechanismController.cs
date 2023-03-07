using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MechanismKinematics
{
    public class MechanismController
    {
        private const int HeightIndent = 10;
        private readonly MainFormModel _mainFormModel;
        private List<Point> _pointsA = null;
        private List<Point> _pointsB = null;
        private Point _shadingPointOne = new Point();
        private Point _shadingPointTwo = new Point();
        private Pen _pen;
        private Rectangle _rectangle;
        private double _rotationAngle;
        private double _xCircleCoordianate;
        private double _yCircleCoordianate;
        private double _omega;

        public MechanismController(MainFormModel mainFormModel)
        {
            _mainFormModel = mainFormModel;
        }

        private int RadiusOne { get => _mainFormModel.RadiusOne; }

        private int RadiusTwo { get => _mainFormModel.RadiusTwo; }

        private double Time { get => _mainFormModel.Time; }

        private Point Center { get => _mainFormModel.Center; }

        private Graphics Graphics { get => _mainFormModel.Graphics; }

        public void SpecifyOmega()
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

        public void ClearDrawing()
        {
            using (SolidBrush solidBrush = new SolidBrush(Color.White))
            { Graphics.FillRectangle(solidBrush, 0, 0, 0, 0); }
            _mainFormModel.MechanismDrawn = false;
            Graphics.Clear(Color.White);
        }

        public void PointCheck()
        {
            ClearDrawing();
            RefreshPicture(clear: true, clearStable: false);
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
            DrawTrajectory(_mainFormModel.PointAChecked, Color.Red, _pointsA);
            DrawTrajectory(_mainFormModel.PointBChecked, Color.Blue, _pointsB);
        }

        public void RefreshPicture(bool clear, bool clearStable)
        {
            int heightIndent = 4 * RadiusOne / 5;
            _pen = new Pen(Color.White);
            _rectangle = new Rectangle
                (Center.X - RadiusOne,
                 Center.Y + heightIndent,
                 2 * RadiusOne,
                 RadiusOne + HeightIndent - heightIndent);
            RefreshWheelOne(clearStable);
            RefreshRotationAngle();
            SetPointsCoordinates(byDefault: true);
            RefreshWheelOneShading(clear);
            RefreshBearing();
            RefreshBearingLines();
            RefreshShading(clearStable);
            RefreshWheelTwo();
            RefreshWheelTwoShading(clear);
            RefreshWeightOne();
            RefreshWeightTwo();
            _pen.Dispose();
        }

        private void DrawTrajectory(bool pointChecked, Color color, List<Point> pointsList)
        {
            if (!pointChecked) return;
            _pen = new Pen(color);
            Point[] pointArray = pointsList.ToArray();
            if (Time != 0) Graphics.DrawCurve(_pen, pointArray);
        }

        private Point GetTrajectoryPoint(int radius)
        {
            var point = new Point();
            _xCircleCoordianate = OnCircleCoordinate(xAxis: true, radius);
            _yCircleCoordianate = OnCircleCoordinate(xAxis: false, radius);
            SetPointCoordinates(ref point);
            return point;
        }

        private void RefreshRotationAngle()
        {
            _rotationAngle = _omega * (RadiusOne + RadiusTwo) * Time / RadiusTwo;
        }

        private void RefreshWheelOne(bool clearStable)
        {
            _omega = _mainFormModel.Omega;
            SpecifyOmega();
            SetPenColor(!clearStable);
            _rectangle.Location = new Point(Center.X - RadiusOne, Center.Y - RadiusOne);
            _rectangle.Size = new Size(2 * RadiusOne, 2 * RadiusOne);
            Graphics.DrawEllipse(_pen, _rectangle);
        }

        private void RefreshWheelOneShading(bool clear)
        {
            SetPenColor(clear);
            _pen.DashStyle = DashStyle.DashDot;
            Graphics.DrawLine(_pen, _shadingPointOne, _shadingPointTwo);
            SetPointsCoordinates(byDefault: false);
        }

        private void SetPenColor(bool clear)
        {
            _pen.Color = clear ? Color.Black : Color.White;
        }

        private void RefreshBearing()
        {
            const int widthIndent = 4, heightIndent = 3, bearingSize = 8;
            _pen.DashStyle = DashStyle.Solid;
            _rectangle.Location = SetCenteredPoint(-widthIndent, -heightIndent);
            _rectangle.Size = new Size(bearingSize, bearingSize);
            Graphics.DrawEllipse(_pen, _rectangle);
        }

        private void RefreshBearingLines()
        {
            const int bearingLinesWidth = 7, bearingLinesHeight = 16;
            Point point = SetCenteredPoint(-bearingLinesWidth, bearingLinesHeight);
            Graphics.DrawLine(_pen, point, Center);
            point = SetCenteredPoint(bearingLinesWidth, bearingLinesHeight);
            Graphics.DrawLine(_pen, Center, point);
        }

        private void RefreshShading(bool clearStable)
        {
            const int shadingWidth = 15, shadingHeight = 16;
            _rectangle.Location = new Point(Center.X - shadingWidth, Center.Y + shadingHeight);
            _rectangle.Size = new Size(shadingWidth * 2, HeightIndent);
            HatchBrush brush = clearStable ?
                  new HatchBrush(HatchStyle.ForwardDiagonal, Color.White, Color.White)
                : new HatchBrush(HatchStyle.ForwardDiagonal, Color.Black, Color.White);
            Graphics.FillRectangle(brush, _rectangle);
            Graphics.DrawLine(_pen,
                Center.X + shadingWidth,
                Center.Y + shadingHeight,
                Center.X - shadingWidth,
                Center.Y + shadingHeight);
        }

        private void RefreshWheelTwo()
        {
            const int widthIndent = 4;
            int heightIndent = RadiusOne / 5;
            _pen.DashStyle = DashStyle.Solid;
            Rectangle rectangle = new Rectangle
                (Center.X - RadiusOne,
                 Center.Y + widthIndent * heightIndent,
                 2 * RadiusOne,
                 RadiusOne + HeightIndent - (widthIndent * heightIndent))
            {
                Location = SetCenteredPoint(-RadiusTwo, -RadiusTwo),
                Size = new Size(2 * RadiusTwo, 2 * RadiusTwo)
            };
            Graphics.DrawEllipse(_pen, rectangle);
        }

        private void RefreshWheelTwoShading(bool clear)
        {
            _pen.Color = clear ? Color.Black : Color.White;
            _pen.DashStyle = DashStyle.DashDot;
            SetPointsCoordinates(byDefault: true);
            Graphics.DrawLine(_pen, _shadingPointOne, _shadingPointTwo);
            SetPointsCoordinates(byDefault: false);
            Graphics.DrawLine(_pen, _shadingPointOne, _shadingPointTwo);
        }

        private void SetPointsCoordinates(bool byDefault)
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
            Graphics.DrawLine(_pen,
                   Convert.ToInt32(Center.X) + radius,
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
            _pen.DashStyle = DashStyle.Solid;
            if (WeightLowestHeightDelta(weightLowestHeight, linearDistance) > Center.Y)
            {
                linearDistance = Math.Abs(linearDistance);
                SignByBoolean(ref linearDistance, omegaPositive, inverse: false);
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
            Graphics.DrawRectangle(_pen, _rectangle);
        }

        private void SetWeightParameters(int radius, int weightLowestPoint, 
            int weightHorizotnalDistance)
        {
            DrawWeightLine(radius, weightLowestPoint);
            _rectangle.Location = new Point(weightHorizotnalDistance, weightLowestPoint);
        }

        private void SignByBoolean(ref double digit, bool boolean, bool inverse)
        {
            digit *= boolean ? 1 : -1;
            digit *= inverse ? -1 : 1;
        }
    }
}