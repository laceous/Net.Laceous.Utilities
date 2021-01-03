namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which unicode escape letter to use
    /// </summary>
    public enum CharEscapeLetter
    {
        /// <summary>
        /// PowerShell: `u{H} (or `u{HH}, `u{HHH}, `u{HHHH})
        /// </summary>
        LowerCaseU1,

        /// <summary>
        /// PowerShell: `u{HH} (or `u{HHH}, `u{HHHH})
        /// </summary>
        LowerCaseU2,

        /// <summary>
        /// PowerShell: `u{HHH} (or `u{HHHH})
        /// </summary>
        LowerCaseU3,

        /// <summary>
        /// CSharp/FSharp: \\uHHHH; PowerShell: `u{HHHH}
        /// </summary>
        LowerCaseU4,

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
