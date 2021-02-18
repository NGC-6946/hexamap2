namespace hexamap.Nézet
{
    partial class View
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
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._currentPlayerLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._roundCounter = new System.Windows.Forms.ToolStripStatusLabel();
            this.button2 = new System.Windows.Forms.Button();
            this._buildSettlementBtn = new System.Windows.Forms.Button();
            this._buildRoadBtn = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(635, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "dobás";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._currentPlayerLabel,
            this._roundCounter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 509);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(825, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // _currentPlayerLabel
            // 
            this._currentPlayerLabel.Name = "_currentPlayerLabel";
            this._currentPlayerLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // _roundCounter
            // 
            this._roundCounter.Name = "_roundCounter";
            this._roundCounter.Size = new System.Drawing.Size(0, 16);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(635, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "Játék léptetése";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // _buildSettlementBtn
            // 
            this._buildSettlementBtn.Location = new System.Drawing.Point(635, 164);
            this._buildSettlementBtn.Name = "_buildSettlementBtn";
            this._buildSettlementBtn.Size = new System.Drawing.Size(153, 35);
            this._buildSettlementBtn.TabIndex = 3;
            this._buildSettlementBtn.Text = "Település építése";
            this._buildSettlementBtn.UseVisualStyleBackColor = true;
            this._buildSettlementBtn.Click += new System.EventHandler(this._buildSettlementBtn_Click);
            // 
            // _buildRoadBtn
            // 
            this._buildRoadBtn.Location = new System.Drawing.Point(635, 239);
            this._buildRoadBtn.Name = "_buildRoadBtn";
            this._buildRoadBtn.Size = new System.Drawing.Size(94, 29);
            this._buildRoadBtn.TabIndex = 4;
            this._buildRoadBtn.Text = "Út építése";
            this._buildRoadBtn.UseVisualStyleBackColor = true;
            this._buildRoadBtn.Click += new System.EventHandler(this._buildRoadBtn_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 531);
            this.Controls.Add(this._buildRoadBtn);
            this.Controls.Add(this._buildSettlementBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Name = "View";
            this.Text = "View";
            this.Load += new System.EventHandler(this.View_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _currentPlayerLabel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripStatusLabel _roundCounter;
        private System.Windows.Forms.Button _buildSettlementBtn;
        private System.Windows.Forms.Button _buildRoadBtn;
    }
}