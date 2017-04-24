USE [Volunteer]
GO
/****** Object:  View [dbo].[ReportStatistics]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportStatistics]
AS
SELECT        TOP (100) PERCENT Reported.Month, Months.MonthName, Reported.Year, Reported.Count AS ReportedCount, ISNULL(Resolved.Count, 0) AS ResolvedCount, ISNULL
                             ((SELECT        COUNT(*) AS Expr1
                                 FROM            dbo.Report
                                 WHERE        (Sent < CAST(CAST(Reported.Year AS VARCHAR(4)) + RIGHT('0' + CAST(Reported.Month + 1 AS VARCHAR(2)), 2) + RIGHT('0' + CAST(1 AS VARCHAR(2)), 2) AS DATETIME)) AND 
                                                          (ResolvedDate >= CAST(CAST(Reported.Year AS VARCHAR(4)) + RIGHT('0' + CAST(Reported.Month + 1 AS VARCHAR(2)), 2) + RIGHT('0' + CAST(1 AS VARCHAR(2)), 2) AS DATETIME) OR
                                                          ResolvedDate IS NULL)), 0) AS UresolvedCount
FROM            (SELECT        MONTH(Sent) AS Month, YEAR(Sent) AS Year, ISNULL(COUNT(*), 0) AS Count
                          FROM            dbo.Report AS r
                          GROUP BY YEAR(Sent), MONTH(Sent)) AS Reported LEFT OUTER JOIN
                             (SELECT        MONTH(ResolvedDate) AS Month, YEAR(ResolvedDate) AS Year, COUNT(*) AS Count
                               FROM            dbo.Report AS r
                               GROUP BY YEAR(ResolvedDate), MONTH(ResolvedDate)) AS Resolved ON Reported.Month = Resolved.Month AND Reported.Year = Resolved.Year LEFT OUTER JOIN
                             (SELECT        number + 1 AS MonthNumber, DATENAME(mm, DATEADD(mm, number, 0)) AS MonthName
                               FROM            master.dbo.spt_values
                               WHERE        (type = 'P') AND (number < 12)) AS Months ON Months.MonthNumber = Reported.Month

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Reported"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Resolved"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Months"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 102
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1905
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportStatistics'
GO
