<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIMain))
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.FrontViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TopViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SideViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Isometric2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Isometric1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripStatusLabel_ZoomValue = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel_ErrorStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNodesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddElementsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddLoadsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SolveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResultViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnDefromedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeformedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.NoContourToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisplacementToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AxialForceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AxialStressToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AxialStrainToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Main_Pic = New System.Windows.Forms.Panel()
        Me.MT_Pic = New System.Windows.Forms.PictureBox()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Main_Pic.SuspendLayout()
        CType(Me.MT_Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1, Me.ToolStripStatusLabel_ZoomValue, Me.ToolStripStatusLabel_ErrorStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 518)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(818, 22)
        Me.StatusStrip1.TabIndex = 0
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FrontViewToolStripMenuItem, Me.TopViewToolStripMenuItem, Me.SideViewToolStripMenuItem, Me.Isometric2ToolStripMenuItem, Me.Isometric1ToolStripMenuItem})
        Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(45, 20)
        Me.ToolStripDropDownButton1.Text = "View"
        '
        'FrontViewToolStripMenuItem
        '
        Me.FrontViewToolStripMenuItem.Name = "FrontViewToolStripMenuItem"
        Me.FrontViewToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.FrontViewToolStripMenuItem.Text = "Front View"
        '
        'TopViewToolStripMenuItem
        '
        Me.TopViewToolStripMenuItem.Name = "TopViewToolStripMenuItem"
        Me.TopViewToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.TopViewToolStripMenuItem.Text = "Top View"
        '
        'SideViewToolStripMenuItem
        '
        Me.SideViewToolStripMenuItem.Name = "SideViewToolStripMenuItem"
        Me.SideViewToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.SideViewToolStripMenuItem.Text = "Side View"
        '
        'Isometric2ToolStripMenuItem
        '
        Me.Isometric2ToolStripMenuItem.Name = "Isometric2ToolStripMenuItem"
        Me.Isometric2ToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.Isometric2ToolStripMenuItem.Text = "Isometric 2"
        '
        'Isometric1ToolStripMenuItem
        '
        Me.Isometric1ToolStripMenuItem.Name = "Isometric1ToolStripMenuItem"
        Me.Isometric1ToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.Isometric1ToolStripMenuItem.Text = "Isometric 1"
        '
        'ToolStripStatusLabel_ZoomValue
        '
        Me.ToolStripStatusLabel_ZoomValue.Name = "ToolStripStatusLabel_ZoomValue"
        Me.ToolStripStatusLabel_ZoomValue.Size = New System.Drawing.Size(119, 17)
        Me.ToolStripStatusLabel_ZoomValue.Text = "ToolStripStatusLabel1"
        '
        'ToolStripStatusLabel_ErrorStatus
        '
        Me.ToolStripStatusLabel_ErrorStatus.ForeColor = System.Drawing.Color.Red
        Me.ToolStripStatusLabel_ErrorStatus.Name = "ToolStripStatusLabel_ErrorStatus"
        Me.ToolStripStatusLabel_ErrorStatus.Size = New System.Drawing.Size(0, 17)
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.AddNodesToolStripMenuItem, Me.AddElementsToolStripMenuItem, Me.AddLoadsToolStripMenuItem, Me.SolveToolStripMenuItem, Me.ResultViewToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(818, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.SaveToolStripMenuItem, Me.OpenToolStripMenuItem, Me.OptionsToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'AddNodesToolStripMenuItem
        '
        Me.AddNodesToolStripMenuItem.Name = "AddNodesToolStripMenuItem"
        Me.AddNodesToolStripMenuItem.Size = New System.Drawing.Size(78, 20)
        Me.AddNodesToolStripMenuItem.Text = "Add Nodes"
        '
        'AddElementsToolStripMenuItem
        '
        Me.AddElementsToolStripMenuItem.Name = "AddElementsToolStripMenuItem"
        Me.AddElementsToolStripMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.AddElementsToolStripMenuItem.Text = "Add Elements"
        '
        'AddLoadsToolStripMenuItem
        '
        Me.AddLoadsToolStripMenuItem.Name = "AddLoadsToolStripMenuItem"
        Me.AddLoadsToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.AddLoadsToolStripMenuItem.Text = "Add Loads"
        '
        'SolveToolStripMenuItem
        '
        Me.SolveToolStripMenuItem.Name = "SolveToolStripMenuItem"
        Me.SolveToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.SolveToolStripMenuItem.Text = "Solve"
        '
        'ResultViewToolStripMenuItem
        '
        Me.ResultViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UnDefromedToolStripMenuItem, Me.DeformedToolStripMenuItem, Me.ToolStripSeparator1, Me.NoContourToolStripMenuItem, Me.DisplacementToolStripMenuItem, Me.AxialForceToolStripMenuItem, Me.AxialStressToolStripMenuItem, Me.AxialStrainToolStripMenuItem})
        Me.ResultViewToolStripMenuItem.Name = "ResultViewToolStripMenuItem"
        Me.ResultViewToolStripMenuItem.Size = New System.Drawing.Size(79, 20)
        Me.ResultViewToolStripMenuItem.Text = "Result View"
        '
        'UnDefromedToolStripMenuItem
        '
        Me.UnDefromedToolStripMenuItem.Name = "UnDefromedToolStripMenuItem"
        Me.UnDefromedToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.UnDefromedToolStripMenuItem.Text = "Un Defromed"
        '
        'DeformedToolStripMenuItem
        '
        Me.DeformedToolStripMenuItem.Name = "DeformedToolStripMenuItem"
        Me.DeformedToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.DeformedToolStripMenuItem.Text = "Deformed"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(143, 6)
        '
        'NoContourToolStripMenuItem
        '
        Me.NoContourToolStripMenuItem.Name = "NoContourToolStripMenuItem"
        Me.NoContourToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.NoContourToolStripMenuItem.Text = "No Contour"
        '
        'DisplacementToolStripMenuItem
        '
        Me.DisplacementToolStripMenuItem.Name = "DisplacementToolStripMenuItem"
        Me.DisplacementToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.DisplacementToolStripMenuItem.Text = "Displacement"
        '
        'AxialForceToolStripMenuItem
        '
        Me.AxialForceToolStripMenuItem.Name = "AxialForceToolStripMenuItem"
        Me.AxialForceToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AxialForceToolStripMenuItem.Text = "Axial Force"
        '
        'AxialStressToolStripMenuItem
        '
        Me.AxialStressToolStripMenuItem.Name = "AxialStressToolStripMenuItem"
        Me.AxialStressToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AxialStressToolStripMenuItem.Text = "Axial Stress"
        '
        'AxialStrainToolStripMenuItem
        '
        Me.AxialStrainToolStripMenuItem.Name = "AxialStrainToolStripMenuItem"
        Me.AxialStrainToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AxialStrainToolStripMenuItem.Text = "Axial Strain"
        '
        'Main_Pic
        '
        Me.Main_Pic.BackColor = System.Drawing.Color.White
        Me.Main_Pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Main_Pic.Controls.Add(Me.MT_Pic)
        Me.Main_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Main_Pic.Location = New System.Drawing.Point(0, 24)
        Me.Main_Pic.Name = "Main_Pic"
        Me.Main_Pic.Size = New System.Drawing.Size(818, 494)
        Me.Main_Pic.TabIndex = 2
        '
        'MT_Pic
        '
        Me.MT_Pic.BackColor = System.Drawing.Color.Transparent
        Me.MT_Pic.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MT_Pic.Enabled = False
        Me.MT_Pic.Location = New System.Drawing.Point(0, 0)
        Me.MT_Pic.Name = "MT_Pic"
        Me.MT_Pic.Size = New System.Drawing.Size(814, 490)
        Me.MT_Pic.TabIndex = 0
        Me.MT_Pic.TabStop = False
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 540)
        Me.Controls.Add(Me.Main_Pic)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MDIMain"
        Me.Text = "3D Truss Analysis ----- Developed by Samson Mano <https://sites.goolge.com/site/s" &
    "amsoninfinite/>"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Main_Pic.ResumeLayout(False)
        CType(Me.MT_Pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents FrontViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddNodesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddElementsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddLoadsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SolveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ResultViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplacementToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AxialForceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AxialStressToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AxialStrainToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SideViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Isometric2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Isometric1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Main_Pic As System.Windows.Forms.Panel
    Friend WithEvents MT_Pic As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStripStatusLabel_ZoomValue As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnDefromedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeformedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NoContourToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabel_ErrorStatus As System.Windows.Forms.ToolStripStatusLabel
End Class
