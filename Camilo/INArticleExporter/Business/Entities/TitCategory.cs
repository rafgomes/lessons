namespace Imprensa
{
    namespace Business
    {
        namespace Entities
        {
            public class TitCategory
            {
                private string _titDescription;
                public string TitDescription
                {
                    get
                    {
                        return _titDescription;
                    }
                    set
                    {
                        _titDescription = value;
                    }
                }

                private string _titIdSiorg;
                public string TitIdSiorg
                {
                    get
                    {
                        return _titIdSiorg;
                    }
                    set
                    {
                        _titIdSiorg = value;
                    }
                }

                private string _titCode;
                public string TitCode
                {
                    get
                    {
                        return _titCode;
                    }
                    set
                    {
                        _titCode = value;
                    }
                }

                private bool _isRoot;
                public bool IsRoot
                {
                    get
                    {
                        return _isRoot;
                    }
                    set
                    {
                        _isRoot = value;
                    }
                }
            }
        }
    }
}
