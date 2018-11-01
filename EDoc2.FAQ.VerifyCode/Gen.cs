using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace EDoc2.FAQ.VerifyCode
{
    public class Gen
    {
        public enum CharType
        {
            Lower = 0,
            Upper = 1,
            Number = 2
        }

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="charTypes"></param>
        /// <returns></returns>
        public static char CreateChar(params CharType[] charTypes)
        {
            var random = new Random();
            foreach (var charType in charTypes)
            {
                switch (charType)
                {
                    case CharType.Lower:
                        return (char)random.Next(97, 122);
                    case CharType.Upper:
                        return (char)random.Next(65, 90);
                    case CharType.Number:
                        return (char)random.Next(0, 9);
                }
            }
            return ' ';
        }

        public static string CreateCode(int length)
        {
            var random = new Random();
            var code = new StringBuilder();
            for (var i = 0; i < length; i++)
                code.Append(CreateChar((CharType)random.Next(0, 2)));

            return code.ToString();
        }

        /// <summary>
        /// 生成验证码图片流
        /// </summary>
        /// <param name="code"></param>
        /// <param name="fontSize"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="fontFamily"></param>
        /// <returns>流</returns>
        public static byte[] CreateImageBytes(string code, int fontSize = 18, int width = 0, int height = 0, string fontFamily = "华文楷体")
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            width = width <= 0 ? fontSize * 2 * (code.Length - 1) + 2 * 4 : width;
            height = height <= 0 ? fontSize * 2 : height;

            var image = new Bitmap(width, height);
            var g = Graphics.FromImage(image);
            g.Clear(Color.White);
            g.DrawLine(new Pen(Color.DarkRed), 0, height / 2, width, height / 2);

            var font = new Font(fontFamily, fontSize, (FontStyle.Bold | FontStyle.Italic));
            var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1f, true);
            var rectangle = new Rectangle(Point.Empty, new Size(width, height));
            var fomart = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            g.DrawString(code, font, brush, rectangle, fomart);
            
            var random = new Random();
            for (var i = 0; i < 100; i++)
            {
                image.SetPixel(random.Next(image.Width), random.Next(image.Height), Color.FromArgb(random.Next()));
            }

            //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            image.Dispose();

            var output = stream.ToArray();
            stream.Dispose();

            return output;
        }
    }
}
