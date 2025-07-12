Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions

Public Class Load_Form
    Private _tSelected_nd_List As New List(Of Integer)

    Public ReadOnly Property Selected_nd_list() As List(Of Integer)
        Get
            Return _tSelected_nd_List
        End Get
    End Property

    Private Sub Load_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.is_childFormOpen = False '-- Update the main form that this form is closed
        MDIMain.is_loadFormOpen = False
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub Load_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(MDIMain.Location.X + 0, MDIMain.Location.Y + 56)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Opacity = 0.9
        Me.BringToFront()
        Me.TopMost = True
        Me.Load_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Load_DataGridView.AllowUserToResizeRows = False
        Initialize_DataGridView()
    End Sub

    Public Sub Initialize_DataGridView()
        '---- Initialize the Data grid view with existing Node List
        If MDIMain.FElements.tNodes.Count <> 0 Then '-- Check whether the Node count is zero or not
            Dim Nd_count As Integer = 0
            For Each Nodes In MDIMain.FElements.tNodes
                Dim DatGrid_row As String() = New String() {Nodes.N_ID + 1, Nodes.Node_Force.X_force, Nodes.Node_Force.Y_force, Nodes.Node_Force.Z_force, _
                                                                            Nodes.Node_Constraint.is_X_fixed, Nodes.Node_Constraint.is_Y_fixed, Nodes.Node_Constraint.is_Z_fixed}
                Load_DataGridView.Rows.Add(DatGrid_row) '--- Add the node ID, x,y,z as string to Data grid view
            Next
        End If
    End Sub

    Public Sub Selection_of_Node_fromMDImain(ByVal Selected_node As Integer, ByVal Is_shift_Select As Boolean)
        '--- Node is Selected in the MDI Main which is updated in the data grid view
        Dim Add_Node_to_selection As Boolean
        If Is_shift_Select = False Then
            _tSelected_nd_List = New List(Of Integer)
            Load_DataGridView.ClearSelection()
            _tSelected_nd_List.Add(Selected_node)
            Add_Node_to_selection = True
        Else
            '--- Shift key is down -- so has to remove if the node is already in the list to avoid multiple addition of same nodes
            If _tSelected_nd_List.Contains(Selected_node) Then '-- check whether the selected node list contains the node
                _tSelected_nd_List.RemoveAt(_tSelected_nd_List.FindIndex(Function(T) T = Selected_node))
                Add_Node_to_selection = False
            Else '-- Selected node is unique so add to the list
                _tSelected_nd_List.Add(Selected_node)
                Add_Node_to_selection = True
            End If
        End If

        Update_DataGridView_for_Single_add_remove(MDIMain.FElements.tNodes(Selected_node).N_ID + 1, Add_Node_to_selection)
    End Sub

    Public Sub Update_DataGridView_for_Single_add_remove(ByVal Selected_N_id As Integer, ByVal is_add As Boolean)
        'Step : 2 Below routine is to update the datagridview in any case
        'Node_DataGridView.ClearSelection()
        'Dim Selected_N_id As Integer = MDIMain.FElements.tNodes(Selected_node).N_ID + 1

        Dim i As Integer
        For i = 0 To (Load_DataGridView.Rows.Count - 1) '-- Go thro all nodes and match the node id
            If Load_DataGridView.Rows(i).Cells("Column1").Value = Selected_N_id Then
                If is_add = True Then
                    Load_DataGridView.Rows(i).Selected = True '-- select the row which matches the ID
                Else
                    Load_DataGridView.Rows(i).Selected = False '-- select the row which matches the ID
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub Load_DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Load_DataGridView.SelectionChanged
        _tSelected_nd_List = New List(Of Integer)
        For i = 0 To (Load_DataGridView.SelectedRows.Count - 1)
            '(Node_DataGridView.Rows(i).Cells("Column1").Value-1) stores the node id
            Dim D_id As Integer = i
            Dim Index_of_nodes_selected As Integer
            Index_of_nodes_selected = MDIMain.FElements.tNodes.FindIndex(Function(T) T.N_ID = (Load_DataGridView.SelectedRows(D_id).Cells("Column1").Value - 1))
            If Index_of_nodes_selected <> -1 Then '-1 means coundn't find the nodes
                _tSelected_nd_List.Add(Index_of_nodes_selected)
            End If
        Next
        If Load_DataGridView.SelectedRows.Count <> 0 Then
            Load_DataGridView.FirstDisplayedScrollingRowIndex = Load_DataGridView.SelectedRows(0).Index
        End If

        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub Button_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_add.Click
        If _tSelected_nd_List.Count <> 0 Then '-- Check whether any node is selected
            '--- Check text boxes for anamolly
            If Test_a_textboxvalue_validity(TextBox_Fx.Text, 0, 0) = True And _
               Test_a_textboxvalue_validity(TextBox_Fy.Text, 0, 0) = True And _
               Test_a_textboxvalue_validity(TextBox_Fz.Text, 0, 0) = True Then

          
                Dim inp_fx, inp_fy, inp_fz As Double
                Dim inp_tx, inp_ty, inp_tz As Boolean

                '---- Force Inputs
                inp_fx = Val(TextBox_Fx.Text)
                inp_fy = Val(TextBox_Fy.Text)
                inp_fz = Val(TextBox_Fz.Text)

                '---- Constraints Inputs
                inp_tx = CheckBox_Tx.Checked
                inp_ty = CheckBox_Ty.Checked
                inp_tz = CheckBox_Tz.Checked

                '---- Change the constraint (Loads and Fixity) of all the selected nodes
                For Each S_NId In _tSelected_nd_List
                    MDIMain.FElements.tNodes(S_NId).Update_My_Constraints(inp_fx, inp_fy, inp_fz, inp_tx, inp_ty, inp_tz)
                Next


                '---- Re Configure the maximum load
                MDIMain.FElements.Scale_MaxLoad = 0 '--- start by keeping the max load as zero
                For Each Nd In MDIMain.FElements.tNodes
                    Dim Max_of_ForceXYZ As Double = Math.Max(Math.Abs(Nd.Node_Force.X_force), Math.Abs(Nd.Node_Force.Y_force)) '-- Absoulute max of X & Y Force
                    Max_of_ForceXYZ = Math.Max(Max_of_ForceXYZ, Math.Abs(Nd.Node_Force.Z_force)) '-- Absolute max of XY & Z force
                    If Max_of_ForceXYZ > MDIMain.FElements.Scale_MaxLoad Then
                        MDIMain.FElements.Scale_MaxLoad = Max_of_ForceXYZ
                    End If
                Next

                '---- Update the DataGrid View After addition of load values to the table
                '------ Very uneconomical Updation !@!@!@!@!@!@!@!@! Bit of a compromise here
                Dim store_id_list As New List(Of Integer)
                store_id_list = _tSelected_nd_List


                Load_DataGridView.Rows.Clear()
                Initialize_DataGridView()
                Load_DataGridView.ClearSelection()

                _tSelected_nd_List = store_id_list
                Dim Selected_Nodes As New List(Of FE_Objects_Store.Nodes_Store)
                For Each id In _tSelected_nd_List
                    Selected_Nodes.Add(MDIMain.FElements.tNodes(id))
                Next

                For i = 0 To (Load_DataGridView.Rows.Count - 1)
                    Dim N_V As Integer = i
                    If Selected_Nodes.Exists(Function(T) T.N_ID = Load_DataGridView.Rows(N_V).Cells("Column1").Value - 1) Then
                        Load_DataGridView.Rows(i).Selected = True
                    End If
                Next

            End If
        End If
        MDIMain.MT_Pic.Refresh()
    End Sub
End Class