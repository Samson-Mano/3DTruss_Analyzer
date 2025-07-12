Imports System.Drawing.Drawing2D
Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions
Imports System.IO
Imports System.Collections.Specialized
Imports System.Runtime.Serialization.Formatters.Binary

'---------------------------- Programmed by Samson Mano ----------------------------------
'-----------------------------------------------------------------------------------------
'-------- Completed on 6th Jan 2018 Saturday 11:32 AM Singapore Standard Time ------------
'-------- Total Number of lines: 7351 ----------------------------------------------------

Public Class MDIMain
    '--- Zoom Variable
    Dim ZM As Double = 1
    Public threeD_Control As New Cube_Rotation(0, 0)

    '--- Main Finite Element Variable
    Public FElements As New FE_Objects_Store

    '--- Rotate and Pan operation variables
    Private RotateStartDrag As Point
    Private PanStartDrag As Point
    Private MidPt As Point
    Private Cursor_Pt As Point
    Private Shared isLeftDrag As Boolean = False
    Private Shared isMiddleDrag As Boolean = False

    '----- View Control Variables
    Public Is_DrawNodeNum As Boolean
    Public Is_DrawNodeCoord As Boolean
    Public Is_DrawElemLength As Boolean
    Public Is_DrawGrid As Boolean
    Public Is_DrawZeroPt As Boolean
    Public Is_DrawLoad As Boolean
    Public Is_DrawLoadValue As Boolean
    Public Is_DrawConst As Boolean
    Public Is_DrawResultValues As Boolean
    Public Is_DrawUndeformedView As Boolean
    Public Round_offVal As Integer

    '--- Form Variables
    Public is_ShiftKeyDown As Boolean = False
    Public is_childFormOpen As Boolean = False '-- Variable to control opening of one form
    Public is_nodeFormOpen As Boolean = False '-- Variable to note down Node_form is open
    Public is_elementFormOpen As Boolean = False '-- Variable to note down Element_form is open
    Public is_loadFormOpen As Boolean = False '-- Variable to note down Load_form is open
    Public AddNode_form As Node_Form
    Public AddElement_form As Element_Form
    Public AddLoad_form As Load_Form
    Dim Option_form As Options_Form

    '--- Result View Variables
    Public is_Analysis_Success As Boolean

#Region "Main Pic Zoom, Rotate and Paint Events"
    Private Sub Main_Pic_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Pic.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Left And (is_nodeFormOpen = True Or is_elementFormOpen = True Or is_loadFormOpen = True) Then
            Dim Cursor_Pt As New Point((e.X / ZM) - MidPt.X, (e.Y / ZM) - MidPt.Y)
            '--- Node Highlight during Node form open and selct the node in database
            FElements.MouseOver_Node_Index = FElements.tNodes.FindIndex(Function(T) T.Test_Point_in_Circle(Cursor_Pt, 6) = True)
            If FElements.MouseOver_Node_Index <> -1 Then
                If is_nodeFormOpen = True Then
                    AddNode_form.Selection_of_Node_fromMDImain(FElements.MouseOver_Node_Index, is_ShiftKeyDown)
                ElseIf is_elementFormOpen = True Then
                    AddElement_form.Selection_of_Node_fromMDImain(FElements.tNodes(FElements.MouseOver_Node_Index).N_ID)
                ElseIf is_loadFormOpen = True Then
                    AddLoad_form.Selection_of_Node_fromMDImain(FElements.MouseOver_Node_Index, is_ShiftKeyDown)
                End If
            End If
            If is_elementFormOpen = True Then
                If AddElement_form.is_Draw_statictempline = False And AddElement_form.is_Draw_dynamictempline = False Then
                    '---- Highlight the element when cursor is over it
                    FElements.MouseOver_Element_Index = FElements.tElements.FindIndex(Function(T) T.Test_Point_on_line(Cursor_Pt, 400 * ZM))
                    If FElements.MouseOver_Element_Index <> -1 Then
                        AddElement_form.Selection_of_Element_fromMDImain(FElements.MouseOver_Element_Index, is_ShiftKeyDown)
                    End If
                End If
            End If

            MT_Pic.Refresh()
        End If
    End Sub

    Private Sub Main_Pic_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Main_Pic.MouseEnter
        If is_childFormOpen = False Then
            Main_Pic.Focus()
        End If
    End Sub

    Private Sub Main_Pic_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Pic.MouseDown
        '------ Mouse down for Click and drag
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            '---- Pan Operation
            isMiddleDrag = True
            PanStartDrag = New Point((e.X / ZM) - MidPt.X, (e.Y / ZM) - MidPt.Y)
        End If
        If e.Button = Windows.Forms.MouseButtons.Left Then
            '---- Rotate Operation
            isLeftDrag = True
            RotateStartDrag = New Point(e.X, e.Y)
            threeD_Control.startRotationDrag(RotateStartDrag) '--- Fix the current rotation as previous rotation becoz new rotation is about to commence
        End If

        MT_Pic.Refresh()
    End Sub

    Private Sub Main_Pic_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Pic.MouseMove
        '------ Mouse move drag
        If isMiddleDrag = True Then
            '---- Pan Operation 
            MidPt = New Point((e.X / ZM) - PanStartDrag.X, (e.Y / ZM) - PanStartDrag.Y)
            MT_Pic.Refresh()
        End If
        If isLeftDrag = True Then
            '---- Rotate Operaion
            Dim tempAux As New Point(e.X, e.Y)
            threeD_Control.Rotationdrag(tempAux) '--- New Rotation Drag
            threeD_Control.Update_Rotation_Matrix()
            MT_Pic.Refresh()
        End If
        If isMiddleDrag = False And isLeftDrag = False And (is_nodeFormOpen = True Or is_elementFormOpen = True Or is_loadFormOpen = True) Then
            Cursor_Pt = New Point((e.X / ZM) - MidPt.X, (e.Y / ZM) - MidPt.Y)
            '--- Node Highlight during Node form and Element form open
            FElements.MouseOver_Node_Index = FElements.tNodes.FindIndex(Function(T) T.Test_Point_in_Circle(Cursor_Pt, 6) = True)

            '--- Element Highlight During Element form open
            If is_elementFormOpen = True Then
                If AddElement_form.is_Draw_statictempline = False And AddElement_form.is_Draw_dynamictempline = False Then
                    '---- Highlight the element when cursor is over it
                    FElements.MouseOver_Element_Index = FElements.tElements.FindIndex(Function(T) T.Test_Point_on_line(Cursor_Pt, 360 * ZM))
                ElseIf AddElement_form.is_Draw_dynamictempline = True Then
                    AddElement_form.Dyn_Cursor_Pt = Cursor_Pt '--- Send the cursor point to draw the Dynamic line
                End If
            End If
            MT_Pic.Refresh()
        End If
    End Sub

    Private Sub Main_Pic_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Pic.MouseUp
        '------ Mouse up for Click and drag
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            '--- Pan Operation Stops
            isMiddleDrag = False
        End If
        If e.Button = Windows.Forms.MouseButtons.Left Then
            '--- Rotate Operation Stops
            isLeftDrag = False
        End If

        ToolStripStatusLabel_ErrorStatus.Text = ""
        MT_Pic.Refresh()
    End Sub

    Private Sub Main_Pic_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Main_Pic.MouseWheel
        Dim xw, yw As Single
        Main_Pic.Focus()
        xw = (e.X / ZM) - MidPt.X
        yw = (e.Y / ZM) - MidPt.Y
        If e.Delta > 0 Then
            If ZM < 100 Then
                ZM = ZM + 0.1
            End If
        ElseIf e.Delta < 0 Then
            If ZM > 0.101 Then
                ZM = ZM - 0.1

            End If
        End If
        MidPt.X = (e.X / ZM) - xw
        MidPt.Y = (e.Y / ZM) - yw

        ToolStripStatusLabel_ZoomValue.Text = "Zoom: " & ZM * 100 & "%"
        MT_Pic.Refresh()
    End Sub

    Private Sub Main_Pic_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Main_Pic.Resize
        threeD_Control.Update_Arcball_InResize(Main_Pic.Width, Main_Pic.Height) '-- Update Arc ball to center of the screen
    End Sub

    Private Sub Main_Pic_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Main_Pic.Paint

        e.Graphics.ScaleTransform(ZM, ZM)
        e.Graphics.TranslateTransform(MidPt.X, MidPt.Y)


        threeD_Control.The_Cube.Draw_Cube(e.Graphics)
        FElements.Paint_All_FElements(e.Graphics) '-- Paint All Finite Elements

    End Sub
#End Region

#Region "View - Status Tool strip events"
    Private Sub Isometric2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Isometric2ToolStripMenuItem.Click
        threeD_Control.set_pre_RotationMatrix(5)
        MT_Pic.Refresh()
    End Sub

    Private Sub Isometric1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Isometric1ToolStripMenuItem.Click
        threeD_Control.set_pre_RotationMatrix(4)
        MT_Pic.Refresh()
    End Sub

    Private Sub FrontViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrontViewToolStripMenuItem.Click
        threeD_Control.set_pre_RotationMatrix(3)
        MT_Pic.Refresh()
    End Sub

    Private Sub SideViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SideViewToolStripMenuItem.Click
        threeD_Control.set_pre_RotationMatrix(2)
        MT_Pic.Refresh()
    End Sub

    Private Sub TopViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TopViewToolStripMenuItem.Click
        threeD_Control.set_pre_RotationMatrix(1)
        MT_Pic.Refresh()
    End Sub
#End Region

#Region "File - Menu Tool strip events"
    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        Dim MsgRslt As DialogResult
        If FElements.tNodes.Count <> 0 Then
            MsgRslt = MessageBox.Show("Do you want to start new model? Unsaved items will be discarded", "Samson Mano", MessageBoxButtons.YesNo)
        End If
        If MsgRslt = Windows.Forms.DialogResult.Yes Then
            MidPt = New Point(Main_Pic.Width / 8, Main_Pic.Height / 1.3) '--- Mid point
            threeD_Control = New Cube_Rotation(Main_Pic.Width, Main_Pic.Height)

            '--- Load the user Settings
            Is_DrawNodeNum = My.Settings.Sett_IsDrawNodeNum
            Is_DrawNodeCoord = My.Settings.Sett_IsDrawNodeCoord
            Is_DrawElemLength = My.Settings.Sett_IsDrawElemLen
            Is_DrawGrid = My.Settings.Sett_IsDrawGrid
            Is_DrawZeroPt = My.Settings.Sett_IsDrawZeroPt
            Is_DrawLoad = My.Settings.Sett_IsDrawLoad
            Is_DrawLoadValue = My.Settings.Sett_IsDrawLoadValue
            Is_DrawConst = My.Settings.Sett_IsDrawConstraint

            ToolStripStatusLabel_ZoomValue.Text = "Zoom: 100%"

            FElements = New FE_Objects_Store

            ResultView_Initialize(False)
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Dim SW As New SaveFileDialog
        SW.DefaultExt = ".3dr"
        SW.Filter = "Samson Mano's 3D - Truss Object Files (*.3dr)|*.3dr"
        SW.FileName = "Surface"
        DialogResult = SW.ShowDialog()

        Dim trobject As New List(Of Object)
        trobject.Add(800 / threeD_Control.Sc_val4)
        trobject.Add(FElements)


        Dim psf As Stream = File.Create(SW.FileName)
        Dim serializer As New BinaryFormatter
        serializer.Serialize(psf, trobject)
        psf.Close()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim OW As New OpenFileDialog
        OW.DefaultExt = ".3dr"
        OW.Filter = "Samson Mano's 3D - Truss Object Files (*.3dr)|*.3dr"
        OW.ShowDialog()
        If File.Exists(OW.FileName) Then
            Dim trobject As New List(Of Object)
            Dim gsf As Stream = File.OpenRead(OW.FileName)
            Dim deserializer As New BinaryFormatter
            Try
                trobject = CType(deserializer.Deserialize(gsf), Object)

                FElements = New FE_Objects_Store
                threeD_Control._UpdateScale = trobject(0)
                FElements = trobject(1)
                is_Analysis_Success = False

                ResultView_Initialize(False)

                MT_Pic.Refresh()
            Catch ex As Exception
                MessageBox.Show("Sorry!!!!! Unable to Open.. File Reading Error", "Samson Mano", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionsToolStripMenuItem.Click
        If is_childFormOpen = False Then
            Option_form = New Options_Form '-- create a instance for node form
            Option_form.Show()
            is_childFormOpen = True
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
#End Region

#Region "Add Nodes, Elements, Loads, Constraints Tool strip events"
    Private Sub AddLoadsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLoadsToolStripMenuItem.Click
        If is_childFormOpen = False Then
            AddLoad_form = New Load_Form '-- create a instance for node form
            AddLoad_form.Show()
            is_childFormOpen = True
            is_loadFormOpen = True
        Else
            '--- Prompt the user to close other opened forms
            MsgBox("Close other windows before opening this !!", MsgBoxStyle.OkOnly, "Samson Mano")
        End If
    End Sub

    Private Sub AddElementsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddElementsToolStripMenuItem.Click
        If is_childFormOpen = False Then
            AddElement_form = New Element_Form '-- create a instance for node form
            AddElement_form.Show()
            is_childFormOpen = True
            is_elementFormOpen = True
        Else
            '--- Prompt the user to close other opened forms
            MsgBox("Close other windows before opening this !!", MsgBoxStyle.OkOnly, "Samson Mano")
        End If
    End Sub

    Private Sub AddNodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddNodesToolStripMenuItem.Click
        If is_childFormOpen = False Then
            'Me.IsMdiContainer = True
            AddNode_form = New Node_Form '-- create a instance for node form
            'AddNode_form.MdiParent = Me
            'Me.Main_Pic.Controls.Add(AddNode_form) '-- Add this line to bring the add Node form to front of MainPic (which is a panel)
            AddNode_form.Show()
            'AddNode_form.BringToFront()
            is_childFormOpen = True
            is_nodeFormOpen = True
        Else
            '--- Prompt the user to close other opened forms
            MsgBox("Close other windows before opening this !!", MsgBoxStyle.OkOnly, "Samson Mano")
        End If
    End Sub
#End Region

#Region "Solve & Result View - Menu Tool strip events"
    Private Sub SolveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SolveToolStripMenuItem.Click
        Dim A As New ThreeD_Truss_Solver
        A.ShowDialog()
    End Sub

    Private Sub UnDefromedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnDefromedToolStripMenuItem.Click
        DeformedToolStripMenuItem.Checked = False
        UnDefromedToolStripMenuItem.Checked = True
        MT_Pic.Refresh()

    End Sub

    Private Sub DeformedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeformedToolStripMenuItem.Click
        DeformedToolStripMenuItem.Checked = True
        UnDefromedToolStripMenuItem.Checked = False
        MT_Pic.Refresh()
    End Sub

    Private Sub DisplacementToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplacementToolStripMenuItem.Click
        NoContourToolStripMenuItem.Checked = False
        AxialForceToolStripMenuItem.Checked = False
        AxialStrainToolStripMenuItem.Checked = False
        AxialStressToolStripMenuItem.Checked = False
        DisplacementToolStripMenuItem.Checked = True
        MT_Pic.Refresh()
    End Sub

    Private Sub AxialForceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AxialForceToolStripMenuItem.Click
        NoContourToolStripMenuItem.Checked = False
        AxialForceToolStripMenuItem.Checked = True
        AxialStrainToolStripMenuItem.Checked = False
        AxialStressToolStripMenuItem.Checked = False
        DisplacementToolStripMenuItem.Checked = False
        MT_Pic.Refresh()
    End Sub

    Private Sub AxialStressToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AxialStressToolStripMenuItem.Click
        NoContourToolStripMenuItem.Checked = False
        AxialForceToolStripMenuItem.Checked = False
        AxialStrainToolStripMenuItem.Checked = False
        AxialStressToolStripMenuItem.Checked = True
        DisplacementToolStripMenuItem.Checked = False
        MT_Pic.Refresh()
    End Sub

    Private Sub AxialStrainToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AxialStrainToolStripMenuItem.Click
        NoContourToolStripMenuItem.Checked = False
        AxialForceToolStripMenuItem.Checked = False
        AxialStrainToolStripMenuItem.Checked = True
        AxialStressToolStripMenuItem.Checked = False
        DisplacementToolStripMenuItem.Checked = False
        MT_Pic.Refresh()
    End Sub

    Private Sub NoContourToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NoContourToolStripMenuItem.Click
        NoContourToolStripMenuItem.Checked = True
        AxialForceToolStripMenuItem.Checked = False
        AxialStrainToolStripMenuItem.Checked = False
        AxialStressToolStripMenuItem.Checked = False
        DisplacementToolStripMenuItem.Checked = False
        MT_Pic.Refresh()
    End Sub

    Public Sub ResultView_Initialize(ByVal Is_analysis_complete As Boolean)
        If Is_analysis_complete = False Then
            DeformedToolStripMenuItem.Checked = False
            UnDefromedToolStripMenuItem.Checked = True

            NoContourToolStripMenuItem.Checked = True
            AxialForceToolStripMenuItem.Checked = False
            AxialStrainToolStripMenuItem.Checked = False
            AxialStressToolStripMenuItem.Checked = False
            DisplacementToolStripMenuItem.Checked = False
        Else
            DeformedToolStripMenuItem.Checked = True
            UnDefromedToolStripMenuItem.Checked = False

            NoContourToolStripMenuItem.Checked = False
            AxialForceToolStripMenuItem.Checked = False
            AxialStrainToolStripMenuItem.Checked = False
            AxialStressToolStripMenuItem.Checked = False
            DisplacementToolStripMenuItem.Checked = True

        End If

        MT_Pic.Refresh()
    End Sub
#End Region

    Private Sub MDIMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '--- Save the user settings
        My.Settings.Sett_IsDrawNodeNum = Is_DrawNodeNum
        My.Settings.Sett_IsDrawNodeCoord = Is_DrawNodeCoord
        My.Settings.Sett_IsDrawElemLen = Is_DrawElemLength
        My.Settings.Sett_IsDrawGrid = Is_DrawGrid
        My.Settings.Sett_IsDrawZeroPt = Is_DrawZeroPt
        My.Settings.Sett_IsDrawLoad = Is_DrawLoad
        My.Settings.Sett_IsDrawLoadValue = Is_DrawLoadValue
        My.Settings.Sett_IsDrawConstraint = Is_DrawConst
        My.Settings.Sett_IsDrawResultVal = Is_DrawResultValues
        My.Settings.Sett_IsDrawUndeformedView = Is_DrawUndeformedView
        My.Settings.Sett_RoundOffVal = Round_offVal
    End Sub

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        '--- Key down event handlers
        If e.Shift = True Then
            is_ShiftKeyDown = True
        End If
        If e.KeyCode = 13 And is_childFormOpen = True Then
            If is_nodeFormOpen = True Then
                AddNode_form.Focus()
            End If
            If is_elementFormOpen = True Then
                AddElement_form.Focus()
            End If
        End If
    End Sub

    Private Sub MDIMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        '--- Key Up event
        is_ShiftKeyDown = False
    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MidPt = New Point(Main_Pic.Width / 8, Main_Pic.Height / 1.3) '--- Mid point
        threeD_Control = New Cube_Rotation(Main_Pic.Width, Main_Pic.Height)

        '--- Load the user Settings
        Is_DrawNodeNum = My.Settings.Sett_IsDrawNodeNum
        Is_DrawNodeCoord = My.Settings.Sett_IsDrawNodeCoord
        Is_DrawElemLength = My.Settings.Sett_IsDrawElemLen
        Is_DrawGrid = My.Settings.Sett_IsDrawGrid
        Is_DrawZeroPt = My.Settings.Sett_IsDrawZeroPt
        Is_DrawLoad = My.Settings.Sett_IsDrawLoad
        Is_DrawLoadValue = My.Settings.Sett_IsDrawLoadValue
        Is_DrawConst = My.Settings.Sett_IsDrawConstraint
        Is_DrawResultValues = My.Settings.Sett_IsDrawResultVal
        Is_DrawUndeformedView = My.Settings.Sett_IsDrawUndeformedView
        Round_offVal = My.Settings.Sett_RoundOffVal

        ToolStripStatusLabel_ZoomValue.Text = "Zoom: 100%"

        ResultView_Initialize(False)
    End Sub

End Class