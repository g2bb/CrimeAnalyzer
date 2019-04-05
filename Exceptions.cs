using System;

namespace CrimeAnalyzer {
	[Serializable()]
    public class InvalidCSVHeadersException : System.Exception {
        public InvalidCSVHeadersException() : base() { }
        public InvalidCSVHeadersException(string message) : base(message) { }
        public InvalidCSVHeadersException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidCSVHeadersException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class InvalidCSVLineException : System.Exception {
        public InvalidCSVLineException() : base() { }
        public InvalidCSVLineException(string message) : base(message) { }
        public InvalidCSVLineException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidCSVLineException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class InvalidCSVValueException : System.Exception {
        public InvalidCSVValueException() : base() { }
        public InvalidCSVValueException(string message) : base(message) { }
        public InvalidCSVValueException(string message, System.Exception inner) : base(message, inner) { }

        protected InvalidCSVValueException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class CSVFileReadException : System.Exception {
        public CSVFileReadException() : base() { }
        public CSVFileReadException(string message) : base(message) { }
        public CSVFileReadException(string message, System.Exception inner) : base(message, inner) { }

        protected CSVFileReadException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class CrimeReportFileWriteException : System.Exception {
        public CrimeReportFileWriteException() : base() { }
        public CrimeReportFileWriteException(string message) : base(message) { }
        public CrimeReportFileWriteException(string message, System.Exception inner) : base(message, inner) { }

        protected CrimeReportFileWriteException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [Serializable()]
    public class CrimeReportEmptyDataException : System.Exception {
        public CrimeReportEmptyDataException() : base() { }
        public CrimeReportEmptyDataException(string message) : base(message) { }
        public CrimeReportEmptyDataException(string message, System.Exception inner) : base(message, inner) { }

        protected CrimeReportEmptyDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
