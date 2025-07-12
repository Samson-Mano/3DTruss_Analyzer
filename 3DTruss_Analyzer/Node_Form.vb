Imports System.IO
Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions

Public Class Node_Form
    Private _tSelected_nd_List As New List(Of Integer)
    Private _lastSuccessfull_add As FE_Objects_Store.Nodes_Store

    Public ReadOnly Property Selected_nd_list() As List(Of Integer)
        Get
            Return _tSelected_nd_List
        End Get
    End Property

#Region "Form Opening/ Closing Operation"
    Private Sub Node_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.is_childFormOpen = False '-- Update the main form that this form is closed
        MDIMain.is_nodeFormOpen = False
        MDIMain.MT_Pic.Refresh()


    End Sub

    Private Sub Node_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(MDIMain.Location.X + 0, MDIMain.Location.Y + 56)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Opacity = 0.9
        Me.BringToFront()
        Me.TopMost = True
        Me.Node_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        Node_DataGridView.AllowUserToResizeRows = False
        Initialize_DataGridView()
    End Sub
#End Region

#Region "Button Add Delete Import Click"
    Private Sub Button_AddNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_AddNode.Click
        '--- Adding Node (one at a time)
        If Test_a_textboxvalue_validity(TextBox_XCoord.Text, 0, 0) = True And _
           Test_a_textboxvalue_validity(TextBox_YCoord.Text, 0, 0) = True And _
           Test_a_textboxvalue_validity(TextBox_ZCoord.Text, 0, 0) = True Then
            Dim tinp_x, tinp_y, tinp_z As Double
            tinp_x = Val(TextBox_XCoord.Text)
            tinp_y = Val(TextBox_YCoord.Text)
            tinp_z = Val(TextBox_ZCoord.Text)

            ''MDIMain.FElements.tNodes.Exists

            Add_Node(tinp_x, tinp_y, tinp_z)
        End If
        MDIMain.MT_Pic.Refresh() '-- Redraw every add of node
    End Sub

    Public Sub Add_Node(ByVal inp_x As Double, ByVal inp_y As Double, ByVal inp_z As Double)
        '--- Void Function which Adds Node one by one
        '----------- Adding unique IDs based on the missing ids
        Dim node_ID As Integer = 0
        If MDIMain.FElements.tNodes.Count <> 0 Then
            node_ID = MDIMain.FElements.tNodes.Count
            Dim i As Integer
            Dim n_id_list As New List(Of Integer)
            For i = 0 To (MDIMain.FElements.tNodes.Count) - 1 Step +1
                n_id_list.Add(MDIMain.FElements.tNodes(i).N_ID)
            Next
            n_id_list.Sort()
            For i = 0 To (MDIMain.FElements.tNodes.Count) - 1 Step +1
                If i <> n_id_list(i) Then
                    node_ID = i
                    Exit For
                End If
            Next
        End If


        If MDIMain.FElements.tNodes.Contains(New FE_Objects_Store.Nodes_Store(node_ID, inp_x, inp_y, inp_z)) = False Then '--- avoid adding existing nodes
            MDIMain.FElements.tNodes.Add(New FE_Objects_Store.Nodes_Store(node_ID, inp_x, inp_y, inp_z))

            Dim node_count As Integer = MDIMain.FElements.tNodes.Count
            Dim DatGrid_row As String() = New String() {MDIMain.FElements.tNodes(node_count - 1).N_ID + 1, _
                                                                    MDIMain.FElements.tNodes(node_count - 1).N_Coord.X, _
                                                                    MDIMain.FElements.tNodes(node_count - 1).N_Coord.Y, _
                                                                    MDIMain.FElements.tNodes(node_count - 1).N_Coord.Z}
            Node_DataGridView.Rows.Add(DatGrid_row) '--- Add the node ID, x,y,z as string to Data grid view
        End If
    End Sub

    Private Sub Button_DeleteNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DeleteNode.Click
        '--- Deleting Nodes (multiple delete is allowed)
        If Node_DataGridView.SelectedRows.Count <> 0 Then
            '_tSelected_nd_List.Clear() '-- Clear the selection id list
            '--- update the data grid view (remove the rows of deleted nodes)
            Dim Selected_id_list As New List(Of Integer)
            '--- Step :1 Note down the Node_id of the Nodes being deleted
            Dim i As Integer = 0
            For Each DataGrid_row In Node_DataGridView.SelectedRows
                Selected_id_list.Add(Node_DataGridView.SelectedRows(i).Cells("Column1").Value) '-- Find the index of the item which are being deleted
                i = i + 1
            Next
            '--- Step: 1A Check whether the Node is connected to any Element
            For i = 0 To (MDIMain.FElements.tElements.Count - 1)
                Selected_id_list.RemoveAll(Function(T) (T = (MDIMain.FElements.tElements(i).EN.N_ID + 1) Or T = (MDIMain.FElements.tElements(i).SN.N_ID + 1)))
            Next

            '--- Step :2 Remove these items from the tNodes variable
            For Each Id In Selected_id_list
                Dim N_v As Integer = (Id - 1) '-- To avoid the iteration variable in lamda expression Warning !!
                MDIMain.FElements.tNodes.RemoveAt(MDIMain.FElements.tNodes.FindIndex(Function(x) x.N_ID = N_v))
            Next
            '--- Step :3 Remove the Rows from datagridview
            Dim Prompt_user As Boolean = False
            Dim Selected_DataGrid_id As New List(Of Integer)
            For i = 0 To (Node_DataGridView.SelectedRows.Count - 1) Step +1
                If Selected_id_list.Contains(Node_DataGridView.SelectedRows(i).Cells("Column1").Value) = True Then
                    Selected_DataGrid_id.Add(i) '--- Add the Data Grid Row id which are going to be deleted
                Else
                    Prompt_user = True
                End If
            Next
            Selected_DataGrid_id.Sort()
            Selected_DataGrid_id.Reverse()
            For Each DG_id In Selected_DataGrid_id
                Node_DataGridView.Rows.Remove(Node_DataGridView.SelectedRows(DG_id))
            Next

            '--- Finally prompt user that nodes which are connected to elements cant be deleted
            If Prompt_user = True Then
                MsgBox("Unable to Delete Nodes which are connected to elements !!!!", MsgBoxStyle.OkOnly, "Samson Mano")
            End If

        End If

        MDIMain.MT_Pic.Refresh() '-- Redraw after deleting nodes
    End Sub

    Private Sub Button_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Import.Click
        '--- Import of Nodes from a Text File
        Dim openFileDialog1 As New OpenFileDialog()

        'openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                Dim TxtReader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(openFileDialog1.FileName)

                If (TxtReader IsNot Nothing) Then
                    ' code to read the stream.
                    Dim C_val As Double = TxtReader.ReadLine '-- First line stores Scale Value
                    MDIMain.threeD_Control._UpdateScale = 800 / C_val
                    '--- Update the Load, Constraint and deformation scale depends on Input scale values
                    MDIMain.FElements.Load_scaleLen = 5 * C_val
                    MDIMain.FElements.Const_scaleLen = 2 * C_val
                    MDIMain.FElements.Defrom_scaleLen = 4 * C_val

                    TxtReader.ReadLine() '--- One Dummy Line
                    Dim TxtLine As String = TxtReader.ReadLine() '-- first line of Node values
                    While (IsNothing(TxtLine) = False)
                        Dim Node_Coords As New List(Of Double) '--- initialize a temp Node Coord list
                        For Each field As String In TxtLine.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                            Node_Coords.Add(Double.Parse(field))
                        Next
                        Add_Node(Node_Coords(0), Node_Coords(1), Node_Coords(2)) '-- x,y,z
                        TxtLine = TxtReader.ReadLine()
                    End While
                End If
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            End Try
            MDIMain.MT_Pic.Refresh()
        End If
        openFileDialog1.Dispose()
    End Sub
#End Region

#Region "DataGridView Events"
    Public Sub Initialize_DataGridView()
        '---- Initialize the Data grid view with existing Node List
        If MDIMain.FElements.tNodes.Count <> 0 Then '-- Check whether the Node count is zero or not
            Dim Nd_count As Integer = 0
            For Each Nodes In MDIMain.FElements.tNodes
                Dim DatGrid_row As String() = New String() {Nodes.N_ID + 1, Nodes.N_Coord.X, Nodes.N_Coord.Y, Nodes.N_Coord.Z}
                Node_DataGridView.Rows.Add(DatGrid_row) '--- Add the node ID, x,y,z as string to Data grid view
            Next
        End If
    End Sub

    Public Sub Selection_of_Node_fromMDImain(ByVal Selected_node As Integer, ByVal Is_shift_Select As Boolean)
        '--- Node is Selected in the MDI Main which is updated in the data grid view
        Dim Add_Node_to_selection As Boolean
        If Is_shift_Select = False Then
            _tSelected_nd_List = New List(Of Integer)
            Node_DataGridView.ClearSelection()
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
        For i = 0 To (Node_DataGridView.Rows.Count - 1) '-- Go thro all nodes and match the node id
            If Node_DataGridView.Rows(i).Cells("Column1").Value = Selected_N_id Then
                If is_add = True Then
                    Node_DataGridView.Rows(i).Selected = True '-- select the row which matches the ID
                Else
                    Node_DataGridView.Rows(i).Selected = False '-- select the row which matches the ID
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub Node_DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Node_DataGridView.SelectionChanged
        _tSelected_nd_List = New List(Of Integer)
        For i = 0 To (Node_DataGridView.SelectedRows.Count - 1)
            '(Node_DataGridView.Rows(i).Cells("Column1").Value-1) stores the node id
            Dim D_id As Integer = i
            Dim Index_of_nodes_selected As Integer
            Index_of_nodes_selected = MDIMain.FElements.tNodes.FindIndex(Function(T) T.N_ID = (Node_DataGridView.SelectedRows(D_id).Cells("Column1").Value - 1))
            If Index_of_nodes_selected <> -1 Then '-1 means coundn't find the nodes
                _tSelected_nd_List.Add(Index_of_nodes_selected)
            End If
        Next
        If Node_DataGridView.SelectedRows.Count <> 0 Then
            Node_DataGridView.FirstDisplayedScrollingRowIndex = Node_DataGridView.SelectedRows(0).Index
        End If

        MDIMain.MT_Pic.Refresh()
    End Sub
#End Region

End Class