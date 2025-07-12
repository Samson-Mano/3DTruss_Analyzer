Namespace Common_Usable_Functions
    Public Class Co_Functions

        ''' <summary>
        ''' Function to Check the valid of Numerical text from textbox.text
        ''' </summary>
        ''' <param name="tB_txt">Textbox.text value</param>
        ''' <param name="Negative_check">Is negative number Not allowed (True) or allowed (False)</param>
        ''' <param name="zero_check">Is zero Not allowed (True) or allowed (False)</param>
        ''' <returns>Return the validity (True means its valid) </returns>
        ''' <remarks></remarks>
        Public Shared Function Test_a_textboxvalue_validity(ByVal tB_txt As String, ByVal Negative_check As Boolean, ByVal zero_check As Boolean) As Boolean
            Dim Am_I_valid As Boolean = False
            ''This function returns false if the textbox doesn't contains number 
            If Double.TryParse(tB_txt, 0) = True Then
                Am_I_valid = True
                '-- Additional modificaiton to avoid negative number
                If Negative_check = True Then '--- have to check for negative number
                    If Val(tB_txt) < 0 Then
                        Am_I_valid = False
                    End If
                End If
                If zero_check = True Then '--- have to check for zero number
                    If Val(tB_txt) = 0 Then
                        Am_I_valid = False
                    End If

                End If

            End If
            Return Am_I_valid
        End Function

        Public Shared Function Return_Mid_Pt(ByRef Pt1 As Point, ByRef Pt2 As Point) As Point
            Return New Point((Pt1.X + Pt2.X) * 0.5, (Pt1.Y + Pt2.Y) * 0.5)
        End Function

#Region "HSL to RGB Fundamental code -Not by Me"
        '---- The below code is from https://www.programmingalgorithms.com/algorithm/hsl-to-rgb?lang=VB.Net
        '0    : blue   (hsl(240, 100%, 50%))
        '0.25 : cyan   (hsl(180, 100%, 50%))
        '0.5  : green  (hsl(120, 100%, 50%))
        '0.75 : yellow (hsl(60, 100%, 50%))
        '1    : red    (hsl(0, 100%, 50%))
        Public Shared Function HSLToRGB(ByVal Alpha_I As Integer, ByVal hsl_H As Single, ByVal hsl_S As Single, ByVal hsl_L As Single) As Color
            Dim r As Byte = 0
            Dim g As Byte = 0
            Dim b As Byte = 0

            If hsl_S = 0 Then
                r = CByte(Math.Truncate(hsl_L * 255))
                g = CByte(Math.Truncate(hsl_L * 255))
                b = CByte(Math.Truncate(hsl_L * 255))
            Else
                Dim v1, v2 As Single
                Dim hue As Single = CSng(hsl_H) / 360

                v2 = If((hsl_L < 0.5), (hsl_L * (1 + hsl_S)), ((hsl_L + hsl_S) - (hsl_L * hsl_S)))
                v1 = 2 * hsl_L - v2

                r = CByte(Math.Truncate(255 * HueToRGB(v1, v2, hue + (1.0F / 3))))
                g = CByte(Math.Truncate(255 * HueToRGB(v1, v2, hue)))
                b = CByte(Math.Truncate(255 * HueToRGB(v1, v2, hue - (1.0F / 3))))
            End If

            Return Color.FromArgb(Alpha_I, r, g, b)
        End Function

        Private Shared Function HueToRGB(ByVal v1 As Single, ByVal v2 As Single, ByVal vH As Single) As Single
            If vH < 0 Then
                vH += 1
            End If

            If vH > 1 Then
                vH -= 1
            End If

            If (6 * vH) < 1 Then
                Return (v1 + (v2 - v1) * 6 * vH)
            End If

            If (2 * vH) < 1 Then
                Return v2
            End If

            If (3 * vH) < 2 Then
                Return (v1 + (v2 - v1) * ((2.0F / 3) - vH) * 6)
            End If

            Return v1
        End Function
#End Region

    End Class
End Namespace

