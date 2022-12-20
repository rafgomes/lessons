#region CodigoAntigo
/*
Imports TeraDP.GN4.AddIns.Imprensa.Business.Entities

Namespace Imprensa
    Namespace Business
        Public Class DocumentsMerge

            Protected xDocs As List(Of KafkaArticleDocument)
            Dim savedDocument = New Queue(Of XDocument)
            Dim isRelated = False

            Public Sub New(xDocs As List(Of KafkaArticleDocument), Optional isRelated As Boolean = False)
                Me.xDocs = xDocs
                Me.isRelated = isRelated
            End Sub

            Public Overridable Function GetMergedArticle() As KafkaArticleDocument
                Me.xDocs = (From a In Me.xDocs
                            Order By a.PageArticle.Article.INMateriaSeq, a.PageArticle.Article.Name
                            Select a).ToList

                Return Me.MergeArticle()
            End Function

            Protected Function MergeArticle() As KafkaArticleDocument

                Dim pageArticles = Me.xDocs

                Dim currentDoc = New XDocument
                Dim firstARticle As KafkaArticleDocument = Nothing

                For index As Integer = 0 To pageArticles.Count - 1
                    Dim currentArticle = pageArticles(index).PageArticle.Article
                    currentDoc = pageArticles(index).KafkaXDocument

                    If (index = 0) Then

                        firstARticle = pageArticles(index)

                        Continue For
                        Else
                            Dim firtsXdoc = firstARticle.KafkaXDocument
                        Dim FirstText = firtsXdoc.Element("materia").Element("textoHTML")
                        Dim FirstTextEstruturado = firtsXdoc.Element("materia").Element("textoEstruturado")

                        Dim firstDataTexto = firtsXdoc.Element("materia").Element("estrutura").Element("dataTexto")

                        Dim firstIdentifica = firtsXdoc.Element("materia").Element("estrutura").Element("identificacao")

                        Dim firstTitulo = firtsXdoc.Element("materia").Element("estrutura").Element("titulo")

                        Dim firstSubtitulo = firtsXdoc.Element("materia").Element("estrutura").Element("subtitulo")

                        Dim firstEmenta = firtsXdoc.Element("materia").Element("estrutura").Element("ementa")

                        Dim assinaturaNodes = currentDoc.Element("materia").Element("estrutura").Elements("assinaturas")

                        currentDoc.Element("materia").Element("metadados").Element("destaque")
                        Try
                            If (assinaturaNodes.Descendants("assinante").Count > 0) Then

                                Dim assinaturasNode = firtsXdoc.Element("materia").Element("estrutura")
                                For Each node In assinaturaNodes
                                    assinaturasNode.Add(node)
                                Next
                            End If
                        Catch
                        End Try

                        Dim dataTexto = currentDoc.Element("materia").Element("estrutura").Element("dataTexto")
                        Dim identifica = currentDoc.Element("materia").Element("estrutura").Element("identificacao")
                        Dim titulo = currentDoc.Element("materia").Element("estrutura").Element("titulo")
                        Dim subtitulo = currentDoc.Element("materia").Element("estrutura").Element("subtitulo")
                        Dim ementa = currentDoc.Element("materia").Element("estrutura").Element("ementa")

                        If (String.IsNullOrEmpty(firstDataTexto.Value)) Then
                            firstDataTexto.Value = dataTexto.Value
                        End If

                        If (String.IsNullOrEmpty(firstIdentifica.Value)) Then
                            firstIdentifica.Value = identifica.Value
                        End If

                        If (String.IsNullOrEmpty(firstTitulo.Value)) Then
                            firstTitulo.Value = titulo.Value
                        End If

                        If (String.IsNullOrEmpty(firstSubtitulo.Value)) Then
                            firstSubtitulo.Value = subtitulo.Value
                        End If

                        If (String.IsNullOrEmpty(firstEmenta.Value)) Then
                            firstEmenta.Value = ementa.Value
                        End If

                        If firstARticle.PageArticle.EditionProperties.TitleName <> "DO1A" Then
                            Dim textoHTMLNodes = currentDoc.Element("materia").Element("textoHTML").Nodes()

                            For Each node In textoHTMLNodes

                                Dim cdata = FirstText.DescendantNodes().OfType(Of XCData).ToList()

                                For Each cd In cdata
                                    cd.Parent.Add(cd.Value)
                                    cd.Remove()
                                Next

                                FirstText.Add(node)
                            Next
                        End If
                    End If
                Next

                Return firstARticle
            End Function
        End Class
        Public Class MergeRelated
            Inherits DocumentsMerge

            Dim TitleName As String
            Public Sub New(ByRef xDocs As List(Of KafkaArticleDocument), titleName As String)
                MyBase.New(xDocs, True)
                Me.TitleName = titleName
            End Sub
            Public Overrides Function GetMergedArticle() As KafkaArticleDocument

                MyBase.xDocs = (From a In MyBase.xDocs Order By a.PageArticle.Article.INChildPosition Select a).ToList

                Dim chidlArticles = (From a In MyBase.xDocs Where a.PageArticle.Article.HasParent Select a).ToList

                Dim parentArticle = MyBase.MergeArticle()

                If TitleName = "DO1A" Then
                    AddChildsOrc(chidlArticles, parentArticle)
                Else
                    chidlArticles.Add(parentArticle)
                    AddChilds(chidlArticles, parentArticle.KafkaXDocument)
                End If

                Return parentArticle
            End Function

            Private Sub AddChildsOrc(childsArticles As List(Of KafkaArticleDocument), ByRef parentXdoc As KafkaArticleDocument)
                Dim relatedArticles = parentXdoc.KafkaXDocument.Element("materia").Element("metadados").Element("materiasRelacionadas")

                If relatedArticles Is Nothing Then
                    Throw New Exception("materiasRelacionadas is missing")
                End If
                childsArticles = (From a In childsArticles Order By a.PageArticle.Article.INChildPosition).ToList()
                Dim canReplace = True
                Dim metadados = parentXdoc.KafkaXDocument.Element("materia").Element("metadados")


                Dim index = 0

                For Each item In childsArticles
                    index = index + 1

                    Dim element = New XElement("materiasRelacionadas")

                    Dim editionModel = item.PageArticle.EditionModel

                    Dim webTitle = item.PageArticle.EditionModel.webEditionNumber

                    Dim idxml = String.Format("{0}+{1}+{2}", webTitle, editionModel.PublicationDate.ToString("yyyyMMdd"), item.PageArticle.Article.INMateriaId)

                    Dim idMateria = New XElement("idXML", idxml)
                    Dim ordem = New XElement("ordem", index)

                    element.Add(idMateria)
                    element.Add(ordem)

                    If (canReplace = True) Then
                        relatedArticles.ReplaceWith(element)
                        canReplace = False
                    Else
                        metadados.Add(element)
                    End If
                Next
            End Sub



            Private Sub AddChilds(childsArticles As List(Of KafkaArticleDocument), ByRef parentXdoc As XDocument)
                Dim relatedArticles = parentXdoc.Element("materia").Element("metadados").Element("materiasComposicaoTexto")

                If relatedArticles Is Nothing Then
                    Throw New Exception("materiasComposicaoTexto is missing")

                End If
                childsArticles = (From a In childsArticles Order By a.PageArticle.Article.INChildPosition).ToList()
                Dim canReplace = True
                Dim metadados = parentXdoc.Element("materia").Element("metadados")
                Dim index = 0

                For Each item In childsArticles
                    index = index + 1

                    Dim element = New XElement("materiasComposicaoTexto")

                    Dim idMateria = New XElement("idMateria", item.PageArticle.Article.INMateriaId)
                    Dim idOficio = New XElement("idOficio", item.PageArticle.Article.INOficioId)
                    Dim ordem = New XElement("ordem", index)

                    element.Add(idMateria)
                    element.Add(idOficio)
                    element.Add(ordem)

                    If (canReplace = True) Then
                        relatedArticles.ReplaceWith(element)
                        canReplace = False
                    Else
                        metadados.Add(element)
                    End If
                Next
            End Sub
        End Class
        Public Class MergeArticles
            Dim kafkaArticleDocs As List(Of KafkaArticleDocument)

            Public Sub New(kafkaArticleDocs As List(Of KafkaArticleDocument))
                Me.kafkaArticleDocs = kafkaArticleDocs
            End Sub
            Public Function GetResults() As List(Of KafkaArticleDocument)
                MergeSplittedArticles()
                MergeRelatedArticles()
                Return Me.kafkaArticleDocs
            End Function

            Private Sub MergeRelatedArticles()

                Dim parents = (From a In Me.kafkaArticleDocs
                               Where a.PageArticle.Article.HasParent
                               Group a By a.PageArticle.Article.INParentId Into Group).ToList()

                If (parents.Count = 0) Then
                    Return
                End If

                For Each obj In parents

                    Dim relatedARticles = New List(Of KafkaArticleDocument)

                    Dim childs As List(Of KafkaArticleDocument) = (From a In Me.kafkaArticleDocs
                                                                   Where a.PageArticle.Article.INParentId = obj.INParentId
                                                                   Select a).ToList

                    Dim parent = (From a In Me.kafkaArticleDocs
                                  Where a.PageArticle.Article.INMateriaId = obj.INParentId
                                  Select a).First()
                    parent.PageArticle.Article.INChildPosition = 0

                    relatedARticles.Add(parent)
                    relatedARticles.AddRange(childs)

                    Dim mergeRelated = New MergeRelated(relatedARticles, parent.PageArticle.EditionProperties.TitleName)

                    Dim resultDoc = mergeRelated.GetMergedArticle()

                    parent.KafkaXDocument = resultDoc.KafkaXDocument

                    Me.kafkaArticleDocs.RemoveAll(Function(a) a.PageArticle.Article.INMateriaId = obj.INParentId)

                    Me.kafkaArticleDocs.Add(parent)
                Next
            End Sub

            Private Sub MergeSplittedArticles()
                Dim splittedArticlesIds = (From a In Me.kafkaArticleDocs
                                           Group a By a.PageArticle.Article.INMateriaId Into Group
                                           Where Group.Count > 1).ToList()

                For Each item In splittedArticlesIds

                    Dim splittedArticles As List(Of KafkaArticleDocument) = (From a In Me.kafkaArticleDocs
                                                                             Where a.PageArticle.Article.INMateriaId = item.INMateriaId
                                                                             Select a).ToList
                    Me.kafkaArticleDocs.RemoveAll(Function(a) a.PageArticle.Article.INMateriaId = item.INMateriaId)


                    Dim documentsMerge = New DocumentsMerge(splittedArticles)

                    Dim kafkaARticleDoc = documentsMerge.GetMergedArticle()

                    Me.kafkaArticleDocs.Add(kafkaARticleDoc)
                Next
            End Sub
        End Class
    End Namespace
End Namespace
*/
#endregion


using INPerformanceTest.Business.Interfaces;


namespace Imprensa
{
    namespace Business
    {
        public enum MergeType
        {
            Related,
            Splitted
        }
        public class DocumentMergeFactory : IDocumentMergeFactory
        {
            private readonly IPublicationInfo publicationInfo;

            public DocumentMergeFactory(IPublicationInfo publicationInfo)
            {
                this.publicationInfo = publicationInfo;
            }

            public IDocumentsMerge GetDocumentMerge(MergeType mergeType)
            {
                if (mergeType == MergeType.Related)
                {
                    return new MergeRelated(publicationInfo);
                }

                if (mergeType == MergeType.Splitted)
                {
                    return new DocumentsMerge(publicationInfo);
                }

                return new DocumentsMerge(publicationInfo);
            }
        }
    }
}
