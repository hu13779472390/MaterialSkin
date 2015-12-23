using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSkin
{

    public class LightTheme : ThemeScheme
    {

        #region Colors

        //Constant color values
        public override Color PRIMARY_TEXT { get { return Color.FromArgb(222, 0, 0, 0); } }
        public override Color SECONDARY_TEXT { get { return Color.FromArgb(138, 0, 0, 0); } }
        public override Color DISABLED_OR_HINT_TEXT { get { return Color.FromArgb(66, 0, 0, 0); } }
        public override Color DIVIDERS { get { return Color.FromArgb(31, 0, 0, 0); } }
        public override Color ICON_COLOR { get { return Color.FromArgb(31, 0, 0, 0); } }

        // Checkbox colors
        public override Color CHECKBOX_OFF { get { return Color.FromArgb(138, 0, 0, 0); } }
        public override Color CHECKBOX_OFF_DISABLED { get { return Color.FromArgb(66, 0, 0, 0); } }

        //Raised button
        public override Color RAISED_BUTTON_BACKGROUND { get { return Color.FromArgb(255, 255, 255, 255); } }

        //Flat button
        public override Color FLAT_BUTTON_BACKGROUND_HOVER { get { return Color.FromArgb(20.PercentageToColorComponent(), 0x999999.ToColor()); } }
        public override Color FLAT_BUTTON_BACKGROUND_PRESSED { get { return Color.FromArgb(40.PercentageToColorComponent(), 0x999999.ToColor()); } }
        public override Color FLAT_BUTTON_DISABLEDTEXT { get { return Color.FromArgb(26.PercentageToColorComponent(), 0x000000.ToColor()); } }
        public override Color FLAT_BUTTON_BACKGROUND { get { return Color.FromArgb(255, 255, 255, 255); } }
        public override Color FLAT_BUTTON_BACKGROUND_ACTIVE { get { return Color.FromArgb(255, 255, 255, 255); } }

        //ContextMenuStrip
        public override Color CMS_BACKGROUND_HOVER { get { return Color.FromArgb(255, 238, 238, 238); } }

        //Application background
        public override Color BACKGROUND { get { return Color.FromArgb(255, 255, 255, 255); } }

        #endregion

    }

}
