Imports DevExpress.XtraRichEdit.API.Native
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace DocumentIteratorExample
    Partial Public Class Form1
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Public Sub New()
            InitializeComponent()
            ribbonControl1.SelectedPage = pageIterator
            richEditControl1.LoadDocument("TextWithDifferentFonts.docx")
        End Sub

        Private Sub btnIteratorRun_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnIteratorRun.ItemClick
'            #Region "#runvisitor"
            Dim visitor As New MyVisitor()
            Dim [iterator] As New DocumentIterator(richEditControl1.Document, True)
            Do While [iterator].MoveNext()
                [iterator].Current.Accept(visitor)
            Loop
            listBoxControl1.DataSource = visitor.DocumentFonts
'            #End Region ' #runvisitor
        End Sub
    End Class
    #Region "#myvisitorclass"
    Public Class MyVisitor
        Inherits DocumentVisitorBase

        Private _Fonts As List(Of String)

        Public ReadOnly Property DocumentFonts() As List(Of String)
            Get
                Return _Fonts
            End Get
        End Property

        Public Sub New()
            _Fonts = New List(Of String)()
        End Sub

        Public Overrides Sub Visit(ByVal text As DocumentText)
            If Not _Fonts.Contains(text.TextProperties.FontName) Then
                _Fonts.Add(text.TextProperties.FontName)
            End If
        End Sub
    End Class
    #End Region ' #myvisitorclass
End Namespace
