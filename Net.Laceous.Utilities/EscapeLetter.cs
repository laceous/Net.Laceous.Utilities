namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which unicode escape letter to use
    /// </summary>
    public enum EscapeLetter
    {
        /// <summary>
        /// CSharp/FSharp: \\uHHHH
        /// </summary>
        LowerCaseU4,

        /// <summary>
        /// CSharp/FSharp: \\UHHHHHHHH
        /// </summary>
        UpperCaseU8,

        /// <summary>
        /// CSharp: \\xH (or \\xHH, \\xHHH, \\xHHHH)
        /// </summary>
        LowerCaseX1,

        /// <summary>
        /// CSharp: \\xHH (or \\xHHH, \\xHHHH); FSharp: \\xHH (or \\uHHHH)
        /// </summary>
        LowerCaseX2,

        /// <summary>
        /// CSharp: \\xHHH (or \\xHHHH)
        /// </summary>
        LowerCaseX3,

        /// <summary>
        /// CSharp: \\xHHHH
        /// </summary>
        LowerCaseX4,

        /// <summary>
        /// FSharp: \\DDD (or \\uHHHH)
        /// </summary>
        Decimal3
    }
}
