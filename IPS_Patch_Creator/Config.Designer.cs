namespace IPS_Patch_Creator
{
    partial class Config
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.button_save_config = new System.Windows.Forms.Button();
            this.label_NFIM_max = new System.Windows.Forms.Label();
            this.label_ES_max = new System.Windows.Forms.Label();
            this.label_FS_max = new System.Windows.Forms.Label();
            this.label_NFIM_min = new System.Windows.Forms.Label();
            this.label_ES_min = new System.Windows.Forms.Label();
            this.label_FS_min = new System.Windows.Forms.Label();
            this.textBox_NFIM_MAX = new System.Windows.Forms.TextBox();
            this.textBox_NFIM_MIN = new System.Windows.Forms.TextBox();
            this.textBox_ESMAX = new System.Windows.Forms.TextBox();
            this.textBox_ESMIN = new System.Windows.Forms.TextBox();
            this.textBox_FSMAX = new System.Windows.Forms.TextBox();
            this.textBox_FSMIN = new System.Windows.Forms.TextBox();
            this.button_reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_save_config
            // 
            this.button_save_config.Location = new System.Drawing.Point(6, 168);
            this.button_save_config.Name = "button_save_config";
            this.button_save_config.Size = new System.Drawing.Size(100, 23);
            this.button_save_config.TabIndex = 25;
            this.button_save_config.Text = "Save";
            this.button_save_config.UseVisualStyleBackColor = true;
            this.button_save_config.Click += new System.EventHandler(this.button_save_config_Click);
            // 
            // label_NFIM_max
            // 
            this.label_NFIM_max.AutoSize = true;
            this.label_NFIM_max.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NFIM_max.Location = new System.Drawing.Point(3, 136);
            this.label_NFIM_max.Name = "label_NFIM_max";
            this.label_NFIM_max.Size = new System.Drawing.Size(232, 16);
            this.label_NFIM_max.TabIndex = 24;
            this.label_NFIM_max.Text = "NFIM maximum nca size limit (bytes) :";
            // 
            // label_ES_max
            // 
            this.label_ES_max.AutoSize = true;
            this.label_ES_max.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ES_max.Location = new System.Drawing.Point(3, 84);
            this.label_ES_max.Name = "label_ES_max";
            this.label_ES_max.Size = new System.Drawing.Size(219, 16);
            this.label_ES_max.TabIndex = 23;
            this.label_ES_max.Text = "ES maximum nca size limit (bytes) :";
            // 
            // label_FS_max
            // 
            this.label_FS_max.AutoSize = true;
            this.label_FS_max.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FS_max.Location = new System.Drawing.Point(3, 32);
            this.label_FS_max.Name = "label_FS_max";
            this.label_FS_max.Size = new System.Drawing.Size(218, 16);
            this.label_FS_max.TabIndex = 22;
            this.label_FS_max.Text = "FS maximum nca size limit (bytes) :";
            // 
            // label_NFIM_min
            // 
            this.label_NFIM_min.AutoSize = true;
            this.label_NFIM_min.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NFIM_min.Location = new System.Drawing.Point(3, 110);
            this.label_NFIM_min.Name = "label_NFIM_min";
            this.label_NFIM_min.Size = new System.Drawing.Size(228, 16);
            this.label_NFIM_min.TabIndex = 21;
            this.label_NFIM_min.Text = "NFIM minimum nca size limit (bytes) :";
            // 
            // label_ES_min
            // 
            this.label_ES_min.AutoSize = true;
            this.label_ES_min.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ES_min.Location = new System.Drawing.Point(3, 58);
            this.label_ES_min.Name = "label_ES_min";
            this.label_ES_min.Size = new System.Drawing.Size(215, 16);
            this.label_ES_min.TabIndex = 20;
            this.label_ES_min.Text = "ES minimum nca size limit (bytes) :";
            // 
            // label_FS_min
            // 
            this.label_FS_min.AutoSize = true;
            this.label_FS_min.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FS_min.Location = new System.Drawing.Point(3, 6);
            this.label_FS_min.Name = "label_FS_min";
            this.label_FS_min.Size = new System.Drawing.Size(214, 16);
            this.label_FS_min.TabIndex = 19;
            this.label_FS_min.Text = "FS minimum nca size limit (bytes) :";
            // 
            // textBox_NFIM_MAX
            // 
            this.textBox_NFIM_MAX.Location = new System.Drawing.Point(254, 135);
            this.textBox_NFIM_MAX.Name = "textBox_NFIM_MAX";
            this.textBox_NFIM_MAX.Size = new System.Drawing.Size(67, 20);
            this.textBox_NFIM_MAX.TabIndex = 18;
            this.textBox_NFIM_MAX.TextChanged += new System.EventHandler(this.textBox_NFIM_MAX_TextChanged);
            // 
            // textBox_NFIM_MIN
            // 
            this.textBox_NFIM_MIN.Location = new System.Drawing.Point(254, 109);
            this.textBox_NFIM_MIN.Name = "textBox_NFIM_MIN";
            this.textBox_NFIM_MIN.Size = new System.Drawing.Size(67, 20);
            this.textBox_NFIM_MIN.TabIndex = 17;
            this.textBox_NFIM_MIN.TextChanged += new System.EventHandler(this.textBox_NFIM_MIN_TextChanged);
            // 
            // textBox_ESMAX
            // 
            this.textBox_ESMAX.Location = new System.Drawing.Point(254, 83);
            this.textBox_ESMAX.Name = "textBox_ESMAX";
            this.textBox_ESMAX.Size = new System.Drawing.Size(67, 20);
            this.textBox_ESMAX.TabIndex = 16;
            this.textBox_ESMAX.TextChanged += new System.EventHandler(this.textBox_ESMAX_TextChanged);
            // 
            // textBox_ESMIN
            // 
            this.textBox_ESMIN.Location = new System.Drawing.Point(254, 57);
            this.textBox_ESMIN.Name = "textBox_ESMIN";
            this.textBox_ESMIN.Size = new System.Drawing.Size(67, 20);
            this.textBox_ESMIN.TabIndex = 15;
            this.textBox_ESMIN.TextChanged += new System.EventHandler(this.textBox_ESMIN_TextChanged);
            // 
            // textBox_FSMAX
            // 
            this.textBox_FSMAX.Location = new System.Drawing.Point(254, 31);
            this.textBox_FSMAX.Name = "textBox_FSMAX";
            this.textBox_FSMAX.Size = new System.Drawing.Size(67, 20);
            this.textBox_FSMAX.TabIndex = 14;
            this.textBox_FSMAX.TextChanged += new System.EventHandler(this.textBox_FSMAX_TextChanged);
            // 
            // textBox_FSMIN
            // 
            this.textBox_FSMIN.Location = new System.Drawing.Point(254, 5);
            this.textBox_FSMIN.Name = "textBox_FSMIN";
            this.textBox_FSMIN.Size = new System.Drawing.Size(67, 20);
            this.textBox_FSMIN.TabIndex = 13;
            this.textBox_FSMIN.TextChanged += new System.EventHandler(this.textBox_FSMIN_TextChanged);
            // 
            // button_reset
            // 
            this.button_reset.Location = new System.Drawing.Point(221, 168);
            this.button_reset.Name = "button_reset";
            this.button_reset.Size = new System.Drawing.Size(100, 23);
            this.button_reset.TabIndex = 26;
            this.button_reset.Text = "Reset Defaults";
            this.button_reset.UseVisualStyleBackColor = true;
            this.button_reset.Click += new System.EventHandler(this.button_reset_Click);
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 198);
            this.Controls.Add(this.button_reset);
            this.Controls.Add(this.button_save_config);
            this.Controls.Add(this.label_NFIM_max);
            this.Controls.Add(this.label_ES_max);
            this.Controls.Add(this.label_FS_max);
            this.Controls.Add(this.label_NFIM_min);
            this.Controls.Add(this.label_ES_min);
            this.Controls.Add(this.label_FS_min);
            this.Controls.Add(this.textBox_NFIM_MAX);
            this.Controls.Add(this.textBox_NFIM_MIN);
            this.Controls.Add(this.textBox_ESMAX);
            this.Controls.Add(this.textBox_ESMIN);
            this.Controls.Add(this.textBox_FSMAX);
            this.Controls.Add(this.textBox_FSMIN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_save_config;
        private System.Windows.Forms.Label label_NFIM_max;
        private System.Windows.Forms.Label label_ES_max;
        private System.Windows.Forms.Label label_FS_max;
        private System.Windows.Forms.Label label_NFIM_min;
        private System.Windows.Forms.Label label_ES_min;
        private System.Windows.Forms.Label label_FS_min;
        private System.Windows.Forms.TextBox textBox_NFIM_MAX;
        private System.Windows.Forms.TextBox textBox_NFIM_MIN;
        private System.Windows.Forms.TextBox textBox_ESMAX;
        private System.Windows.Forms.TextBox textBox_ESMIN;
        private System.Windows.Forms.TextBox textBox_FSMAX;
        private System.Windows.Forms.TextBox textBox_FSMIN;
        private System.Windows.Forms.Button button_reset;
    }
}