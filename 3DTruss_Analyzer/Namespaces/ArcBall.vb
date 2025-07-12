Imports System
Imports System.Drawing

Namespace ArcBall
    '/*
    '* This code is a part of NeHe tutorials and was converted from C++ into C# then to VB.Net. 
    '* Matrix/Vector manipulations have been updated.
    '*/
    Public Class ArcBall
        Private Const Epsilon As Single = 0.00001F

        Private StVec As Vector3f
        'Saved click vector
        Private EnVec As Vector3f
        'Saved drag vector
        Private adjustWidth As Single
        'Mouse bounds width
        Private adjustHeight As Single
        'Mouse bounds height
        Public Sub New(ByVal NewWidth As Single, ByVal NewHeight As Single)
            StVec = New Vector3f()
            EnVec = New Vector3f()
            setBounds(NewWidth, NewHeight)
        End Sub

        Private Sub mapToSphere(ByVal point As Point, ByVal vector As Vector3f)
            Dim tempPoint As New Point2f(point.X, point.Y)

            'Adjust point coords and scale down to range of [-1 ... 1]
            'tempPoint.x = (tempPoint.x * Me.adjustWidth) - 1.0F
            tempPoint.x = 1.0F - (tempPoint.x * Me.adjustWidth)
            tempPoint.y = 1.0F - (tempPoint.y * Me.adjustHeight)
            'tempPoint.y = (tempPoint.y * Me.adjustHeight) - 1.0F

            'Compute square of the length of the vector from this point to the center
            Dim length As Single = (tempPoint.x * tempPoint.x) + (tempPoint.y * tempPoint.y)

            'If the point is mapped outside the sphere... (length > radius squared)
            If length > 1.0F Then
                'Compute a normalizing factor (radius / sqrt(length))
                Dim norm As Single = CSng(1.0 / Math.Sqrt(length))

                'Return the "normalized" vector, a point on the sphere
                vector.x = tempPoint.x * norm
                vector.y = tempPoint.y * norm
                vector.z = 0.0F
            Else
                'Else it's inside
                'Return a vector to a point mapped inside the sphere sqrt(radius squared - length)
                vector.x = tempPoint.x
                vector.y = tempPoint.y
                vector.z = CSng(System.Math.Sqrt(1.0F - length))
            End If
        End Sub

        Public Sub setBounds(ByVal NewWidth As Single, ByVal NewHeight As Single)
            'Set adjustment factor for width/height
            adjustWidth = 1.0F / ((NewWidth - 1.0F) * 0.5F)
            adjustHeight = 1.0F / ((NewHeight - 1.0F) * 0.5F)
        End Sub

        'Mouse down
        Public Overridable Sub click(ByVal NewPt As Point)
            mapToSphere(NewPt, Me.StVec)
        End Sub

        'Mouse drag, calculate rotation
        Public Sub drag(ByVal NewPt As Point, ByVal NewRot As Quat4f)
            'Map the point to the sphere
            Me.mapToSphere(NewPt, EnVec)

            'Return the quaternion equivalent to the rotation
            If NewRot IsNot Nothing Then
                Dim Perp As New Vector3f()

                'Compute the vector perpendicular to the begin and end vectors
                Vector3f.cross(Perp, StVec, EnVec)

                'Compute the length of the perpendicular vector
                If Perp.length() > Epsilon Then
                    'if its non-zero
                    'We're ok, so return the perpendicular vector as the transform after all
                    NewRot.x = Perp.x
                    NewRot.y = Perp.y
                    NewRot.z = Perp.z
                    'In the quaternion values, w is cosine (theta / 2), where theta is the rotation angle
                    NewRot.w = Vector3f.dot(StVec, EnVec)
                Else
                    'if it is zero
                    'The begin and end vectors coincide, so return an identity transform
                    NewRot.x = 0.0
                    NewRot.y = 0.0
                    NewRot.z = 0.0
                    NewRot.w = 0.0
                End If
            End If
        End Sub
    End Class

    Public Class Matrix4f
        Private M As Double(,)

        Public ReadOnly Property Rotation_M() As Double(,)
            Get
                Return M
            End Get
        End Property

        Public Sub New(ByVal Vt As Integer)
            SetIdentity(Vt)
        End Sub

        Public Sub SetIdentity(ByVal viewtype As Integer)
            Me.M = New Double(3, 3) {}
            If viewtype = 0 Then
                ' set to zero
                For i As Integer = 0 To 3
                    Me.M(i, i) = 1.0F
                Next
                Me.M(0, 0) = 1
                Me.M(0, 1) = 0
                Me.M(0, 2) = 0

                Me.M(1, 0) = 0
                Me.M(1, 1) = 1
                Me.M(1, 2) = 0

                Me.M(2, 0) = 0
                Me.M(2, 1) = 0
                Me.M(2, 2) = 1

            ElseIf viewtype = 1 Then
                '---- Set Top View
                Me.M(0, 0) = 1
                Me.M(0, 1) = 0
                Me.M(0, 2) = 0

                Me.M(1, 0) = 0
                Me.M(1, 1) = -1
                Me.M(1, 2) = 0

                Me.M(2, 0) = 0
                Me.M(2, 1) = 0
                Me.M(2, 2) = -1
            ElseIf viewtype = 2 Then
                '---- Set Side View
                Me.M(0, 0) = 0
                Me.M(0, 1) = 1
                Me.M(0, 2) = 0

                Me.M(1, 0) = 0
                Me.M(1, 1) = 0
                Me.M(1, 2) = -1

                Me.M(2, 0) = -1
                Me.M(2, 1) = 0
                Me.M(2, 2) = 0
            ElseIf viewtype = 3 Then
                '---- Set Front View
                Me.M(0, 0) = 1
                Me.M(0, 1) = 0
                Me.M(0, 2) = 0

                Me.M(1, 0) = 0
                Me.M(1, 1) = 0
                Me.M(1, 2) = -1

                Me.M(2, 0) = 0
                Me.M(2, 1) = 1
                Me.M(2, 2) = 0
            ElseIf viewtype = 4 Then
                '---- Set Isometric View 1
                Me.M(0, 0) = 0.67969810962677
                Me.M(0, 1) = 0.718355774879456
                Me.M(0, 2) = -0.148240596055985

                Me.M(1, 0) = 0.199809849262238
                Me.M(1, 1) = -0.375794619321823
                Me.M(1, 2) = -0.904905796051025

                Me.M(2, 0) = -0.705752372741699
                Me.M(2, 1) = 0.585442781448364
                Me.M(2, 2) = -0.398961365222931
            ElseIf viewtype = 5 Then
                '---- Set Isometric View 2
                Me.M(0, 0) = -0.665954828262329
                Me.M(0, 1) = 0.745265483856201
                Me.M(0, 2) = -0.0329171679913998

                Me.M(1, 0) = 0.497498512268066
                Me.M(1, 1) = 0.41080915927887
                Me.M(1, 2) = -0.764022886753082

                Me.M(2, 0) = -0.555877268314362
                Me.M(2, 1) = -0.525181233882904
                Me.M(2, 2) = -0.644348919391632
            End If

        End Sub

        Public Sub set_Renamed(ByVal m1 As Matrix4f)
            Me.M = m1.M
        End Sub

        Public Sub MatrixMultiply(ByVal m1 As Matrix4f, ByVal m2 As Matrix4f)
            Dim MulMat As Single() = New Single(15) {}
            Dim elMat As Single = 0.0F
            Dim k As Integer = 0

            For i As Integer = 0 To 3
                For j As Integer = 0 To 3
                    For l As Integer = 0 To 3
                        elMat += m1.M(i, l) * m2.M(l, j)
                    Next
                    MulMat(k) = elMat
                    elMat = 0.0F
                    k += 1
                Next
            Next

            k = 0
            For i As Integer = 0 To 3
                For j As Integer = 0 To 3
                    m1.M(i, j) = MulMat(k)
                    k += 1
                Next
            Next
        End Sub

        Public WriteOnly Property Rotation() As Quat4f
            Set(ByVal value As Quat4f)
                Dim n As Single, s As Single
                Dim xs As Single, ys As Single, zs As Single
                Dim wx As Single, wy As Single, wz As Single
                Dim xx As Single, xy As Single, xz As Single
                Dim yy As Single, yz As Single, zz As Single

                M = New Double(3, 3) {}

                n = (value.x * value.x) + (value.y * value.y) + (value.z * value.z) + (value.w * value.w)
                s = If((n > 0.0F), 2.0F / n, 0.0F)

                xs = value.x * s
                ys = value.y * s
                zs = value.z * s
                wx = value.w * xs
                wy = value.w * ys
                wz = value.w * zs
                xx = value.x * xs
                xy = value.x * ys
                xz = value.x * zs
                yy = value.y * ys
                yz = value.y * zs
                zz = value.z * zs

                ' rotation
                M(0, 0) = 1.0F - (yy + zz)
                M(0, 1) = xy - wz
                M(0, 2) = xz + wy

                M(1, 0) = xy + wz
                M(1, 1) = 1.0F - (xx + zz)
                M(1, 2) = yz - wx

                M(2, 0) = xz - wy
                M(2, 1) = yz + wx
                M(2, 2) = 1.0F - (xx + yy)

                M(3, 3) = 1.0F

            End Set
        End Property
    End Class

    Public Class Point2f
        Public x As Single, y As Single

        Public Sub New(ByVal x As Single, ByVal y As Single)
            Me.x = x
            Me.y = y
        End Sub
    End Class

    Public Class Quat4f
        Public x As Single, y As Single, z As Single, w As Single
    End Class

    Public Class Vector3f
        Public x As Single, y As Single, z As Single

        ''' <summary>
        ''' Constructor
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Constructor - overload 1
        ''' </summary>
        ''' <param name="X__1">x coord</param>
        ''' <param name="Y__2">y coord</param>
        ''' <param name="Z__3">z coord</param>
        Public Sub New(ByVal X__1 As Single, ByVal Y__2 As Single, ByVal Z__3 As Single)
            x = X__1
            y = Y__2
            z = Z__3
        End Sub

        ''' <summary>
        ''' Cross Product of Two Vectors.
        ''' </summary>
        ''' <param name="Result">Resultant Vector</param>
        ''' <param name="v1">Vector 1</param>
        ''' <param name="v2">Vector 2</param>
        Public Shared Sub cross(ByVal Result As Vector3f, ByVal v1 As Vector3f, ByVal v2 As Vector3f)
            Result.x = (v1.y * v2.z) - (v1.z * v2.y)
            Result.y = (v1.z * v2.x) - (v1.x * v2.z)
            Result.z = (v1.x * v2.y) - (v1.y * v2.x)
        End Sub

        ''' <summary>
        ''' Dot Product of Two Vectors.
        ''' </summary>
        ''' <param name="v1">Vector 1</param>
        ''' <param name="v2">Vector 2</param>
        ''' <returns></returns>
        Public Shared Function dot(ByVal v1 As Vector3f, ByVal v2 As Vector3f) As Single
            Return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z + v2.z)
        End Function

        ''' <summary>
        ''' Magnitude of the Vector
        ''' </summary>
        ''' <returns></returns>
        Public Function length() As Single
            Return CSng(System.Math.Sqrt(x * x + y * y + z * z))
        End Function
    End Class
End Namespace
