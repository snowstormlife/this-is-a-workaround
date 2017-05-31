    //
    // Note: This is sample code for demonstration purposes only and is provided as-is.   
    //
// Arrange
var sut = new LogAggregator();
ShimDirectory.GetFilesStringString = (dir, pattern) => new string[]
                    {
                        @"C:\someLogDir\Log_20121001.log",
                        @"C:\someLogDir\Log_20121002.log",
                        @"C:\someLogDir\Log_20121005.log",
                    };
ShimFile.ReadAllLinesString = (path) =>
                {
                    switch (path)
                   {
                       case @"C:\someLogDir\Log_20121001.log":
                             return new string[] {"OctFirstLine1", "OctFirstLine2"};
                       case @"C:\someLogDir\Log_20121002.log":
                             return new string[] {"ThreeDaysAgoFirstLine", "OctSecondLine2"};
                       case @"C:\someLogDir\Log_20121005.log":
                             return new string[] {"OctFifthLine1", "TodayLastLine"};
                     }
                     return new string[] {};
                };
 
// Act
var result = sut.AggregateLogs(@"C:\SomeLogDir", daysInPast: 3);
 
// Assert
Assert.AreEqual(4, result.Length, "Number of aggregated lines incorrect.");
CollectionAssert.Contains(result, "ThreeDaysAgoFirstLine", "Expected line missing.");
CollectionAssert.Contains(result, "TodayLastLine", "Expected line missing.");