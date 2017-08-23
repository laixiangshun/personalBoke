using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.Common
{
    /// <summary>
    /// 图片相关操作类
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// 创建缩略图
        /// </summary>
        /// <param name="originalPicture">原图地址</param>
        /// <param name="thumbnail">缩略图地址</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool CreateThumbnail(string originalPicture, string thumbnail, int width, int height)
        {
            Image _original = Image.FromFile(originalPicture);
            RectangleF _originalArea = new RectangleF();
            float _ratio =(float) width / height;//宽高比
            if (_ratio > ((float)_original.Width / _original.Height))
            {
                _originalArea.X = 0;
                _originalArea.Width = _original.Width;
                _originalArea.Height = _originalArea.Width / _ratio;
                _originalArea.Y = (_original.Height - _originalArea.Height) / 2;
            }
            else
            {
                _originalArea.Y = 0;
                _originalArea.Height = _original.Height;
                _originalArea.Width = _originalArea.Height * _ratio;
                _originalArea.X = (_original.Width - _originalArea.Width) / 2;
            }
            Bitmap bitmap = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmap);
            //设置图片的质量
            g.InterpolationMode = InterpolationMode.High;
            g.SmoothingMode = SmoothingMode.HighQuality;
            //绘制图片
            g.Clear(Color.Transparent);
            g.DrawImage(_original, new RectangleF(0, 0, bitmap.Width, bitmap.Height), _originalArea, GraphicsUnit.Pixel);
            //保存
            bitmap.Save(thumbnail);
            g.Dispose();
            _original.Dispose();
            bitmap.Dispose();
            return true;
        }
    }
}
