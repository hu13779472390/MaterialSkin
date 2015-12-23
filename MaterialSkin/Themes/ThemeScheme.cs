using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSkin
{

    public abstract class ThemeScheme
    {

        #region Colors

        //Constant color values
        public virtual Color PRIMARY_TEXT { get { return Color.FromArgb(222, 0, 0, 0); } }
        public virtual Color SECONDARY_TEXT { get { return Color.FromArgb(138, 0, 0, 0); } }
        public virtual Color DISABLED_OR_HINT_TEXT { get { return Color.FromArgb(66, 0, 0, 0); } }
        public virtual Color DIVIDERS { get { return Color.FromArgb(31, 0, 0, 0); } }
        public virtual Color ICON_COLOR { get { return Color.FromArgb(31, 0, 0, 0); } }

        // Checkbox colors
        public virtual Color CHECKBOX_OFF { get { return Color.FromArgb(179, 255, 255, 255); } }
        public virtual Color CHECKBOX_OFF_DISABLED { get { return Color.FromArgb(77, 255, 255, 255); } }

        //Raised button
        public virtual Color RAISED_BUTTON_BACKGROUND { get { return Color.FromArgb(255, 255, 255, 255); } }

        //Flat button
        public virtual Color FLAT_BUTTON_BACKGROUND_HOVER { get { return Color.FromArgb(15.PercentageToColorComponent(), 0xCCCCCC.ToColor()); } }
        public virtual Color FLAT_BUTTON_BACKGROUND_PRESSED { get { return Color.FromArgb(25.PercentageToColorComponent(), 0xCCCCCC.ToColor()); } }
        public virtual Color FLAT_BUTTON_DISABLEDTEXT { get { return Color.FromArgb(30.PercentageToColorComponent(), 0xFFFFFF.ToColor()); } }
        public virtual Color FLAT_BUTTON_BACKGROUND { get { return Color.FromArgb(255, 51, 51, 51); } }
        public virtual Color FLAT_BUTTON_BACKGROUND_ACTIVE { get { return Color.FromArgb(150, 51, 51, 51); } }

        //ContextMenuStrip
        public virtual Color CMS_BACKGROUND_HOVER { get { return Color.FromArgb(38, 204, 204, 204); } }

        //Application background
        public virtual Color BACKGROUND { get { return Color.FromArgb(255, 51, 51, 51); } }

        #endregion

        #region Constant

        //Constant color values
        public virtual Brush PRIMARY_TEXT_BRUSH { get { return new SolidBrush(PRIMARY_TEXT); } }
        public virtual Brush SECONDARY_TEXT_BRUSH { get { return new SolidBrush(SECONDARY_TEXT); } }
        public virtual Brush DISABLED_OR_HINT_TEXT_BRUSH { get { return new SolidBrush(DISABLED_OR_HINT_TEXT); } }
        public virtual Brush DIVIDERS_BRUSH { get { return new SolidBrush(DIVIDERS); } }

        // Checkbox colors
        public virtual Brush CHECKBOX_OFF_BRUSH { get { return new SolidBrush(CHECKBOX_OFF); } }
        public virtual Brush CHECKBOX_OFF_DISABLED_BRUSH { get { return new SolidBrush(CHECKBOX_OFF_DISABLED); } }

        //Raised button
        public virtual Brush RAISED_BUTTON_BACKGROUND_BRUSH { get { return new SolidBrush(RAISED_BUTTON_BACKGROUND); } }
        public virtual Color RAISED_BUTTON_TEXT { get { return PRIMARY_TEXT; } }
        public virtual Brush RAISED_BUTTON_TEXT_BRUSH { get { return new SolidBrush(RAISED_BUTTON_TEXT); } }

        //Flat button
        public virtual Brush FLAT_BUTTON_BACKGROUND_HOVER_BRUSH { get { return new SolidBrush(FLAT_BUTTON_BACKGROUND_HOVER); } }
        public virtual Brush FLAT_BUTTON_BACKGROUND_PRESSED_BRUSH { get { return new SolidBrush(FLAT_BUTTON_BACKGROUND_PRESSED); } }
        public virtual Brush FLAT_BUTTON_BACKGROUND_ACTIVE_BRUSH { get { return new SolidBrush(FLAT_BUTTON_BACKGROUND_ACTIVE); } }
        public virtual Brush FLAT_BUTTON_DISABLEDTEXT_BRUSH { get { return new SolidBrush(FLAT_BUTTON_DISABLEDTEXT); } }

        //ContextMenuStrip
        public virtual Brush CMS_BACKGROUND_HOVER_BRUSH { get { return new SolidBrush(CMS_BACKGROUND_HOVER); } }

        //Application background
        public virtual Brush BACKGROUND_BRUSH { get { return new SolidBrush(BACKGROUND); } }

        #endregion

    }

}
