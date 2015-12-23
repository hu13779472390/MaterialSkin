using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialSkin.Animations;
using System;

namespace MaterialSkin.Controls
{
    public class MaterialSmallCard : Control, IMaterialControl
    {
        #region Variables

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        
        Image image;

        string information = "Info";
        string fontcolor = "#33b679";
        string thumbnailcolor = "#33b679";

        Color BgColor;
        Color StringColor;
        Color ThumbnailBGColor;
        Color BorderColor = ColorTranslator.FromHtml("#dbdbdb");
    
        #endregion

        #region Properties

        [Category("Appearance")]
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public string ThumbnailColor
        {
            get { return thumbnailcolor; }
            set
            {
                thumbnailcolor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public string FontColor
        {
            get { return fontcolor; }
            set
            {
                fontcolor = value;
                Invalidate();
            }
        }
        [Category("Appearance")]
        public string Info
        {
            get { return information; }
            set
            {
                information = value;
                Invalidate();
            }
        }

        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Height = 52;
        }

        public MaterialSmallCard()
        {
            Height = 52; 
            Width = 182; 
            DoubleBuffered = true;

            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            ForeColor = Color.Transparent;
            BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            StringColor = ColorTranslator.FromHtml(fontcolor);
            ThumbnailBGColor = ColorTranslator.FromHtml(thumbnailcolor);

            var BG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 3, 1);
            var ThumbnailBG = DrawHelper.CreateLeftRoundRect(1, 1, 50, 49, 1);

            g.FillPath(new SolidBrush(BgColor), BG);
            g.DrawPath(new Pen(BorderColor), BG);

            g.FillPath(new SolidBrush(ThumbnailBGColor), ThumbnailBG);
            g.DrawPath(new Pen(ThumbnailBGColor), ThumbnailBG);

            if (image != null)
            { g.DrawImage(image, 3, 3, 48, 47); }
            if (Enabled)
            { g.DrawString(Text, SkinManager.ROBOTO_MEDIUM_10, new SolidBrush(StringColor), new PointF(58.6f, 9f)); }
            else
            { g.DrawString("Wait...", SkinManager.ROBOTO_MEDIUM_10, new SolidBrush(StringColor), new PointF(58.6f, 9f)); }

            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(information, SkinManager.ROBOTO_MEDIUM_10, new SolidBrush(ColorTranslator.FromHtml("#999999")), new PointF(59.1f, 26f));
        }
    }
}