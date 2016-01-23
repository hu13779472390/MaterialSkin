using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MaterialSkin.Controls
{
    public partial class MaterialListBox : ListBox, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public Point MouseLocation { get; set; }

        HashSet<Rectangle> ItemRectangles;
        Rectangle ActualItemRectangle;

        const int ITEM_PADDING = 12;

        public MaterialListBox()
        {
            BackColor = SkinManager.GetApplicationBackgroundColor();
            BorderStyle = BorderStyle.None;
            DrawMode = DrawMode.OwnerDrawVariable;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            ResizeRedraw = true;
            MouseLocation = new Point(-1, -1);

            ItemRectangles = new HashSet<Rectangle>();
            ActualItemRectangle = new Rectangle(-1, -1, 0, 0);

            MouseHover += delegate
            {
                MouseState = MouseState.HOVER;
                MouseLocation = new Point(-1, -1);
                Invalidate();
            };
            MouseLeave += delegate
            {
                MouseState = MouseState.OUT;
                MouseLocation = new Point(-1, -1);
                Invalidate();
            };
            MouseMove += delegate (object sender, MouseEventArgs args)
            {
                MouseLocation = args.Location;
                // if mouse is out of rectangle
                if (!ActualItemRectangle.Contains(MouseLocation))
                {
                    // redraw actual hovered item
                    Invalidate(ActualItemRectangle);
                    // reset ActualItemRectangle before assign - kills flickering
                    ActualItemRectangle = new Rectangle(-1, -1, 0, 0);
                    // cycle
                    foreach (Rectangle r in ItemRectangles)
                    {
                        // this is correct rectangle
                        if (r.Contains(MouseLocation))
                        {
                            // set as actual
                            ActualItemRectangle = r;
                            // invalidate region
                            Invalidate(r);
                            break;
                        }
                    }
                }
            };
            MouseDown += delegate { MouseState = MouseState.DOWN; };
            MouseUp += delegate { MouseState = MouseState.HOVER; };
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            e.ItemHeight = 40;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // for height eq 0 just call base method and jump out
            if (e.Bounds.Height == 0 || Items.Count == 0)
            {
                base.OnDrawItem(e);
                return;
            }

            //We draw the current line of items (= item with subitems) on a temp bitmap, then draw the bitmap at once. This is to reduce flickering.
            var b = new Bitmap(e.Bounds.Width, e.Bounds.Height);
            var g = Graphics.FromImage(b);

            // Always draw default background
            g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(new Point(0, 0), e.Bounds.Size));

            // draw selected background
            if (e.State.HasFlag(DrawItemState.Selected))
            {
                g.FillRectangle(SkinManager.GetFlatButtonPressedBackgroundBrush(), new Rectangle(new Point(0, 0), e.Bounds.Size));
            }
            // draw hover background
            else if (e.Bounds.Contains(MouseLocation))
            {
                g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), new Rectangle(new Point(0, 0), e.Bounds.Size));
            }

            // antialias
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //Draw text
            g.DrawString(
                GetItemText(Items[e.Index]),
                SkinManager.ROBOTO_MEDIUM_10,
                SkinManager.GetPrimaryTextBrush(),
                new Rectangle(
                    new Point(ITEM_PADDING, ITEM_PADDING),
                    new Size(e.Bounds.Width - 2 * ITEM_PADDING, e.Bounds.Height - 2 * ITEM_PADDING)),
                getStringFormat());

            //Draw separator
            g.DrawLine(new Pen(SkinManager.GetDividersColor()), e.Bounds.Left, 0, e.Bounds.Right, 0);

            e.Graphics.DrawImage((Image)b.Clone(), e.Bounds.Location);
            b.Dispose();
            g.Dispose();
            // ---
            ItemRectangles.Add(e.Bounds);
        }

        private StringFormat getStringFormat()
        {
            return new StringFormat
            {
                FormatFlags = StringFormatFlags.MeasureTrailingSpaces,
                Trimming = StringTrimming.EllipsisWord,
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center
            };
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //This is a hax for the needed padding.
            //Another way would be intercepting all ListViewItems and changing the sizes, but really, that will be a lot of work
            //This will do for now.
            Font = new Font(SkinManager.ROBOTO_MEDIUM_12.FontFamily, 24);
        }
    }
}

