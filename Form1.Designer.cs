
namespace apProjetoRotasTrem
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvGrafo = new System.Windows.Forms.DataGridView();
            this.btnOrdTop = new System.Windows.Forms.Button();
            this.txtSaida = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrafo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGrafo
            // 
            this.dgvGrafo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrafo.Location = new System.Drawing.Point(13, 54);
            this.dgvGrafo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvGrafo.Name = "dgvGrafo";
            this.dgvGrafo.RowHeadersWidth = 51;
            this.dgvGrafo.RowTemplate.Height = 24;
            this.dgvGrafo.Size = new System.Drawing.Size(1000, 478);
            this.dgvGrafo.TabIndex = 0;
            // 
            // btnOrdTop
            // 
            this.btnOrdTop.Location = new System.Drawing.Point(13, 10);
            this.btnOrdTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOrdTop.Name = "btnOrdTop";
            this.btnOrdTop.Size = new System.Drawing.Size(226, 39);
            this.btnOrdTop.TabIndex = 1;
            this.btnOrdTop.Text = "Ordenação Topológica";
            this.btnOrdTop.UseVisualStyleBackColor = true;
            this.btnOrdTop.Click += new System.EventHandler(this.btnOrdTop_Click);
            // 
            // txtSaida
            // 
            this.txtSaida.Location = new System.Drawing.Point(247, 14);
            this.txtSaida.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSaida.Name = "txtSaida";
            this.txtSaida.Size = new System.Drawing.Size(765, 30);
            this.txtSaida.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 546);
            this.Controls.Add(this.txtSaida);
            this.Controls.Add(this.btnOrdTop);
            this.Controls.Add(this.dgvGrafo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrafo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGrafo;
        private System.Windows.Forms.Button btnOrdTop;
        private System.Windows.Forms.TextBox txtSaida;
    }
}

