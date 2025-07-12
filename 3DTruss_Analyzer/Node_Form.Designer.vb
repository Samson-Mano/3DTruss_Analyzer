<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Node_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Node_Form))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button_AddNode = New System.Windows.Forms.Button()
        Me.TextBox_ZCoord = New System.Windows.Forms.TextBox()
        Me.TextBox_YCoord = New System.Windows.Forms.TextBox()
        Me.TextBox_XCoord = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Node_DataGridView = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button_DeleteNode = New System.Windows.Forms.Button()
        Me.Button_Import = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Node_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Button_AddNode)
        Me.GroupBox1.Controls.Add(Me.TextBox_ZCoord)
        Me.GroupBox1.Controls.Add(Me.TextBox_YCoord)
        Me.GroupBox1.Controls.Add(Me.TextBox_XCoord)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(163, 250)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Add Node"
        '
        'Button_AddNode
        '
        Me.Button_AddNode.Location = New System.Drawing.Point(30, 167)
        Me.Button_AddNode.Name = "Button_AddNode"
        Me.Button_AddNode.Size = New System.Drawing.Size(110, 40)
        Me.Button_AddNode.TabIndex = 4
        Me.Button_AddNode.Text = "Add Node"
        Me.Button_AddNode.UseVisualStyleBackColor = True
        '
        'TextBox_ZCoord
        '
        Me.TextBox_ZCoord.Location = New System.Drawing.Point(49, 120)
        Me.TextBox_ZCoord.Name = "TextBox_ZCoord"
        Me.TextBox_ZCoord.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_ZCoord.TabIndex = 3
        '
        'TextBox_YCoord
        '
        Me.TextBox_YCoord.Location = New System.Drawing.Point(49, 80)
        Me.TextBox_YCoord.Name = "TextBox_YCoord"
        Me.TextBox_YCoord.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_YCoord.TabIndex = 2
        '
        'TextBox_XCoord
        '
        Me.TextBox_XCoord.Location = New System.Drawing.Point(49, 40)
        Me.TextBox_XCoord.Name = "TextBox_XCoord"
        Me.TextBox_XCoord.Size = New System.Drawing.Size(100, 23)
        Me.TextBox_XCoord.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Z :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 83)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Y : "
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "X : "
        '
        'Node_DataGridView
        '
        Me.Node_DataGridView.AllowUserToAddRows = False
        Me.Node_DataGridView.AllowUserToDeleteRows = False
        Me.Node_DataGridView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Node_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Node_DataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.Column3, Me.Column4})
        Me.Node_DataGridView.Location = New System.Drawing.Point(185, 12)
        Me.Node_DataGridView.Name = "Node_DataGridView"
        Me.Node_DataGridView.ReadOnly = True
        Me.Node_DataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Node_DataGridView.Size = New System.Drawing.Size(411, 196)
        Me.Node_DataGridView.TabIndex = 5
        '
        'Column1
        '
        Me.Column1.HeaderText = "ID"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 50
        '
        'Column2
        '
        Me.Column2.HeaderText = "X Coord"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.HeaderText = "Y Coord"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        '
        'Column4
        '
        Me.Column4.HeaderText = "Z Coord"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        '
        'Button_DeleteNode
        '
        Me.Button_DeleteNode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_DeleteNode.Location = New System.Drawing.Point(451, 216)
        Me.Button_DeleteNode.Name = "Button_DeleteNode"
        Me.Button_DeleteNode.Size = New System.Drawing.Size(118, 40)
        Me.Button_DeleteNode.TabIndex = 6
        Me.Button_DeleteNode.Text = "Delete Node(s)"
        Me.Button_DeleteNode.UseVisualStyleBackColor = True
        '
        'Button_Import
        '
        Me.Button_Import.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button_Import.Location = New System.Drawing.Point(237, 216)
        Me.Button_Import.Name = "Button_Import"
        Me.Button_Import.Size = New System.Drawing.Size(118, 40)
        Me.Button_Import.TabIndex = 8
        Me.Button_Import.Text = "Import Nodes"
        Me.Button_Import.UseVisualStyleBackColor = True
        '
        'Node_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 270)
        Me.Controls.Add(Me.Button_Import)
        Me.Controls.Add(Me.Button_DeleteNode)
        Me.Controls.Add(Me.Node_DataGridView)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Node_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add / Delete Nodes"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Node_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox_ZCoord As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_YCoord As System.Windows.Forms.TextBox
    Friend WithEvents TextBox_XCoord As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button_AddNode As System.Windows.Forms.Button
    Friend WithEvents Node_DataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button_DeleteNode As System.Windows.Forms.Button
    Friend WithEvents Button_Import As System.Windows.Forms.Button
End Class
