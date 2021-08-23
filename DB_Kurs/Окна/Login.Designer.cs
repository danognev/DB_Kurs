
namespace DB_Kurs
{
  partial class Login
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.panel1 = new System.Windows.Forms.Panel();
      this.Button = new System.Windows.Forms.Button();
      this.PasswordBox = new System.Windows.Forms.TextBox();
      this.LoginBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.Button);
      this.panel1.Controls.Add(this.PasswordBox);
      this.panel1.Controls.Add(this.LoginBox);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Location = new System.Drawing.Point(12, 12);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(270, 130);
      this.panel1.TabIndex = 0;
      // 
      // Button
      // 
      this.Button.Enabled = false;
      this.Button.Location = new System.Drawing.Point(24, 66);
      this.Button.Name = "Button";
      this.Button.Size = new System.Drawing.Size(230, 49);
      this.Button.TabIndex = 4;
      this.Button.Text = "Авторизация";
      this.Button.UseVisualStyleBackColor = true;
      this.Button.Click += new System.EventHandler(this.ButtonClick);
      // 
      // PasswordBox
      // 
      this.PasswordBox.Location = new System.Drawing.Point(69, 40);
      this.PasswordBox.Name = "PasswordBox";
      this.PasswordBox.PasswordChar = '*';
      this.PasswordBox.Size = new System.Drawing.Size(185, 20);
      this.PasswordBox.TabIndex = 3;
      this.PasswordBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyUp);
      // 
      // LoginBox
      // 
      this.LoginBox.Location = new System.Drawing.Point(69, 14);
      this.LoginBox.Name = "LoginBox";
      this.LoginBox.Size = new System.Drawing.Size(185, 20);
      this.LoginBox.TabIndex = 2;
      this.LoginBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyUp);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 47);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(48, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Пароль:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(21, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Логин:";
      // 
      // Login
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.ClientSize = new System.Drawing.Size(294, 154);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Login";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Авторизация";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button Button;
    private System.Windows.Forms.TextBox PasswordBox;
    private System.Windows.Forms.TextBox LoginBox;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
  }
}

