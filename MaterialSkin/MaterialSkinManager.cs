using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin.Properties;

namespace MaterialSkin
{

    public class MaterialSkinManager
    {

        //Singleton instance
        private static MaterialSkinManager instance;

        //Forms to control
        private readonly List<MaterialForm> formsToManage = new List<MaterialForm>();

        //Theme
        private ThemeScheme theme;
        public ThemeScheme Theme
        {
            get { return theme; }
            set
            {
                theme = value;
                UpdateBackgrounds();
            }
        }

        private ColorScheme colorScheme;
        public ColorScheme ColorScheme
        {
            get { return colorScheme; }
            set
            {
                colorScheme = value;
                UpdateBackgrounds();
            }
        }

        //Application action bar
        public readonly Color ACTION_BAR_TEXT = Color.FromArgb(255, 255, 255, 255);
        public readonly Brush ACTION_BAR_TEXT_BRUSH = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public readonly Color ACTION_BAR_TEXT_SECONDARY = Color.FromArgb(153, 255, 255, 255);
        public readonly Brush ACTION_BAR_TEXT_SECONDARY_BRUSH = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

        #region Getters

        public Color GetPrimaryTextColor()
        {
            return theme.PRIMARY_TEXT;
        }

        public Brush GetPrimaryTextBrush()
        {
            return theme.PRIMARY_TEXT_BRUSH;
        }

        public Color GetSecondaryTextColor()
        {
            return theme.SECONDARY_TEXT;
        }

        public Brush GetSecondaryTextBrush()
        {
            return theme.SECONDARY_TEXT_BRUSH;
        }

        public Color GetDisabledOrHintColor()
        {
            return theme.DISABLED_OR_HINT_TEXT;
        }

        public Brush GetDisabledOrHintBrush()
        {
            return theme.DISABLED_OR_HINT_TEXT_BRUSH;
        }

        public Color GetDividersColor()
        {
            return theme.DIVIDERS;
        }

        public Brush GetDividersBrush()
        {
            return theme.DIVIDERS_BRUSH;
        }

        public Color GetCheckboxOffColor()
        {
            return theme.CHECKBOX_OFF;
        }

        public Brush GetCheckboxOffBrush()
        {
            return theme.CHECKBOX_OFF_BRUSH;
        }

        public Color GetCheckBoxOffDisabledColor()
        {
            return theme.CHECKBOX_OFF_DISABLED;
        }

        public Brush GetCheckBoxOffDisabledBrush()
        {
            return theme.CHECKBOX_OFF_DISABLED_BRUSH;
        }

        public Brush GetRaisedButtonBackgroundBrush()
        {
            return theme.RAISED_BUTTON_BACKGROUND_BRUSH;
        }

        public Brush GetRaisedButtonTextBrush(bool primary)
        {
            return theme.RAISED_BUTTON_TEXT_BRUSH;
        }

        public Color GetFlatButtonHoverBackgroundColor()
        {
            return theme.FLAT_BUTTON_BACKGROUND_HOVER;
        }
        public Color GetFlatButtonBackgroundColor()
        {
            return theme.FLAT_BUTTON_BACKGROUND;
        }

        public Brush GetFlatButtonHoverBackgroundBrush()
        {
            return theme.FLAT_BUTTON_BACKGROUND_HOVER_BRUSH;
        }

        public Color GetFlatButtonPressedBackgroundColor()
        {
            return theme.FLAT_BUTTON_BACKGROUND_PRESSED;
        }

        public Color GetFlatButtonPressedBackgroundActiveColor()
        {
            return theme.FLAT_BUTTON_BACKGROUND_ACTIVE;
        }

        public Color GetIconColor()
        {
            return theme.ICON_COLOR;
        }

        public Brush GetFlatButtonPressedBackgroundActiveBrush()
        {
            return theme.FLAT_BUTTON_BACKGROUND_ACTIVE_BRUSH;
        }

        public Brush GetFlatButtonPressedBackgroundBrush()
        {
            return theme.FLAT_BUTTON_BACKGROUND_PRESSED_BRUSH;
        }

        public Brush GetFlatButtonDisabledTextBrush()
        {
            return theme.FLAT_BUTTON_DISABLEDTEXT_BRUSH;
        }

        public Brush GetCmsSelectedItemBrush()
        {
            return theme.CMS_BACKGROUND_HOVER_BRUSH;
        }

        public Color GetApplicationBackgroundColor()
        {
            return theme.BACKGROUND;
        }

        #endregion

        //Roboto font
        public Font ROBOTO_MEDIUM_12;
        public Font ROBOTO_REGULAR_11;
        public Font ROBOTO_MEDIUM_11;
        public Font ROBOTO_MEDIUM_10;

        //Other constants
        public int FORM_PADDING = 14;

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);

        private MaterialSkinManager()
        {
            ROBOTO_MEDIUM_12 = new Font(LoadFont(Resources.Roboto_Medium), 12f);
            ROBOTO_MEDIUM_10 = new Font(LoadFont(Resources.Roboto_Medium), 10f);
            ROBOTO_REGULAR_11 = new Font(LoadFont(Resources.Roboto_Regular), 11f);
            ROBOTO_MEDIUM_11 = new Font(LoadFont(Resources.Roboto_Medium), 11f);
            Theme = new LightTheme();
            ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        public static MaterialSkinManager Instance
        {
            get { return instance ?? (instance = new MaterialSkinManager()); }
        }

        public void AddFormToManage(MaterialForm materialForm)
        {
            formsToManage.Add(materialForm);
            UpdateBackgrounds();
        }

        public void RemoveFormToManage(MaterialForm materialForm)
        {
            formsToManage.Remove(materialForm);
        }

        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();

        private FontFamily LoadFont(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
        }

        private void UpdateBackgrounds()
        {
            var newBackColor = GetApplicationBackgroundColor();
            foreach (var materialForm in formsToManage)
            {
                materialForm.BackColor = newBackColor;
                UpdateControl(materialForm, newBackColor);
            }
        }

        private void UpdateToolStrip(ToolStrip toolStrip, Color newBackColor)
        {
            if (toolStrip == null) return;

            toolStrip.BackColor = newBackColor;
            foreach (ToolStripItem control in toolStrip.Items)
            {
                control.BackColor = newBackColor;
                if (control is MaterialToolStripMenuItem && (control as MaterialToolStripMenuItem).HasDropDown)
                {

                    //recursive call
                    UpdateToolStrip((control as MaterialToolStripMenuItem).DropDown, newBackColor);
                }
            }
        }

        private void UpdateControl(Control controlToUpdate, Color newBackColor)
        {
            if (controlToUpdate == null) return;

            if (controlToUpdate.ContextMenuStrip != null)
            {
                UpdateToolStrip(controlToUpdate.ContextMenuStrip, newBackColor);
            }
            var tabControl = controlToUpdate as MaterialTabControl;
            if (tabControl != null)
            {
                foreach (TabPage tabPage in tabControl.TabPages)
                {
                    tabPage.BackColor = newBackColor;
                }
            }

            if (controlToUpdate is MaterialDivider)
            {
                controlToUpdate.BackColor = GetDividersColor();
            }

            if (controlToUpdate is MaterialListView)
            {
                controlToUpdate.BackColor = newBackColor;

            }

            //recursive call
            foreach (Control control in controlToUpdate.Controls)
            {
                UpdateControl(control, newBackColor);
            }

            controlToUpdate.Invalidate();
        }

    }

}
