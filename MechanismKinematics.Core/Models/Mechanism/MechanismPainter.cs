using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MechanismKinematics
{
    public class MechanismPainter
    {
        private Pen _pen;
        private readonly MainFormModel _mainFormModel;
        private MechanismController _mechanismController;

        public MechanismPainter(MainFormModel mainFormModel) => _mainFormModel = mainFormModel;

        public MechanismController MechanismController { set => _mechanismController = value; }

        public Graphics Graphics => _mainFormModel.Graphics;

        public void ClearDrawing()
        {
            using (SolidBrush solidBrush = new SolidBrush(Color.White))
            { 
                Graphics.FillRectangle(solidBrush, 0, 0, 0, 0); 
            }

            _mainFormModel.MechanismDrawn = false;
            Graphics.Clear(Color.White);
        }

        public void RefreshPicture(bool clear, bool clearStable)
        {
            _pen = new Pen(Color.White);
            _mechanismController.RefreshMechanism(clear, clearStable);
            _pen.Dispose();
        }

        public void SetPenColor(bool clear)
            => _pen.Color = clear ? Color.Black : Color.White;

        public void SetPenDashStyle(bool solid)
            => _pen.DashStyle = solid ? DashStyle.Solid : DashStyle.DashDot;

        public void DrawLine(Point point1, Point point2) 
            => Graphics.DrawLine(_pen, point1, point2);

        public void DrawLine(float x1, float y1, float x2, float y2) 
            => Graphics.DrawLine(_pen, x1, y1, x2, y2);

        public void DrawEllipse(Rectangle rectangle, bool isRectangle = false)
        {
            if (isRectangle)
            {
                Graphics.DrawRectangle(_pen, rectangle);
            }
            else
            {
                Graphics.DrawEllipse(_pen, rectangle);
            }
        }

        public void DrawTrajectory(bool pointChecked, Color color, List<Point> pointsList)
        {
            if (!pointChecked) return;

            _pen = new Pen(color);
            Point[] pointArray = pointsList.ToArray();

            if (_mechanismController.Time != 0) Graphics.DrawCurve(_pen, pointArray);
        }

        public void FillRectangle(bool clearStable, Rectangle rectangle)
        {
            HatchBrush brush = clearStable ?
                  new HatchBrush(HatchStyle.ForwardDiagonal, Color.White, Color.White)
                : new HatchBrush(HatchStyle.ForwardDiagonal, Color.Black, Color.White);
            Graphics.FillRectangle(brush, rectangle);
        }

        public void RefreshWheelOneShading(bool clear)
        {
            SetPenColor(clear);
            _pen.DashStyle = DashStyle.DashDot;
            Graphics.DrawLine(_pen, _mechanismController.ShadingPointOne, _mechanismController.ShadingPointTwo);
            _mechanismController.SetPointsCoordinates(byDefault: false);
        }

        public void RefreshWheelTwoShading(bool clear)
        {
            SetPenColor(clear);
            SetPenDashStyle(solid: false);
            _mechanismController.SetPointsCoordinates(byDefault: true);
            DrawLine(_mechanismController.ShadingPointOne, _mechanismController.ShadingPointTwo);
            _mechanismController.SetPointsCoordinates(byDefault: false);
            DrawLine(_mechanismController.ShadingPointOne, _mechanismController.ShadingPointTwo);
        }
    }
}
