<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Load_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Load_Form))
        Me.Load_DataGridView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox_Fz = New System.Windows.Forms.TextBox()
        Me.TextBox_Fy = New System.Windows.Forms.TextBox()
        Me.TextBox_Fx = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBox_Tz = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Ty = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Tx = New System.Windows.Forms.CheckBox()
        Me.Button_add = New System.Windows.Forms.Button()
        CType(Me.Load_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Load_DataGridView
        '
        Me.Load_DataGridView.AllowUserToAddRows = False
        Me.Load_DataGridView.AllowUserToDeleteRows = False
        Me.Load_DataGridView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Load_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Load_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        Me.Load_DataGridView.Location = New System.Drawing.Point(215, 18)
        Me.Load_DataGridView.Margin = New System.Windows.Forms.Padding(4)
        Me.Load_DataGridView.Name = "Load_DataGridView"
        Me.Load_DataGridView.ReadOnly = True
        Me.Load_DataGridView.Size = New System.Drawing.Size(577, 282)
        Me.Load_DataGridView.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 60
        '
        'Column2
        '
        Me.Column2.HeaderText = "FX"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "FY"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "FZ"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Column5
        '
        Me.Column5.HeaderText = "TX"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 50
        '
        'Column6
        '
        Me.Column6.HeaderText = "TY"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 50
        '
        'Column7
        '
        Me.Column7.HeaderText = "TZ"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        Me.Column7.Width = 50
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBox_Fz)
        Me.GroupBox1.Controls.Add(Me.TextBox_Fy)
        Me.GroupBox1.Controls.Add(Me.TextBox_Fx)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(193, 144)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Loads"
        '
        'TextBox_Fz
        '
        Me.TextBox_Fz.Location = New System.Drawing.Point(55, 107)
        Me.TextBox_Fz.Name = "TextBox_Fz"
        Me.TextBox_Fz.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Fz.TabIndex = 5
        Me.TextBox_Fz.Text = "0"
        '
        'TextBox_Fy
        '
        Me.TextBox_Fy.Location = New System.Drawing.Point(55, 67)
        Me.TextBox_Fy.Name = "TextBox_Fy"
        Me.TextBox_Fy.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Fy.TabIndex = 4
        Me.TextBox_Fy.Text = "0"
        '
        'TextBox_Fx
        '
        Me.TextBox_Fx.Location = New System.Drawing.Point(55, 27)
        Me.TextBox_Fx.Name = "TextBox_Fx"
        Me.TextBox_Fx.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_Fx.TabIndex = 3
        Me.TextBox_Fx.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(24, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "FZ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(24, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "FY"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(24, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "FX"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.CheckBox_Tz)
        Me.GroupBox2.Controls.Add(Me.CheckBox_Ty)
        Me.GroupBox2.Controls.Add(Me.CheckBox_Tx)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(193, 73)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Constraints"
        '
        'CheckBox_Tz
        '
        Me.CheckBox_Tz.AutoSize = True
        Me.CheckBox_Tz.Location = New System.Drawing.Point(137, 36)
        Me.CheckBox_Tz.Name = "CheckBox_Tz"
        Me.CheckBox_Tz.Size = New System.Drawing.Size(44, 20)
        Me.CheckBox_Tz.TabIndex = 2
        Me.CheckBox_Tz.Text = "TZ"
        Me.CheckBox_Tz.UseVisualStyleBackColor = True
        '
        'CheckBox_Ty
        '
        Me.CheckBox_Ty.AutoSize = True
        Me.CheckBox_Ty.Location = New System.Drawing.Point(77, 36)
        Me.CheckBox_Ty.Name = "CheckBox_Ty"
        Me.CheckBox_Ty.Size = New System.Drawing.Size(44, 20)
        Me.CheckBox_Ty.TabIndex = 1
        Me.CheckBox_Ty.Text = "TY"
        Me.CheckBox_Ty.UseVisualStyleBackColor = True
        '
        'CheckBox_Tx
        '
        Me.CheckBox_Tx.AutoSize = True
        Me.CheckBox_Tx.Location = New System.Drawing.Point(17, 36)
        Me.CheckBox_Tx.Name = "CheckBox_Tx"
        Me.CheckBox_Tx.Size = New System.Drawing.Size(44, 20)
        Me.CheckBox_Tx.TabIndex = 0
        Me.CheckBox_Tx.Text = "TX"
        Me.CheckBox_Tx.UseVisualStyleBackColor = True
        '
        'Button_add
        '
        Me.Button_add.Location = New System.Drawing.Point(49, 241)
        Me.Button_add.Name = "Button_add"
        Me.Button_add.Size = New System.Drawing.Size(118, 40)
        Me.Button_add.TabIndex = 3
        Me.Button_add.Text = "Add"
        Me.Button_add.UseVisualStyleBackColor = True
        '
        'Load_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 313)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Load_DataGridView)
        Me.Controls.Add(Me.Button_add)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Load_Form"
        Me.Text = "Add / Delete Loads & Constraints"
        CType(Me.Load_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Load_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox_Fz As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Fy As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Fx As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_Tz As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Ty As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Tx As System.Windows.Forms.CheckBox
    Friend WithEvents Button_add As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
