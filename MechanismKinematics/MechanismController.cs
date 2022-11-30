using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MechanismKinematics
{
    public class MechanismController
    {
        private static readonly int s_heightIndent = 10;
        private readonly MainFormModel _mainFormModel;
        private Graphics _graphics;
        private List<Point> _pointAList = null;
        private List<Point> _pointBList = null;
        private Point _shadingPointOne = new Point();
        private Point _shadingPointTwo = new Point();
        private Point _center;
        private Pen _pen;
        private Rectangle _rectangle;
        private double _rotationAngle;
        private double _xCircleCoordianate;
        private double _yCircleCoordianate;
        private double _omega;
        private double _time;
        private int _radiusOne;
        private int _radiusTwo;

        public MechanismController(MainFormModel mainFormModel)
        {
            _mainFormModel = mainFormModel;
            RefreshFields();
        }

        public void RefreshFields()
        {
            _radiusOne = _mainFormModel.RadiusOne;
            _radiusTwo = _mainFormModel.RadiusTwo;
            _omega = _mainFormModel.Omega;
            SpecifyOmega();
            _graphics = _mainFormModel.Graphics;
            _time = _mainFormModel.Time;
            _center = _mainFormModel.Center;
        }

        public void SpecifyOmega()
        {
            int timerOriginalInterval = 100,
                timerIntervalPayback = timerOriginalInterval / _mainFormModel.TimerInterval;
            _omega /= timerIntervalPayback;
        }

        public void AddPointsToLists()
        {
            _pointAList.Clear();
            _pointBList.Clear();
            _pointAList.Add(TrajectoryPoint(_radiusOne));
            _pointBList.Add(TrajectoryPoint(_radiusTwo));
        }

        public void ClearDrawing()
        {
            SolidBrush solidBrush = new SolidBrush(Color.White);
            _graphics.FillRectangle(solidBrush, 0, 0, 0, 0);
            solidBrush.Dispose();
            _mainFormModel.MechanismDrawn = false;
            _graphics.Clear(Color.White);
        }

        public void PointCheck()
        {
            ClearDrawing();
            RefreshPicture(clear: true, clearStable: false);
            CountTrajectory();
        }

        public void ClearPointsLists()
        {
            _pointAList = new List<Point>();
            _pointBList = new List<Point>();
        }

        public void CountTrajectory()
        {
            _pointAList.Add(TrajectoryPoint(_radiusOne));
            _pointBList.Add(TrajectoryPoint(_radiusTwo));
            DrawTrajectory(_mainFormModel.PointAChecked, Color.Red, _pointAList);
            DrawTrajectory(_mainFormModel.PointBChecked, Color.Blue, _pointBList);
        }

        public void RefreshPicture(bool clear, bool clearStable)
        {
            int heightIndent = 4 * _radiusOne / 5;
            _pen = new Pen(Color.White);
            _rectangle = new Rectangle
                (_center.X - _radiusOne,
                 _center.Y + heightIndent,
                 2 * _radiusOne,
                 _radiusOne + s_heightIndent - heightIndent);
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
            if (_time != 0) _graphics.DrawCurve(_pen, pointArray);
        }

        private Point TrajectoryPoint(int radius)
        {
            RefreshRotationAngle();
            Point point = new Point();
            _xCircleCoordianate = OnCircleCoordinate(xAxis: true, radius);
            _yCircleCoordianate = OnCircleCoordinate(xAxis: false, radius);
            SetPointCoordinates(ref point);
            return point;
        }

        private void RefreshRotationAngle()
        {
            _rotationAngle = _omega * (_radiusOne + _radiusTwo) * _time / _radiusTwo;
        }

        private void RefreshWheelOne(bool clearStable)
        {
            RefreshFields();
            SetPenColor(!clearStable);
            _rectangle.Location = new Point(_center.X - _radiusOne, _center.Y - _radiusOne);
            _rectangle.Size = new Size(2 * _radiusOne, 2 * _radiusOne);
            _graphics.DrawEllipse(_pen, _rectangle);
        }

        private void RefreshWheelOneShading(bool clear)
        {
            SetPenColor(clear);
            _pen.DashStyle = DashStyle.DashDot;
            _graphics.DrawLine(_pen, _shadingPointOne, _shadingPointTwo);
            SetPointsCoordinates(byDefault: false);
        }

        private void SetPenColor(bool clear)
        {
            _pen.Color = clear ? Color.Black : Color.White;
        }

        private void RefreshBearing()
        {
            int widthIndent = 4, heightIndent = 3, bearingSize = 8;
            _pen.DashStyle = DashStyle.Solid;
            _rectangle.Location = SetPoint(-widthIndent, -heightIndent);
            _rectangle.Size = new Size(bearingSize, bearingSize);
            _graphics.DrawEllipse(_pen, _rectangle);
        }

        private void RefreshBearingLines()
        {
            int bearingLinesWidth = 7, bearingLinesHeight = 16;
            Point point = SetPoint(-bearingLinesWidth, bearingLinesHeight);
            _graphics.DrawLine(_pen, point, _center);
            point = SetPoint(bearingLinesWidth, bearingLinesHeight);
            _graphics.DrawLine(_pen, _center, point);
        }

        private void RefreshShading(bool clearStable)
        {
            int shadingWidth = 15, shadingHeight = 16;
            _rectangle.Location = new Point(_center.X - shadingWidth, _center.Y + shadingHeight);
            _rectangle.Size = new Size(shadingWidth * 2, s_heightIndent);
            HatchBrush brush = clearStable ?
                  new HatchBrush(HatchStyle.ForwardDiagonal, Color.White, Color.White)
                : new HatchBrush(HatchStyle.ForwardDiagonal, Color.Black, Color.White);
            _graphics.FillRectangle(brush, _rectangle);
            _graphics.DrawLine(_pen,
                _center.X + shadingWidth,
                _center.Y + shadingHeight,
                _center.X - shadingWidth,
                _center.Y + shadingHeight);
        }

        private void RefreshWheelTwo()
        {
            int heightIndent = _radiusOne / 5, widthIndent = 4;
            _pen.DashStyle = DashStyle.Solid;
            Rectangle rectangle = new Rectangle
                (_center.X - _radiusOne,
                 _center.Y + widthIndent * heightIndent,
                 2 * _radiusOne,
                 _radiusOne + s_heightIndent - (widthIndent * heightIndent))
            {
                Location = SetPoint(-_radiusTwo, -_radiusTwo),
                Size = new Size(2 * _radiusTwo, 2 * _radiusTwo)
            };
            _graphics.DrawEllipse(_pen, rectangle);
        }

        private void RefreshWheelTwoShading(bool clear)
        {
            _pen.Color = clear ? Color.Black : Color.White;
            _pen.DashStyle = DashStyle.DashDot;
            SetPointsCoordinates(byDefault: true);
            _graphics.DrawLine(_pen, _shadingPointOne, _shadingPointTwo);
            SetPointsCoordinates(byDefault: false);
            _graphics.DrawLine(_pen, _shadingPointOne, _shadingPointTwo);
        }

        private void SetPointsCoordinates(bool byDefault)
        {
            _xCircleCoordianate = -OnCircleCoordinate(byDefault, _radiusTwo);
            _yCircleCoordianate = OnCircleCoordinate(!byDefault, _radiusTwo);
            SignByBoolean(ref _yCircleCoordianate, byDefault, inverse: true);
            SetPointCoordinates(ref _shadingPointOne);
            _xCircleCoordianate *= -1; _yCircleCoordianate *= -1;
            SetPointCoordinates(ref _shadingPointTwo);
        }

        private void SetPointCoordinates(ref Point point)
        {
            point.X = Convert.ToInt32(_center.X + _xCircleCoordianate);
            point.Y = Convert.ToInt32(_center.Y + _yCircleCoordianate);
        }

        private Point SetPoint(int xOffset, int yOffset)
        {
            return new Point(_center.X + xOffset, _center.Y + yOffset);
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
            _graphics.DrawLine(_pen,
                   Convert.ToInt32(_center.X) + radius,
                   Convert.ToInt32(_center.Y),
                   Convert.ToInt32(_center.X) + radius,
                   weightLowestPossiblePoint);
        }

        private int WeightLowestHeightDelta(double weightLowestHeight, double linearDistance)
        {
            return Convert.ToInt32(weightLowestHeight + linearDistance);
        }

        private void RefreshWeight(bool isRadiusOne)
        {
            int weightHeight = isRadiusOne ? 40 : 20,
                      radius = isRadiusOne ? _radiusOne : _radiusTwo,
                weightLowestHeight = _center.Y + radius + weightHeight,
                panelHeight = 139, weightHeightSize = 31, weightWidth = 15,
                weightLowestHeightPossible = _mainFormModel.GetPictureBoxHeight() - weightHeightSize,
                weightCurrentLowestHeight = _mainFormModel.GetPictureBoxHeight() - panelHeight
                + (radius - weightHeight * 2) * 2;
            double linearSpeed = _omega * radius,
                   linearDistance = linearSpeed * _time;
            bool omegaPositive = _omega > 0;
            SignByBoolean(ref linearDistance, isRadiusOne, inverse: true);
            radius *= isRadiusOne ? 1 : -1;
            int weightHorizotnalDistance = _center.X + radius - 8;
            _pen.DashStyle = DashStyle.Solid;
            if (WeightLowestHeightDelta(weightLowestHeight, linearDistance) > _center.Y)
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
            else _rectangle.Location = new Point(weightHorizotnalDistance, Convert.ToInt32(_center.Y));
            _rectangle.Size = new Size(weightWidth, weightWidth * 2);
            _graphics.DrawRectangle(_pen, _rectangle);
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