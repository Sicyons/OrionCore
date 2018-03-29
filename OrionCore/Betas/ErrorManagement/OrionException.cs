using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OrionCore.ErrorManagement
{
    /// <summary>
    /// Provides a generic exception for orion classes.
    /// </summary>
    /// <remarks>Orion classes using <see cref="OrionException" /> can set custom data through <b>System.Exception.Data</b> inherited property.</remarks>
    [Serializable()]
    public class OrionException : Exception
    {
        #region Properties
        public new IDictionary Data { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new empty instance of the <see cref="OrionException" /> class. 
        /// </summary>
        public OrionException()
        {
        }// OrionException()
         /// <summary>
         /// Initializes a new instance of the <see cref="OrionException" /> class using a specified custom error message.
         /// </summary>
         /// <remarks><see ref="OrionException.Message" /> property value is set to <b>customMessage</b> parameter.</remarks>
         /// <param name="customMessage">The custom error message that explains the reason for the exception.</param>
         /// <seealso ref="OrionException.Message" />
        public OrionException(String customMessage)
            : base(customMessage)
        {
        }// OrionException()
         /// <summary>
         /// Initializes a new instance of the <see cref="OrionException" /> class using a specified custom error message and additional data values.
         /// </summary>
         /// <remarks><see ref="OrionException.Message" /> property value is set to <b>customMessage</b> parameter.</remarks>
         /// <param name="customMessage">The custom error message that explains the reason for the exception.</param>
         /// <param name="dataValues"><b>String</b> array containing items to be inserted in the <b>System.Exception.Data</b> inherited collection property.<br/>Strings should be formatted like <i>key=value</i>.</param>
         /// <seealso ref="OrionException.Message" />
        public OrionException(String customMessage, params String[] dataValues)
            : base(customMessage)
        {
            this.AddCustomDatas(dataValues);
        }// OrionException()
         /// <summary>
         /// Initializes a new instance of the <see cref="OrionException" /> class using a specified custom error message and a reference to the inner exception that is the cause of this exceptions.
         /// </summary>
         /// <param name="customMessage">The custom error message that explains the reason for the exception.</param>
         /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
         /// <seealso ref="OrionException.Message" />
         /// <seealso ref="OrionException.InnerException" />
        public OrionException(String customMessage, Exception innerException)
            : base(customMessage, innerException)
        {
        }// OrionException()
         /// <summary>
         /// Initializes a new instance of the <see cref="OrionException" /> class using a specified custom error message, a reference to the inner exception that is the cause of this exception and additional data values.
         /// </summary>
         /// <param name="customMessage">The custom error message that explains the reason for the exception.</param>
         /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
         /// <param name="dataValues"><b>String</b> array containing items to be inserted in the <b>System.Exception.Data</b> inherited collection property.<br/>Strings should be formatted like <i>key=value</i>.</param>
         /// <seealso ref="OrionException.Message" />
         /// <seealso ref="OrionException.InnerException" />
        public OrionException(String customMessage, Exception innerException, params String[] dataValues)
            : base(customMessage, innerException)
        {
            this.AddCustomDatas(dataValues);
        }// OrionException()
        /// <summary>
        /// Initializes a new instance of the <see cref="OrionException" /> class with serialized data.
        /// </summary>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <seealso cref="SerializationInfo" />
        /// <seealso cref="StreamingContext" />
        protected OrionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }// OrionException()
        #endregion

        #region Interface implementations
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }// GetObjectData()
        #endregion

        #region Private procedures
        private void AddCustomDatas(params String[] dataValues)
        {
            Int32 iEqualIndex;

            this.Data = new Dictionary<String, String>();

            if (dataValues != null)
                foreach (String strDataValueTemp in dataValues)
                    if (String.IsNullOrWhiteSpace(strDataValueTemp) == false)
                    {
                        iEqualIndex = strDataValueTemp.IndexOf('=');
                        if (iEqualIndex > -1)
                            this.Data.Add(strDataValueTemp.Substring(0, iEqualIndex), strDataValueTemp.Substring(iEqualIndex + 1));
                    }
        }// AddCustomDatas()
        #endregion
    }
}
