﻿' Copyright (c) Microsoft Corporation. All rights reserved.
Public Enum Methods
    Comparar
    CompararOrdinal
    Concatenar
    AcabaCon
    Formatear
    IndiceDe
    IndiceDeCualquiera
    Insertar
    Unir
    UltimoIndiceDe
    UltimoIndiceDeCualquiera
    RellenaIzquierda
    RellenaDerecha
    Eliminar
    Sustituir
    Dividir
    EmpiezaCon
    SubCadena
    Minusculas
    Mayusculas
    QuitarEspacios
    QuitarEspaciosFin
    QuitarEspaciosInicio
End Enum

Public Class MethodCall

    Private sampleValue As String
    Private parameter1Value As Parameter
    Private parameter2Value As Parameter
    Private parameter3Value As Parameter
    Private methodValue As Methods
    Private Shared displayFormValue As MainForm
    Private isSharedValue As Boolean

    ' Constructor for instance methods.
    Public Sub New(ByVal newSample As String, ByVal newParameter1 As Parameter, _
        ByVal newParameter2 As Parameter, ByVal newParameter3 As Parameter, _
        ByVal newMethod As Methods)
        sampleValue = newSample
        parameter1Value = newParameter1
        parameter2Value = newParameter2
        parameter3Value = newParameter3
        isSharedValue = False
        methodValue = newMethod
    End Sub

    ' Constructor for shared methods.
    Public Sub New(ByVal newParameter1 As Parameter, _
        ByVal newParameter2 As Parameter, ByVal newParameter3 As Parameter, _
        ByVal newMethod As Methods)
        sampleValue = Nothing
        parameter1Value = newParameter1
        parameter2Value = newParameter2
        parameter3Value = newParameter3
        isSharedValue = True
        methodValue = newMethod
    End Sub

    Public Property IsShared() As Boolean
        Get
            Return isSharedValue
        End Get
        Set(ByVal Value As Boolean)
            isSharedValue = Value
        End Set
    End Property

    Public Shared Property DisplayForm() As MainForm
        Get
            Return displayFormValue
        End Get
        Set(ByVal Value As MainForm)
            displayFormValue = Value
        End Set
    End Property

    Public Property Method() As Methods
        Get
            Return methodValue
        End Get
        Set(ByVal Value As Methods)
            methodValue = Value
        End Set
    End Property

    Public ReadOnly Property MethodName() As String
        Get
            Return System.Enum.GetName(GetType(Methods), Me.Method)
        End Get
    End Property

    Public Property Sample() As String
        Get
            Return sampleValue
        End Get
        Set(ByVal Value As String)
            sampleValue = Value
        End Set
    End Property

    Public Property Parameter1() As Parameter
        Get
            Return parameter1Value
        End Get
        Set(ByVal Value As Parameter)
            parameter1Value = Value
        End Set
    End Property

    Public Property Parameter2() As Parameter
        Get
            Return parameter2Value
        End Get
        Set(ByVal Value As Parameter)
            parameter2Value = Value
        End Set
    End Property

    Public Property Parameter3() As Parameter
        Get
            Return parameter3Value
        End Get
        Set(ByVal Value As Parameter)
            parameter3Value = Value
        End Set
    End Property

    Public Function GetResult() As Object
        Select Case Me.Method
            Case Methods.Comparar
                Return String.Compare(CStr(Parameter1.Value), CStr(Parameter2.Value))
            Case Methods.CompararOrdinal
                Return String.CompareOrdinal(CStr(Parameter1.Value), CStr(Parameter2.Value))
            Case Methods.Concatenar
                Return String.Concat(Parameter1.Value, Parameter2.Value, Parameter3.Value)
            Case Methods.AcabaCon
                Return Sample.EndsWith(CStr(Parameter1.Value))
            Case Methods.Formatear
                Return String.Format(CStr(Parameter1.Value), Parameter2.Value, Parameter3.Value)
            Case Methods.IndiceDe
                Return Sample.IndexOf(CStr(Parameter1.Value))
            Case Methods.IndiceDeCualquiera
                Return Sample.IndexOfAny(CStr(Parameter1.Value).ToCharArray)
            Case Methods.Insertar
                Return Sample.Insert(CInt(Parameter1.Value), CStr(Parameter2.Value))
            Case Methods.Unir
                Dim tokens() As String = CStr(Parameter2.Value).Split(", ".ToCharArray)
                Return String.Join(CStr(Parameter1.Value), tokens)
            Case Methods.UltimoIndiceDe
                Return Sample.LastIndexOf(CStr(Parameter1.Value))
            Case Methods.UltimoIndiceDeCualquiera
                Return Sample.LastIndexOfAny(CStr(Parameter1.Value).ToCharArray)
            Case Methods.RellenaIzquierda
                Return Sample.PadLeft(CInt(Parameter1.Value), CStr(Parameter2.Value).ToCharArray()(0))
            Case Methods.RellenaDerecha
                Return Sample.PadRight(CInt(Parameter1.Value), CStr(Parameter2.Value).ToCharArray()(0))
            Case Methods.Eliminar
                Return Sample.Remove(CInt(Parameter1.Value), CInt(Parameter2.Value))
            Case Methods.Sustituir
                Return Sample.Replace(CStr(Parameter1.Value), CStr(Parameter2.Value))
            Case Methods.Dividir
                Dim tokens() As String = Sample.Split(CStr(Parameter1.Value).ToCharArray)
                For Each token As String In tokens
                    token = """" & token & """"
                Next
                Return "{" & String.Join("}, {", tokens) & "}"
            Case Methods.EmpiezaCon
                Return Sample.StartsWith(CStr(Parameter1.Value))
            Case Methods.SubCadena
                Return Sample.Substring(CInt(Parameter1.Value), CInt(Parameter2.Value))
            Case Methods.Minusculas
                Return Sample.ToLower()
            Case Methods.Mayusculas
                Return Sample.ToUpper()
            Case Methods.QuitarEspacios
                Return Sample.Trim(CStr(Parameter1.Value).ToCharArray())
            Case Methods.QuitarEspaciosFin
                Return Sample.TrimEnd(CStr(Parameter1.Value).ToCharArray())
            Case Methods.QuitarEspaciosInicio
                Return Sample.TrimStart(CStr(Parameter1.Value).ToCharArray())
            Case Else
                Return "Error"
        End Select
    End Function


    Public Function FormatFunctionCall() As String
        Dim callString As New System.Text.StringBuilder
        If Not IsShared Then
            callString.Append("Ejemplo")
        Else
            callString.Append("Cadena")
        End If

        callString.Append("." & Me.MethodName)

        callString.Append("(")
        If parameter1 IsNot Nothing Then
            callString.Append(Parameter1.ToString)
        End If

        If Parameter2 IsNot Nothing Then
            callString.Append(", ")
            callString.Append(Parameter2.ToString)
        End If

        If Parameter3 IsNot Nothing Then
            callString.Append(", ")
            callString.Append(Parameter3.ToString)
        End If

        callString.Append(")")

        Return callString.ToString

    End Function

    Public Sub ResetToDefaults()
        If Me.Parameter1 IsNot Nothing Then Me.Parameter1.Value = Me.Parameter1.ParameterDefault
        If Me.Parameter2 IsNot Nothing Then Me.Parameter2.Value = Me.Parameter2.ParameterDefault
        If Me.Parameter3 IsNot Nothing Then Me.Parameter3.Value = Me.Parameter3.ParameterDefault
    End Sub

    Public Sub DisplayOnScreen()
        DisplayForm.txtSample.Visible = False
        DisplayForm.txtPrm1.Visible = False
        DisplayForm.lblPrm1.Visible = False
        DisplayForm.txtPrm2.Visible = False
        DisplayForm.lblPrm2.Visible = False
        DisplayForm.txtPrm3.Visible = False
        DisplayForm.lblPrm3.Visible = False

        If Not Me.IsShared Then
            DisplayForm.txtSample.Visible = True
            DisplayForm.txtSample.Text = Me.Sample
        End If

        If Parameter1 IsNot Nothing Then
            DisplayForm.txtPrm1.Visible = True
            DisplayForm.txtPrm1.Text = Parameter1.Value.ToString
            DisplayForm.lblPrm1.Visible = True
            DisplayForm.lblPrm1.Text = Parameter1.Name
        End If

        If Parameter2 IsNot Nothing Then
            DisplayForm.txtPrm2.Visible = True
            DisplayForm.txtPrm2.Text = Parameter2.Value.ToString
            DisplayForm.lblPrm2.Visible = True
            DisplayForm.lblPrm2.Text = Parameter2.Name
        End If

        If Parameter3 IsNot Nothing Then
            DisplayForm.txtPrm3.Visible = True
            DisplayForm.txtPrm3.Text = Parameter3.Value.ToString
            DisplayForm.lblPrm3.Visible = True
            DisplayForm.lblPrm3.Text = Parameter3.Name
        End If

    End Sub

End Class
