using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MaterialSkin.Controls
{
    public class MaterialListView : ListView, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        [Browsable(false)]
        public Point MouseLocation { get; set; }

        private const int ITEM_PADDING = 12;

        public MaterialListView()
        {
            GridLines = false;
            FullRowSelect = true;
            HeaderStyle = ColumnHeaderStyle.Nonclickable;
            View = View.Details;
            OwnerDraw = true;
            ResizeRedraw = true;
            BorderStyle = BorderStyle.None;
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);

            //Fix for hovers, by default it doesn't redraw
            //TODO: should only redraw when the hovered line changed, this to reduce unnecessary redraws
            MouseLocation = new Point(-1, -1);
            MouseState = MouseState.OUT;
            MouseEnter += delegate { MouseState = MouseState.HOVER; };
            MouseLeave += delegate
            {
                MouseState = MouseState.OUT;
                MouseLocation = new Point(-1, -1);
                Invalidate();
            };
            MouseDown += delegate { MouseState = MouseState.DOWN; };
            MouseUp += delegate { MouseState = MouseState.HOVER; };
            MouseMove += delegate (object sender, MouseEventArgs args)
            {
                MouseLocation = args.Location;
                Invalidate();
            };
            ColumnWidthChanged += delegate
            {
                Invalidate();
            };
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            // antialias
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            e.Graphics.FillRectangle(
                new SolidBrush(SkinManager.GetApplicationBackgroundColor()),
                new Rectangle(e.Bounds.Location, new Size(Bounds.Width - e.Bounds.X, e.Bounds.Height)));

            e.Graphics.DrawString(e.Header.Text,
                SkinManager.ROBOTO_MEDIUM_10,
                SkinManager.GetSecondaryTextBrush(),
                new Rectangle(e.Bounds.X + ITEM_PADDING, e.Bounds.Y + ITEM_PADDING, e.Bounds.Width - ITEM_PADDING * 2, e.Bounds.Height - ITEM_PADDING * 2),
                getStringFormat());
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            //We draw the current line of items (= item with subitems) on a temp bitmap, then draw the bitmap at once. This is to reduce flickering.
            var b = new Bitmap(e.Item.Bounds.Width, e.Item.Bounds.Height);
            var g = Graphics.FromImage(b);

            //always draw default background
            g.FillRectangle(new SolidBrush(SkinManager.GetApplicationBackgroundColor()), new Rectangle(new Point(0, 0), e.Bounds.Size));

            if (e.State.HasFlag(ListViewItemStates.Selected))
            {
                //selected background
                g.FillRectangle(SkinManager.GetFlatButtonPressedBackgroundBrush(), new Rectangle(new Point(0, 0), e.Bounds.Size));
            }
            else if (e.Bounds.Contains(MouseLocation) && MouseState == MouseState.HOVER)
            {
                //hover background
                g.FillRectangle(SkinManager.GetFlatButtonHoverBackgroundBrush(), new Rectangle(new Point(0, 0), e.Bounds.Size));
            }

            //Draw separator
            g.DrawLine(new Pen(SkinManager.GetDividersColor()), e.Bounds.Left, 0, e.Bounds.Right, 0);

            e.Graphics.DrawImage((Image)b.Clone(), e.Item.Bounds.Location);
            g.Dispose();
            b.Dispose();
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            // antialias
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            // We draw string for subitem
            e.Graphics.DrawString(
                e.SubItem.Text,
                SkinManager.ROBOTO_MEDIUM_10,
                SkinManager.GetPrimaryTextBrush(),
                new Rectangle(
                    new Point(e.Bounds.X + ITEM_PADDING, e.Bounds.Y + ITEM_PADDING),
                    new Size(e.Bounds.Width - 2 * ITEM_PADDING, e.Bounds.Height - 2 * ITEM_PADDING)),
                getStringFormat());
        }

        private StringFormat getStringFormat()
        {
            return new StringFormat
            {
                FormatFlags = StringFormatFlags.LineLimit,
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
