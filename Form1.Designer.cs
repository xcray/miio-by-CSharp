namespace miio
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.btToken = new System.Windows.Forms.Button();
            this.tbEnc = new System.Windows.Forms.TextBox();
            this.tbJson = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btDec = new System.Windows.Forms.Button();
            this.btEnc = new System.Windows.Forms.Button();
            this.btSend = new System.Windows.Forms.Button();
            this.tbDid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(60, 8);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(105, 21);
            this.tbIP.TabIndex = 0;
            this.tbIP.Text = "10.0.1.151";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(168, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Token：";
            // 
            // tbToken
            // 
            this.tbToken.Location = new System.Drawing.Point(209, 8);
            this.tbToken.Name = "tbToken";
            this.tbToken.Size = new System.Drawing.Size(205, 21);
            this.tbToken.TabIndex = 3;
            this.tbToken.Text = "d58e8397a80fd4347d56e2b5fcd02e64";
            // 
            // btToken
            // 
            this.btToken.Location = new System.Drawing.Point(508, 8);
            this.btToken.Name = "btToken";
            this.btToken.Size = new System.Drawing.Size(43, 21);
            this.btToken.TabIndex = 4;
            this.btToken.Text = "获取";
            this.btToken.UseVisualStyleBackColor = true;
            this.btToken.Click += new System.EventHandler(this.btToken_Click);
            // 
            // tbEnc
            // 
            this.tbEnc.Location = new System.Drawing.Point(372, 53);
            this.tbEnc.Multiline = true;
            this.tbEnc.Name = "tbEnc";
            this.tbEnc.Size = new System.Drawing.Size(354, 123);
            this.tbEnc.TabIndex = 5;
            // 
            // tbJson
            // 
            this.tbJson.Location = new System.Drawing.Point(12, 53);
            this.tbJson.Multiline = true;
            this.tbJson.Name = "tbJson";
            this.tbJson.Size = new System.Drawing.Size(354, 123);
            this.tbJson.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "JSON消息：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(380, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "密文：";
            // 
            // btDec
            // 
            this.btDec.Location = new System.Drawing.Point(575, 8);
            this.btDec.Name = "btDec";
            this.btDec.Size = new System.Drawing.Size(41, 21);
            this.btDec.TabIndex = 9;
            this.btDec.Text = "解密";
            this.btDec.UseVisualStyleBackColor = true;
            this.btDec.Click += new System.EventHandler(this.btDec_Click);
            // 
            // btEnc
            // 
            this.btEnc.Location = new System.Drawing.Point(622, 8);
            this.btEnc.Name = "btEnc";
            this.btEnc.Size = new System.Drawing.Size(41, 21);
            this.btEnc.TabIndex = 10;
            this.btEnc.Text = "加密";
            this.btEnc.UseVisualStyleBackColor = true;
            this.btEnc.Click += new System.EventHandler(this.btEnc_Click);
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(683, 8);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(41, 21);
            this.btSend.TabIndex = 11;
            this.btSend.Text = "发送";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // tbDid
            // 
            this.tbDid.Location = new System.Drawing.Point(445, 8);
            this.tbDid.Name = "tbDid";
            this.tbDid.Size = new System.Drawing.Size(57, 21);
            this.tbDid.TabIndex = 12;
            this.tbDid.Text = "00030c12";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(415, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "DID：";
            // 
            // tbInfo
            // 
            this.tbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbInfo.Location = new System.Drawing.Point(12, 182);
            this.tbInfo.Multiline = true;
            this.tbInfo.Name = "tbInfo";
            this.tbInfo.Size = new System.Drawing.Size(714, 143);
            this.tbInfo.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 327);
            this.Controls.Add(this.tbInfo);
            this.Controls.Add(this.tbDid);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.btEnc);
            this.Controls.Add(this.btDec);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbJson);
            this.Controls.Add(this.tbEnc);
            this.Controls.Add(this.btToken);
            this.Controls.Add(this.tbToken);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "miio by C#";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbToken;
        private System.Windows.Forms.Button btToken;
        private System.Windows.Forms.TextBox tbEnc;
        private System.Windows.Forms.TextBox tbJson;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btDec;
        private System.Windows.Forms.Button btEnc;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.TextBox tbDid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbInfo;
    }
}

