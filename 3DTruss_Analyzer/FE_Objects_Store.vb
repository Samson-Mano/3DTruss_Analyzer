
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports _3DTruss_Analyzer.Common_Usable_Functions.Co_Functions

<Serializable()> _
Public Class FE_Objects_Store
    Const HSLLight As Single = 0.4

    Public Load_scaleLen As Double = 5
    Public Const_scaleLen As Double = 2 '-- Size of the Constraint Polygon
    Public Defrom_scaleLen As Double = 4

    Public tNodes As List(Of Nodes_Store)
    Public tElements As List(Of Elements_Store)
    Public MouseOver_Node_Index As Integer = -1 '-- Stores the index of node which has the mouse hover over <only works when add nodes or adding constraints form opened>
    Public MouseOver_Element_Index As Integer = -1 '-- Stores the index of element which has the mouse hover over <only works when add elements form opened>
    Public Scale_MaxLoad As Double = 1
    Public Scale_MaxDisplacement As Double = 1


    ''' <summary>
    ''' Void function to Paint all the objects of finte elements Nodes, Elements, Constraints
    ''' </summary>
    ''' <param name="Gr0"></param>
    ''' <remarks></remarks>
    Public Sub Paint_All_FElements(ByRef Gr0 As System.Drawing.Graphics)

        '---- Paint Elements First
        Dim gr1 As System.Drawing.Graphics = Gr0

        '--- Draw static and Dynamic temporary element line - event triggered only when Add Element form is open
        If MDIMain.is_elementFormOpen = True Then
            If MDIMain.AddElement_form.is_Draw_dynamictempline = True Then
                MDIMain.AddElement_form.Node_From.Paint_SelectedHighlight_Me(gr1)

                '--- Draw a Dynamic line
                Using Elem_Pen As New Pen(Color.Empty, 2)
                    Elem_Pen.Color = Color.DarkBlue
                    Elem_Pen.DashStyle = DashStyle.Dash
                    Gr0.DrawLine(Elem_Pen, MDIMain.AddElement_form.Node_From.N_Coord.Sc_Scaled_point, MDIMain.AddElement_form.Dyn_Cursor_Pt)
                End Using
            ElseIf MDIMain.AddElement_form.is_Draw_statictempline = True Then
                '--- Highlight the from Node
                MDIMain.AddElement_form.Node_From.Paint_SelectedHighlight_Me(gr1)

                '---Highlight the To Node
                MDIMain.AddElement_form.Node_To.Paint_SelectedHighlight_Me(gr1)

                '--- Draw a static line
                Using Elem_Pen As New Pen(Color.Empty, 2)
                    Elem_Pen.Color = Color.DarkBlue
                    Elem_Pen.DashStyle = DashStyle.Dash
                    Gr0.DrawLine(Elem_Pen, MDIMain.AddElement_form.Node_From.N_Coord.Sc_Scaled_point, MDIMain.AddElement_form.Node_To.N_Coord.Sc_Scaled_point)
                End Using
            Else
                '--- Highlight the element - event triggered only when Add Element form is open
                If MouseOver_Element_Index <> -1 Then
                    tElements(MouseOver_Element_Index).Paint_Highlight_Me(gr1)
                End If
            End If

            '--- Highlight the selected Elements - event triggered only when Add Element form is open
            If MDIMain.AddElement_form.Selected_elm_list.Count <> 0 Then
                Dim selected_Element_sublist As New List(Of Elements_Store)
                selected_Element_sublist = tElements.FindAll(Function(X) MDIMain.AddElement_form.Selected_elm_list.Find(Function(Y) Y = tElements.IndexOf(X)))
                '!!!!!!!!!!!!!! There is an error of not including the zeroth index element of telements !!!!!!!!!!!  <----------
                If MDIMain.AddElement_form.Selected_elm_list.Contains(0) = True Then
                    selected_Element_sublist.Add(tElements(0))
                End If
                selected_Element_sublist.ForEach(Function(obj) obj.Paint_SelectedHighlight_Me(gr1))
            End If

        End If

        tElements.ForEach(Function(obj) obj.Paint_Draw_Me(gr1, (Defrom_scaleLen / (Scale_MaxDisplacement * MDIMain.threeD_Control.Sc_val4))))


        '--- Highlight the selected Nodes - event triggered only when Add Node form is open
        If MDIMain.is_nodeFormOpen = True Then
            If MDIMain.AddNode_form.Selected_nd_list.Count <> 0 Then
                Dim selected_Node_sublist As New List(Of Nodes_Store)
                selected_Node_sublist = tNodes.FindAll(Function(X) MDIMain.AddNode_form.Selected_nd_list.Find(Function(Y) Y = tNodes.IndexOf(X)))
                '!!!!!!!!!!!!!! There is an error of not including the zeroth index element of tnodes !!!!!!!!!!!  <----------
                If MDIMain.AddNode_form.Selected_nd_list.Contains(0) = True Then
                    selected_Node_sublist.Add(tNodes(0))
                End If
                selected_Node_sublist.ForEach(Function(obj) obj.Paint_SelectedHighlight_Me(gr1))
            End If
        End If

        '--- Highlight the selected Nodes - event triggered only when Add Constraint form is open
        If MDIMain.is_loadFormOpen = True Then
            If MDIMain.AddLoad_form.Selected_nd_list.Count <> 0 Then
                Dim selected_Node_sublist As New List(Of Nodes_Store)
                selected_Node_sublist = tNodes.FindAll(Function(X) MDIMain.AddLoad_form.Selected_nd_list.Find(Function(Y) Y = tNodes.IndexOf(X)))
                '!!!!!!!!!!!!!! There is an error of not including the zeroth index element of tnodes !!!!!!!!!!!  <----------
                If MDIMain.AddLoad_form.Selected_nd_list.Contains(0) = True Then
                    selected_Node_sublist.Add(tNodes(0))
                End If
                selected_Node_sublist.ForEach(Function(obj) obj.Paint_SelectedHighlight_Me(gr1))
            End If
        End If

        '--- Highlight the node where mouse is hovered -  event triggered only when Add Node, Add element or Add Constraint forms are open
        If MouseOver_Node_Index <> -1 Then
            tNodes(MouseOver_Node_Index).Paint_Highlight_Me(gr1)
        End If

        '---- Paint Nodes on Top of Elements
        tNodes.ForEach(Function(obj) obj.Paint_Draw_Me(gr1, (Load_scaleLen / (Scale_MaxLoad * MDIMain.threeD_Control.Sc_val4)), (Const_scaleLen / MDIMain.threeD_Control.Sc_val4), (Defrom_scaleLen / (Scale_MaxDisplacement * MDIMain.threeD_Control.Sc_val4))))


    End Sub

    ''' <summary>
    '''  Construstore of all the FE Objects -> one time initialization during form load
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        'CS_scaleValue = 10
        tNodes = New List(Of Nodes_Store)
        tElements = New List(Of Elements_Store)
    End Sub

    ''' <summary>
    ''' Stores the Node Datas
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    <Serializable()> _
    Public Class Nodes_Store
        Inherits Constraints_Store
        Implements IEquatable(Of Nodes_Store)

        '---- Nodal Coordinate and ID is stored here
        Private tN_ID As Integer
        Private tN_Coord As Cube_Rotation.ThreeD_Point
        Private tN_isNodehasForce As Boolean
        Private tN_isNodehasConstraint As Boolean
        Private tN_Disp_Color As Color

        ''' <summary>
        ''' Returns the ID of the Node
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property N_ID() As Integer
            Get
                Return tN_ID
            End Get
        End Property

        ''' <summary>
        ''' Returns the ID in String form to Draw
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property N_ID_string() As String
            Get
                Return CStr(tN_ID + 1)
            End Get
        End Property

        Public ReadOnly Property N_Coord_string() As String
            Get
                Return "(" & tN_Coord.X & ", " & tN_Coord.Y & ", " & tN_Coord.Z & ")"
            End Get
        End Property

        ''' <summary>
        ''' Returns the Nodal coordinates as threeD_Point
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property N_Coord() As Cube_Rotation.ThreeD_Point
            Get
                Return tN_Coord
            End Get
        End Property

        Public ReadOnly Property N_Disp_Color() As Color
            Get
                Return tN_Disp_Color
            End Get
        End Property

        Public ReadOnly Property is_NodehasForce() As Boolean
            Get
                Return tN_isNodehasForce
            End Get
        End Property

        Public ReadOnly Property is_NodehasConstraint() As Boolean
            Get
                Return tN_isNodehasConstraint
            End Get
        End Property

        Public Sub New(ByVal i_ID As Integer, ByVal i_x As Double, ByVal i_y As Double, ByVal i_z As Double)
            tN_ID = i_ID
            tN_Coord = New Cube_Rotation.ThreeD_Point(i_x, i_y, i_z)
            Update_NodeForce = New XYZ_Force(0, 0, 0)
            Update_NodeConstraint = New XYZ_DOF(False, False, False)
            tN_isNodehasForce = False
            tN_isNodehasConstraint = False
        End Sub

        Public Sub Update_My_Displacements(ByVal i_XDisp As Double, _
                                           ByVal i_YDisp As Double, _
                                           ByVal i_ZDisp As Double, ByVal Max_Rslt_Disp As Double, _
                                           ByVal i_ReactX As Double, _
                                           ByVal i_ReactY As Double, _
                                           ByVal i_ReactZ As Double)

            Update_NodeDisplacement = New XYZ_Displacement(i_XDisp, i_YDisp, i_ZDisp)
            Dim Rslt_Disp As Double
            Rslt_Disp = Math.Sqrt((i_XDisp ^ 2) + (i_YDisp ^ 2) + (i_ZDisp ^ 2))
            tN_Disp_Color = HSLToRGB(240, (1 - (Rslt_Disp / Max_Rslt_Disp)) * 240, 1, HSLLight)

            Update_NodeReactionForce = New XYZ_Force(i_ReactX, i_ReactY, i_ReactZ)
        End Sub

        Public Sub Update_My_Constraints(ByVal i_forceX As Double, _
                                      ByVal i_forceY As Double, _
                                      ByVal i_forceZ As Double, _
                                      ByVal i_consX As Boolean, _
                                      ByVal i_consY As Boolean, _
                                      ByVal i_consZ As Boolean)
            Update_NodeConstraint = New XYZ_DOF(i_consX, i_consY, i_consZ)
            Update_NodeForce = New XYZ_Force(i_forceX, i_forceY, i_forceZ)
            '-- Update whether Node has Force or Constraints to Draw
            tN_isNodehasConstraint = False
            tN_isNodehasForce = False
            If i_forceX <> 0 Or i_forceY <> 0 Or i_forceZ <> 0 Then
                tN_isNodehasForce = True
            End If
            If i_consX <> False Or i_consY <> False Or i_consZ <> False Then
                tN_isNodehasConstraint = True
            End If
        End Sub

        Public Sub Update_Node_IDs_for_Analysis(ByRef U_NId As Integer)
            tN_ID = U_NId
        End Sub

        Public Shared Operator =(ByVal N1 As Nodes_Store, ByVal N2 As Nodes_Store) As Boolean
            Dim Rslt As Boolean = False
            If N1.Equals(Nothing) = False And N2.Equals(Nothing) = False Then
                If N1.N_Coord.X = N2.N_Coord.X And _
               N1.N_Coord.Y = N2.N_Coord.Y And _
               N1.N_Coord.Z = N2.N_Coord.Z Then
                    Rslt = True
                Else
                    Rslt = False
                End If
            Else
                Rslt = False
            End If
            Return Rslt
        End Operator

        Public Shared Operator <>(ByVal N1 As Nodes_Store, ByVal N2 As Nodes_Store) As Boolean
            Return Not (N1 = N2)
        End Operator

        Public Function Equals1(ByVal other As Nodes_Store) As Boolean Implements System.IEquatable(Of Nodes_Store).Equals
            If Me.tN_Coord.X = other.tN_Coord.X And Me.tN_Coord.Y = other.tN_Coord.Y _
               And Me.tN_Coord.Z = other.tN_Coord.Z Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Function which check whether a Point is inside the circle
        ''' </summary>
        ''' <param name="Circle_center">Center of Circle</param>
        ''' <param name="Circle_Radii">Raidus of the Circle</param>
        ''' <returns>Returns True or False depends on whether the point is inside the circle or not</returns>
        ''' <remarks></remarks>
        Public Function Test_Point_in_Circle(ByRef Circle_center As Point, ByRef Circle_Radii As Double) As Boolean
            Dim Rslt As Boolean = False
            If Circle_center.X - Circle_Radii < tN_Coord.Sc_Scaled_point.X And _
               Circle_center.X + Circle_Radii > tN_Coord.Sc_Scaled_point.X And _
               Circle_center.Y - Circle_Radii < tN_Coord.Sc_Scaled_point.Y And _
               Circle_center.Y + Circle_Radii > tN_Coord.Sc_Scaled_point.Y Then
                Rslt = True
            End If
            Return Rslt
        End Function

        ''' <summary>
        ''' Function which returns Rectangle for the ellipse to be drawn
        ''' </summary>
        ''' <param name="Pt">Center of circle</param>
        ''' <param name="El_radius">Redius of circle</param>
        ''' <returns>Returns Rectangle which can be used in drawellipse or fillellipse functions</returns>
        ''' <remarks></remarks>
        Public Function Return_Ellipse_Rectangle(ByRef Pt As Point, ByRef El_radius As Integer) As Rectangle
            Return New Rectangle(Pt.X - El_radius, Pt.Y - El_radius, El_radius * 2, El_radius * 2)
        End Function

        Public Function Paint_Draw_Me(ByRef Gr0 As System.Drawing.Graphics, ByRef tF_scale As Double, ByRef tC_scale As Double, ByRef tD_scale As Double)
            Using Node_Pen As New Pen(Color.Empty, 2)
                If MDIMain.is_Analysis_Success = True And MDIMain.NoContourToolStripMenuItem.Checked = False Then '--- Analysis Complete
                    If MDIMain.DeformedToolStripMenuItem.Checked = True Then '--- Deformed view ** So need to draw undeformed skeleton of the model **
                        If MDIMain.Is_DrawUndeformedView = True Then
                            Node_Pen.Color = Color.Gray
                            Gr0.FillEllipse(Node_Pen.Brush, Return_Ellipse_Rectangle(N_Coord.Sc_Scaled_point, 3))

                            '-- Draw String - Node number and Node Coordinates
                            Node_Pen.Color = Color.LightGray
                            If MDIMain.Is_DrawNodeCoord = True Or MDIMain.Is_DrawNodeNum = True Then
                                Dim NStr As String = If(MDIMain.Is_DrawNodeNum = True, N_ID_string, "") & If(MDIMain.Is_DrawNodeCoord = True, N_Coord_string, "")
                                Gr0.DrawString(NStr, New Font("Verdana", 10), Node_Pen.Brush, N_Coord.Sc_Scaled_point.X, N_Coord.Sc_Scaled_point.Y)
                            End If

                        End If

                        '-- Paint Loads
                        If is_NodehasForce = True Then
                            Paint_Me_Loads(Gr0, tF_scale, Color.DarkGray)
                        End If

                        '-- Paint Constraints
                        If is_NodehasConstraint = True Then
                            Paint_Me_Constraints(Gr0, tC_scale, Color.DarkGray)

                            '---- Paint Reaction Forces
                            Paint_Me_ReactionForces(Gr0, tF_scale, Color.Green)
                        End If

                        Node_Pen.Color = N_Disp_Color
                        '--- Draw the deformed contour view
                        Dim temp_NCoord As Cube_Rotation.ThreeD_Point
                        temp_NCoord = New Cube_Rotation.ThreeD_Point(N_Coord.X + (Node_Displacement.X_Disp * tD_scale), _
                                                                      N_Coord.Y + (Node_Displacement.Y_Disp * tD_scale), _
                                                                      N_Coord.Z + (Node_Displacement.Z_Disp * tD_scale))

                        Gr0.FillEllipse(Node_Pen.Brush, Return_Ellipse_Rectangle(temp_NCoord.Sc_Scaled_point, 3))


                        If MDIMain.DisplacementToolStripMenuItem.Checked = True And MDIMain.Is_DrawResultValues = True Then
                            '---- Deformation Values have to be displaued
                            Node_Pen.Color = N_Disp_Color

                            'Dim NStr As String = "(" & Node_Displacement.X_Disp & ", " & Node_Displacement.Y_Disp & ", " & Node_Displacement.Z_Disp & ")"
                            Gr0.DrawString(Node_Displacement.Disp_Str, New Font("Verdana", 10), Node_Pen.Brush, temp_NCoord.Sc_Scaled_point.X, temp_NCoord.Sc_Scaled_point.Y)

                        End If
                    Else
                        Node_Pen.Color = N_Disp_Color
                        Gr0.FillEllipse(Node_Pen.Brush, Return_Ellipse_Rectangle(N_Coord.Sc_Scaled_point, 3))

                        '-- Paint Loads
                        If is_NodehasForce = True Then
                            Paint_Me_Loads(Gr0, tF_scale, Color.DarkGray)
                        End If

                        '-- Paint Constraints
                        If is_NodehasConstraint = True Then
                            Paint_Me_Constraints(Gr0, tC_scale, Color.DarkGray)

                            '---- Paint Reaction Forces
                            Paint_Me_ReactionForces(Gr0, tF_scale, Color.Green)
                        End If


                        '---- Deformation Values have to be displaued
                        If MDIMain.DisplacementToolStripMenuItem.Checked = True And MDIMain.Is_DrawResultValues = True Then
                            '-- Draw String - Node Displacement Values
                            Node_Pen.Color = N_Disp_Color

                            'Dim NStr As String = "(" & Node_Displacement.X_Disp & ", " & Node_Displacement.Y_Disp & ", " & Node_Displacement.Z_Disp & ")"
                            Gr0.DrawString(Node_Displacement.Disp_Str, New Font("Verdana", 10), Node_Pen.Brush, N_Coord.Sc_Scaled_point.X, N_Coord.Sc_Scaled_point.Y)

                        End If
                    End If
                Else
                    '----- Analysis not complete - Model View
                    Node_Pen.Color = Color.BlueViolet
                    Gr0.FillEllipse(Node_Pen.Brush, Return_Ellipse_Rectangle(N_Coord.Sc_Scaled_point, 3))

                    '-- Draw String - Node number and Node Coordinates
                    'Node_Pen.Color = Color.LightBlue
                    If MDIMain.Is_DrawNodeCoord = True Or MDIMain.Is_DrawNodeNum = True Then
                        Dim NStr As String = If(MDIMain.Is_DrawNodeNum = True, N_ID_string, "") & If(MDIMain.Is_DrawNodeCoord = True, N_Coord_string, "")
                        Gr0.DrawString(NStr, New Font("Verdana", 10), Node_Pen.Brush, N_Coord.Sc_Scaled_point.X, N_Coord.Sc_Scaled_point.Y)
                    End If

                    '-- Paint Loads
                    If is_NodehasForce = True Then
                        Paint_Me_Loads(Gr0, tF_scale, Color.Green)
                    End If

                    '-- Paint Constraints
                    If is_NodehasConstraint = True Then
                        Paint_Me_Constraints(Gr0, tC_scale, Color.Brown)
                    End If
                End If
            End Using
            Return False
        End Function

        Public Function Paint_Highlight_Me(ByRef Gr0 As System.Drawing.Graphics)
            Using Node_Pen As New Pen(Color.Empty, 3)
                Node_Pen.Color = Color.Cyan
                Gr0.DrawEllipse(Node_Pen, Return_Ellipse_Rectangle(N_Coord.Sc_Scaled_point, 3))
            End Using
            Return False
        End Function

        Public Function Paint_SelectedHighlight_Me(ByRef Gr0 As System.Drawing.Graphics)
            Using Node_Pen As New Pen(Color.Empty, 2)
                Node_Pen.Color = Color.Cyan
                Gr0.DrawEllipse(Node_Pen, Return_Ellipse_Rectangle(N_Coord.Sc_Scaled_point, 4))
            End Using
            Return False
        End Function

        Private Sub Paint_Me_Loads(ByRef Gr0 As System.Drawing.Graphics, ByRef F_scale As Double, ByRef L_Color As Color)
            '--- F_Scale is to scale the load accordingly F_scale = Load_scaleLen / (Scale_MaxLoad * MDIMain.threeD_Control.Sc_val4)
            If MDIMain.Is_DrawLoad = True Then
                Using StartCapPath As New GraphicsPath
                    StartCapPath.AddLine(0, 0, -2, -2) '-- create cap
                    StartCapPath.AddLine(0, 0, 2, -2)
                    Using Start_cap As New CustomLineCap(Nothing, StartCapPath)
                        Using Load_pen As New Pen(Color.Empty, 3)
                            Load_pen.Color = L_Color
                            Load_pen.CustomStartCap = Start_cap
                            Dim temp_Coord As Cube_Rotation.ThreeD_Point
                            If Node_Force.X_force <> 0 Then
                                temp_Coord = New Cube_Rotation.ThreeD_Point(N_Coord.X - (Node_Force.X_force * F_scale), N_Coord.Y, N_Coord.Z)
                                Gr0.DrawLine(Load_pen, N_Coord.Sc_Scaled_point, temp_Coord.Sc_Scaled_point)
                                If MDIMain.Is_DrawLoadValue = True Then
                                    Gr0.DrawString(Math.Abs(Node_Force.X_force), New Font("Verdana", 10), Load_pen.Brush, temp_Coord.Sc_Scaled_point.X, temp_Coord.Sc_Scaled_point.Y)
                                End If
                            End If
                            If Node_Force.Y_force <> 0 Then
                                temp_Coord = New Cube_Rotation.ThreeD_Point(N_Coord.X, N_Coord.Y - (Node_Force.Y_force * F_scale), N_Coord.Z)
                                Gr0.DrawLine(Load_pen, N_Coord.Sc_Scaled_point, temp_Coord.Sc_Scaled_point)
                                If MDIMain.Is_DrawLoadValue = True Then
                                    Gr0.DrawString(Math.Abs(Node_Force.Y_force), New Font("Verdana", 10), Load_pen.Brush, temp_Coord.Sc_Scaled_point.X, temp_Coord.Sc_Scaled_point.Y)
                                End If
                            End If
                            If Node_Force.Z_force <> 0 Then
                                temp_Coord = New Cube_Rotation.ThreeD_Point(N_Coord.X, N_Coord.Y, N_Coord.Z - (Node_Force.Z_force * F_scale))
                                Gr0.DrawLine(Load_pen, N_Coord.Sc_Scaled_point, temp_Coord.Sc_Scaled_point)
                                If MDIMain.Is_DrawLoadValue = True Then
                                    Gr0.DrawString(Math.Abs(Node_Force.Z_force), New Font("Verdana", 10), Load_pen.Brush, temp_Coord.Sc_Scaled_point.X, temp_Coord.Sc_Scaled_point.Y)
                                End If
                            End If
                        End Using
                    End Using
                End Using
            End If

        End Sub

        Private Sub Paint_Me_ReactionForces(ByRef Gr0 As System.Drawing.Graphics, ByRef F_scale As Double, ByRef L_Color As Color)
            Using StartCapPath As New GraphicsPath
                StartCapPath.AddLine(0, 0, -2, -2) '-- create cap
                StartCapPath.AddLine(0, 0, 2, -2)
                Using Start_cap As New CustomLineCap(Nothing, StartCapPath)
                    Using Reaction_pen As New Pen(Color.Empty, 3)
                        Reaction_pen.Color = L_Color
                        Reaction_pen.CustomStartCap = Start_cap
                        Dim temp_Coord As Cube_Rotation.ThreeD_Point
                        If Node_Constraint.is_X_fixed = True Then
                            temp_Coord = New Cube_Rotation.ThreeD_Point(N_Coord.X - (Node_Reactions.X_force * F_scale), N_Coord.Y, N_Coord.Z)
                            Gr0.DrawLine(Reaction_pen, N_Coord.Sc_Scaled_point, temp_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawLoadValue = True Then
                                Gr0.DrawString(Node_Reactions.X_Reac_Str, New Font("Verdana", 10), Reaction_pen.Brush, temp_Coord.Sc_Scaled_point.X, temp_Coord.Sc_Scaled_point.Y)
                            End If
                        End If
                        If Node_Constraint.is_Y_fixed = True Then
                            temp_Coord = New Cube_Rotation.ThreeD_Point(N_Coord.X, N_Coord.Y - (Node_Reactions.Y_force * F_scale), N_Coord.Z)
                            Gr0.DrawLine(Reaction_pen, N_Coord.Sc_Scaled_point, temp_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawLoadValue = True Then
                                Gr0.DrawString(Node_Reactions.Y_Reac_Str, New Font("Verdana", 10), Reaction_pen.Brush, temp_Coord.Sc_Scaled_point.X, temp_Coord.Sc_Scaled_point.Y)
                            End If
                        End If
                        If Node_Constraint.is_Z_fixed = True Then
                            temp_Coord = New Cube_Rotation.ThreeD_Point(N_Coord.X, N_Coord.Y, N_Coord.Z - (Node_Reactions.Z_force * F_scale))
                            Gr0.DrawLine(Reaction_pen, N_Coord.Sc_Scaled_point, temp_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawLoadValue = True Then
                                Gr0.DrawString(Node_Reactions.Z_Reac_Str, New Font("Verdana", 10), Reaction_pen.Brush, temp_Coord.Sc_Scaled_point.X, temp_Coord.Sc_Scaled_point.Y)
                            End If
                        End If
                    End Using
                End Using
            End Using
        End Sub

        Private Sub Paint_Me_Constraints(ByRef Gr0 As System.Drawing.Graphics, ByRef C_scale As Double, ByRef Const_Color As Color)
            If MDIMain.Is_DrawConst = True Then
                '--- C_Scale is to scale the constraint accordingly C_scale = (Const_scaleLen / MDIMain.threeD_Control.Sc_val4)
                Dim ptL, ptR As Cube_Rotation.ThreeD_Point

                '--- X is Fixed
                If Node_Constraint.is_X_fixed = True Then
                    ptL = New Cube_Rotation.ThreeD_Point(N_Coord.X - C_scale, N_Coord.Y - (C_scale * 0.5), N_Coord.Z)
                    ptR = New Cube_Rotation.ThreeD_Point(N_Coord.X - C_scale, N_Coord.Y + (C_scale * 0.5), N_Coord.Z)
                    Using StartCapPath As New GraphicsPath
                        StartCapPath.AddPolygon(New Point() {N_Coord.Sc_Scaled_point, ptL.Sc_Scaled_point, ptR.Sc_Scaled_point, N_Coord.Sc_Scaled_point})
                        Using Constr_pen As New Pen(Color.Empty, 2)
                            Constr_pen.Color = Const_Color
                            Gr0.DrawPath(Constr_pen, StartCapPath)
                        End Using
                    End Using
                End If
                '--- Y is Fixed
                If Node_Constraint.is_Y_fixed = True Then
                    ptL = New Cube_Rotation.ThreeD_Point(N_Coord.X, N_Coord.Y - C_scale, N_Coord.Z - (C_scale * 0.5))
                    ptR = New Cube_Rotation.ThreeD_Point(N_Coord.X, N_Coord.Y - C_scale, N_Coord.Z + (C_scale * 0.5))
                    Using StartCapPath As New GraphicsPath
                        StartCapPath.AddPolygon(New Point() {N_Coord.Sc_Scaled_point, ptL.Sc_Scaled_point, ptR.Sc_Scaled_point, N_Coord.Sc_Scaled_point})
                        Using Constr_pen As New Pen(Color.Empty, 2)
                            Constr_pen.Color = Const_Color
                            Gr0.DrawPath(Constr_pen, StartCapPath)
                        End Using
                    End Using
                End If
                '--- Z is Fixed
                If Node_Constraint.is_Z_fixed = True Then
                    ptL = New Cube_Rotation.ThreeD_Point(N_Coord.X - (C_scale * 0.5), N_Coord.Y, N_Coord.Z - C_scale)
                    ptR = New Cube_Rotation.ThreeD_Point(N_Coord.X + (C_scale * 0.5), N_Coord.Y, N_Coord.Z - C_scale)
                    Using StartCapPath As New GraphicsPath
                        StartCapPath.AddPolygon(New Point() {N_Coord.Sc_Scaled_point, ptL.Sc_Scaled_point, ptR.Sc_Scaled_point, N_Coord.Sc_Scaled_point})
                        Using Constr_pen As New Pen(Color.Empty, 2)
                            Constr_pen.Color = Const_Color
                            Gr0.DrawPath(Constr_pen, StartCapPath)
                        End Using
                    End Using
                End If
            End If
        End Sub
    End Class

    <Serializable()> _
    Public Class Elements_Store
        Implements IEquatable(Of Elements_Store)

        '----- Element Start Node End Node and Properties are stored here
        Private tE_ID As Integer
        Private tSN As Nodes_Store
        Private tEN As Nodes_Store
        Private tLength As Double
        Private tEModulus As Double
        Private trho As Double
        Private tCArea As Double
        Private tMemberRlts As New Member_Results

        Public ReadOnly Property E_ID() As Integer
            Get
                Return tE_ID
            End Get
        End Property

        Public ReadOnly Property SN() As Nodes_Store
            Get
                Return tSN
            End Get
        End Property

        Public ReadOnly Property EN() As Nodes_Store
            Get
                Return tEN
            End Get
        End Property

        Public ReadOnly Property Rho_Density() As Double
            Get
                Return trho
            End Get
        End Property

        Public ReadOnly Property ELength() As Double
            Get
                Return tLength
            End Get
        End Property

        Public ReadOnly Property ELength_Str() As String
            Get
                Return Math.Round(tLength, MDIMain.Round_offVal).ToString
            End Get
        End Property

        Public ReadOnly Property EModulus() As Double
            Get
                Return tEModulus
            End Get
        End Property

        Public ReadOnly Property ECArea() As Double
            Get
                Return tCArea
            End Get
        End Property

        Public ReadOnly Property MemberRlts() As Member_Results
            Get
                Return tMemberRlts
            End Get
        End Property

        Public Sub New(ByVal Elem_ID As Integer, ByRef i_SN As Nodes_Store, ByRef i_EN As Nodes_Store, ByVal i_E As Double, ByVal i_rho As Double, ByVal i_A As Double)
            tE_ID = Elem_ID
            tSN = i_SN
            tEN = i_EN
            tLength = Math.Sqrt(((tSN.N_Coord.X - tEN.N_Coord.X) ^ 2) + _
                                ((tSN.N_Coord.Y - tEN.N_Coord.Y) ^ 2) + _
                                ((tSN.N_Coord.Z - tEN.N_Coord.Z) ^ 2)) '--- Length between two nodes
            trho = i_rho
            tEModulus = i_E
            tCArea = i_A
        End Sub

        Public Function Equals1(ByVal other As Elements_Store) As Boolean Implements System.IEquatable(Of Elements_Store).Equals
            If (Me.tSN.N_ID = other.tSN.N_ID And Me.tEN.N_ID = other.tEN.N_ID) Or _
               (Me.tSN.N_ID = other.tEN.N_ID And Me.tEN.N_ID = other.tSN.N_ID) Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Function which checks whether a point lies on the line
        ''' </summary>
        ''' <param name="Pt">Point to be checked</param>
        ''' <param name="Threshold">Threshold for highlighting the line</param>
        ''' <returns>Returns True or False depends on whether the point lies on the line or not</returns>
        ''' <remarks></remarks>
        Public Function Test_Point_on_line(ByRef Pt As Point, ByRef Threshold As Double)
            Dim Rslt As Boolean = False
            '-- Step: 1 Find the cross product
            Dim dxc, dyc As Integer '--- Vector 1 Between Given Point and First point of the line
            dxc = Pt.X - tSN.N_Coord.Sc_Scaled_point.X
            dyc = Pt.Y - tSN.N_Coord.Sc_Scaled_point.Y

            Dim dx1, dy1 As Integer '--- Vector 2 Between the Second and First point of the line
            dx1 = tEN.N_Coord.Sc_Scaled_point.X - tSN.N_Coord.Sc_Scaled_point.X
            dy1 = tEN.N_Coord.Sc_Scaled_point.Y - tSN.N_Coord.Sc_Scaled_point.Y

            Dim CrossPrd As Double
            CrossPrd = (dxc * dy1) - (dyc * dx1) 'Vector cross product

            If Math.Abs(CrossPrd) <= Threshold Then '-- Check whether the cross product is within the threshold (other wise Not on the line)
                If Math.Abs(dx1) >= Math.Abs(dy1) Then '--- The line is more horizontal or 45 degree
                    'If dx1 > 0 = true ' EPt.x is bigger else 'Spt.x is bigger
                    Rslt = If(dx1 > 0, If((tSN.N_Coord.Sc_Scaled_point.X < Pt.X And Pt.X < tEN.N_Coord.Sc_Scaled_point.X), True, False), If((tEN.N_Coord.Sc_Scaled_point.X < Pt.X And Pt.X < tSN.N_Coord.Sc_Scaled_point.X), True, False))
                Else '-- The line is more vertical
                    'If dy1 > 0 = true ' EPt.y is bigger else 'Spt.y is bigger
                    Rslt = If(dy1 > 0, If((tSN.N_Coord.Sc_Scaled_point.Y < Pt.Y And Pt.Y < tEN.N_Coord.Sc_Scaled_point.Y), True, False), If((tEN.N_Coord.Sc_Scaled_point.Y < Pt.Y And Pt.Y < tSN.N_Coord.Sc_Scaled_point.Y), True, False))
                End If
            End If
            Return Rslt
        End Function

        Public Sub Update_Member_results(ByRef tAForce As Double, ByRef tAForce_maxRatio As Double, _
                                         ByRef tAStress As Double, ByRef tAStress_maxRatio As Double, _
                                         ByRef tAStrain As Double, ByRef tAStrain_maxRatio As Double)


            tMemberRlts = New Member_Results(tAForce, tAForce_maxRatio, _
                                          tAStress, tAStress_maxRatio, _
                                          tAStrain, tAStrain_maxRatio)
        End Sub

        Public Function Paint_Draw_Me(ByRef Gr0 As System.Drawing.Graphics, ByRef tD_scale As Double)
            Using Elem_Pen As New Pen(Color.Empty, 2)
                If MDIMain.is_Analysis_Success = True And MDIMain.NoContourToolStripMenuItem.Checked = False Then '--- Analysis Complete
                    If MDIMain.DeformedToolStripMenuItem.Checked = True Then '--- Deformed view ** So need to draw undeformed skeleton of the model **
                        If MDIMain.Is_DrawUndeformedView = True Then
                            Elem_Pen.Color = Color.LightGray
                            Elem_Pen.DashStyle = DashStyle.Dot
                            Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawElemLength = True Then
                                Gr0.DrawString(ELength_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point))
                            End If
                        End If

                        '--- Draw the deformed contour view
                        Dim temp_SNCoord As Cube_Rotation.ThreeD_Point
                        temp_SNCoord = New Cube_Rotation.ThreeD_Point(tSN.N_Coord.X + (tSN.Node_Displacement.X_Disp * tD_scale), _
                                                                      tSN.N_Coord.Y + (tSN.Node_Displacement.Y_Disp * tD_scale), _
                                                                      tSN.N_Coord.Z + (tSN.Node_Displacement.Z_Disp * tD_scale))

                        Dim temp_ENCoord As Cube_Rotation.ThreeD_Point
                        temp_ENCoord = New Cube_Rotation.ThreeD_Point(tEN.N_Coord.X + (tEN.Node_Displacement.X_Disp * tD_scale), _
                                                                      tEN.N_Coord.Y + (tEN.Node_Displacement.Y_Disp * tD_scale), _
                                                                      tEN.N_Coord.Z + (tEN.Node_Displacement.Z_Disp * tD_scale))

                        Elem_Pen.DashStyle = DashStyle.Solid
                        If MDIMain.DisplacementToolStripMenuItem.Checked = True Then
                            '---- Deformation Contour
                            Try
                                Using Lin_GrBrush As New LinearGradientBrush(temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point, tSN.N_Disp_Color, tEN.N_Disp_Color)
                                    Gr0.DrawLine(New Pen(Lin_GrBrush, 2), temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point)
                                End Using
                            Catch ex As Exception
                                MDIMain.ToolStripStatusLabel_ErrorStatus.Text = "Linear Gradient Brush Error Avoided"
                            End Try
                        ElseIf MDIMain.AxialForceToolStripMenuItem.Checked = True Then
                            '---- Axial Force Contour
                            Elem_Pen.Color = tMemberRlts.Force_Color
                            Gr0.DrawLine(Elem_Pen, temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point)
                            If MDIMain.Is_DrawResultValues = True Then
                                Gr0.DrawString(tMemberRlts.Axial_force_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point))
                            End If
                        ElseIf MDIMain.AxialStressToolStripMenuItem.Checked = True Then
                            '---- Axial Stress Contour
                            Elem_Pen.Color = tMemberRlts.Stress_Color
                            Gr0.DrawLine(Elem_Pen, temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point)
                            If MDIMain.Is_DrawResultValues = True Then
                                Gr0.DrawString(tMemberRlts.Axial_stress_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point))
                            End If
                        ElseIf MDIMain.AxialStrainToolStripMenuItem.Checked = True Then
                            '---- Axial Strain Contour
                            Elem_Pen.Color = tMemberRlts.Strain_Color
                            Gr0.DrawLine(Elem_Pen, temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point)
                            If MDIMain.Is_DrawResultValues = True Then
                                Gr0.DrawString(tMemberRlts.Axial_strain_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(temp_SNCoord.Sc_Scaled_point, temp_ENCoord.Sc_Scaled_point))
                            End If
                        End If
                    Else
                        If MDIMain.DisplacementToolStripMenuItem.Checked = True Then
                            '---- Deformation Contour
                            Try
                                Using Lin_GrBrush As New LinearGradientBrush(tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point, tSN.N_Disp_Color, tEN.N_Disp_Color)
                                    Gr0.DrawLine(New Pen(Lin_GrBrush, 2), tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
                                End Using
                            Catch ex As Exception
                                MDIMain.ToolStripStatusLabel_ErrorStatus.Text = "Linear Gradient Brush Error Avoided"
                            End Try
                        ElseIf MDIMain.AxialForceToolStripMenuItem.Checked = True Then
                            '---- Axial Force Contour
                            Elem_Pen.Color = tMemberRlts.Force_Color
                            Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawResultValues = True Then
                                Gr0.DrawString(tMemberRlts.Axial_force_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point))
                            End If
                        ElseIf MDIMain.AxialStressToolStripMenuItem.Checked = True Then
                            '---- Axial Stress Contour
                            Elem_Pen.Color = tMemberRlts.Stress_Color
                            Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawResultValues = True Then
                                Gr0.DrawString(tMemberRlts.Axial_stress_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point))
                            End If
                        ElseIf MDIMain.AxialStrainToolStripMenuItem.Checked = True Then
                            '---- Axial Strain Contour
                            Elem_Pen.Color = tMemberRlts.Strain_Color
                            Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
                            If MDIMain.Is_DrawResultValues = True Then
                                Gr0.DrawString(tMemberRlts.Axial_strain_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point))
                            End If
                        End If
                    End If
                Else
                    Elem_Pen.Color = Color.DarkBlue
                    Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)

                    If MDIMain.Is_DrawElemLength = True Then
                        Gr0.DrawString(ELength_Str, New Font("Verdana", 10), Elem_Pen.Brush, Return_Mid_Pt(tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point))
                    End If

                End If

            End Using
            Return False
        End Function

        Public Function Paint_Highlight_Me(ByRef Gr0 As System.Drawing.Graphics)
            Using Elem_Pen As New Pen(Color.Empty, 5)
                Elem_Pen.Color = Color.Cyan
                Elem_Pen.DashStyle = DashStyle.Dash
                Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
            End Using
            Return False
        End Function

        Public Function Paint_SelectedHighlight_Me(ByRef Gr0 As System.Drawing.Graphics)
            Using Elem_Pen As New Pen(Color.Empty, 5)
                Elem_Pen.Color = Color.Cyan
                Gr0.DrawLine(Elem_Pen, tSN.N_Coord.Sc_Scaled_point, tEN.N_Coord.Sc_Scaled_point)
            End Using
            Return False
        End Function

        ''' <summary>
        ''' Structure to Store Memeber Results
        ''' </summary>
        ''' <remarks></remarks>
        <Serializable()> _
        Public Structure Member_Results
            Public Axial_force As Double
            Public Axial_stress As Double
            Public Axial_strain As Double

            Public ReadOnly Property Axial_force_Str() As String
                Get
                    Return If(Math.Round(Axial_force, MDIMain.Round_offVal) <> 0, Math.Round(Axial_force, MDIMain.Round_offVal).ToString, _
                                                                               If(Math.Round(Axial_force, 12) <> 0, (Axial_force.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Axial_stress_Str() As String
                Get
                    Return If(Math.Round(Axial_stress, MDIMain.Round_offVal) <> 0, Math.Round(Axial_stress, MDIMain.Round_offVal).ToString, _
                                                                              If(Math.Round(Axial_stress, 12) <> 0, (Axial_stress.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Axial_strain_Str() As String
                Get
                    Return If(Math.Round(Axial_strain, MDIMain.Round_offVal) <> 0, Math.Round(Axial_strain, MDIMain.Round_offVal).ToString, _
                                                                              If(Math.Round(Axial_strain, 12) <> 0, (Axial_strain.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public Force_Color As Color
            Public Stress_Color As Color
            Public Strain_Color As Color

            Public Sub New(ByRef tAForce As Double, ByRef tAForce_maxRatio As Double, _
                           ByRef tAStress As Double, ByRef tAStress_maxRatio As Double, _
                           ByRef tAStrain As Double, ByRef tAStrain_maxRatio As Double)
                Axial_force = tAForce
                Axial_stress = tAStress
                Axial_strain = tAStrain

                Force_Color = HSLToRGB(240, (1 - tAForce_maxRatio) * 240, 1, HSLLight)
                Stress_Color = HSLToRGB(240, (1 - tAStress_maxRatio) * 240, 1, HSLLight)
                Strain_Color = HSLToRGB(240, (1 - tAStrain_maxRatio) * 240, 1, HSLLight)
            End Sub
        End Structure
    End Class

    <Serializable()> _
    Public Class Constraints_Store
        Private tNode_Constraint As New XYZ_DOF
        Private tNode_Force As New XYZ_Force
        Private tNode_Displacements As New XYZ_Displacement
        Private tNode_Reacton As New XYZ_Force

        Public WriteOnly Property Update_NodeForce() As XYZ_Force
            Set(ByVal value As XYZ_Force)
                tNode_Force = value
            End Set
        End Property

        Public WriteOnly Property Update_NodeConstraint() As XYZ_DOF
            Set(ByVal value As XYZ_DOF)
                tNode_Constraint = value
            End Set
        End Property

        Public WriteOnly Property Update_NodeDisplacement() As XYZ_Displacement
            Set(ByVal value As XYZ_Displacement)
                tNode_Displacements = value
            End Set
        End Property

        Public WriteOnly Property Update_NodeReactionForce() As XYZ_Force
            Set(ByVal value As XYZ_Force)
                tNode_Reacton = value
            End Set
        End Property

        Public ReadOnly Property Node_Constraint() As XYZ_DOF
            Get
                Return tNode_Constraint
            End Get
        End Property

        Public ReadOnly Property Node_Force() As XYZ_Force
            Get
                Return tNode_Force
            End Get
        End Property

        Public ReadOnly Property Node_Displacement() As XYZ_Displacement
            Get
                Return tNode_Displacements
            End Get
        End Property

        Public ReadOnly Property Node_Reactions() As XYZ_Force
            Get
                Return tNode_Reacton
            End Get
        End Property

        Public Sub New()

        End Sub

        ''' <summary>
        ''' Structure to store the Degree of Freedom of the Node (True = Fixed)
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        <Serializable()> _
        Public Structure XYZ_DOF
            Public is_X_fixed As Boolean
            Public is_Y_fixed As Boolean
            Public is_Z_fixed As Boolean

            Public Sub New(ByVal is_XFix As Boolean, ByVal is_YFix As Boolean, ByVal is_ZFix As Boolean)
                is_X_fixed = is_XFix
                is_Y_fixed = is_YFix
                is_Z_fixed = is_ZFix
            End Sub
        End Structure

        ''' <summary>
        ''' Structure to store the Nodal forces in Global coordinate
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        <Serializable()> _
        Public Structure XYZ_Force
            Public X_force As Double
            Public Y_force As Double
            Public Z_force As Double

            Public ReadOnly Property X_Reac_Str() As String
                Get
                    Return If(Math.Round(X_force, MDIMain.Round_offVal) <> 0, Math.Round(Math.Abs(X_force), MDIMain.Round_offVal).ToString, _
                                                                              If(Math.Round(X_force, 12) <> 0, (X_force.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Y_Reac_Str() As String
                Get
                    Return If(Math.Round(Y_force, MDIMain.Round_offVal) <> 0, Math.Round(Math.Abs(Y_force), MDIMain.Round_offVal).ToString, _
                                                                              If(Math.Round(Y_force, 12) <> 0, (Y_force.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Z_Reac_Str() As String
                Get

                    Return If(Math.Round(Z_force, MDIMain.Round_offVal) <> 0, Math.Round(Math.Abs(Z_force), MDIMain.Round_offVal).ToString, _
                                                                              If(Math.Round(Z_force, 12) <> 0, (Z_force.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property


            Public Sub New(ByVal i_xF As Double, ByVal i_yF As Double, ByVal i_zF As Double)
                X_force = i_xF
                Y_force = i_yF
                Z_force = i_zF
            End Sub
        End Structure

        ''' <summary>
        ''' Structure to store the Nodal Displacements in Global coordinate
        ''' </summary>
        ''' <remarks></remarks>
        ''' 
        <Serializable()> _
        Public Structure XYZ_Displacement
            Public X_Disp As Double
            Public Y_Disp As Double
            Public Z_Disp As Double

            Public ReadOnly Property X_Disp_Str() As String
                Get
                    Return If(Math.Round(X_Disp, MDIMain.Round_offVal) <> 0, Math.Round(X_Disp, MDIMain.Round_offVal).ToString, _
                                                                             If(Math.Round(X_Disp, 12) <> 0, (X_Disp.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Y_Disp_Str() As String
                Get
                    Return If(Math.Round(Y_Disp, MDIMain.Round_offVal) <> 0, Math.Round(Y_Disp, MDIMain.Round_offVal).ToString, _
                                                                             If(Math.Round(Y_Disp, 12) <> 0, (Y_Disp.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Z_Disp_Str() As String
                Get
                    Return If(Math.Round(Z_Disp, MDIMain.Round_offVal) <> 0, Math.Round(Z_Disp, MDIMain.Round_offVal).ToString, _
                                                                             If(Math.Round(Z_Disp, 12) <> 0, (Z_Disp.ToString("e" & MDIMain.Round_offVal)), "0"))
                End Get
            End Property

            Public ReadOnly Property Disp_Str() As String
                Get
                    Return "(" & X_Disp_Str & ", " & Y_Disp_Str & ", " & Z_Disp_Str & ")"
                End Get
            End Property

            Public Sub New(ByVal i_xD As Double, ByVal i_yD As Double, ByVal i_zD As Double)
                X_Disp = i_xD
                Y_Disp = i_yD
                Z_Disp = i_zD
            End Sub
        End Structure
    End Class
End Class
