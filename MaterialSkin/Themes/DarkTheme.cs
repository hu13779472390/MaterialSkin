using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSkin
{

    public class DarkTheme : ThemeScheme
    {

        #region Colors

        //Constant color values
        public override Color PRIMARY_TEXT { get { return Color.FromArgb(255, 255, 255, 255); } }
        public override Color SECONDARY_TEXT { get { return Color.FromArgb(179, 255, 255, 255); } }
        public override Color DISABLED_OR_HINT_TEXT { get { return Color.FromArgb(77, 255, 255, 255); } }
        public override Color DIVIDERS { get { return Color.FromArgb(31, 255, 255, 255); } }
        public override Color ICON_COLOR { get { return Color.FromArgb(31, 255, 255, 255); } }

        // Checkbox colors
        public override Color CHECKBOX_OFF { get { return Color.FromArgb(179, 255, 255, 255); } }
        public override Color CHECKBOX_OFF_DISABLED { get { return Color.FromArgb(77, 255, 255, 255); } }

        //Raised button
        public override Color RAISED_BUTTON_BACKGROUND { get { return Color.FromArgb(255, 255, 255, 255); } }

        //Flat button
        public override Color FLAT_BUTTON_BACKGROUND_HOVER { get { return Color.FromArgb(15.PercentageToColorComponent(), 0xCCCCCC.ToColor()); } }
        public override Color FLAT_BUTTON_BACKGROUND_PRESSED { get { return Color.FromArgb(25.PercentageToColorComponent(), 0xCCCCCC.ToColor()); } }
        public override Color FLAT_BUTTON_DISABLEDTEXT { get { return Color.FromArgb(30.PercentageToColorComponent(), 0xFFFFFF.ToColor()); } }
        public override Color FLAT_BUTTON_BACKGROUND { get { return Color.FromArgb(255, 51, 51, 51); } }
        public override Color FLAT_BUTTON_BACKGROUND_ACTIVE { get { return Color.FromArgb(255, 51, 51, 51); } }

        //ContextMenuStrip
        public override Color CMS_BACKGROUND_HOVER { get { return Color.FromArgb(38, 204, 204, 204); } }

        //Application background
        public override Color BACKGROUND { get { return Color.FromArgb(255, 51, 51, 51); } }

        #endregion

    }

}
