namespace IAM.Seedwork.Abstractions
{
    /// <summary>
    /// ���������� �����
    /// </summary>
    internal interface IHashCalculator
    {
        /// <summary>
        /// ���������� ���
        /// </summary>
        /// <param name="value">��������</param>
        /// <returns>���</returns>
        internal string Compute(string value);
    }
}
