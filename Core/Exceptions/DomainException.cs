namespace Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }

        public class ResourceInvalidOperationException : DomainException
        {
            public ResourceInvalidOperationException(string message) : base(message)
            {
            }
        }

        public class ResourceNotFoundException : DomainException
        {
            public ResourceNotFoundException(string message) : base(message)
            {
            }
        }

        public class ResourceUnauthorizedAccessException : DomainException
        {
            public ResourceUnauthorizedAccessException(string message) : base(message)
            {
            }
        }

        public class ResourceArgumentException : DomainException
        {
            public ResourceArgumentException(string message) : base(message)
            {
            }
        }

        public class ResourceValiationException : DomainException
        {
            public ResourceValiationException(string message) : base(message)
            {
            }
        }
    }
}
