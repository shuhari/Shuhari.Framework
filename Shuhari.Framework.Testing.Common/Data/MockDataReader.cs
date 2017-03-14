using System;
using System.Data;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.Data
{
    /// <summary>
    /// Mock data reader
    /// </summary>
    class MockDataReader : IDataReader
    {
        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="schemaTable"></param>
        /// <param name="data"></param>
        public MockDataReader(DataTable schemaTable, object[][] data)
        {
            Expect.IsNotNull(schemaTable, nameof(schemaTable));
            Expect.IsNotNull(data, nameof(data));

            _schemaTable = schemaTable;
            _data = data;
            _rowIndex = -1;
        }

        private readonly DataTable _schemaTable;
        private object[][] _data;
        private int _rowIndex;

        /// <inheritdoc />
        public int FieldCount
        {
            get { return _schemaTable.Rows.Count; }
        }

        /// <inheritdoc />
        public DataTable GetSchemaTable()
        {
            return _schemaTable;
        }

        /// <inheritdoc />
        public bool Read()
        {
            _rowIndex++;
            return IsValidRow();
        }

        private bool IsValidRow()
        {
            return (_rowIndex >= 0 && _rowIndex < _data.Length);
        }

        /// <inheritdoc />
        public object this[int i]
        {
            get
            {
                if (!IsValidRow())
                    throw new IndexOutOfRangeException();
                var row = _data[_rowIndex];
                if (i < 0 || i >= row.Length)
                    throw new IndexOutOfRangeException();
                return row[i];
            }
        }

        public object this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public int Depth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsClosed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int RecordsAffected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }


        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }
    }
}
