
namespace OrcaFascio
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtCaminhoArquivoComposicoes = new System.Windows.Forms.TextBox();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnCaminhoArquivoComposicoes = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.cmbProjetos = new System.Windows.Forms.ComboBox();
            this.cmbFiliais = new System.Windows.Forms.ComboBox();
            this.cmbColigadas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCaminhoArquivoOrcamento = new System.Windows.Forms.TextBox();
            this.btnCaminhoArquivoOrcamento = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtCaminhoArquivoComposicoes
            // 
            this.txtCaminhoArquivoComposicoes.Location = new System.Drawing.Point(91, 97);
            this.txtCaminhoArquivoComposicoes.Name = "txtCaminhoArquivoComposicoes";
            this.txtCaminhoArquivoComposicoes.Size = new System.Drawing.Size(238, 23);
            this.txtCaminhoArquivoComposicoes.TabIndex = 19;
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(122, 159);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(75, 23);
            this.btnImportar.TabIndex = 18;
            this.btnImportar.Text = "Importar";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnCaminhoArquivoComposicoes
            // 
            this.btnCaminhoArquivoComposicoes.Location = new System.Drawing.Point(335, 97);
            this.btnCaminhoArquivoComposicoes.Name = "btnCaminhoArquivoComposicoes";
            this.btnCaminhoArquivoComposicoes.Size = new System.Drawing.Size(94, 23);
            this.btnCaminhoArquivoComposicoes.TabIndex = 21;
            this.btnCaminhoArquivoComposicoes.Text = "Selecionar";
            this.btnCaminhoArquivoComposicoes.UseVisualStyleBackColor = true;
            this.btnCaminhoArquivoComposicoes.Click += new System.EventHandler(this.btnCaminhoArquivo_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(251, 159);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // cmbProjetos
            // 
            this.cmbProjetos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProjetos.FormattingEnabled = true;
            this.cmbProjetos.Location = new System.Drawing.Point(91, 64);
            this.cmbProjetos.Name = "cmbProjetos";
            this.cmbProjetos.Size = new System.Drawing.Size(338, 23);
            this.cmbProjetos.TabIndex = 16;
            // 
            // cmbFiliais
            // 
            this.cmbFiliais.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiliais.FormattingEnabled = true;
            this.cmbFiliais.Location = new System.Drawing.Point(91, 35);
            this.cmbFiliais.Name = "cmbFiliais";
            this.cmbFiliais.Size = new System.Drawing.Size(338, 23);
            this.cmbFiliais.TabIndex = 15;
            this.cmbFiliais.SelectedIndexChanged += new System.EventHandler(this.cmbFiliais_SelectedIndexChanged);
            // 
            // cmbColigadas
            // 
            this.cmbColigadas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColigadas.FormattingEnabled = true;
            this.cmbColigadas.Location = new System.Drawing.Point(91, 6);
            this.cmbColigadas.Name = "cmbColigadas";
            this.cmbColigadas.Size = new System.Drawing.Size(338, 23);
            this.cmbColigadas.TabIndex = 14;
            this.cmbColigadas.SelectedIndexChanged += new System.EventHandler(this.cmbColigadas_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Composições:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Orçamento:";
            // 
            // txtCaminhoArquivoOrcamento
            // 
            this.txtCaminhoArquivoOrcamento.Location = new System.Drawing.Point(91, 127);
            this.txtCaminhoArquivoOrcamento.Name = "txtCaminhoArquivoOrcamento";
            this.txtCaminhoArquivoOrcamento.Size = new System.Drawing.Size(238, 23);
            this.txtCaminhoArquivoOrcamento.TabIndex = 24;
            // 
            // btnCaminhoArquivoOrcamento
            // 
            this.btnCaminhoArquivoOrcamento.Location = new System.Drawing.Point(335, 127);
            this.btnCaminhoArquivoOrcamento.Name = "btnCaminhoArquivoOrcamento";
            this.btnCaminhoArquivoOrcamento.Size = new System.Drawing.Size(94, 23);
            this.btnCaminhoArquivoOrcamento.TabIndex = 25;
            this.btnCaminhoArquivoOrcamento.Text = "Selecionar";
            this.btnCaminhoArquivoOrcamento.UseVisualStyleBackColor = true;
            this.btnCaminhoArquivoOrcamento.Click += new System.EventHandler(this.btnCaminhoArquivoOrcamento_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 15);
            this.label3.TabIndex = 26;
            this.label3.Text = "Coligada:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 15);
            this.label4.TabIndex = 27;
            this.label4.Text = "Filial:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 28;
            this.label5.Text = "Projeto:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 187);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCaminhoArquivoOrcamento);
            this.Controls.Add(this.txtCaminhoArquivoOrcamento);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCaminhoArquivoComposicoes);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnCaminhoArquivoComposicoes);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.cmbProjetos);
            this.Controls.Add(this.cmbFiliais);
            this.Controls.Add(this.cmbColigadas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtCaminhoArquivoComposicoes;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnCaminhoArquivoComposicoes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ComboBox cmbProjetos;
        private System.Windows.Forms.ComboBox cmbFiliais;
        private System.Windows.Forms.ComboBox cmbColigadas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaminhoArquivoOrcamento;
        private System.Windows.Forms.Button btnCaminhoArquivoOrcamento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

