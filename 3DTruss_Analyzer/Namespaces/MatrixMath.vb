Imports System.Collections.Generic
Imports System.Text

Namespace MatrixMath
    Public Class MatrixMath
        Private mInnerMatrix As Double(,)

        Private mRowCount As Integer = 0
        Public ReadOnly Property RowCount() As Integer
            Get
                Return mRowCount
            End Get
        End Property

        Private mColumnCount As Integer = 0
        Public ReadOnly Property ColumnCount() As Integer
            Get
                Return mColumnCount
            End Get
        End Property

        Public Sub New()
        End Sub

        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="rowCount">Row count - Integer (starting with 1 ie., for 3x3 matrix the value of row count is 3)</param>
        ''' <param name="columnCount">Column count - Integer (starting with 1 ie., for 3x3 matrix the value of column count is 3)</param>
        Public Sub New(ByVal rowCount As Integer, ByVal columnCount As Integer) '---- only here use count number not index
            mRowCount = rowCount
            mColumnCount = columnCount
            mInnerMatrix = New Double(rowCount - 1, columnCount - 1) {}
        End Sub

        ''' <summary>
        ''' Item of the matrix
        ''' </summary>
        ''' <param name="rowNumber">Row index - Integer (starting with 0)</param>
        ''' <param name="columnNumber">Column index - Integer (starting with 0)</param>
        ''' <returns>
        ''' Returns individual item of the matrix (Double)
        ''' </returns>
        Default Public Property Item(ByVal rowNumber As Integer, ByVal columnNumber As Integer) As Double
            Get
                Return mInnerMatrix(rowNumber, columnNumber)
            End Get
            Set(ByVal value As Double)
                mInnerMatrix(rowNumber, columnNumber) = value
            End Set
        End Property

        ''' <summary>
        ''' Returns any single row of the matrix
        ''' </summary>
        ''' <param name="rowIndex">Row index - Integer (starting with 0 ie., to get row 3 of 4x4 matrix the input should be 2)</param>
        ''' <returns>
        ''' Returns a row of the matrix as an array of Double
        ''' </returns>
        Public Function GetRow(ByVal rowIndex As Integer) As Double()
            Dim rowValues As Double() = New Double(mColumnCount - 1) {}
            For i As Integer = 0 To mColumnCount - 1
                rowValues(i) = mInnerMatrix(rowIndex, i)
            Next
            Return rowValues
        End Function

        ''' <summary>
        ''' Sets any single row of the matrix
        ''' </summary>
        ''' <param name="rowIndex">Row index - Integer (starting with 0 ie., to set row 3 of 4x4 matrix the input should be 2)</param>
        ''' <param name="value">Values as array of double of size equals to column count</param>
        Public Sub SetRow(ByVal rowIndex As Integer, ByVal value As Double())
            If value.Length <> mColumnCount Then
                Throw New Exception("Shape Mismatch")
            End If
            For i As Integer = 0 To value.Length - 1
                mInnerMatrix(rowIndex, i) = value(i)
            Next
        End Sub

        ''' <summary>
        ''' Returns any single column of the matrix
        ''' </summary>
        ''' <param name="columnIndex">Column index - Integer (starting with 0 ie., to get column 3 of 4x4 matrix the input should be 2)</param>
        ''' <returns>
        ''' Returns a column of the matrix as an array of Double
        ''' </returns>
        Public Function GetColumn(ByVal columnIndex As Integer) As Double()
            Dim columnValues As Double() = New Double(mRowCount - 1) {}
            For i As Integer = 0 To mRowCount - 1
                columnValues(i) = mInnerMatrix(i, columnIndex)
            Next
            Return columnValues
        End Function

        ''' <summary>
        ''' Sets any single column of the matrix
        ''' </summary>
        ''' <param name="columnIndex">Column index - Integer (starting with 0 ie., to set column 3 of 4x4 matrix the input should be 2)</param>
        ''' <param name="value">Values as array of double of size equals to row count</param>
        Public Sub SetColumn(ByVal columnIndex As Integer, ByVal value As Double())
            If value.Length <> mRowCount Then
                Throw New Exception("Shape Mismatch")
            End If
            For i As Integer = 0 To value.Length - 1
                mInnerMatrix(i, columnIndex) = value(i)
            Next
        End Sub

        Public ReadOnly Property GetMatrixDouble() As Double(,)
            Get
                Return mInnerMatrix
            End Get
        End Property
        

        ''' <summary>
        ''' Adds two Matrix
        ''' </summary>
        ''' <param name="pMatrix1">Object1 of MatrixMath</param>
        ''' <param name="pMatrix2">Object2 of MatrixMath</param>
        ''' <returns>
        ''' Returns the sum of two matrix (object of Matrix math)
        ''' </returns>
        Public Shared Operator +(ByVal pMatrix1 As MatrixMath, ByVal pMatrix2 As MatrixMath) As MatrixMath
            If Not (pMatrix1.RowCount = pMatrix2.RowCount AndAlso pMatrix1.ColumnCount = pMatrix2.ColumnCount) Then
                Throw New Exception("Shape Mismatch")
            End If
            Dim returnMartix As New MatrixMath(pMatrix1.RowCount, pMatrix2.ColumnCount)
            For i As Integer = 0 To pMatrix1.RowCount - 1
                For j As Integer = 0 To pMatrix1.ColumnCount - 1
                    returnMartix(i, j) = pMatrix1(i, j) + pMatrix2(i, j)
                Next
            Next
            Return returnMartix
        End Operator

        ''' <summary>
        ''' Scalar Multiplication of Matrix
        ''' </summary>
        ''' <param name="scalarValue">Scalar value to scale the matrix</param>
        ''' <param name="pMatrix">Object of MatrixMath</param>
        ''' <returns>
        ''' Returns the scaled matrix (object of Matrix math)
        ''' </returns>
        Public Shared Operator *(ByVal scalarValue As Double, ByVal pMatrix As MatrixMath) As MatrixMath
            Dim returnMartix As New MatrixMath(pMatrix.RowCount, pMatrix.ColumnCount)
            For i As Integer = 0 To pMatrix.RowCount - 1
                For j As Integer = 0 To pMatrix.ColumnCount - 1
                    returnMartix(i, j) = pMatrix(i, j) * scalarValue
                Next
            Next
            Return returnMartix
        End Operator

        ''' <summary>
        ''' Subracts two Matrix
        ''' </summary>
        ''' <param name="pMatrix1">Object1 of MatrixMath</param>
        ''' <param name="pMatrix2">Object2 of MatrixMath</param>
        ''' <returns>
        ''' Returns the difference of two matrix (object of Matrix math)
        ''' </returns>
        Public Shared Operator -(ByVal pMatrix1 As MatrixMath, ByVal pMatrix2 As MatrixMath) As MatrixMath
            If Not (pMatrix1.RowCount = pMatrix2.RowCount AndAlso pMatrix1.ColumnCount = pMatrix2.ColumnCount) Then
                Throw New Exception("Shape Mismatch")
            End If
            Dim negative_pMatrix2 As New MatrixMath
            negative_pMatrix2 = (-1) * pMatrix2
            Return pMatrix1 + negative_pMatrix2
        End Operator

        ''' <summary>
        ''' Checks whether two Matrix is equal
        ''' </summary>
        ''' <param name="pMatrix1">Object1 of MatrixMath</param>
        ''' <param name="pMatrix2">Object2 of MatrixMath</param>
        ''' <returns>
        ''' Returns true if equal (False if not equal) (Boolean)
        ''' </returns>
        Public Shared Operator =(ByVal pMatrix1 As MatrixMath, ByVal pMatrix2 As MatrixMath) As Boolean
            If Not (pMatrix1.RowCount = pMatrix2.RowCount AndAlso pMatrix1.ColumnCount = pMatrix2.ColumnCount) Then
                'boyut uyusmazligi
                Return False
            End If
            For i As Integer = 0 To pMatrix1.RowCount - 1
                For j As Integer = 0 To pMatrix1.ColumnCount - 1
                    If pMatrix1(i, j) <> pMatrix2(i, j) Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Operator

        Public Shared Operator <>(ByVal pMatrix1 As MatrixMath, ByVal pMatrix2 As MatrixMath) As Boolean
            Return Not (pMatrix1 = pMatrix2)
        End Operator

        Public Shared Operator -(ByVal pMatrix As MatrixMath) As MatrixMath
            Return -1 * pMatrix
        End Operator

        ''' <summary>
        ''' Multiplication of two Matrix
        ''' </summary>
        ''' <param name="pMatrix1">Object1 of MatrixMath</param>
        ''' <param name="pMatrix2">Object2 of MatrixMath</param>
        ''' <returns>
        ''' Returns the multiplied matrix (object of Matrix math)
        ''' </returns>
        ''' <remarks>Usage
        ''' Standard Matrix multiplication example: [4 x 3] * [3 x 1 ] = [4 x 1]
        ''' Avoid shape mismatch error (column count of first matrix must be equal to row count of second matrix)
        ''' </remarks>
        Public Shared Operator *(ByVal pMatrix1 As MatrixMath, ByVal pMatrix2 As MatrixMath) As MatrixMath
            If pMatrix1.ColumnCount <> pMatrix2.RowCount Then
                Throw New Exception("Shape Mismatch")
            End If
            Dim returnMatrix As New MatrixMath(pMatrix1.RowCount, pMatrix2.ColumnCount)
            For i As Integer = 0 To pMatrix1.RowCount - 1
                Dim rowValues As Double() = pMatrix1.GetRow(i)
                For j As Integer = 0 To pMatrix2.ColumnCount - 1
                    Dim columnValues As Double() = pMatrix2.GetColumn(j)
                    Dim value As Double = 0
                    For a As Integer = 0 To rowValues.Length - 1
                        value = value + (rowValues(a) * columnValues(a))
                    Next
                    returnMatrix(i, j) = value
                Next
            Next
            Return returnMatrix
        End Operator

        ''' <summary>
        ''' Transpose of a Matrix
        ''' </summary>
        ''' <returns>
        ''' Returns the transpose of itself (object of Matrix math)
        ''' </returns>
        Public Function Transpose() As MatrixMath
            Dim mReturnMartix As New MatrixMath(ColumnCount, RowCount)
            For i As Integer = 0 To mRowCount - 1
                For j As Integer = 0 To mColumnCount - 1
                    mReturnMartix(j, i) = mInnerMatrix(i, j)
                Next
            Next
            Return mReturnMartix
        End Function

        ''' <summary>
        ''' returns true if itself is a zero Matrix (all entries are zero)
        ''' </summary>
        Public Function IsZeroMatrix() As Boolean
            For i As Integer = 0 To Me.RowCount - 1
                For j As Integer = 0 To Me.ColumnCount - 1
                    If mInnerMatrix(i, j) <> 0 Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function

        ''' <summary>
        ''' returns true if itself is a square Matrix (N x N matrix)
        ''' </summary>
        Public Function IsSquareMatrix() As Boolean
            Return (Me.RowCount = Me.ColumnCount)
        End Function

        ''' <summary>
        ''' returns true if itself is a lower triangular Matrix (all entries above the pricipal diagonal are zero)
        ''' </summary>
        Public Function IsLowerTriangle() As Boolean
            If Not Me.IsSquareMatrix() Then
                Return False
            End If
            For i As Integer = 0 To Me.RowCount - 1
                For j As Integer = i + 1 To Me.ColumnCount - 1
                    If mInnerMatrix(i, j) <> 0 Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function

        ''' <summary>
        ''' returns true if itself is a upper triangle Matrix (all entries below the pricipal diagonal are zero)
        ''' </summary>
        Public Function IsUpperTriangle() As Boolean
            If Not Me.IsSquareMatrix() Then
                Return False
            End If
            For i As Integer = 0 To Me.RowCount - 1
                For j As Integer = 0 To i - 1
                    If mInnerMatrix(i, j) <> 0 Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function

        ''' <summary>
        ''' returns true if itself is a Diagonal Matrix (A square matrix with principal diagonal are non-zero and all other elements are zero)
        ''' </summary>
        Public Function IsDiagonalMatrix() As Boolean
            If Not Me.IsSquareMatrix() Then
                Return False
            End If
            For i As Integer = 0 To Me.RowCount - 1
                For j As Integer = 0 To Me.ColumnCount - 1
                    If i <> j AndAlso mInnerMatrix(i, j) <> 0 Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function

        ''' <summary>
        ''' returns true if itself is a Identirty Matrix (A square matrix with principal diagonal are ones and all other elements are zero)
        ''' </summary>
        Public Function IsIdentityMatrix() As Boolean
            If Not Me.IsSquareMatrix() Then
                Return False
            End If
            For i As Integer = 0 To Me.RowCount - 1
                For j As Integer = 0 To Me.ColumnCount - 1
                    Dim checkValue As Double = 0
                    If i = j Then
                        checkValue = 1
                    End If
                    If mInnerMatrix(i, j) <> checkValue Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function

        ''' <summary>
        ''' returns true if itself is a Symmetric Matrix (A square matrix with entries symmetrical about main diagonal)
        ''' </summary>
        Public Function IsSymetricMatrix() As Boolean
            If Not Me.IsSquareMatrix() Then
                Return False
            End If
            Dim transposeMatrix As MatrixMath = Me.Transpose()
            If Me Is transposeMatrix Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function Print_Matrix() As String
            Dim Str As String = vbNewLine
            For i = 0 To (Me.RowCount - 1) Step +1
                Str = Str & "|"
                For j = 0 To (Me.ColumnCount - 1) Step +1
                    Str = Str & vbTab & (Me.Item(i, j).ToString("N8"))
                Next
                Str = Str & vbTab & "|" & vbNewLine
            Next
            Str = Str & vbNewLine
            Return Str
        End Function
    End Class
End Namespace
