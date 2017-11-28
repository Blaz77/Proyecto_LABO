namespace InterfazLavadora
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
            this.cmbPortName = new System.Windows.Forms.ComboBox();
            this.lblComPort = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblSend = new System.Windows.Forms.Label();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.rtfTerminal = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTemperatura = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTiempoGiro = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTimeoutCarga = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.chkModificar = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // tmrCheckComPorts
            // 
            this.tmrCheckComPorts.Enabled = true;
            this.tmrCheckComPorts.Interval = 500;
            // 
            // cmbPortName
            // 
            this.cmbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPortName.FormattingEnabled = true;
            this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
            this.cmbPortName.Location = new System.Drawing.Point(20, 36);
            this.cmbPortName.Name = "cmbPortName";
            this.cmbPortName.Size = new System.Drawing.Size(67, 21);
            this.cmbPortName.TabIndex = 3;
            // 
            // lblComPort
            // 
            this.lblComPort.AutoSize = true;
            this.lblComPort.Location = new System.Drawing.Point(17, 20);
            this.lblComPort.Name = "lblComPort";
            this.lblComPort.Size = new System.Drawing.Size(56, 13);
            this.lblComPort.TabIndex = 2;
            this.lblComPort.Text = "COM Port:";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBaudRate.Location = new System.Drawing.Point(108, 36);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(69, 21);
            this.cmbBaudRate.TabIndex = 5;
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(105, 20);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
            this.lblBaudRate.TabIndex = 4;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.Location = new System.Drawing.Point(330, 34);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 7;
            this.btnOpenPort.Text = "&Open Port";
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(330, 94);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblSend
            // 
            this.lblSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(17, 99);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(61, 13);
            this.lblSend.TabIndex = 8;
            this.lblSend.Text = "Send &Data:";
            // 
            // txtSendData
            // 
            this.txtSendData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSendData.Location = new System.Drawing.Point(86, 96);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(238, 20);
            this.txtSendData.TabIndex = 9;
            // 
            // rtfTerminal
            // 
            this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtfTerminal.Location = new System.Drawing.Point(12, 145);
            this.rtfTerminal.Name = "rtfTerminal";
            this.rtfTerminal.Size = new System.Drawing.Size(393, 68);
            this.rtfTerminal.TabIndex = 11;
            this.rtfTerminal.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Temperatura";
            // 
            // txtTemperatura
            // 
            this.txtTemperatura.Enabled = false;
            this.txtTemperatura.Location = new System.Drawing.Point(157, 236);
            this.txtTemperatura.Name = "txtTemperatura";
            this.txtTemperatura.Size = new System.Drawing.Size(112, 20);
            this.txtTemperatura.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tiempo de giro";
            // 
            // txtTiempoGiro
            // 
            this.txtTiempoGiro.Location = new System.Drawing.Point(157, 262);
            this.txtTiempoGiro.Name = "txtTiempoGiro";
            this.txtTiempoGiro.Size = new System.Drawing.Size(112, 20);
            this.txtTiempoGiro.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Timeout de carga";
            // 
            // txtTimeoutCarga
            // 
            this.txtTimeoutCarga.Location = new System.Drawing.Point(157, 288);
            this.txtTimeoutCarga.Name = "txtTimeoutCarga";
            this.txtTimeoutCarga.Size = new System.Drawing.Size(112, 20);
            this.txtTimeoutCarga.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Estado";
            // 
            // txtEstado
            // 
            this.txtEstado.Enabled = false;
            this.txtEstado.Location = new System.Drawing.Point(157, 314);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(112, 20);
            this.txtEstado.TabIndex = 13;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(289, 288);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(91, 46);
            this.btnEnviar.TabIndex = 14;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // chkModificar
            // 
            this.chkModificar.AutoSize = true;
            this.chkModificar.Location = new System.Drawing.Point(289, 261);
            this.chkModificar.Name = "chkModificar";
            this.chkModificar.Size = new System.Drawing.Size(69, 17);
            this.chkModificar.TabIndex = 15;
            this.chkModificar.Text = "Modificar";
            this.chkModificar.UseVisualStyleBackColor = true;
            this.chkModificar.CheckedChanged += new System.EventHandler(this.chkModificar_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 373);
            this.Controls.Add(this.chkModificar);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtEstado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTimeoutCarga);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTiempoGiro);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTemperatura);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtfTerminal);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblSend);
            this.Controls.Add(this.txtSendData);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.cmbBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.cmbPortName);
            this.Controls.Add(this.lblComPort);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrCheckComPorts;
        private System.Windows.Forms.ComboBox cmbPortName;
        private System.Windows.Forms.Label lblComPort;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.RichTextBox rtfTerminal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTemperatura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTiempoGiro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTimeoutCarga;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.CheckBox chkModificar;
    }
}

