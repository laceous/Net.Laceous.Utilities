namespace Net.Laceous.Utilities
{
    /// <summary>
    /// Which unicode escape letter to use
    /// </summary>
    public enum CharEscapeLetter
    {
        /// <summary>
        /// Python: \\O
        /// </summary>
        None1,

        /// <summary>
        /// Python: \\OO
        /// </summary>
        None2,

        /// <summary>
        /// FSharp: \\DDD, Python: \\OOO
        /// </summary>
        None3,

        /// <summary>
        /// PowerShell: `u{H}
        /// </summary>
        LowerCaseU1,

        /// <summary>
        /// PowerShell: `u{HH}
        /// </summary>
        LowerCaseU2,

        /// <summary>
        /// PowerShell: `u{HHH}
        /// </summary>
        LowerCaseU3,

        /// <summary>
        /// CSharp/FSharp/Python: \\uHHHH; PowerShell: `u{HHHH}
        /// </summary>
        LowerCaseU4,

        /// <summary>
        /// PowerShell: `u{HHHHH}
        /// </summary>
        LowerCaseU5,

        /// <summary>
        /// PowerShell: `u{HHHHHH}
        /// </summary>
        LowerCaseU6,

        /// <summary>
        /// CSharp: \\xH
        /// </summary>
        LowerCaseX1,

        /// <summary>
        /// CSharp/FSharp/Python: \\xHH
        /// </summary>
        LowerCaseX2,

        /// <summary>
        /// CSharp: \\xHHH
        /// </summary>
        LowerCaseX3,

        /// <summary>
        /// CSharp: \\xHHHH
        /// </summary>
        LowerCaseX4,

        /// <summary>
        /// Python: \N{name}
        /// </summary>
        UpperCaseN1,

        /// <summary>
        /// CSharp/FSharp/Python: \\UHHHHHHHH
        /// </summary>
        UpperCaseU8
    }
}
