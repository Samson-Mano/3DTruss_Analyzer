<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ThreeD_Truss_Solver
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ThreeD_Truss_Solver))
        Me.CheckBox_StrSelfWt = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_Ax = New System.Windows.Forms.TextBox()
        Me.TextBox_Ay = New System.Windows.Forms.TextBox()
        Me.TextBox_Az = New System.Windows.Forms.TextBox()
        Me.RichTextBox_AnalysisUpdate = New System.Windows.Forms.RichTextBox()
        Me.Label_S = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CheckBox_StrSelfWt
        '
        Me.CheckBox_StrSelfWt.AutoSize = True
        Me.CheckBox_StrSelfWt.Location = New System.Drawing.Point(10, 21)
        Me.CheckBox_StrSelfWt.Name = "CheckBox_StrSelfWt"
        Me.CheckBox_StrSelfWt.Size = New System.Drawing.Size(216, 20)
        Me.CheckBox_StrSelfWt.TabIndex = 1
        Me.CheckBox_StrSelfWt.Text = "Include Structure Selfweight"
        Me.CheckBox_StrSelfWt.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(376, 102)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 34)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Ax : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(57, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Ay : "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(55, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Az : "
        '
        'TextBox_Ax
        '
        Me.TextBox_Ax.Location = New System.Drawing.Point(101, 55)
        Me.TextBox_Ax.Name = "TextBox_Ax"
        Me.TextBox_Ax.Size = New System.Drawing.Size(105, 23)
        Me.TextBox_Ax.TabIndex = 6
        Me.TextBox_Ax.Text = "0"
        '
        'TextBox_Ay
        '
        Me.TextBox_Ay.Location = New System.Drawing.Point(101, 84)
        Me.TextBox_Ay.Name = "TextBox_Ay"
        Me.TextBox_Ay.Size = New System.Drawing.Size(105, 23)
        Me.TextBox_Ay.TabIndex = 7
        Me.TextBox_Ay.Text = "0"
        '
        'TextBox_Az
        '
        Me.TextBox_Az.Location = New System.Drawing.Point(101, 113)
        Me.TextBox_Az.Name = "TextBox_Az"
        Me.TextBox_Az.Size = New System.Drawing.Size(105, 23)
        Me.TextBox_Az.TabIndex = 8
        Me.TextBox_Az.Text = "-9.80665"
        '
        'RichTextBox_AnalysisUpdate
        '
        Me.RichTextBox_AnalysisUpdate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox_AnalysisUpdate.Location = New System.Drawing.Point(12, 151)
        Me.RichTextBox_AnalysisUpdate.Name = "RichTextBox_AnalysisUpdate"
        Me.RichTextBox_AnalysisUpdate.Size = New System.Drawing.Size(616, 136)
        Me.RichTextBox_AnalysisUpdate.TabIndex = 9
        Me.RichTextBox_AnalysisUpdate.Text = ""
        '
        'Label_S
        '
        Me.Label_S.AutoSize = True
        Me.Label_S.Location = New System.Drawing.Point(233, 9)
        Me.Label_S.Name = "Label_S"
        Me.Label_S.Size = New System.Drawing.Size(49, 16)
        Me.Label_S.TabIndex = 10
        Me.Label_S.Text = "Label4"
        '
        'ThreeD_Truss_Solver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(640, 299)
        Me.Controls.Add(Me.Label_S)
        Me.Controls.Add(Me.RichTextBox_AnalysisUpdate)
        Me.Controls.Add(Me.TextBox_Az)
        Me.Controls.Add(Me.TextBox_Ay)
        Me.Controls.Add(Me.TextBox_Ax)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CheckBox_StrSelfWt)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ThreeD_Truss_Solver"
        Me.Text = "3D_Truss_Solver"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBox_StrSelfWt As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox_Ax As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Ay As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_Az As System.Windows.Forms.TextBox
    Friend WithEvents RichTextBox_AnalysisUpdate As System.Windows.Forms.RichTextBox
    Friend WithEvents Label_S As System.Windows.Forms.Label
End Class
