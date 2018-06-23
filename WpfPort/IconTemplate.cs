using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfPort
{
    public class IconTemplate
    {
        public string ImageTag
        {
            get;
            set;
        }

        public string ImageIconSource { get; set; }

        public ImageBrush IconImage
        {
            get
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(this.ImageIconSource, UriKind.Relative));
                return brush;
            }
            set { this.IconImage = value; }
        }
        public string Name
        {
            get;
            set;
        }
    }
}
