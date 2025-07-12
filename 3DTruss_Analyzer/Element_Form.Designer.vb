<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Element_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Element_Form))
        Me.Element_DataGridView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox_Erho = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_CArea = New System.Windows.Forms.TextBox()
        Me.TextBox_EMod = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button_add = New System.Windows.Forms.Button()
        Me.TextBox_TN = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox_FN = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button_Import = New System.Windows.Forms.Button()
        Me.Button_Delete = New System.Windows.Forms.Button()
        CType(Me.Element_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Element_DataGridView
        '
        Me.Element_DataGridView.AllowUserToAddRows = False
        Me.Element_DataGridView.AllowUserToDeleteRows = False
        Me.Element_DataGridView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Element_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Element_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4, Me.Column5, Me.Column6, Me.Column7})
        Me.Element_DataGridView.Location = New System.Drawing.Point(207, 15)
        Me.Element_DataGridView.Margin = New System.Windows.Forms.Padding(4)
        Me.Element_DataGridView.Name = "Element_DataGridView"
        Me.Element_DataGridView.ReadOnly = True
        Me.Element_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Element_DataGridView.Size = New System.Drawing.Size(616, 225)
        Me.Element_DataGridView.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 40
        '
        'Column2
        '
        Me.Column2.HeaderText = "From"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 50
        '
        'Column3
        '
        Me.Column3.HeaderText = "To "
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 50
        '
        'Column4
        '
        Me.Column4.HeaderText = "Length"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 80
        '
        'Column5
        '
        Me.Column5.HeaderText = "Young's E"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 120
        '
        'Column6
        '
        Me.Column6.HeaderText = "CS - Area"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Width = 120
        '
        'Column7
        '
        Me.Column7.HeaderText = "Rho"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.TextBox_Erho)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBox_CArea)
        Me.GroupBox1.Controls.Add(Me.TextBox_EMod)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(188, 118)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bar Element Properties"
        '
        'TextBox_Erho
        '
        Me.TextBox_Erho.Location = New System.Drawing.Point(51, 22)
        Me.TextBox_Erho.Name = "TextBox_Erho"
        Me.TextBox_Erho.Size = New System.Drawing.Size(126, 23)
        Me.TextBox_Erho.TabIndex = 7
        Me.TextBox_Erho.Text = "7850"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 16)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "rho : "
        '
        'TextBox_CArea
        '
        Me.TextBox_CArea.Location = New System.Drawing.Point(51, 82)
        Me.TextBox_CArea.Name = "TextBox_CArea"
        Me.TextBox_CArea.Size = New System.Drawing.Size(126, 23)
        Me.TextBox_CArea.TabIndex = 5
        Me.TextBox_CArea.Text = "0.00718"
        '
        'TextBox_EMod
        '
        Me.TextBox_EMod.Location = New System.Drawing.Point(51, 52)
        Me.TextBox_EMod.Name = "TextBox_EMod"
        Me.TextBox_EMod.Size = New System.Drawing.Size(126, 23)
        Me.TextBox_EMod.TabIndex = 4
        Me.TextBox_EMod.Text = "210000000000"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "A :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "E :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.Button_add)
        Me.GroupBox2.Controls.Add(Me.TextBox_TN)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.TextBox_FN)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 139)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(188, 164)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Member Connectivity"
        '
        'Button_add
        '
        Me.Button_add.Location = New System.Drawing.Point(31, 103)
        Me.Button_add.Name = "Button_add"
        Me.Button_add.Size = New System.Drawing.Size(110, 40)
        Me.Button_add.TabIndex = 3
        Me.Button_add.Text = "Add Element"
        Me.Button_add.UseVisualStyleBackColor = True
        '
        'TextBox_TN
        '
        Me.TextBox_TN.Location = New System.Drawing.Point(103, 58)
        Me.TextBox_TN.Name = "TextBox_TN"
        Me.TextBox_TN.Size = New System.Drawing.Size(58, 23)
        Me.TextBox_TN.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 16)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "To Node : "
        '
        'TextBox_FN
        '
        Me.TextBox_FN.Location = New System.Drawing.Point(103, 27)
        Me.TextBox_FN.Name = "TextBox_FN"
        Me.TextBox_FN.Size = New System.Drawing.Size(58, 23)
        Me.TextBox_FN.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(93, 16)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "From Node : "
        '
        'Button_Import
        '
        Me.Button_Import.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Import.Location = New System.Drawing.Point(369, 254)
        Me.Button_Import.Name = "Button_Import"
        Me.Button_Import.Size = New System.Drawing.Size(135, 40)
        Me.Button_Import.TabIndex = 6
        Me.Button_Import.Text = "Import Elements"
        Me.Button_Import.UseVisualStyleBackColor = True
        '
        'Button_Delete
        '
        Me.Button_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Delete.Location = New System.Drawing.Point(637, 254)
        Me.Button_Delete.Name = "Button_Delete"
        Me.Button_Delete.Size = New System.Drawing.Size(135, 40)
        Me.Button_Delete.TabIndex = 7
        Me.Button_Delete.Text = "Delete Element(s)"
        Me.Button_Delete.UseVisualStyleBackColor = True
        '
        'Element_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(839, 309)
        Me.Controls.Add(Me.Button_Delete)
        Me.Controls.Add(Me.Button_Import)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Element_DataGridView)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Element_Form"
        Me.Text = "Add / Delete Elements"
        CType(Me.Element_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Element_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox_CArea As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_EMod As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox_TN As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TextBox_FN As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button_add As System.Windows.Forms.Button
    Friend WithEvents Button_Import As System.Windows.Forms.Button
    Friend WithEvents Button_Delete As System.Windows.Forms.Button
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TextBox_Erho As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
