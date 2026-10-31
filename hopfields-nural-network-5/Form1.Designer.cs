namespace hopfields_nural_network_5
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
      canvasBox = new PictureBox();
      clearBtn = new Button();
      valueBox = new TextBox();
      rBtn = new Button();
      predictBt = new Button();
      canvasBox2 = new PictureBox();
      comboB = new ComboBox();
      ((System.ComponentModel.ISupportInitialize)canvasBox).BeginInit();
      ((System.ComponentModel.ISupportInitialize)canvasBox2).BeginInit();
      SuspendLayout();
      // 
      // canvasBox
      // 
      canvasBox.Location = new Point(329, 40);
      canvasBox.Name = "canvasBox";
      canvasBox.Size = new Size(300, 300);
      canvasBox.TabIndex = 0;
      canvasBox.TabStop = false;
      // 
      // clearBtn
      // 
      clearBtn.Location = new Point(709, 135);
      clearBtn.Name = "clearBtn";
      clearBtn.Size = new Size(66, 40);
      clearBtn.TabIndex = 1;
      clearBtn.Text = "Clear";
      clearBtn.UseVisualStyleBackColor = true;
      // 
      // valueBox
      // 
      valueBox.Location = new Point(635, 102);
      valueBox.Name = "valueBox";
      valueBox.Size = new Size(140, 27);
      valueBox.TabIndex = 2;
      // 
      // rBtn
      // 
      rBtn.Location = new Point(635, 135);
      rBtn.Name = "rBtn";
      rBtn.Size = new Size(68, 39);
      rBtn.TabIndex = 3;
      rBtn.Text = "Add to";
      rBtn.UseVisualStyleBackColor = true;
      // 
      // predictBt
      // 
      predictBt.Location = new Point(635, 181);
      predictBt.Name = "predictBt";
      predictBt.Size = new Size(140, 39);
      predictBt.TabIndex = 6;
      predictBt.Text = "Restore Image";
      predictBt.UseVisualStyleBackColor = true;
      // 
      // canvasBox2
      // 
      canvasBox2.Location = new Point(12, 40);
      canvasBox2.Name = "canvasBox2";
      canvasBox2.Size = new Size(300, 300);
      canvasBox2.TabIndex = 8;
      canvasBox2.TabStop = false;
      // 
      // comboB
      // 
      comboB.FormattingEnabled = true;
      comboB.Location = new Point(635, 40);
      comboB.Name = "comboB";
      comboB.Size = new Size(140, 28);
      comboB.TabIndex = 9;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(789, 382);
      Controls.Add(comboB);
      Controls.Add(canvasBox2);
      Controls.Add(predictBt);
      Controls.Add(rBtn);
      Controls.Add(valueBox);
      Controls.Add(clearBtn);
      Controls.Add(canvasBox);
      Name = "Form1";
      Text = "Form1";
      Load += Form1_Load;
      ((System.ComponentModel.ISupportInitialize)canvasBox).EndInit();
      ((System.ComponentModel.ISupportInitialize)canvasBox2).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    public PictureBox canvasBox;
    private Button clearBtn;
    private TextBox valueBox;
    private Button rBtn;
    private Button predictBt;
    public PictureBox canvasBox2;
    private ComboBox comboB;
  }
}
