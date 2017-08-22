using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NinesKy.Common.CheckCode
{
     public class Security
    {
         /// <summary>
         /// 生成验证码字符
         /// </summary>
         /// <param name="length"></param>
         /// <returns></returns>
         public static string CreateVerificationText(int length)
         {
             char[] _verification = new char[length];
             char[] _dictionnary={'A','B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
             Random random = new Random();
             for (int i = 0; i < length; i++)
             {
                 _verification[i] = _dictionnary[random.Next(_dictionnary.Length - 1)];
             }
             return new string(_verification);
         }

         /// <summary>
         /// 根据验证码生成图片
         /// 使用GDI+创建画布，使用伪随机数生成器生成渐变画刷，然后创建渐变文字
         /// </summary>
         /// <param name="verificationText"></param>
         /// <param name="width"></param>
         /// <param name="height"></param>
         /// <returns></returns>
         public static Bitmap CreateVerificationImage(string verificationText, int width, int height)
         {
             Pen pen = new Pen(Color.Black);
             Font font = new Font("Arial", 14, FontStyle.Bold);
             Brush brush = null;//刷子
             Bitmap bitmap = new Bitmap(width, height);//创建指定大小的GDI+位图对象;位图由图形图像及其属性的像素数据组成
             Graphics g = Graphics.FromImage(bitmap);
             SizeF totalSizeF = g.MeasureString(verificationText, font);//绘制字符串
             SizeF curCharSizeF;
             PointF startPointF = new PointF((width - totalSizeF.Width) / 2, (height - totalSizeF.Height) / 2);
             Random random = new Random();
             g.Clear(Color.White);//清除整个绘图并以指定颜色填充
             for (int i = 0; i < verificationText.Length; i++)
             {
                 brush = new LinearGradientBrush(new Point(0, 0), new Point(1, 1), Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)), 
                     Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                 g.DrawString(verificationText[i].ToString(), font, brush, startPointF);
                 curCharSizeF = g.MeasureString(verificationText[i].ToString(), font);//绘制文字
                 startPointF.X += curCharSizeF.Width;//SizeF结构的水平分量
             }
             g.Dispose();
             return bitmap;
         }

         /// <summary>
         /// Sha256加密方法
         /// </summary>
         /// <param name="str"></param>
         /// <returns></returns>
         public static string Sha256(string str){
             SHA256Managed sha256=new SHA256Managed();
             byte[] cipherText=sha256.ComputeHash(Encoding.Default.GetBytes(str));
             return Convert.ToBase64String(cipherText);
         }
    }
}
