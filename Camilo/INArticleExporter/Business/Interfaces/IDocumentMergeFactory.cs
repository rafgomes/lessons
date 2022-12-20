namespace Imprensa.Business
{
    public interface IDocumentMergeFactory
    {
        IDocumentsMerge GetDocumentMerge(MergeType mergeType);
    }
}