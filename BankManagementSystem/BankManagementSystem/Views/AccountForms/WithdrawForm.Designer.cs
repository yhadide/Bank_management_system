namespace BankManagementSystem
{
    partial class WithdrawForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WithdrawForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtWithdrawAmount = new System.Windows.Forms.TextBox();
            this.btnSubmitWithdraw = new System.Windows.Forms.Button();
            this.amountLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtWithdrawAmount);
            this.panel1.Controls.Add(this.btnSubmitWithdraw);
            this.panel1.Controls.Add(this.amountLbl);
            this.panel1.Location = new System.Drawing.Point(259, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 127);
            this.panel1.TabIndex = 4;
            // 
            // txtWithdrawAmount
            // 
            this.txtWithdrawAmount.Location = new System.Drawing.Point(56, 41);
            this.txtWithdrawAmount.Name = "txtWithdrawAmount";
            this.txtWithdrawAmount.Size = new System.Drawing.Size(100, 20);
            this.txtWithdrawAmount.TabIndex = 1;
            // 
            // btnSubmitWithdraw
            // 
            this.btnSubmitWithdraw.Location = new System.Drawing.Point(56, 77);
            this.btnSubmitWithdraw.Name = "btnSubmitWithdraw";
            this.btnSubmitWithdraw.Size = new System.Drawing.Size(75, 23);
            this.btnSubmitWithdraw.TabIndex = 2;
            this.btnSubmitWithdraw.Text = "Submit";
            this.btnSubmitWithdraw.UseVisualStyleBackColor = true;
            this.btnSubmitWithdraw.Click += new System.EventHandler(this.btnSubmitWithdraw_Click);
            // 
            // amountLbl
            // 
            this.amountLbl.AutoSize = true;
            this.amountLbl.Location = new System.Drawing.Point(3, 44);
            this.amountLbl.Name = "amountLbl";
            this.amountLbl.Size = new System.Drawing.Size(46, 13);
            this.amountLbl.TabIndex = 0;
            this.amountLbl.Text = "Amount ";
            // 
            // WithdrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WithdrawForm";
            this.Text = "WithdrawForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtWithdrawAmount;
        private System.Windows.Forms.Button btnSubmitWithdraw;
        private System.Windows.Forms.Label amountLbl;
    }
}