using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace MyStamp.ViewModel
{
    /// <summary>
    /// 参考URL：http://www.makcraft.com/blog/meditation/2014/04/16/drawing-circles-using-the-mvvm-pattern-in-wpf-app/
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        private const int WIDTH = 1200;

        private const int HEIGHT = 800;

        private RenderTargetBitmap _original;
        private List<DrawingVisual> _drawingVisuals = new List<DrawingVisual>();
        private List<DrawingVisual> _workingVisuals = new List<DrawingVisual>();
        private Point _starting;

        public MainWindowViewModel()
        {
            _starting = new Point(0, 0);
            Point tmp = new Point(200, 200);
            initImage();
            imageMouseReleaseExecute(tmp);
        }

        private void initImage()
        {

            _original = new RenderTargetBitmap(WIDTH, HEIGHT,

                (Double)96,

                (Double)96,

                PixelFormats.Default);

            resetImage();

        }



        // イメージを初期状態にリセット

        private void resetImage()
        {

            Bitmap = (RenderTargetBitmap)_original.Clone();

        }

        public void Render()
        {

            // 表示しているイメージを初期状態にリセット

            resetImage();

            // DrawingVisual の追加履歴順にイメージに描画

            _drawingVisuals.ForEach(n => _bitmap.Render(n));

            // 追加しようとして操作中の枠線を描画

            _workingVisuals.ForEach(n => _bitmap.Render(n));

        }

        private RenderTargetBitmap _bitmap;
        public RenderTargetBitmap Bitmap
        {

            get { return _bitmap; }

            private set
            {
                _bitmap = value;
                base.RaisePropertyChanged(() => Bitmap);
            }
        }

        private void imageMouseReleaseExecute(Point position)
        {
            //_isDraged = false;
            _workingVisuals.Clear();

            var rand = new Random();
            var brush = new SolidColorBrush(
                Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256)));

            var dv = new DrawingVisual();
            var x = Math.Abs(_starting.X - position.X);
            var y = Math.Abs(_starting.Y - position.Y);

            using (var dc = dv.RenderOpen())
            {
                dc.DrawEllipse(brush, null, _starting, x, y);
            }
            _drawingVisuals.Add(dv);
            Render();
        }

    }

}
