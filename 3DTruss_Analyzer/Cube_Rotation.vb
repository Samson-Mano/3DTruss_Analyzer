Imports _3DTruss_Analyzer.ArcBall
Imports System.Drawing.Drawing2D

Public Class Cube_Rotation
    ' Rotation Variables
    Private matrixLock As New System.Object()
    Private arcBall As ArcBall.ArcBall
    Private LastTransformation As New Matrix4f(4)
    Private Shared ThisTransformation As New Matrix4f(4)
    Private P_the_cube As The_CubeDraw
    Private Shared R_Matrix(,) As Double
    Private Shared sc_val As Double '--- This value determines how much unit is available in 100% Zoom view

    Public ReadOnly Property The_Cube() As The_CubeDraw
        Get
            Return P_the_cube
        End Get
    End Property

    Public Shared ReadOnly Property My_Transformation() As Matrix4f
        Get
            Return ThisTransformation
        End Get
    End Property

    Public WriteOnly Property _UpdateRotation() As Double(,)
        Set(ByVal value As Double(,))
            ReDim R_Matrix(2, 2)

            R_Matrix(0, 0) = value(0, 0)
            R_Matrix(1, 0) = value(1, 0)
            R_Matrix(2, 0) = value(2, 0)

            R_Matrix(0, 1) = value(0, 1)
            R_Matrix(1, 1) = value(1, 1)
            R_Matrix(2, 1) = value(2, 1)

            R_Matrix(0, 2) = value(0, 2)
            R_Matrix(1, 2) = value(1, 2)
            R_Matrix(2, 2) = value(2, 2)
        End Set
    End Property

    Public ReadOnly Property Sc_val4() As Double '--- Used onle 4 times (1 time in Option form, 3 times in x,y,z grid)
        Get
            Return (800 / sc_val)
        End Get
    End Property

    Public WriteOnly Property _UpdateScale() As Double
        Set(ByVal value As Double)
            sc_val = value
        End Set
    End Property

    <Serializable()> _
    Public Class ThreeD_Point
        Protected m_x As Double, m_y As Double, m_z As Double

        Public Sub New(ByVal x As Double, ByVal y As Double, ByVal z As Double)
            Me.X = x
            Me.Y = y
            Me.Z = z
        End Sub

        Public Property X() As Double
            Get
                Return m_x
            End Get
            Set(ByVal value As Double)
                m_x = value
            End Set
        End Property

        Public Property Y() As Double
            Get
                Return m_y
            End Get
            Set(ByVal value As Double)
                m_y = value
            End Set
        End Property

        Public Property Z() As Double
            Get
                Return m_z
            End Get
            Set(ByVal value As Double)
                m_z = value
            End Set
        End Property

        Public ReadOnly Property Sc_Point() As Point
            Get
                Return New Point(CInt((R_Matrix(0, 0) * Me.X) + (R_Matrix(0, 1) * Me.Y) + (R_Matrix(0, 2) * Me.Z)), _
                                 CInt((R_Matrix(1, 0) * Me.X) + (R_Matrix(1, 1) * Me.Y) + (R_Matrix(1, 2) * Me.Z)))
            End Get
        End Property

        Public ReadOnly Property Sc_Scaled_point() As Point
            Get
                Return New Point(CInt((R_Matrix(0, 0) * sc_val * Me.X) + (R_Matrix(0, 1) * sc_val * Me.Y) + (R_Matrix(0, 2) * sc_val * Me.Z)), _
                                 CInt((R_Matrix(1, 0) * sc_val * Me.X) + (R_Matrix(1, 1) * sc_val * Me.Y) + (R_Matrix(1, 2) * sc_val * Me.Z)))

            End Get
        End Property

        Public Function Special_Roataion(ByVal Rot_matrix(,) As Double) As ThreeD_Point
            Dim spl_x, spl_y, spl_z As Double

            spl_x = Rot_matrix(0, 0) * Me.X + Rot_matrix(0, 1) * Me.Y + Rot_matrix(0, 2) * Me.Z
            spl_y = Rot_matrix(1, 0) * Me.X + Rot_matrix(1, 1) * Me.Y + Rot_matrix(1, 2) * Me.Z
            spl_z = Rot_matrix(2, 0) * Me.X + Rot_matrix(2, 1) * Me.Y + Rot_matrix(2, 2) * Me.Z

            Return New ThreeD_Point(spl_x, spl_y, spl_z)
        End Function

        Public Function Project(ByVal viewWidth, ByVal viewHeight, ByVal fov, ByVal viewDistance)
            Dim factor As Double, Xn As Double, Yn As Double
            factor = fov / (viewDistance + Me.Z)
            Xn = Me.X * factor + viewWidth / 2
            Yn = Me.Y * factor + viewHeight / 2


            Return New ThreeD_Point(Xn, Yn, Me.Z)
        End Function
    End Class

#Region "Rotation Control"
    Public Sub startRotationDrag(ByVal MousePt As Point)
        SyncLock matrixLock
            ' Set Last Static Rotation To Last Dynamic One
            LastTransformation.set_Renamed(ThisTransformation)
        End SyncLock
        arcBall.click(MousePt)
        ' Update Start Vector And Prepare For Dragging
        'RotateStartDrag = MousePt

    End Sub

    Public Sub Rotationdrag(ByVal MousePt As Point)
        Dim ThisQuat As New Quat4f()

        arcBall.drag(MousePt, ThisQuat)
        ' Update End Vector And Get Rotation As Quaternion
        SyncLock matrixLock
            'rotate
            ThisTransformation.Rotation = ThisQuat
            ThisTransformation.MatrixMultiply(ThisTransformation, LastTransformation)
        End SyncLock
    End Sub
#End Region

    Public Sub Update_Arcball_InResize(ByVal MPWidth As Integer, ByVal MPHeight As Integer)
        arcBall = New ArcBall.ArcBall(MPWidth, MPHeight) '--- Update ArcBall due to Resize of the Main_Pic
    End Sub

    Public Sub set_pre_RotationMatrix(ByVal Pd_Rotation As Integer)
        LastTransformation = New Matrix4f(Pd_Rotation)
        ThisTransformation = New Matrix4f(Pd_Rotation)
        Update_Rotation_Matrix()
    End Sub

    Public Sub Update_Rotation_Matrix()
        _UpdateRotation = ThisTransformation.Rotation_M '--- Here is where the matrix update is happening
    End Sub

    Public Sub New(ByVal MPWidth As Integer, ByVal MPHeight As Integer)
        Update_Arcball_InResize(MPWidth, MPHeight) '--- Load ArcBall
        P_the_cube = New The_CubeDraw '--- For Drawing the cube
        Update_Rotation_Matrix()
        _UpdateScale = 800 / 10 '-- 800 is the extreme boundary and 10 is a arbitrary value
        set_pre_RotationMatrix(4) '-- Intially set as isometric view 1
    End Sub
End Class

Public Class The_CubeDraw
    Dim Cube_Vertices As List(Of Cube_Rotation.ThreeD_Point)
    Dim Cube_Faces(5) As List(Of Integer)
    Dim Cube_Colors As List(Of Color)
    Dim Cube_face_brush As List(Of Brush)

    Public Sub New()
        Dim side_a As Integer = 10
        Dim orgin_shift As Integer = -80
        '---- Cube Veritces
        Cube_Vertices = New List(Of Cube_Rotation.ThreeD_Point)
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(-side_a + orgin_shift, side_a + orgin_shift, -side_a))
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(side_a + orgin_shift, side_a + orgin_shift, -side_a))
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(side_a + orgin_shift, -side_a + orgin_shift, -side_a))
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(-side_a + orgin_shift, -side_a + orgin_shift, -side_a))

        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(-side_a + orgin_shift, side_a + orgin_shift, side_a))
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(side_a + orgin_shift, side_a + orgin_shift, side_a))
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(side_a + orgin_shift, -side_a + orgin_shift, side_a))
        Cube_Vertices.Add(New Cube_Rotation.ThreeD_Point(-side_a + orgin_shift, -side_a + orgin_shift, side_a))

        '----- Cube Faces
        For i As Integer = 0 To 5 Step +1
            Cube_Faces(i) = New List(Of Integer)
        Next
        Cube_Faces(0).AddRange(New Integer() {0, 1, 2, 3})
        Cube_Faces(1).AddRange(New Integer() {1, 5, 6, 2})
        Cube_Faces(2).AddRange(New Integer() {5, 4, 7, 6})
        Cube_Faces(3).AddRange(New Integer() {4, 0, 3, 7})
        Cube_Faces(4).AddRange(New Integer() {0, 4, 5, 1})
        Cube_Faces(5).AddRange(New Integer() {3, 2, 6, 7})

        '---- Cube Colors
        Cube_Colors = New List(Of Color)
        Cube_Colors.Add(Color.Magenta)
        Cube_Colors.Add(Color.Red)
        Cube_Colors.Add(Color.Green)
        Cube_Colors.Add(Color.Cyan)
        Cube_Colors.Add(Color.Blue)
        Cube_Colors.Add(Color.Yellow)

        '---- Face is painted with brush
        Cube_face_brush = New List(Of Brush)
        For Each Clr In Cube_Colors
            Cube_face_brush.Add(New SolidBrush(Clr))
        Next
    End Sub

    Public Sub Draw_Cube(ByVal Gr0 As Graphics)
        Gr0.SmoothingMode = SmoothingMode.AntiAlias

        '--- Zero line
        If MDIMain.Is_DrawZeroPt = True Then
            Gr0.DrawLine(Pens.Black, 0, 0 - 5, 0, 0 + 5)
            Gr0.DrawLine(Pens.Black, 0 - 5, 0, 0 + 5, 0)
            Gr0.DrawString("(0,0,0)", New Font("Verdana", 10), Brushes.DarkSlateGray, 0, 0)
        End If

        'Gr0.DoubleBuffered = True

        '---- X Y and Z Axis
        Dim orgin_shift As Integer = -80
        Dim RS_Pt As New Cube_Rotation.ThreeD_Point(orgin_shift, orgin_shift, 0)

        Dim RE_Pt As New Cube_Rotation.ThreeD_Point(100, orgin_shift, 0)
        Dim BE_Pt As New Cube_Rotation.ThreeD_Point(orgin_shift, 100, 0)
        Dim GE_Pt As New Cube_Rotation.ThreeD_Point(orgin_shift, orgin_shift, 100 + (-orgin_shift))

        Using EndCapPath As New GraphicsPath
            EndCapPath.AddLine(0, 0, -2, -2) '-- create cap
            EndCapPath.AddLine(0, 0, 2, -2)
            Using End_cap As New CustomLineCap(Nothing, EndCapPath)
                Using Grd_pen As New Pen(Color.Empty, 2)
                    Grd_pen.CustomEndCap = End_cap '--- set cap here
                    Grd_pen.Color = Color.Red '-- X color is red
                    Gr0.DrawLine(Grd_pen, RS_Pt.Sc_Point, RE_Pt.Sc_Point)
                    Gr0.DrawString("X", New Font("Verdana", 10), Grd_pen.Brush, RE_Pt.Sc_Point.X, RE_Pt.Sc_Point.Y)
                    Grd_pen.Color = Color.Blue '-- Y color is blue
                    Gr0.DrawLine(Grd_pen, RS_Pt.Sc_Point, BE_Pt.Sc_Point)
                    Gr0.DrawString("Y", New Font("Verdana", 10), Grd_pen.Brush, BE_Pt.Sc_Point.X, BE_Pt.Sc_Point.Y)
                    Grd_pen.Color = Color.Green '-- Z color is green
                    Gr0.DrawLine(Grd_pen, RS_Pt.Sc_Point, GE_Pt.Sc_Point)
                    Gr0.DrawString("Z", New Font("Verdana", 10), Grd_pen.Brush, GE_Pt.Sc_Point.X, GE_Pt.Sc_Point.Y)
                End Using
            End Using
        End Using






        '----- NOT from ME
        '----- Code from Ralf Meier (Germany) from  codeproject.com
        Dim t(8) As Cube_Rotation.ThreeD_Point
        Dim f(4) As Integer
        Dim v As Cube_Rotation.ThreeD_Point
        Dim avgZ(6) As Double
        Dim order(6) As Integer
        Dim tmp As Double
        Dim iMax As Integer

        ' Clear the window
        'Gr0.Clear(Color.LightBlue)
        'Dim TRACKBSize As Integer = 300
        'Gr0.DrawEllipse(Pens.OrangeRed, 0 - CInt(TRACKBSize / 2), 0 - CInt(TRACKBSize / 2), TRACKBSize, TRACKBSize)

        ' Transform all the points and store them on the "t" array.
        For i = 0 To 7
            Dim b As Brush = New SolidBrush(Color.White)
            v = Cube_Vertices(i)
            't(i) = v.RotateX(xAngle).RotateY(yAngle).RotateZ(zAngle)
            't(i) = v.Special_Roataion(CTrackBall.Track_RotationMatrix) '' This is where the magic is happening
            t(i) = v.Special_Roataion(Cube_Rotation.My_Transformation.Rotation_M)
            't(i) = t(i).Project(Main_Pic.Width, Main_Pic.Height, 256, 4)
        Next

        ' Compute the average Z value of each face.
        For i = 0 To 5
            avgZ(i) = (t(Cube_Faces(i)(0)).Z + t(Cube_Faces(i)(1)).Z + t(Cube_Faces(i)(2)).Z + t(Cube_Faces(i)(3)).Z) / 4.0
            order(i) = i
        Next

        ' Next we sort the faces in descending order based on the Z value.
        ' The objective is to draw distant faces first. This is called 
        ' the PAINTERS ALGORITHM. So, the visible faces will hide the invisible ones.
        ' The sorting algorithm used is the SELECTION SORT.
        For i = 0 To 4
            iMax = i
            For j = i + 1 To 5
                If avgZ(j) > avgZ(iMax) Then
                    iMax = j
                End If
            Next
            If iMax <> i Then
                tmp = avgZ(i)
                avgZ(i) = avgZ(iMax)
                avgZ(iMax) = tmp

                tmp = order(i)
                order(i) = order(iMax)
                order(iMax) = tmp
            End If
        Next

        ' Draw the faces using the PAINTERS ALGORITHM (distant faces first, closer faces last).
        For i = 0 To 5
            Dim points() As Point
            Dim index As Integer = order(i)
            points = New Point() { _
                New Point(CInt(t(Cube_Faces(index)(0)).X), CInt(t(Cube_Faces(index)(0)).Y)), _
                New Point(CInt(t(Cube_Faces(index)(1)).X), CInt(t(Cube_Faces(index)(1)).Y)), _
                New Point(CInt(t(Cube_Faces(index)(2)).X), CInt(t(Cube_Faces(index)(2)).Y)), _
                New Point(CInt(t(Cube_Faces(index)(3)).X), CInt(t(Cube_Faces(index)(3)).Y)) _
            }
            Gr0.FillPolygon(Cube_face_brush(index), points)
        Next


        '------------------------ Draw the Major Axis of the Grid lines
        If MDIMain.Is_DrawGrid = True Then
            Dim Spt As Cube_Rotation.ThreeD_Point
            Dim Ept As Cube_Rotation.ThreeD_Point
            Dim grid_Length As Integer = 10

            '---- X Grid
            Spt = New Cube_Rotation.ThreeD_Point(0, 0, 0)
            Ept = New Cube_Rotation.ThreeD_Point(800, 0, 0)
            Gr0.DrawLine(Pens.DarkGray, Spt.Sc_Point, Ept.Sc_Point)

            For i = 1 To grid_Length Step +1
                Spt = New Cube_Rotation.ThreeD_Point(0, (i * 80), 0)
                Ept = New Cube_Rotation.ThreeD_Point(800, (i * 80), 0)

                Gr0.DrawLine(Pens.DarkGray, Spt.Sc_Point, Ept.Sc_Point)
                Gr0.DrawString((i * (MDIMain.threeD_Control.Sc_val4 / grid_Length)), New Font("Verdana", 10), Brushes.DarkSlateGray, Spt.Sc_Point.X, Spt.Sc_Point.Y)
            Next

            '----- Y Grid
            Spt = New Cube_Rotation.ThreeD_Point(0, 0, 0)
            Ept = New Cube_Rotation.ThreeD_Point(0, 800, 0)
            Gr0.DrawLine(Pens.DarkGray, Spt.Sc_Point, Ept.Sc_Point)

            For i = 1 To grid_Length Step +1
                Spt = New Cube_Rotation.ThreeD_Point((i * 80), 0, 0)
                Ept = New Cube_Rotation.ThreeD_Point((i * 80), 800, 0)

                Gr0.DrawLine(Pens.DarkGray, Spt.Sc_Point, Ept.Sc_Point)
                Gr0.DrawString((i * (MDIMain.threeD_Control.Sc_val4 / grid_Length)), New Font("Verdana", 10), Brushes.DarkSlateGray, Spt.Sc_Point.X, Spt.Sc_Point.Y)
            Next

            '----- Z Grid
            Spt = New Cube_Rotation.ThreeD_Point(0, 0, 0)
            Ept = New Cube_Rotation.ThreeD_Point(0, 0, 800)
            Gr0.DrawLine(Pens.DarkGray, Spt.Sc_Point, Ept.Sc_Point)

            For i = 1 To grid_Length Step +1
                Spt = New Cube_Rotation.ThreeD_Point(0, 0, (i * 80))
                Ept = New Cube_Rotation.ThreeD_Point(10, 10, (i * 80))
                Gr0.DrawLine(Pens.DarkGray, Spt.Sc_Point, Ept.Sc_Point)

                Gr0.DrawString((i * (MDIMain.threeD_Control.Sc_val4 / grid_Length)), New Font("Verdana", 10), Brushes.DarkSlateGray, Spt.Sc_Point.X, Spt.Sc_Point.Y)
            Next

        End If
    End Sub
End Class
