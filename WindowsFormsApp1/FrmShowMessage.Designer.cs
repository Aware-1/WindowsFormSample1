namespace WindowsFormsApp1
{
    partial class FrmShowMessage
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowMessage));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnCncl = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureOk = new System.Windows.Forms.PictureBox();
            this.pictureDanger = new System.Windows.Forms.PictureBox();
            this.pictureWarn = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDanger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWarn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(22, 102);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(22, 131);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Visible = false;
            this.btnNo.Click += new System.EventHandler(this.BtnNo_Click);
            // 
            // btnCncl
            // 
            this.btnCncl.Location = new System.Drawing.Point(22, 73);
            this.btnCncl.Name = "btnCncl";
            this.btnCncl.Size = new System.Drawing.Size(75, 23);
            this.btnCncl.TabIndex = 2;
            this.btnCncl.Text = "cancle";
            this.btnCncl.UseVisualStyleBackColor = true;
            this.btnCncl.Visible = false;
            this.btnCncl.Click += new System.EventHandler(this.BtnCncl_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(19, 23);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(0, 13);
            this.label.TabIndex = 4;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1491254409-truecheckacceptapprove_82933.ico");
            this.imageList1.Images.SetKeyName(1, "emblemimportant_103451.ico");
            this.imageList1.Images.SetKeyName(2, "sign-warning-icon_34355.ico");
            // 
            // pictureOk
            // 
            this.pictureOk.Image = global::WindowsFormsApp1.Properties.Resources.anydo_104098;
            this.pictureOk.Location = new System.Drawing.Point(408, 94);
            this.pictureOk.Name = "pictureOk";
            this.pictureOk.Size = new System.Drawing.Size(61, 60);
            this.pictureOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureOk.TabIndex = 5;
            this.pictureOk.TabStop = false;
            this.pictureOk.Visible = false;
            // 
            // pictureDanger
            // 
            this.pictureDanger.Image = global::WindowsFormsApp1.Properties.Resources.emblemimportant_103451;
            this.pictureDanger.Location = new System.Drawing.Point(408, 94);
            this.pictureDanger.Name = "pictureDanger";
            this.pictureDanger.Size = new System.Drawing.Size(61, 60);
            this.pictureDanger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureDanger.TabIndex = 6;
            this.pictureDanger.TabStop = false;
            this.pictureDanger.Visible = false;
            // 
            // pictureWarn
            // 
            this.pictureWarn.Image = global::WindowsFormsApp1.Properties.Resources.sign_warning_icon_34355;
            this.pictureWarn.Location = new System.Drawing.Point(381, 87);
            this.pictureWarn.Name = "pictureWarn";
            this.pictureWarn.Size = new System.Drawing.Size(102, 67);
            this.pictureWarn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureWarn.TabIndex = 7;
            this.pictureWarn.TabStop = false;
            this.pictureWarn.Visible = false;
            // 
            // FrmShowMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 174);
            this.Controls.Add(this.pictureWarn);
            this.Controls.Add(this.pictureDanger);
            this.Controls.Add(this.pictureOk);
            this.Controls.Add(this.label);
            this.Controls.Add(this.btnCncl);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnOk);
            this.Name = "FrmShowMessage";
            this.Text = "FrmShowMessage";
            ((System.ComponentModel.ISupportInitialize)(this.pictureOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDanger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWarn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnCncl;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureOk;
        private System.Windows.Forms.PictureBox pictureDanger;
        private System.Windows.Forms.PictureBox pictureWarn;
    }
}