<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options_Form))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox_ScValue = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox_RoundOff = New System.Windows.Forms.TextBox()
        Me.CheckBox_UndeformedView = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ResultValues = New System.Windows.Forms.CheckBox()
        Me.TextBox_DeformScale = New System.Windows.Forms.TextBox()
        Me.TextBox_ConstScale = New System.Windows.Forms.TextBox()
        Me.TextBox_loadScale = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox_Constraints = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Loadvalue = New System.Windows.Forms.CheckBox()
        Me.CheckBox_Load = New System.Windows.Forms.CheckBox()
        Me.CheckBox_OrginPt = New System.Windows.Forms.CheckBox()
        Me.CheckBox_GridLines = New System.Windows.Forms.CheckBox()
        Me.CheckBox_ElemLen = New System.Windows.Forms.CheckBox()
        Me.CheckBox_NodeCoord = New System.Windows.Forms.CheckBox()
        Me.CheckBox_NodeNumb = New System.Windows.Forms.CheckBox()
        Me.Button_Ok = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Scale Value : "
        '
        'TextBox_ScValue
        '
        Me.TextBox_ScValue.Location = New System.Drawing.Point(136, 17)
        Me.TextBox_ScValue.Name = "TextBox_ScValue"
        Me.TextBox_ScValue.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_ScValue.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TextBox_RoundOff)
        Me.GroupBox1.Controls.Add(Me.CheckBox_UndeformedView)
        Me.GroupBox1.Controls.Add(Me.CheckBox_ResultValues)
        Me.GroupBox1.Controls.Add(Me.TextBox_DeformScale)
        Me.GroupBox1.Controls.Add(Me.TextBox_ConstScale)
        Me.GroupBox1.Controls.Add(Me.TextBox_loadScale)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.CheckBox_Constraints)
        Me.GroupBox1.Controls.Add(Me.CheckBox_Loadvalue)
        Me.GroupBox1.Controls.Add(Me.CheckBox_Load)
        Me.GroupBox1.Controls.Add(Me.CheckBox_OrginPt)
        Me.GroupBox1.Controls.Add(Me.CheckBox_GridLines)
        Me.GroupBox1.Controls.Add(Me.CheckBox_ElemLen)
        Me.GroupBox1.Controls.Add(Me.CheckBox_NodeCoord)
        Me.GroupBox1.Controls.Add(Me.CheckBox_NodeNumb)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 54)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(271, 495)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View Control"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(27, 361)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(133, 16)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Significant Digits : "
        '
        'TextBox_RoundOff
        '
        Me.TextBox_RoundOff.Location = New System.Drawing.Point(167, 358)
        Me.TextBox_RoundOff.Name = "TextBox_RoundOff"
        Me.TextBox_RoundOff.Size = New System.Drawing.Size(74, 23)
        Me.TextBox_RoundOff.TabIndex = 16
        '
        'CheckBox_UndeformedView
        '
        Me.CheckBox_UndeformedView.AutoSize = True
        Me.CheckBox_UndeformedView.Location = New System.Drawing.Point(19, 240)
        Me.CheckBox_UndeformedView.Name = "CheckBox_UndeformedView"
        Me.CheckBox_UndeformedView.Size = New System.Drawing.Size(155, 20)
        Me.CheckBox_UndeformedView.TabIndex = 15
        Me.CheckBox_UndeformedView.Text = "Un-Deformed Model"
        Me.CheckBox_UndeformedView.UseVisualStyleBackColor = True
        '
        'CheckBox_ResultValues
        '
        Me.CheckBox_ResultValues.AutoSize = True
        Me.CheckBox_ResultValues.Location = New System.Drawing.Point(19, 210)
        Me.CheckBox_ResultValues.Name = "CheckBox_ResultValues"
        Me.CheckBox_ResultValues.Size = New System.Drawing.Size(113, 20)
        Me.CheckBox_ResultValues.TabIndex = 14
        Me.CheckBox_ResultValues.Text = "Result Values"
        Me.CheckBox_ResultValues.UseVisualStyleBackColor = True
        '
        'TextBox_DeformScale
        '
        Me.TextBox_DeformScale.Location = New System.Drawing.Point(167, 457)
        Me.TextBox_DeformScale.Name = "TextBox_DeformScale"
        Me.TextBox_DeformScale.Size = New System.Drawing.Size(74, 23)
        Me.TextBox_DeformScale.TabIndex = 13
        '
        'TextBox_ConstScale
        '
        Me.TextBox_ConstScale.Location = New System.Drawing.Point(167, 422)
        Me.TextBox_ConstScale.Name = "TextBox_ConstScale"
        Me.TextBox_ConstScale.Size = New System.Drawing.Size(74, 23)
        Me.TextBox_ConstScale.TabIndex = 12
        '
        'TextBox_loadScale
        '
        Me.TextBox_loadScale.Location = New System.Drawing.Point(167, 390)
        Me.TextBox_loadScale.Name = "TextBox_loadScale"
        Me.TextBox_loadScale.Size = New System.Drawing.Size(74, 23)
        Me.TextBox_loadScale.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 460)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 16)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Deformation Scale : "
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 425)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(132, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Constraint Scale : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(65, 393)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Load Scale : "
        '
        'CheckBox_Constraints
        '
        Me.CheckBox_Constraints.AutoSize = True
        Me.CheckBox_Constraints.Location = New System.Drawing.Point(19, 180)
        Me.CheckBox_Constraints.Name = "CheckBox_Constraints"
        Me.CheckBox_Constraints.Size = New System.Drawing.Size(101, 20)
        Me.CheckBox_Constraints.TabIndex = 7
        Me.CheckBox_Constraints.Text = "Constraints"
        Me.CheckBox_Constraints.UseVisualStyleBackColor = True
        '
        'CheckBox_Loadvalue
        '
        Me.CheckBox_Loadvalue.AutoSize = True
        Me.CheckBox_Loadvalue.Location = New System.Drawing.Point(19, 150)
        Me.CheckBox_Loadvalue.Name = "CheckBox_Loadvalue"
        Me.CheckBox_Loadvalue.Size = New System.Drawing.Size(97, 20)
        Me.CheckBox_Loadvalue.TabIndex = 6
        Me.CheckBox_Loadvalue.Text = "Load Value"
        Me.CheckBox_Loadvalue.UseVisualStyleBackColor = True
        '
        'CheckBox_Load
        '
        Me.CheckBox_Load.AutoSize = True
        Me.CheckBox_Load.Location = New System.Drawing.Point(19, 120)
        Me.CheckBox_Load.Name = "CheckBox_Load"
        Me.CheckBox_Load.Size = New System.Drawing.Size(57, 20)
        Me.CheckBox_Load.TabIndex = 5
        Me.CheckBox_Load.Text = "Load"
        Me.CheckBox_Load.UseVisualStyleBackColor = True
        '
        'CheckBox_OrginPt
        '
        Me.CheckBox_OrginPt.AutoSize = True
        Me.CheckBox_OrginPt.Location = New System.Drawing.Point(19, 311)
        Me.CheckBox_OrginPt.Name = "CheckBox_OrginPt"
        Me.CheckBox_OrginPt.Size = New System.Drawing.Size(98, 20)
        Me.CheckBox_OrginPt.TabIndex = 4
        Me.CheckBox_OrginPt.Text = "Orgin Point"
        Me.CheckBox_OrginPt.UseVisualStyleBackColor = True
        '
        'CheckBox_GridLines
        '
        Me.CheckBox_GridLines.AutoSize = True
        Me.CheckBox_GridLines.Location = New System.Drawing.Point(19, 281)
        Me.CheckBox_GridLines.Name = "CheckBox_GridLines"
        Me.CheckBox_GridLines.Size = New System.Drawing.Size(89, 20)
        Me.CheckBox_GridLines.TabIndex = 3
        Me.CheckBox_GridLines.Text = "Grid Lines"
        Me.CheckBox_GridLines.UseVisualStyleBackColor = True
        '
        'CheckBox_ElemLen
        '
        Me.CheckBox_ElemLen.AutoSize = True
        Me.CheckBox_ElemLen.Location = New System.Drawing.Point(19, 90)
        Me.CheckBox_ElemLen.Name = "CheckBox_ElemLen"
        Me.CheckBox_ElemLen.Size = New System.Drawing.Size(128, 20)
        Me.CheckBox_ElemLen.TabIndex = 2
        Me.CheckBox_ElemLen.Text = "Element Length"
        Me.CheckBox_ElemLen.UseVisualStyleBackColor = True
        '
        'CheckBox_NodeCoord
        '
        Me.CheckBox_NodeCoord.AutoSize = True
        Me.CheckBox_NodeCoord.Location = New System.Drawing.Point(19, 60)
        Me.CheckBox_NodeCoord.Name = "CheckBox_NodeCoord"
        Me.CheckBox_NodeCoord.Size = New System.Drawing.Size(142, 20)
        Me.CheckBox_NodeCoord.TabIndex = 1
        Me.CheckBox_NodeCoord.Text = "Node Coordinates"
        Me.CheckBox_NodeCoord.UseVisualStyleBackColor = True
        '
        'CheckBox_NodeNumb
        '
        Me.CheckBox_NodeNumb.AutoSize = True
        Me.CheckBox_NodeNumb.Location = New System.Drawing.Point(19, 30)
        Me.CheckBox_NodeNumb.Name = "CheckBox_NodeNumb"
        Me.CheckBox_NodeNumb.Size = New System.Drawing.Size(113, 20)
        Me.CheckBox_NodeNumb.TabIndex = 0
        Me.CheckBox_NodeNumb.Text = "Node Number"
        Me.CheckBox_NodeNumb.UseVisualStyleBackColor = True
        '
        'Button_Ok
        '
        Me.Button_Ok.Location = New System.Drawing.Point(92, 564)
        Me.Button_Ok.Name = "Button_Ok"
        Me.Button_Ok.Size = New System.Drawing.Size(109, 43)
        Me.Button_Ok.TabIndex = 3
        Me.Button_Ok.Text = "Ok"
        Me.Button_Ok.UseVisualStyleBackColor = True
        '
        'Options_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(295, 617)
        Me.Controls.Add(Me.Button_Ok)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox_ScValue)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Options_Form"
        Me.Text = "Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_ScValue As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox_NodeNumb As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_OrginPt As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_GridLines As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_ElemLen As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_NodeCoord As System.Windows.Forms.CheckBox
    Friend WithEvents Button_Ok As System.Windows.Forms.Button
    Friend WithEvents CheckBox_Constraints As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Loadvalue As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_Load As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox_DeformScale As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_ConstScale As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_loadScale As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox_ResultValues As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox_UndeformedView As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox_RoundOff As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
