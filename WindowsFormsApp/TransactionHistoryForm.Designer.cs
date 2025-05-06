namespace WindowsFormsApp
{
    partial class TransactionHistoryForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransactions.Location = new System.Drawing.Point(16, 15);
            this.dataGridViewTransactions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.RowHeadersWidth = 51;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(482, 369);
            this.dataGridViewTransactions.TabIndex = 0;
            // 
            // TransactionHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 407);
            this.Controls.Add(this.dataGridViewTransactions);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TransactionHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Історія транзакцій";
            this.Load += new System.EventHandler(this.TransactionHistoryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
