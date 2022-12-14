namespace DELFI_WHM.NET.DELFI_User_Control
{
    partial class uComboBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCaption = new DevExpress.XtraEditors.LabelControl();
            this.cbxValue = new DELFI_WHM.NET.DELFI_User_Control.uComboBox.CustomGridLookUpEdit();
            this.customGridLookUpEdit1View = new DELFI_WHM.NET.DELFI_User_Control.uComboBox.CustomGridView();
            ((System.ComponentModel.ISupportInitialize)(this.cbxValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCaption
            // 
            this.lblCaption.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblCaption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblCaption.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblCaption.Location = new System.Drawing.Point(0, 0);
            this.lblCaption.LookAndFeel.SkinName = "Office 2010 Blue";
            this.lblCaption.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lblCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(100, 21);
            this.lblCaption.TabIndex = 0;
            this.lblCaption.Text = "Caption";
            // 
            // cbxValue
            // 
            this.cbxValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxValue.EditValue = "";
            this.cbxValue.Location = new System.Drawing.Point(100, 0);
            this.cbxValue.Name = "cbxValue";
            this.cbxValue.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.cbxValue.Properties.Appearance.Options.UseForeColor = true;
            this.cbxValue.Properties.AppearanceDropDown.ForeColor = System.Drawing.Color.Black;
            this.cbxValue.Properties.AppearanceDropDown.Options.UseForeColor = true;
            this.cbxValue.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cbxValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxValue.Properties.ImmediatePopup = true;
            this.cbxValue.Properties.LookAndFeel.SkinName = "Office 2010 Blue";
            this.cbxValue.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cbxValue.Properties.NullText = "<Chọn giá trị>";
            this.cbxValue.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.cbxValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cbxValue.Properties.View = this.customGridLookUpEdit1View;
            this.cbxValue.Size = new System.Drawing.Size(200, 20);
            this.cbxValue.TabIndex = 2;
            this.cbxValue.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.cbxValue_QueryPopUp);
            this.cbxValue.EditValueChanged += new System.EventHandler(this.cbxValue_EditValueChanged);
            this.cbxValue.Click += new System.EventHandler(this.cbxValue_Click);
            this.cbxValue.DoubleClick += new System.EventHandler(this.cbxValue_DoubleClick);
            this.cbxValue.Leave += new System.EventHandler(this.cbxValue_Leave);
            this.cbxValue.Validated += new System.EventHandler(this.cbxValue_uValidated);
            // 
            // customGridLookUpEdit1View
            // 
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseFont = true;
            this.customGridLookUpEdit1View.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.customGridLookUpEdit1View.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.customGridLookUpEdit1View.Appearance.Row.Options.UseForeColor = true;
            this.customGridLookUpEdit1View.Appearance.ViewCaption.ForeColor = System.Drawing.Color.Black;
            this.customGridLookUpEdit1View.Appearance.ViewCaption.Options.UseForeColor = true;
            this.customGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit1View.Name = "customGridLookUpEdit1View";
            this.customGridLookUpEdit1View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.customGridLookUpEdit1View.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.customGridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.customGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.customGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // uComboBox
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxValue);
            this.Controls.Add(this.lblCaption);
            this.LookAndFeel.SkinName = "Office 2010 Blue";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "uComboBox";
            this.Size = new System.Drawing.Size(300, 21);
            ((System.ComponentModel.ISupportInitialize)(this.cbxValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblCaption;
        private CustomGridLookUpEdit cbxValue;
        private CustomGridView customGridLookUpEdit1View;
    }
}
