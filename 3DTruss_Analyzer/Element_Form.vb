Imports System.IO
Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions

Public Class Element_Form
    Public is_Draw_dynamictempline As Boolean '-- draw a line to From Node to mouse cursor (After checking whether the FN is valid
    Public is_Draw_statictempline As Boolean

    Private is_TxtBox_FN_focused As Boolean
    Private is_TxtBox_TN_focused As Boolean
    Private is_TxtFN_valid As Boolean
    Private is_TxtTN_valid As Boolean
    Private tNode_S As FE_Objects_Store.Nodes_Store
    Private tNode_E As FE_Objects_Store.Nodes_Store
    Private tCursor_Pt As Point

    Private _tSelected_elm_List As New List(Of Integer)

    Public ReadOnly Property Selected_elm_list() As List(Of Integer)
        Get
            Return _tSelected_elm_List
        End Get
    End Property

    Public Property Dyn_Cursor_Pt() As Point
        Get
            Return tCursor_Pt
        End Get
        Set(ByVal value As Point)
            tCursor_Pt = value
        End Set
    End Property

    Public ReadOnly Property Node_From() As FE_Objects_Store.Nodes_Store
        Get
            Return tNode_S
        End Get
    End Property

    Public ReadOnly Property Node_To() As FE_Objects_Store.Nodes_Store
        Get
            Return tNode_E
        End Get
    End Property

#Region "Form Opening/ Closing Operation"
    Private Sub Element_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIMain.is_childFormOpen = False '-- Update the main form that this form is closed
        MDIMain.is_elementFormOpen = False
        MDIMain.MT_Pic.Refresh()
    End Sub

    Private Sub Element_Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(MDIMain.Location.X + 0, MDIMain.Location.Y + 56)
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Opacity = 0.9
        Me.BringToFront()
        Me.TopMost = True
        Me.Element_DataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        TextBox_FN.Focus()
        is_TxtBox_FN_focused = True

        Element_DataGridView.AllowUserToResizeRows = False

        Initialize_DataGridView()
    End Sub
#End Region

#Region "Button Add Delete Import Click"
    Private Sub Button_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_add.Click
        '--- Adding Element (one at a time)
        If Test_a_textboxvalue_validity(TextBox_FN.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_TN.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_EMod.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_CArea.Text, 1, 1) = True And _
           Test_a_textboxvalue_validity(TextBox_Erho.Text, 1, 1) = True And _
           Val(TextBox_FN.Text) <> Val(TextBox_TN.Text) Then

            Add_Elements(Val(TextBox_FN.Text), Val(TextBox_TN.Text), Val(TextBox_EMod.Text), Val(TextBox_CArea.Text), Val(TextBox_Erho.Text))

        Else
            If Test_a_textboxvalue_validity(TextBox_FN.Text, 1, 1) = False Then
                TextBox_FN.Text = ""
                TextBox_FN.Focus()
            ElseIf Test_a_textboxvalue_validity(TextBox_TN.Text, 1, 1) = False Then
                TextBox_TN.Text = ""
                TextBox_TN.Focus()
            ElseIf Test_a_textboxvalue_validity(TextBox_EMod.Text, 1, 1) = False Then
                TextBox_EMod.SelectAll()
            ElseIf Test_a_textboxvalue_validity(TextBox_CArea.Text, 1, 1) = False Then
                TextBox_CArea.SelectAll()
            ElseIf Test_a_textboxvalue_validity(TextBox_Erho.Text, 1, 1) = False Then
                TextBox_Erho.SelectAll()
            Else
                TextBox_TN.Text = ""
                TextBox_TN.Focus()
            End If
        End If
    End Sub

    Public Sub Add_Elements(ByVal FN_id As Integer, ByVal TN_id As Integer, ByVal EModulus As Double, ByVal CArea As Double, ByVal ERho As Double)
        '--- Void Function which Adds Element one by one
        '----------- Adding unique IDs based on the missing ids
        Dim Elem_ID As Integer = 0
        If MDIMain.FElements.tElements.Count <> 0 Then
            Elem_ID = MDIMain.FElements.tElements.Count
            Dim i As Integer
            Dim n_id_list As New List(Of Integer)
            For i = 0 To (MDIMain.FElements.tElements.Count) - 1 Step +1
                n_id_list.Add(MDIMain.FElements.tElements(i).E_ID)
            Next
            n_id_list.Sort()
            For i = 0 To (MDIMain.FElements.tElements.Count) - 1 Step +1
                If i <> n_id_list(i) Then
                    Elem_ID = i
                    Exit For
                End If
            Next
        End If

        '--- Find the two nodes from list of node_store
        Dim Node_S, Node_E As FE_Objects_Store.Nodes_Store '--- Start Node and End Node are stored here
        Node_S = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = (FN_id - 1))
        Node_E = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = (TN_id - 1))

        If IsNothing(Node_S) Or IsNothing(Node_E) Then '-- Check whether the nodes are already present in the node list
            Exit Sub
        Else
            If MDIMain.FElements.tElements.Contains(New FE_Objects_Store.Elements_Store(Elem_ID, Node_S, Node_E, EModulus, ERho, CArea)) = False Then '--- avoid adding existing nodes
                MDIMain.FElements.tElements.Add(New FE_Objects_Store.Elements_Store(Elem_ID, Node_S, Node_E, EModulus, ERho, CArea))

                Dim elem_count As Integer = MDIMain.FElements.tElements.Count
                Dim DatGrid_row As String() = New String() {MDIMain.FElements.tElements(elem_count - 1).E_ID + 1, _
                                                            (MDIMain.FElements.tElements(elem_count - 1).SN.N_ID + 1), _
                                                            (MDIMain.FElements.tElements(elem_count - 1).EN.N_ID + 1), _
                                                             MDIMain.FElements.tElements(elem_count - 1).ELength, _
                                                             MDIMain.FElements.tElements(elem_count - 1).EModulus, _
                                                             MDIMain.FElements.tElements(elem_count - 1).ECArea, _
                                                             MDIMain.FElements.tElements(elem_count - 1).Rho_Density}
                Element_DataGridView.Rows.Add(DatGrid_row) '--- Add the node ID, x,y,z as string to Data grid view
            End If
            'Me.Hide()
            'Me.Show()
            is_TxtBox_FN_focused = True
            TextBox_FN.Text = ""
            TextBox_TN.Text = ""
            is_TxtFN_valid = False
            is_TxtTN_valid = False
            is_Draw_dynamictempline = False
            is_Draw_statictempline = False
            TextBox_FN.Focus()
            MDIMain.Main_Pic.Focus()
            MDIMain.MT_Pic.Refresh()
        End If

    End Sub

    Private Sub Button_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Delete.Click
        '--- Deleting Nodes (multiple delete is allowed)
        If Element_DataGridView.SelectedRows.Count <> 0 Then
            '--- update the data grid view (remove the rows of deleted nodes)
            Dim Selected_id_list As New List(Of Integer)
            '--- Step :1 Note down the Node_id of the Nodes being deleted
            Dim i As Integer = 0
            For Each DataGrid_row In Element_DataGridView.SelectedRows
                Selected_id_list.Add(Element_DataGridView.SelectedRows(i).Cells("Column1").Value) '-- Find the index of the item which are being deleted
                i = i + 1
            Next
            '--- Step :2 Remove these items from the tNodes variable
            For Each Id In Selected_id_list
                Dim N_v As Integer = (Id - 1) '-- To avoid the iteration variable in lamda expression Warning !!
                MDIMain.FElements.tElements.RemoveAt(MDIMain.FElements.tElements.FindIndex(Function(x) x.E_ID = N_v))
            Next
            '--- Step :3 Remove the Rows from datagridview
            For Each DataGrid_row In Element_DataGridView.SelectedRows
                Element_DataGridView.Rows.Remove(DataGrid_row)
            Next
        End If

        MDIMain.MT_Pic.Refresh() '-- Redraw after deleting nodes
    End Sub

    Private Sub Button_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_Import.Click
        '--- Import of Elements from a Text File
        Dim openFileDialog1 As New OpenFileDialog()

        'openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                Dim TxtReader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(openFileDialog1.FileName)

                If (TxtReader IsNot Nothing) Then
                    'TxtReader.ReadLine() '--- One Dummy Line
                    Dim TxtLine As String = TxtReader.ReadLine() '-- first line of Node values
                    Dim E_mod, C_Area, E_Rho As Double
                    While (IsNothing(TxtLine) = False)
                        Dim Node_Coords As New List(Of Double) '--- initialize a temp Node Coord list
                        For Each field As String In TxtLine.Split(New String() {ControlChars.Tab}, StringSplitOptions.None)
                            Node_Coords.Add(Double.Parse(field))
                        Next
                        If Node_Coords.Count = 2 Then
                            Add_Elements(Node_Coords(0), Node_Coords(1), E_mod, C_Area, E_Rho) '-- SN, EN, E_Mod, C_Area
                        ElseIf Node_Coords.Count = 5 Then
                            Add_Elements(Node_Coords(0), Node_Coords(1), Node_Coords(3), Node_Coords(4), Node_Coords(2)) '-- SN, EN, E_Mod, C_Area
                            E_Rho = Node_Coords(2)
                            E_mod = Node_Coords(3)
                            C_Area = Node_Coords(4)
                        End If
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

#Region "TextBox Focus Control (Also contains Node Selection control from MDImain)"
    Private Sub TextBox_Focus_Control() Handles TextBox_FN.Leave, TextBox_FN.Enter, TextBox_TN.Leave, TextBox_TN.Enter
        '--- Contorl to take note which text box is focused
        If TextBox_FN.Focused = True Then
            is_TxtBox_FN_focused = True
        Else
            is_TxtBox_FN_focused = False
        End If

        If TextBox_TN.Focused = True Then
            is_TxtBox_TN_focused = True
        Else
            is_TxtBox_TN_focused = False
        End If
    End Sub

    Private Sub TextBox_FN_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox_FN.TextChanged
        '--- Check whether the Node Number in From Node text box is valid
        is_TxtFN_valid = False
        TextBox_FN.ForeColor = Color.DarkRed
        If Test_a_textboxvalue_validity(TextBox_FN.Text, 1, 1) = True Then '-- 1. First check whether valid numerical input
            Dim FN_Nid As Integer = Val(TextBox_FN.Text)
            'Dim Node_S As FE_Objects_Store.Nodes_Store '--- find the Start Node from node list
            tNode_S = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = (FN_Nid - 1))
            If IsNothing(tNode_S) = False Then
                TextBox_FN.ForeColor = Color.DarkGreen
                is_TxtFN_valid = True
                Update_Dynamic_static_templine()
            End If
        End If
    End Sub

    Private Sub TextBox_TN_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox_TN.TextChanged
        '--- Check whether the Node Number in From Node text box is valid
        is_TxtTN_valid = False
        TextBox_TN.ForeColor = Color.DarkRed
        If Test_a_textboxvalue_validity(TextBox_TN.Text, 1, 1) = True Then '-- 1. First check whether valid numerical input
            Dim TN_Nid As Integer = Val(TextBox_TN.Text)
            'Dim Node_E As FE_Objects_Store.Nodes_Store '--- find the End Node from node list
            tNode_E = MDIMain.FElements.tNodes.Find(Function(T) T.N_ID = (TN_Nid - 1))
            If IsNothing(tNode_E) = False Then
                TextBox_TN.ForeColor = Color.DarkGreen
                is_TxtTN_valid = True
                Update_Dynamic_static_templine()
            End If
        End If
    End Sub

    Public Sub Selection_of_Node_fromMDImain(ByVal Selected_node As Integer)
        '--- Node is Selected in the MDI Main which is updated in the Text Box
        If is_TxtBox_FN_focused = True Then
            TextBox_FN.Text = Selected_node + 1
            TextBox_TN.Focus()
            is_TxtBox_TN_focused = True
        ElseIf is_TxtBox_TN_focused = True Then
            TextBox_TN.Text = Selected_node + 1
            is_TxtBox_TN_focused = False
            Button_add.Focus()
        End If

    End Sub

    Public Sub Update_Dynamic_static_templine()
        If is_TxtFN_valid = True And is_TxtTN_valid = True Then '-- Check if both start node and end nodes are valid
            If Val(TextBox_FN.Text) <> Val(TextBox_TN.Text) Then '-- Check whether the start node and end node or not the same
                is_Draw_statictempline = True
                is_Draw_dynamictempline = False
                'Button_add.Focus() '-- This line make Text box to lose focus for the earliest found nodes
            End If
        ElseIf is_TxtFN_valid = True Then '-- Check if the start node is valide
            is_Draw_statictempline = False
            is_Draw_dynamictempline = True
        Else '--- None of the nodes are valid draw nothing
            is_Draw_statictempline = False
            is_Draw_dynamictempline = False
        End If
    End Sub
#End Region

#Region "DataGridView Events"
    Public Sub Initialize_DataGridView()
        '---- Initialize the Data grid view with existing Node List
        If MDIMain.FElements.tElements.Count <> 0 Then '-- Check whether the Node count is zero or not
            Dim Nd_count As Integer = 0
            For Each Elements In MDIMain.FElements.tElements
                Dim DatGrid_row As String() = New String() {Elements.E_ID + 1, (Elements.SN.N_ID + 1), (Elements.EN.N_ID + 1), Elements.ELength, Elements.EModulus, Elements.ECArea, Elements.Rho_Density}
                Element_DataGridView.Rows.Add(DatGrid_row) '--- Add the node ID, x,y,z as string to Data grid view
            Next
        End If
    End Sub

    Public Sub Selection_of_Element_fromMDImain(ByVal Selected_element As Integer, ByVal Is_shift_Select As Boolean)
        '--- Element is Selected in the MDI Main which is updated in the data grid view
        Dim Add_Element_to_selection As Boolean
        If Is_shift_Select = False Then
            _tSelected_elm_List = New List(Of Integer)
            Element_DataGridView.ClearSelection()
            _tSelected_elm_List.Add(Selected_element)
            Add_Element_to_selection = True
        Else
            '--- Shift key is down -- so has to remove if the element is already in the list to avoid multiple addition of same nodes
            If _tSelected_elm_List.Contains(Selected_element) Then '-- check whether the selected node list contains the node
                _tSelected_elm_List.RemoveAt(_tSelected_elm_List.FindIndex(Function(T) T = Selected_element))
                Add_Element_to_selection = False
            Else '-- Selected element is unique so add to the list
                _tSelected_elm_List.Add(Selected_element)
                Add_Element_to_selection = True
            End If
        End If

        Update_DataGridView_for_Single_add_remove(MDIMain.FElements.tElements(Selected_element).E_ID + 1, Add_Element_to_selection)
    End Sub

    Public Sub Update_DataGridView_for_Single_add_remove(ByVal Selected_E_id As Integer, ByVal is_add As Boolean)
        'Step : 2 Below routine is to update the datagridview in any case
        'Node_DataGridView.ClearSelection()
        'Dim Selected_N_id As Integer = MDIMain.FElements.tNodes(Selected_node).N_ID + 1

        Dim i As Integer
        For i = 0 To (Element_DataGridView.Rows.Count - 1) '-- Go thro all nodes and match the node id
            If Element_DataGridView.Rows(i).Cells("Column1").Value = Selected_E_id Then
                If is_add = True Then
                    Element_DataGridView.Rows(i).Selected = True '-- select the row which matches the ID
                Else
                    Element_DataGridView.Rows(i).Selected = False '-- select the row which matches the ID
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub Element_DataGridView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Element_DataGridView.SelectionChanged
        _tSelected_elm_List = New List(Of Integer)
        For i = 0 To (Element_DataGridView.SelectedRows.Count - 1)
            '(Element_DataGridView.Rows(i).Cells("Column1").Value-1) stores the node id
            Dim D_id As Integer = i
            Dim Index_of_elements_selected As Integer
            Index_of_elements_selected = MDIMain.FElements.tElements.FindIndex(Function(T) T.E_ID = (Element_DataGridView.SelectedRows(D_id).Cells("Column1").Value - 1))
            If Index_of_elements_selected <> -1 Then '-1 means coundn't find the nodes
                _tSelected_elm_List.Add(Index_of_elements_selected)
            End If
        Next
        If Element_DataGridView.SelectedRows.Count <> 0 Then
            Element_DataGridView.FirstDisplayedScrollingRowIndex = Element_DataGridView.SelectedRows(0).Index
        End If
        MDIMain.MT_Pic.Refresh()
    End Sub
#End Region

End Class