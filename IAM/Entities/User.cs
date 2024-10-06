namespace s3ng.IAM.Entities
{
    /// <summary>
    /// ������������
    /// </summary>
    internal class User
    {
        /// <summary>
        /// �������������
        /// </summary>
        internal required Guid Id { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        internal required string Login { get; set; }

        /// <summary>
        /// ��� ������
        /// </summary>
        internal required string PasswordHash { get; set; }
    }
}